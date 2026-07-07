using MaktabBlog.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabBlog.Persistence.Users;

public class UserModelBuilderConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName)
            .HasColumnType("nvarchar(50)")
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasColumnType("nvarchar(150)")
            .IsRequired();
        
        builder.Property(u => u.NationalId)
            .HasColumnType("nvarchar(10)")
            .IsRequired();
        
        builder.HasIndex(u => u.NationalId)
            .IsUnique();
        builder.HasIndex(x => x.CreatedAt);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}