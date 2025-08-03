namespace simple_blog_api_dot_net.Dto
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}