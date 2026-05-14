using MaktabBlog.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabBlog.Persistence.Posts;

public class PostModelBuilderConfiguration : BaseModelBuilderConfiguration<Post>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Post> modelBuilder)
    {
        modelBuilder.Property(p => p.Title)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        modelBuilder.Property(p => p.Content)
            .HasColumnType("varchar(max)")
            .IsRequired();
    }
}