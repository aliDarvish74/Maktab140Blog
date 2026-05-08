namespace MaktabBlog.Domain.Posts.ViewModels;

public class PostUserViewModel
{
    public Guid UserId { get; private set; }
    public string UserFirstName { get; private set; }
    public string UserLastName { get; private set; }
    public Guid PostId { get; private set; }
    public string PostTitle { get; private set; }
    public string PostContent { get; private set; }
    public DateTime PostCreatedAt { get; private set; }
}