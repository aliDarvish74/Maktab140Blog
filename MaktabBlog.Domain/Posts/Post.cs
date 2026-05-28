using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Users;

namespace MaktabBlog.Domain.Posts;

public class Post : BaseEntity
{
    public Post(string title, string content, Guid userId)
    {
        Title = title;
        Content = content;
        UserId = userId;
        Validate();
    }
    
    public string Title { get; private set; }
    public string Content { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public List<Comment> Comments { get; private set; } = new();
    public List<User> LikedBy { get; private set; } = new();

    public void UpdatePostInfo(string title, string content)
    {
        Title = title;
        Content = content;
        Validate();
        ModifiedAt = DateTime.UtcNow;
    }
    
    public override void Validate()
    {
        
    }
}