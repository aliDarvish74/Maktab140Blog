using MaktabBlog.Domain.Posts;

namespace MaktabBlog.Business.Posts.Contracts.Results.Args;

public record PostArg(
    Guid Id,
    string Title,
    string Content,
    DateTime PostedAt,
    DateTime? UpdatedAt,
    AuthorArg Author)
{
    public static PostArg FromPost(Post post)
    {
        return new PostArg(post.Id,
            post.Title,
            post.Content,
            post.CreatedAt,
            post.ModifiedAt,
            AuthorArg.FromUser(post.User)
            );
    }
};