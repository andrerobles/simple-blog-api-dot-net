using System.ComponentModel.DataAnnotations;

namespace simple_blog_api_dot_net.Dto
{
    public class PostCreateRequest
    {
        [Required(ErrorMessage = "O campo title é obrigatório")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "O campo content é obrigatório")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "O campo userId é obrigatório")]
        public int? UserId { get; set; }
    }
}