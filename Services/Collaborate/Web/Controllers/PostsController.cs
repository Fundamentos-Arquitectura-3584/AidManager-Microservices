using Microsoft.AspNetCore.Mvc;

namespace Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateNewPost([FromBody] object postData)
        {
            return Ok("Not implemented");
        }

        [HttpPut("{postId}")]
        public IActionResult UpdatePost(int postId, [FromBody] object postData)
        {
            return Ok("Not implemented");
        }

        [HttpPut("{postId}/rating/{rating}")]
        public IActionResult UpdatePostRating(int postId, int rating)
        {
            return Ok("Not implemented");
        }

        [HttpGet("{postId}")]
        public IActionResult GetPostById(int postId)
        {
            return Ok("Not implemented");
        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePostById(int postId)
        {
            return Ok("Not implemented");
        }

        [HttpGet("company/{companyId}")]
        public IActionResult GetAllPostsByCompanyId(int companyId)
        {
            return Ok("Not implemented");
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAllPostsByUserId(int userId)
        {
            return Ok("Not implemented");
        }

        [HttpGet("user/{userId}/liked")]
        public IActionResult GetLikedPostsByUserId(int userId)
        {
            return Ok("Not implemented");
        }
    }
}
