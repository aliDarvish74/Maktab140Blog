using MaktabBlog.Domain;
using Microsoft.Data.SqlClient;

namespace MaktabBlog.Persistence;

public abstract class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly string _connectionsString;

    public GenericRepository(string connectionsString)
    {
        _connectionsString = connectionsString;
    }

    public async Task AddAsync(TEntity entity)
    {
        
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var connection = new SqlConnection(_connectionsString);
        
        connection.Open();

        var query = $"Select * From {GetTableName()}";

        var command = new SqlCommand(query, connection);
        var reader = await command.ExecuteReaderAsync();

        var entities = new List<TEntity>();
        while (await reader.ReadAsync())
        {
            var entity = Activator.CreateInstance<TEntity>();
            
        }

        return new List<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return default;
    }

    public async Task DeleteAsync(Guid id)
    {
        
    }

    public async Task UpdateAsync(TEntity entity)
    {
        
    }

    protected abstract string GetTableName();
}