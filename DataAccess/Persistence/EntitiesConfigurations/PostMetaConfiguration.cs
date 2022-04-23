using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class PostMetaConfiguration : IEntityTypeConfiguration<PostMeta>
    {
        public void Configure(EntityTypeBuilder<PostMeta> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[post_meta]");
            #region Properties config
            //Id
            builder.HasKey(pm => pm.Id);
            builder.Property(pm=>pm.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .IsRequired(true)
                .ValueGeneratedOnAdd();
            //Post Id
            builder.Property(pm => pm.PostId)
                .HasColumnName("postId")
                .HasColumnType("bigint")
                .IsRequired(true);
            //Key
            builder.Property(pm => pm.Key)
                .HasColumnName("key")
                .HasColumnType("varchar (50)")
                .HasMaxLength(50)
                .IsRequired(true);
            //Content
            builder.Property(pm => pm.Content)
                .HasColumnName("content")
                .HasColumnType("text")
                .IsRequired(false)
                .HasDefaultValue(null);
            #endregion

            #region Indexes config
            //Index
            builder.HasIndex(pm => pm.PostId)
                .HasName("idx_meta_post");
            builder.HasIndex(pm => new { pm.PostId, pm.Key })
                .IsUnique()
                .HasName("uq_post_meta");
            #endregion

            #region Relationships config
            //Realtionship
            builder.HasOne(pm => pm.Post)
                .WithMany(p => p.PostMetas)
                .HasForeignKey(pm => pm.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_meta_post");
            #endregion
        }
        public PostMetaConfiguration() { }
    }
}
