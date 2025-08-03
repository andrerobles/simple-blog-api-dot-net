using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Interfaces;

namespace simple_blog_api_dot_net.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentResponse> CreateAsync(CommentCreateRequest request);
        Task<IEnumerable<CommentResponse>> GetByPostIdAsync(int postId);
        Task<bool> DeleteAsync(int id);
    }
}