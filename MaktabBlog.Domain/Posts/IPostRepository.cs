using MaktabBlog.Domain.Posts.ViewModels;

namespace MaktabBlog.Domain.Posts;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<List<PostUserViewModel>> GetAllPostsWithUsersAsync();
}