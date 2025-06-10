using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tasks.Application.Features.Projects.Commands.CreateProject;
using Tasks.Application.Features.Projects.Commands.UpdateProject;
using Tasks.Application.Features.Projects.Commands.DeleteProject;
using Tasks.Application.Features.Projects.Commands.UpdateRating;
using Tasks.Application.Features.Projects.Commands.SaveProjectAsFavorite;
using Tasks.Application.Features.Projects.Commands.DeleteProjectFromFavorite;
using Tasks.Application.Features.Projects.Queries.GetProject;
using Tasks.Application.Features.Projects.Queries.GetAllProjects;
using Tasks.Application.Features.Projects.Queries.GetTeamMembers;
using Tasks.Application.Features.Projects.Queries.GetProjectsByUser;
using Tasks.Application.Features.Projects.Queries.GetFavoriteProjectsByUser;

namespace Tasks.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // TODO: Consider adding a dedicated endpoint for UpdateProjectStatusCommand if needed.
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] UpdateProjectCommand command)
        {
            if (projectId != command.Id)
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] int companyId)
        {
            var query = new GetAllProjectsQuery { CompanyId = companyId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var query = new GetProjectQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result.Data == null)
                return NotFound();
            return Ok(result.Data);
        }

        [HttpPatch("{projectId}/rating")]
        public async Task<IActionResult> UpdateRating(int projectId, [FromBody] UpdateRatingCommand command)
        {
            if (projectId != command.ProjectId)
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("{projectId}/team")]
        public async Task<IActionResult> GetTeamMembers(int projectId)
        {
            var query = new GetTeamMembersQuery { ProjectId = projectId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var command = new DeleteProjectCommand { Id = projectId };
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUser(int userId)
        {
            var query = new GetProjectsByUserQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> SaveProjectAsFavorite([FromBody] SaveProjectAsFavoriteCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpDelete("favorite")]
        public async Task<IActionResult> DeleteProjectFromFavorite([FromBody] DeleteProjectFromFavoriteCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("favorite/user/{userId}")]
        public async Task<IActionResult> GetFavoriteProjectsByUser(int userId)
        {
            var query = new GetFavoriteProjectsByUserQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }
    }
}
