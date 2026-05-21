using System.Text.Json;
using System.Text.Json.Serialization;
using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Comments;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;

var dbContext = new MaktabBlogDbContext();
var userRepo = new UserRepository(dbContext);
var postRepo = new PostRepository(dbContext);
var commentRepo = new CommentRepository(dbContext);
var hosseinId = new Guid("019E2A5C-FFF8-7000-80C3-DF6EA644E54D");
var nedaId = new Guid("019E2A67-F841-7000-81E6-21EDD67E8908");
var hosseinPostId = new Guid("019E49BC-4B0A-7000-80C0-6F165F45CEB6");

/*var post = new Post("First Hossein Post", "Hala ye chizi", hosseinId);

await postRepo.AddAsync(post);*/

/*var comment = new Comment("Man oghdei Am kesafat....", nedaId, hosseinPostId);
await commentRepo.AddAsync(comment);*/

var user = await dbContext.Users
    .Include(u => u.Posts)
    .ThenInclude(p => p.Comments)
    .FirstOrDefaultAsync(u => u.Id == hosseinId);

Console.WriteLine(JsonSerializer.Serialize(user, new JsonSerializerOptions
{
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.IgnoreCycles
}));

