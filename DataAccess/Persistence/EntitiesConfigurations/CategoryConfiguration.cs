using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[category]");
            #region Properties config
            //Id
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            //Parent Id
            builder.Property(c => c.ParentId)
                .HasColumnName("parentId")
                .HasColumnType("bigint")
                .IsRequired(false)
                .HasDefaultValue(null);
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
            //Index
            builder.HasIndex(c => c.ParentId)
                .HasName("idx_category_parent");
            #endregion

            #region Relationships Config
            //Relationship
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_category_parent");
            #endregion

        }
        public CategoryConfiguration() { }
    }
}
