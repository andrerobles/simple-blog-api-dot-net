using simple_blog_api_dot_net.Data;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using simple_blog_api_dot_net.Exceptions;
using simple_blog_api_dot_net.Interfaces;
using simple_blog_api_dot_net.WebSockets;

namespace simple_blog_api_dot_net.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _dbContext;
        private readonly PostWebSocketHandler _wsHandler;
        public PostService(AppDbContext dbContext, PostWebSocketHandler wsHandler)
        {
            _dbContext = dbContext;
            _wsHandler = wsHandler;
        }

        public async Task<PostResponse> CreateAsync(PostCreateRequest request) {
            var post = new Post {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId.Value,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();

            var response = MapToResponse(post);
            await _wsHandler.BroadcastAsync(System.Text.Json.JsonSerializer.Serialize(response));

            return response;
        }

        public async Task<IEnumerable<PostResponse>> GetAllAsync()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return posts.Select(MapToResponse);
        }

        public async Task<PostResponse> GetByIdAsync(int id) {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new NotFoundException("O Post não foi encontrado.");
            }

            return MapToResponse(post);
        }

        public async Task<PostResponse> UpdateAsync(int id, PostCreateRequest request, int currentUserId) {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new NotFoundException("O Post não foi encontrado.");
            }

            if (post.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("O usuário não tem permissão para atualizar este post.");        
            }
        
            post.Title = request.Title;
            post.Content = request.Content;
            await _dbContext.SaveChangesAsync();
            return MapToResponse(post);
        }

        public async Task<bool> DeleteAsync(int id, int currentUserId) {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new NotFoundException("O Post não foi encontrado.");
            }

            if (post.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("O usuário não tem permissão para atualizar este post.");        
            }

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