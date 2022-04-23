using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public PostTagConfiguration() { }
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[post_tag]");

            #region Properties config
            //Id
            builder.HasKey(pc => new { pc.PostId, pc.TagId });
            //Post Id
            builder.Property(pc => pc.PostId)
                .HasColumnName("postId")
                .HasColumnType("bigint");
            //Tag Id
            builder.Property(pc => pc.TagId)
                .HasColumnName("tagId")
                .HasColumnType("bigint");
            #endregion

            #region Indexes config
            builder.HasIndex(pc => pc.PostId)
                .HasName("idx_pt_post");
            builder.HasIndex(pc => pc.TagId)
                .HasName("idx_pt_tag");
            #endregion

            #region Relationships config
            builder.HasOne(pc => pc.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_pt_post");

            builder.HasOne(pc => pc.Tag)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pc => pc.TagId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_pt_tag");
            #endregion
        }
    }
}
