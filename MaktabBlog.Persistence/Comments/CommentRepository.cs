using MaktabBlog.Domain.Comments;

namespace MaktabBlog.Persistence.Comments;

public class CommentRepository: GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(MaktabBlogDbContext dbContext) : base(dbContext)
    {
    }
}