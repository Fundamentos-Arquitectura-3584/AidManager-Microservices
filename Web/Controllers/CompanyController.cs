using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AidManager.Iam.Application.Queries;   // Changed namespace
using AidManager.Iam.Application.Commands;  // Changed namespace
using AidManager.Iam.Application.DTOs;    // Changed namespace

namespace AidManager.Iam.Web.Controllers // Assuming this controller should be part of the Iam service
{
    [ApiController]
    [Route("api/iam/[controller]")] // Adjusted route to reflect it's part of Iam microservice
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/iam/company/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCompanyResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GetCompanyResource>> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdQuery(id);
            var company = await _mediator.Send(query); // Query now returns GetCompanyResource?

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/iam/company/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)] // For validation errors or other bad requests
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyResource updateCompanyResource)
        {
            if (updateCompanyResource == null)
            {
                return BadRequest("UpdateCompanyResource cannot be null.");
            }
            // Basic validation example (more complex validation should use FluentValidation or DataAnnotations)
            if (string.IsNullOrWhiteSpace(updateCompanyResource.CompanyName) || string.IsNullOrWhiteSpace(updateCompanyResource.Email))
            {
                return BadRequest("Company name and email are required.");
            }

            var command = new EditCompanyCommand(id, updateCompanyResource.CompanyName, updateCompanyResource.Country, updateCompanyResource.Email);
            var success = await _mediator.Send(command);

            if (!success)
            {
                // Consider if the handler could return a more specific result instead of just bool
                // to differentiate between "not found" and "failed to update for other reasons".
                return NotFound($"Company with ID {id} not found or update failed.");
            }

            return NoContent();
        }
    }
}
