using System.ComponentModel.DataAnnotations;

namespace simple_blog_api_dot_net.Dto
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "O campo name é obrigatório")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo password é obrigatório")]
        public string? Password { get; set; }
    }
}