using Microsoft.AspNetCore.Mvc;

namespace Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateNewEvent([FromBody] object eventData)
        {
            return Ok("Not implemented");
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return Ok("Not implemented");
        }

        [HttpGet("project/{projectId}")]
        public IActionResult GetEventsByProjectId(int projectId)
        {
            return Ok("Not implemented");
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateEvent(int eventId, [FromBody] object eventData)
        {
            return Ok("Not implemented");
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            return Ok("Not implemented");
        }
    }
}
