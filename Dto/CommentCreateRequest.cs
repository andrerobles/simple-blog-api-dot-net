namespace simple_blog_api_dot_net.Dto
{
    public class CommentCreateRequest
    {
        public required string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}