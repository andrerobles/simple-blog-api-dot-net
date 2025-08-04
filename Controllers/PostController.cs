using Microsoft.AspNetCore.Mvc;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using simple_blog_api_dot_net.Extensions;
using simple_blog_api_dot_net.Exceptions;

namespace simple_blog_api_dot_net.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PostCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postService.CreateAsync(request);
            return Ok(post);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] PostUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = User.GetUserId();

            try
            {
                var updated = await _postService.UpdateAsync(id, request, currentUserId);
                return Ok(updated);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = User.GetUserId();
            try
            {
                var success = await _postService.DeleteAsync(id, currentUserId);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}