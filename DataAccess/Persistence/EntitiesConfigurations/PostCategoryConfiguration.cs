using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[post_category]");

            #region Propertíes config
            //Id
            builder.HasKey(pc => new { pc.PostId, pc.CategoryId });
            //Post Id
            builder.Property(pc => pc.PostId)
                .HasColumnName("postId")
                .HasColumnType("bigint");
            //Category Id
            builder.Property(pc => pc.CategoryId)
                .HasColumnName("categoryId")
                .HasColumnType("bigint");
            #endregion

            #region Indexes config
            builder.HasIndex(pc => pc.PostId)
                .HasName("idx_pc_post");
            builder.HasIndex(pc => pc.CategoryId)
                .HasName("idx_pc_category");
            #endregion

            #region Relationships config
            builder.HasOne(pc => pc.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_pc_post");
            builder.HasOne(pc => pc.Category)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_pc_category");
            #endregion
        }
        public PostCategoryConfiguration() { }
    }
}
