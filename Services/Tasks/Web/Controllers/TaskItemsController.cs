using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tasks.Application.Commands; // Corrected namespace
using Tasks.Application.Queries;  // Corrected namespace
// Specific using for each command/query might be needed if names clash, or keep as is if specific enough.
// For now, assuming direct command/query names are unique enough or will be qualified inline if needed.
// The previous version was very specific, which is safer. Let's adjust to use the correct base and then the specific command/query type.
using Tasks.Application.Commands.CreateTaskItem;
using Tasks.Application.Commands.UpdateTaskItem;
using Tasks.Application.Commands.DeleteTaskItem;
using Tasks.Application.Commands.ChangeStatusTaskItem;
using Tasks.Application.Queries.GetTaskItemById;
using Tasks.Application.Queries.GetTasksByProjectId;
using Tasks.Application.Queries.GetTasksByCompanyId;
using Tasks.Application.Queries.GetTasksByUserId;
using Tasks.Application.Queries.GetAllTaskItemsByUserByCompany;


namespace Tasks.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var query = new GetTaskItemByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result.Data == null)
                return NotFound();
            return Ok(result.Data);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatusTaskItem(int id, [FromBody] ChangeStatusTaskItemCommand command) // Assuming command takes { Status = newStatus }
        {
            if (id != command.Id) // Assuming command has an Id property for the TaskItem
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] UpdateTaskItemCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var command = new DeleteTaskItemCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems([FromQuery] int projectId)
        {
            var query = new GetTasksByProjectIdQuery { ProjectId = projectId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetAllTaskItemsByCompany(int companyId)
        {
            var query = new GetTasksByCompanyIdQuery { CompanyId = companyId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpGet("user/{userId}/company/{companyId}")]
        public async Task<IActionResult> GetAllTaskItemsByUserByCompany(int userId, int companyId)
        {
            var query = new GetAllTaskItemsByUserByCompanyQuery { UserId = userId, CompanyId = companyId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllTaskItemsByUser(int userId)
        {
            var query = new GetTasksByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result.Data);
        }
    }
}
