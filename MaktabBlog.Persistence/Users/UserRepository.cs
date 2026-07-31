using System.Linq.Expressions;
using MaktabBlog.Domain;
using MaktabBlog.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MaktabBlog.Persistence.Users;

public class UserRepository : IUserRepository
{
    private readonly MaktabBlogDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public UserRepository(MaktabBlogDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<List<TResult>> QueryUsersAsync<TResult>(
        Expression<Func<User, bool>> predicate,
        Paging paging,
        Expression<Func<User, TResult>> projection)
    {
        return await _dbContext.Users.AsNoTracking().Where(predicate)
            .OrderByDescending(u => u.CreatedAt)
            .Skip(paging.Skip)
            .Take(paging.PageSize)
            .Select(projection)
            .ToListAsync();
    }

    public async Task<User?> GetUserByNationalIdAsync(string nationalId)
    {
        return await _userManager.FindByNameAsync(nationalId);
    }

    public async Task<List<TResult>> GetUsersAsViewModelAsync<TResult>(Expression<Func<User, TResult>> projection) where TResult : BaseEntityViewModel
    {
        return await _dbContext.Users
            .Select(projection)
            .OrderByDescending(vm => vm.CreatedAt)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}