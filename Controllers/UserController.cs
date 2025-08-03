
using Microsoft.AspNetCore.Mvc;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;

namespace simple_blog_api_dot_net.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request);
                return Ok(new { user.Id, user.Name, user.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var user = await _userService.LoginAsync(request);
                return Ok(new { user.Token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}