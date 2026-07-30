using System.Linq.Expressions;
using MaktabBlog.Business.Abstraction.Contracts.Results;
using MaktabBlog.Business.Abstraction.Exceptions;
using MaktabBlog.Business.Posts.Contracts.Commands;
using MaktabBlog.Business.Posts.Contracts.Queries;
using MaktabBlog.Business.Posts.Contracts.Results.Args;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace MaktabBlog.Business.Posts;

public class PostService : IPostService
{
    private readonly UserManager<User> _userManager;
    private readonly IPostRepository _postRepository;

    public PostService(
        UserManager<User> userManager,
        IPostRepository postRepository)
    {
        _userManager = userManager;
        _postRepository = postRepository;
    }

    public async Task<List<PostArg>> GetPostsAsync(GetPostsQuery query)
    {
        Expression<Func<Post, bool>> predicate = x =>
            (!query.AuthorId.HasValue || query.AuthorId.Value == x.UserId) &&
            (string.IsNullOrWhiteSpace(query.Title) || x.Title.Contains(query.Title)) &&
            (string.IsNullOrWhiteSpace(query.Content) || x.Content.Contains(query.Content));

        var posts = await _postRepository.QueryPostsWithUsersAsync(predicate, query.Paging);

        return posts.Select(PostArg.FromPost).ToList();
    }

    public async Task<GeneralResult> AddPostAsync(AddPostCommand command)
    {
        var user = await _userManager.FindByIdAsync(command.AuthorId.ToString());

        if (user is null)
            throw new ItemNotFoundException(nameof(User), typeof(User));
        
        var post = new Post(command.Title, command.Content, command.AuthorId);
        
        await _postRepository.AddAsync(post);
        
        return new GeneralResult(user.Id);
    }
}