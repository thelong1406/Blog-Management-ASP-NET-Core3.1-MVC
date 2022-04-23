using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[post]");
            //Id
            #region Properties config
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.PostId)
                .HasColumnName("postId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            //AuthorId
            builder.Property(p => p.UserId)
                .HasColumnName("authorId")
                .IsRequired();
            //Parent Id
            builder.Property(p => p.ParentId)
                .HasColumnName("parentId")
                .HasColumnType("bigint")
                .IsRequired(false)
                .HasDefaultValue(null);
            //Title
            builder.Property(p => p.Title)
                .HasColumnName("title")
                .HasColumnType("varchar (75)")
                .HasMaxLength(75)
                .IsRequired(true);
            //Meta Title 
            builder.Property(p => p.MetaTitle)
                .HasColumnName("metaTitle")
                .HasColumnType("varchar (100)")
                .HasMaxLength(100)
                .IsRequired(false);
            //Slug
            builder.Property(p => p.Slug)
                .HasColumnName("slug")
                .HasColumnType("varchar (100)")
                .HasMaxLength(100)
                .IsRequired(true);
            //Summary
            builder.Property(p => p.Summary)
                .HasColumnName("summary")
                .HasColumnType("varchar (255)")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired(false);
            //Published
            builder.Property(p => p.Published)
                .HasColumnName("published")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool) true);
            //Created At
            builder.Property(p => p.CreatedAt)
                .HasColumnName("createdAt")
                .HasColumnType("datetime")
                .IsRequired(true);
            //Updated At
            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasDefaultValue(null);
            //Publish At
            builder.Property(p => p.PublishedAt)
                .HasColumnName("publishedAt")
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasDefaultValue(null);
            //Content
            builder.Property(p => p.Content)
                .HasColumnName("content")
                .HasColumnType("text")
                .IsRequired(false)
                .HasDefaultValue(null);
            //IsVideoPost
            builder.Property(p => p.IsVideoPost)
                .HasColumnName("isVideoPost")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            //Minutes spent for reading
            builder.Property(p => p.MinutesSpentForReading)
                .HasColumnName("minutesSpentForReading")
                .HasColumnType("float");
            //IsDeleted 
            builder.Property(p => p.IsDeleted)
                .HasColumnName("isDeleted")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            //Thumbnail
            builder.Property(p => p.Thumbnail)
                .HasColumnName("thumbnail");
            #endregion

            #region Indexes config
            //Index
            builder.HasIndex(p => p.Slug)
                .IsUnique(true)
                .HasName("uq_slug");

            builder.HasIndex(p => p.UserId)
                .HasName("idx_post_user");

            builder.HasIndex(p => p.ParentId)
                .HasName("idx_post_parent");

            builder.HasIndex(p => p.IsDeleted)
                .HasName("idx_post_isDeleted");

            builder.HasIndex(p => p.Title)
                .HasName("idx_post_title");

            builder.HasIndex(p => p.PublishedAt)
                .HasName("idx_post_datepublish");
            
            builder.HasIndex(p => p.Published)
                .HasName("idx_post_published");
            #endregion

            #region Relationships config
            //Relationship
            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_post_user");

            builder.HasOne(p => p.ParentPost)
                .WithMany(u => u.ChildPosts)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_post_parent");
            #endregion
        }
        public PostConfiguration() { }
    }
}
