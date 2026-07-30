using MaktabBlog.Domain.Users;

namespace MaktabBlog.Business.Posts.Contracts.Results.Args;

public record AuthorArg(
    Guid Id,
    string FullName
)
{
    public static AuthorArg FromUser(User user)
    {
        return new AuthorArg(user.Id, user.EvaluateFullName());
    }
};