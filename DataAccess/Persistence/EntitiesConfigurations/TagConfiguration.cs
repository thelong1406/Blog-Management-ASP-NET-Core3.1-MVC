using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public TagConfiguration() { }
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[tag]");
            #region Properties config
            //Id
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            //Title
            builder.Property(c => c.Title)
                .HasColumnName("title")
                .HasColumnType("varchar (75)")
                .HasMaxLength(75)
                .IsRequired(true);
            //Meta Title 
            builder.Property(c => c.MetaTitle)
                .HasColumnName("metaTitle")
                .HasColumnType("varchar (100)")
                .HasMaxLength(100)
                .IsRequired(false)
                .HasDefaultValue(null);
            //Slug
            builder.Property(c => c.Slug)
                .HasColumnName("slug")
                .HasColumnType("varchar (100)")
                .HasMaxLength(100)
                .IsRequired(true);
            //Content
            builder.Property(c => c.Content)
                .HasColumnName("content")
                .HasColumnType("text")
                .IsRequired(false)
                .HasDefaultValue(null);
            //IsDeleted 
            builder.Property(c => c.IsDeleted)
                .HasColumnName("isDeleted")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            #endregion

            #region Indexes Config
            builder.HasIndex(p => p.IsDeleted)
                            .HasName("idx_tag_isDeleted");
            #endregion 

            #region Relationships Config
            #endregion //None
        }
    }
}
//The parentId column is not required in the Tag Table.
//The count of categories remains low since these can be used to form the Main Menu for navigational purposes. The tags can be more as compared to categories.
//Both categories and tags can be used to relate the posts.
//One should assign only a few categories to a post whereas tags count can be more.
