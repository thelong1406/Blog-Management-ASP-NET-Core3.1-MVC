using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("[blog].[invoice]");
            #region Propery config
            //Id
            builder.HasKey(i => new { i.InvoiceId });
            builder.Property(u => u.InvoiceId)
                .HasColumnName("invoiceId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            //Paid Date
            builder.Property(i => i.PaidDate)
                .HasColumnName("paidDate")
                .HasColumnType("datetime")
                .IsRequired();
            //Total price
            builder.Property(i => i.TotalPrice)
                .HasColumnName("totalPrice")
                .HasColumnType("money")
                .IsRequired();
            #endregion

            #region Index config 
            builder.HasIndex(i => i.UserId)
                .HasName("idx_Invoice_UserId");
            builder.HasIndex(i => i.PaidDate)
                .HasName("idx_Invoice_PaidDate");
            #endregion

            #region Relationship Config
            builder.HasOne(i => i.User)
                .WithMany(u => u.Invoices)
                .HasPrincipalKey(i => i.Id)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("pfk_User_Invoice");
            #endregion
        }
        public InvoiceConfiguration() { }
    }
}
