using Dapper;
using MaktabBlog.Domain.Users;
using Microsoft.Data.SqlClient;
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
}