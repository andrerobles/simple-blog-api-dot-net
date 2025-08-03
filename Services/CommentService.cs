using simple_blog_api_dot_net.Data;
using simple_blog_api_dot_net.Dto;
using simple_blog_api_dot_net.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using simple_blog_api_dot_net.Exceptions;
using simple_blog_api_dot_net.Interfaces;

namespace simple_blog_api_dot_net.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<CommentResponse> CreateAsync(CommentCreateRequest request) {


            var user = await _dbContext.Users.FindAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"O usuário não foi encontrado.");
            }

            var post = await _dbContext.Posts.FindAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException($"O comentário não foi encontrado.");
            }

            var comment = new Comment {
                Content = request.Content,
                UserId = request.UserId.Value,
                PostId = request.PostId.Value,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return MapToResponse(comment);
        }

        public async Task<IEnumerable<CommentResponse>> GetByPostIdAsync(int postId) {
            var comments = await _dbContext.Comments.Where(c => c.PostId == postId).ToListAsync();
            return comments.Select(MapToResponse);
        }

        public async Task<bool> DeleteAsync(int id) {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new NotFoundException($"Comment with id {id} not found.");
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private CommentResponse MapToResponse(Comment comment) => new CommentResponse {
            Id = comment.Id,
            Content = comment.Content,
            UserId = comment.UserId,
            PostId = comment.PostId,
            CreatedAt = comment.CreatedAt
        };
    }
}