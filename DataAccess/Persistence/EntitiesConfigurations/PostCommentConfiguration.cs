using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[post_comment]");
            #region Properties config
            //Id
            builder.HasKey(pc => pc.PostCommentId);
            builder.Property(pc => pc.PostCommentId)
                .HasColumnName("postCommentId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            //UserId
            builder.Property(pc => pc.UserId)
                .IsRequired();
            //Post Id
            builder.Property(pc => pc.PostId)
                .HasColumnName("postId")
                .HasColumnType("bigint")
                .IsRequired();
            //Parent Id
            builder.Property(pc => pc.ParentId)
                .HasColumnName("parentId")
                .HasColumnType("bigint")
                .HasDefaultValue(null);
            //Title
            builder.Property(pc => pc.Title)
                .HasColumnName("title")
                .HasColumnType("varchar (100)")
                .HasMaxLength(100)
                .IsRequired();
            //Published
            builder.Property(pc => pc.Published)
                .HasColumnName("published")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)true);
            //Created At
            builder.Property(pc => pc.CreatedAt)
                .HasColumnName("createdAt")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
            //Publish At
            builder.Property(pc => pc.PublishedAt)
                .HasColumnName("publishAt")
                .HasColumnType("datetime");
            //Content
            builder.Property(pc => pc.Content)
                .HasColumnName("content")
                .HasColumnType("text");
            //Is hided by admin
            builder.Property(pc=>pc.IsHidedByAdmin)
                .HasColumnName("isHidedByAdmin")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            //IsDeleted 
            builder.Property(c => c.IsDeleted)
                .HasColumnName("isDeleted")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            #endregion

            #region Indexes config
            //INDEX
            builder.HasIndex(pc => pc.PostId)
                .HasName("idx_comment_post");

            builder.HasIndex(pc => pc.ParentId)
                .HasName("idx_comment_parent");

            builder.HasIndex(pc => pc.IsDeleted)
                .HasName("idx_comment_isDeleted");
            #endregion

            #region Relationships config
            //Relationship
            builder.HasOne(pc => pc.Post)
                .WithMany(p => p.PostComments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_comment_post");

            builder.HasOne(pc => pc.ParentPostComment)
                .WithMany(pc => pc.ChildPostComments)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_comment_parent");

            builder.HasOne(pc => pc.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_comment_user");
            #endregion
        }
        public PostCommentConfiguration() { }
    }
}
