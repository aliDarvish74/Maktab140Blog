using Dapper;
using MaktabBlog.Domain;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence;

public abstract class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly string ConnectionsString;

    public GenericRepository(string connectionsString)
    {
        ConnectionsString = connectionsString;
    }

    public async Task AddAsync(TEntity entity)
    {
        await using var connection = new SqlConnection(ConnectionsString);
        connection.Open();
        
        var columns = string.Join(", ", GetEntityProperties());
        var placeHolders = GetEntityProperties().Select(c => $"@{c}").ToList();
        var parameters = string.Join(", ", placeHolders);

        var query = $"Insert into {GetTableName()} ({columns}) VALUES ({parameters})";

        await connection.ExecuteAsync(query, entity);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        await using var connection = new SqlConnection(ConnectionsString);
        connection.Open();

        var query = $"Select * From {GetTableName()}";

        var result =  await connection.QueryAsync<TEntity>(query);

        return result.ToList();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        await using var connection = new SqlConnection(ConnectionsString);
        
        connection.Open();

        var query = $"Select * From {GetTableName()} where Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<TEntity>(query, new { Id = id });
    }

    public async Task HardDeleteAsync(Guid id)
    {
        await using var connection = new SqlConnection(ConnectionsString);
        
        connection.Open();
        
        var query = $"Delete From {GetTableName()} where Id = @Id";
        
        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        await using var connection = new SqlConnection(ConnectionsString);
        
        connection.Open();
        
        var query = $"UPDATE {GetTableName()} SET IsDeleted = @IsDeleted, DeletedAt = @DeletedAt WHERE Id = @Id";
        
        await connection.ExecuteAsync(query, new
        {
            Id = id,
            IsDeleted = true,
            DeletedAt = DateTime.UtcNow
        });
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await using var connection = new SqlConnection(ConnectionsString);
        connection.Open();
        var idExcludedProperties = GetEntityProperties().Where(p => p != "Id").ToList();
        var updateParams = idExcludedProperties.Select(c => $"{c} = @{c}");

        var query = $@"UPDATE {GetTableName()}
        SET {string.Join(", ", updateParams)}                              
        WHERE Id = @Id";

        await connection.ExecuteAsync(query, entity);
    }

    protected abstract string GetTableName();

    private List<string> GetEntityProperties()
    {
        return typeof(TEntity)
            .GetProperties()
            .Select(p => p.Name)
            .ToList();
    }
}