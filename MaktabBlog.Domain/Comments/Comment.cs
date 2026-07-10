using MaktabBlog.Domain.Users;

namespace MaktabBlog.Domain.Comments;

public class Comment : BaseEntity
{
    public Comment(string text, Guid userId, Guid postId)
    {
        Text = text;
        UserId = userId;
        PostId = postId;
        CreatedById = userId;
    }
    public string Text { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid PostId { get; private set; }
    
    public override void Validate()
    {
        throw new NotImplementedException();
    }
}