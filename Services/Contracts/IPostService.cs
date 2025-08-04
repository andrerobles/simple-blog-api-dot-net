using simple_blog_api_dot_net.Interfaces;
using simple_blog_api_dot_net.Dto;

namespace simple_blog_api_dot_net.Services.Contracts
{
    public interface IPostService
    {
        Task<PostResponse> CreateAsync(PostCreateRequest request);
        Task<IEnumerable<PostResponse>> GetAllAsync();
        Task<PostResponse> GetByIdAsync(int id);
        Task<PostResponse> UpdateAsync(int id, PostCreateRequest request, int currentUserId);
        Task<bool> DeleteAsync(int id, int currentUserId);
    }
}