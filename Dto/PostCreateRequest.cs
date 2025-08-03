namespace simple_blog_api_dot_net.Dto
{
    public class PostCreateRequest
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int UserId { get; set; }
    }
}