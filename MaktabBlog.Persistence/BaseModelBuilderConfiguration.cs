using MaktabBlog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabBlog.Persistence;

public abstract class BaseModelBuilderConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CreatedAt);
        
        ApplyEntityConfiguration(builder);
    }

    protected abstract void ApplyEntityConfiguration(EntityTypeBuilder<TEntity> modelBuilder);
}