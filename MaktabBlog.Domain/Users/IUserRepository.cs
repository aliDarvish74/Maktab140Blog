namespace MaktabBlog.Domain.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByNationalIdAsync(string nationalId);
}