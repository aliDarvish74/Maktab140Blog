using MaktabBlog.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabBlog.Persistence.Users;

public class UserModelBuilderConfigurations : BaseModelBuilderConfiguration<User>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.Property(u => u.FirstName)
            .HasColumnType("nvarchar(50)")
            .IsRequired();
        
        modelBuilder.Property(u => u.LastName)
            .HasColumnType("nvarchar(150)")
            .IsRequired();
        
        modelBuilder.Property(u => u.NationalId)
            .HasColumnType("nvarchar(10)")
            .IsRequired();
        
        modelBuilder.HasIndex(u => u.NationalId)
            .IsUnique();
    }
}