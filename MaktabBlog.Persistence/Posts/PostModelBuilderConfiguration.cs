using MaktabBlog.Domain.Posts;
using MaktabBlog.Domain.Users;
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

        modelBuilder.HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.HasMany(p => p.LikedBy)
            .WithMany(u => u.LikedPosts)
            .UsingEntity<Like>(config =>
            {
                config.ToTable("Likes");
                config.HasOne(l => l.LikedBy).WithMany().HasForeignKey(l => l.LikedById);
                config.HasOne(l => l.LikedPost).WithMany().HasForeignKey(l => l.LikedPostsId);
            });
    }
}