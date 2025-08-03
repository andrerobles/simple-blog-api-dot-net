using simple_blog_api_dot_net.Data;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace simple_blog_api_dot_net.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _dbContext;
        public PostService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostResponse> CreateAsync(PostCreateRequest request) {
            var post = new Post {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return MapToResponse(post);
        }

        public async Task<IEnumerable<PostResponse>> GetAllAsync()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return posts.Select(MapToResponse);
        }

        public async Task<PostResponse> GetByIdAsync(int id) {
            var post = await _dbContext.Posts.FindAsync(id);
            return post == null ? null : MapToResponse(post);
        }

        public async Task<PostResponse> UpdateAsync(int id, PostCreateRequest request) {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null) return null;

            post.Title = request.Title;
            post.Content = request.Content;
            await _dbContext.SaveChangesAsync();
            return MapToResponse(post);
        }

        public async Task<bool> DeleteAsync(int id) {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null) return false;

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private PostResponse MapToResponse(Post post) => new PostResponse {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            UserId = post.UserId,
            CreatedAt = post.CreatedAt
        };
    }
}