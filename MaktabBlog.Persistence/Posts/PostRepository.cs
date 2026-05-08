using Dapper;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Posts.ViewModels;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence.Posts;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(string connectionsString) : base(connectionsString)
    {
    }

    protected override string GetTableName() => "Posts";
    
    public async Task<List<PostUserViewModel>> GetAllPostsWithUsersAsync()
    {
        await using var connection = new SqlConnection(ConnectionsString);
        await connection.OpenAsync();

        var query = @"select
    						u.Id as UserId,
							u.FirstName as UserFirstName,
							u.LastName as UserLastName,
							p.Id as PostId,
							p.Title as PostTitle,
							p.Content as PostContent,
							p.CreatedAt as PostCreatedAt
						from
							Posts p
						left join Users u on
							u.Id = p.UserId
						where p.IsDeleted != 1;";
        
        var posts = await connection.QueryAsync<PostUserViewModel>(query);
        return posts.ToList();
    }
}