using MaktabBlog.Domain;

namespace MaktabBlog.Business.Users.Contracts.Queries;

public class GetUsersQuery
{
    public int? Age { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public Paging Paging { get; set; } = new Paging();
}

