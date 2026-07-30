using System.Text.Json;
using MaktabBlog.Domain.Comments;
using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
using MaktabBlog.Persistence;
using MaktabBlog.Persistence.Comments;
using MaktabBlog.Persistence.Posts;
using MaktabBlog.Persistence.Users;
using Microsoft.EntityFrameworkCore;

// var dbContext = new MaktabBlogDbContext();
// var userRepo = new UserRepository(dbContext);
// var postRepo = new PostRepository(dbContext);
// var commentRepo = new CommentRepository(dbContext);

/*var user = new User("Ali", "Darvish", "123456789", 31);
var post = new Post("First Ali post.", "Content", user.Id);

var user2 = new User("Sina", "Farahmandian", "987654321", 20);
var comment = new Comment("Text", user2.Id, post.Id);

await userRepo.AddAsync(user);
await userRepo.AddAsync(user2);

await postRepo.AddAsync(post);

await commentRepo.AddAsync(comment);

post.LikedBy.Add(user2);

await postRepo.UpdateAsync(post);*/

/*var post = await dbContext.Posts.FirstAsync();
var user = await dbContext.Users.FirstAsync( u => u.FirstName == "Sina");
post.LikedBy.Add(user);

await postRepo.UpdateAsync(post);*/
var t = 12;
/*var post = await dbContext.Posts.Select(p => new PostDto
    {
        Id = p.Id,
        Title = p.Title,
        Content = p.Content,
        User = new UserDto
        {
            Id = p.UserId,
            FullName = p.User.FirstName + " " + p.User.LastName
        },
        CreatedAt = p.CreatedAt,
        Likes = p.LikedBy.Count(),
        Comments = p.Comments.Select(c => new CommentDto
        {
            Text = c.Text,
            User = new UserDto
            {
                Id = c.UserId,
                FullName = c.User.FirstName + " " + c.User.LastName
            }
        })
    })
    .FirstAsync();*/

// Console.WriteLine(JsonSerializer.Serialize(post, new JsonSerializerOptions() { WriteIndented = true }));

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public UserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public IEnumerable<CommentDto> Comments { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}

public class CommentDto
{
    public string Text { get; set; }
    public UserDto User { get; set; }
}