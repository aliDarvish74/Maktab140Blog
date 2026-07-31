using System.Linq.Expressions;

namespace MaktabBlog.Domain.Users;

public interface IUserRepository
{
    Task<List<TResult>> QueryUsersAsync<TResult>(
        Expression<Func<User, bool>> predicate,
        Paging paging,
        Expression<Func<User, TResult>> projection);
    Task<User?> GetUserByNationalIdAsync(string nationalId);

    Task<List<TResult>> GetUsersAsViewModelAsync<TResult>(Expression<Func<User, TResult>> projection)
        where TResult : BaseEntityViewModel;

    Task<User?> GetByIdAsync(Guid id);
}