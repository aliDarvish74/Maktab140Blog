using System.Linq.Expressions;
using Dapper;
using MaktabBlog.Domain;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Posts.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MaktabBlog.Persistence.Posts;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(MaktabBlogDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<List<Post>> QueryPostsWithUsersAsync(Expression<Func<Post, bool>> predicate,
        Paging paging, bool tracking = false)
    {
        var query = DbContext.Posts.AsQueryable();

        if (!tracking) query = query.AsNoTracking();

        return await query
            .Where(predicate)
            .OrderByDescending(p => p.CreatedAt)
            .Skip(paging.Skip)
            .Take(paging.PageSize)
            .Include(p => p.User)
            .ToListAsync();
    }
}