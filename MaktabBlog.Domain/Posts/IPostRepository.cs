using System.Linq.Expressions;
using MaktabBlog.Domain.Posts.ViewModels;

namespace MaktabBlog.Domain.Posts;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<List<Post>> QueryPostsWithUsersAsync(Expression<Func<Post, bool>> predicate, Paging paging,
        bool tracking = false);
}