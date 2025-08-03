using System.ComponentModel.DataAnnotations;

namespace simple_blog_api_dot_net.Dto
{
    public class CommentCreateRequest
    {
        [Required(ErrorMessage = "O campo content é obrigatório")]
        public string? Content { get; set; }
        [Required(ErrorMessage = "O campo userId é obrigatório")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "O campo postId é obrigatório")]
        public int? PostId { get; set; }
    }
}