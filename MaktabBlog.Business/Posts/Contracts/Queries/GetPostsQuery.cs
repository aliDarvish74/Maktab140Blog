using MaktabBlog.Domain;

namespace MaktabBlog.Business.Posts.Contracts.Queries;

public record GetPostsQuery(
    Paging Paging,
    Guid? AuthorId = null,
    string? Title = null,
    string? Content = null);