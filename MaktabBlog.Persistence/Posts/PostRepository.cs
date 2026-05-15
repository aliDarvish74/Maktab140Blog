using Dapper;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Posts.ViewModels;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence.Posts;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(MaktabBlogDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<List<PostUserViewModel>> GetAllPostsWithUsersAsync()
    {
        throw new NotImplementedException();
    }
}