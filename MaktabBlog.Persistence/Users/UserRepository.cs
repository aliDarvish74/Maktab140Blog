using System.Linq.Expressions;
using MaktabBlog.Domain;
using MaktabBlog.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MaktabBlog.Persistence.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(MaktabBlogDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<User?> GetUserByNationalIdAsync(string nationalId)
    {
        return await DbContext.Users.FirstOrDefaultAsync(u => u.NationalId == nationalId);
    }
    
    public async Task<List<TResult>> GetUsersAsViewModelAsync<TResult>(Expression<Func<User, TResult>> projection) where TResult : BaseEntityViewModel
    {
        return await DbContext.Users
            .Select(projection)
            .OrderByDescending(vm => vm.CreatedAt)
            .ToListAsync();
    }
}