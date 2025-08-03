using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Interfaces;
using simple_blog_api_dot_net.Models;

namespace simple_blog_api_dot_net.Services.Contracts
{
    public interface IUserService
    {
        Task<User> RegisterAsync(UserRegisterRequest request);
        Task<LoginResponse> LoginAsync(UserLoginRequest request);
    }
}