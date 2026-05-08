using Dapper;
using MaktabBlog.Domain.Users;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly string _connectionsString;

    public UserRepository(string connectionsString) : base(connectionsString)
    {
        _connectionsString = connectionsString;
    }

    protected override string GetTableName() => "Users";

    public Task<User?> GetUserByNationalIdAsync(string nationalId)
    {
        throw new NotImplementedException();
    }
}