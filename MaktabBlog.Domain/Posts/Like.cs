using MaktabBlog.Domain.Users;

namespace MaktabBlog.Domain.Posts;

public class Like
{
    public string LikedById { get; set; }
    public User LikedBy { get; set; }
    public Guid LikedPostsId { get; set; }
    public Post LikedPost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
