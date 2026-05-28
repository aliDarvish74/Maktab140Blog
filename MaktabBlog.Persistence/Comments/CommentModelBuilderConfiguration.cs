using MaktabBlog.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabBlog.Persistence.Comments;

public class CommentModelBuilderConfiguration : BaseModelBuilderConfiguration<Comment>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Comment> modelBuilder)
    {
        modelBuilder.Property(x => x.Text)
            .HasColumnType("nvarchar(500)")
            .IsRequired();
        
        modelBuilder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}