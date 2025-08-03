using Microsoft.AspNetCore.Mvc;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace simple_blog_api_dot_net.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : Controller
    {

        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
            var comment = await _commentService.CreateAsync(request);
            return Ok(comment);
        }

        [HttpGet("bypost/{postId:int}")]
        public async Task<IActionResult> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}