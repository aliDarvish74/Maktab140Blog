using MaktabBlog.Business.Abstraction.Contracts.Results;
using MaktabBlog.Business.Posts.Contracts.Commands;
using MaktabBlog.Business.Posts.Contracts.Queries;
using MaktabBlog.Business.Posts.Contracts.Results.Args;

namespace MaktabBlog.Business.Posts;

public interface IPostService
{
    Task<List<PostArg>> GetPostsAsync(GetPostsQuery query);
    Task<GeneralResult> AddPostAsync(AddPostCommand request);
}