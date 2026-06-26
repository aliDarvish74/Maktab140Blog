using System.Linq.Expressions;

namespace MaktabBlog.Domain.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByNationalIdAsync(string nationalId);

    Task<List<TResult>> GetUsersAsViewModelAsync<TResult>(Expression<Func<User, TResult>> projection)
        where TResult : BaseEntityViewModel;
}