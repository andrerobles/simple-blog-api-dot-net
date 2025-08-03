using simple_blog_api_dot_net.Models;

namespace simple_blog_api_dot_net;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int PostId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; }
    public Post Post { get; set; }
}
