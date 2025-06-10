using Microsoft.AspNetCore.Mvc;

namespace Collaborate.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostInteractionController : ControllerBase
    {
        [HttpPost("favorite/{postId}/{userId}")]
        public IActionResult FavoritePost(int postId, int userId)
        {
            return Ok("Not implemented");
        }

        [HttpDelete("favorite/{postId}/{userId}")]
        public IActionResult RemoveFavoritePost(int postId, int userId)
        {
            return Ok("Not implemented");
        }

        [HttpGet("favorite/{userId}")]
        public IActionResult GetFavoritePosts(int userId)
        {
            return Ok("Not implemented");
        }
    }
}
