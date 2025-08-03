namespace simple_blog_api_dot_net.Dto
{
    public class UserRegisterRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}