using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class InvoiceProductConfiguration : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            builder.ToTable("[blog].[invoice_product]");
            #region properties config
            //Key
            builder.HasKey(ip => new {ip.InvoiceId, ip.ProductId});
            //ProductId
            builder.Property(ip => ip.ProductId)
                .HasColumnName("productId")
                .HasColumnType("bigint");
            //InvoiceId
            builder.Property(ip => ip.InvoiceId)
                .HasColumnName("invoiceId")
                .HasColumnType("bigint");
            //Quantity
            builder.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("tinyint")
                .IsRequired();
            //Current Price
            builder.Property(i => i.CurrentPice)
                .HasColumnName("currentPice")
                .HasColumnType("money")
                .IsRequired();
            #endregion

            #region Index config
            builder.HasIndex(ip => ip.InvoiceId)
                .HasName("idx_invoice_product_invoiceId");
            builder.HasIndex(ip => ip.ProductId)
                .HasName("idx_invoice_product_productId");
            #endregion

            #region Relationship config
            builder.HasOne(ip=>ip.Invoice)
                .WithMany(i=>i.InvoiceProducts)
                .HasForeignKey(ip => ip.InvoiceId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_invoice_invoiceproduct");

            builder.HasOne(ip => ip.Product)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_product_invoiceproduct");
            #endregion
        }
        public InvoiceProductConfiguration() { }
    }
}
