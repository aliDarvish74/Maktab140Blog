namespace MaktabBlog.Business.Posts.Contracts.Commands;

public record AddPostCommand(string Title, string Content, Guid AuthorId);