using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("[blog].[cart]");
            #region properties config
            //Key
            builder.HasKey(c => c.CartId);
            //ProductId
            builder.Property(c => c.ProductId)
                .HasColumnName("productId")
                .HasColumnType("bigint");
            //ProductId
            //builder.Property(c => c.Id)
            //    .HasColumnName("userId")
            //    .HasColumnType("bigint");
            //Quantity
            builder.Property(c => c.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("tinyint")
                .IsRequired();
            //Date Added
            builder.Property(c => c.DateAdded)
                .HasColumnName("dateAdded")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("Getdate()");
            //Is Paid
            builder.Property(i => i.IsPaid)
                .HasColumnName("isPaid")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue((bool)false);
            #endregion

            #region Index config
            builder.HasIndex(ip => ip.Id)
                .HasName("idx_cart_user_userId");
            #endregion

            #region Relationship config
            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user_cart");

            builder.HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(ip => ip.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_product_cart");
            #endregion
        }
        public CartConfiguration() { }
    }
}
