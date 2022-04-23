using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Table's name ref
            builder.ToTable("[blog].[product]");
            //Product ID
            #region Properties config
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId)
                .HasColumnName("productId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            //Product Name
            builder.Property(p => p.ProductName)
                .HasColumnName("productName")
                .HasColumnType("nvarchar (150)")
                .IsRequired(true);
            //Price
            builder.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("money")
                .IsRequired(true);
            //OnsalePrice
            builder.Property(p => p.OnSalePrice)
                .HasColumnName("onSalePrice")
                .HasColumnType("money");
            //Thumbnail
            builder.Property(p => p.Thumbnail)
                .HasColumnName("thumbnail");
            //IsDeleted 
            builder.Property(p => p.IsDeleted)
                .HasColumnName("isDeleted")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            #endregion

            #region Indexes config
            //Index
            builder.HasIndex(p => p.IsDeleted)
                .HasName("idx_product_isDeleted");
            #endregion

            #region Relationships config
            //Relationship
            //builder.HasOne(p => p.User)
            //    .WithMany(u => u.Posts)
            //    .HasForeignKey(p => p.AuthorId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasConstraintName("fk_post_user");
            //builder.HasOne(p => p.ParentPost)
            //    .WithMany(u => u.ChildPosts)
            //    .HasForeignKey(p => p.ParentId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasConstraintName("fk_post_parent");
            #endregion
        }
        public ProductConfiguration() { }
    }
}
