using System.ComponentModel.DataAnnotations;

namespace simple_blog_api_dot_net.Dto
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo password é obrigatório")]
        public string? Password { get; set; }
    }
}