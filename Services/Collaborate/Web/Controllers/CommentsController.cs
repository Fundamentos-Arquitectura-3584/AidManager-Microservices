using Microsoft.AspNetCore.Mvc;

namespace Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetComments()
        {
            return Ok("Not implemented");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            return Ok("Not implemented");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            return Ok("Not implemented");
        }

        [HttpGet("company/{companyId}")]
        public IActionResult GetCommentsByCompany(int companyId)
        {
            return Ok("Not implemented");
        }

        [HttpPost("post/{postId}")]
        public IActionResult AddPostComment(int postId, [FromBody] string commentText)
        {
            return Ok("Not implemented");
        }
    }
}
