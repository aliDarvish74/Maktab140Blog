using MaktabBlog.Domain.Users;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence.Users;

public class UserRepository : IUserRepository
{
    private readonly string _connectionsString;

    public UserRepository(string connectionsString)
    {
        _connectionsString = connectionsString;
    }
    
    public async Task AddAsync(User entity)
    {
        await using var connection = new SqlConnection(_connectionsString);

        connection.Open();
        var query = @"
            INSERT INTO Users
                (Id,
                 FirstName,
                 LastName,
                 NationalId,
                 Age,
                 CreatedAt,
                 IsDeleted
                 )
             VALUES (
                @Id,
                @FirstName,
                @LastName,
                @NationalId,
                @Age,
                @CreatedAt,
                @IsDeleted
             )";
        
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@NationalId", entity.NationalId);
        command.Parameters.AddWithValue("@Age", entity.Age);
        command.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);
        command.Parameters.AddWithValue("@IsDeleted", entity.IsDeleted);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        var users = new List<User>();

        await using var connection = new SqlConnection(_connectionsString);

        connection.Open();

        var query = $"Select * From Users";

        var command = new SqlCommand(query, connection);
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var user = new User();

            user.Id = reader.GetGuid(0);
            user.FirstName = reader.GetString(1);
            user.LastName = reader.GetString(2);
            user.NationalId = reader.GetString(3);
            user.Age = reader.GetInt32(4);
            user.CreatedAt = reader.GetDateTime(5);
            user.ModifiedAt = await reader.IsDBNullAsync(6) ? null : reader.GetDateTime(6);
            user.IsDeleted = reader.GetBoolean(7);
            user.DeletedAt = await reader.IsDBNullAsync(8) ? null : reader.GetDateTime(8);

            users.Add(user);
        }
        
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = new User();

        await using var connection = new SqlConnection(_connectionsString);

        connection.Open();

        var query = @"SELECT * FROM Users WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", id);

        var reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();
        
        if(await reader.IsDBNullAsync(0)) return null;

        user.Id = reader.GetGuid(0);
        user.FirstName = reader.GetString(1);
        user.LastName = reader.GetString(2);
        user.NationalId = reader.GetString(3);
        user.Age = reader.GetInt32(4);
        user.CreatedAt = reader.GetDateTime(5);
        user.ModifiedAt = await reader.IsDBNullAsync(6) ? null : reader.GetDateTime(6);
        user.IsDeleted = reader.GetBoolean(7);
        user.DeletedAt = await reader.IsDBNullAsync(8) ? null : reader.GetDateTime(8);

        return user;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}