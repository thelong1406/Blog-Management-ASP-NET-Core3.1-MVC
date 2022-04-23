using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table's name ref
            //builder.ToTable("[blog].[user]");
            #region Properties config
            //Id
            //builder.HasKey(u => u.Id);
            //builder.Property(u => u.Id)
            //    .HasColumnName("id")
            //    .HasColumnType("bigint")
            //    .ValueGeneratedOnAdd();
            //FirstName
            builder.Property(u=>u.FirstName)
                .HasColumnName("firstName")
                .HasColumnType("varchar (50)")
                .HasMaxLength(50)
                .IsRequired(false)
                .HasDefaultValue(null);
            //MiddleName
            builder.Property(u=>u.MiddleName)
                .HasColumnName("middleName")
                .HasColumnType("varchar (50)")
                .HasMaxLength(50)
                .IsRequired(false)
                .HasDefaultValue(null);
            //LastName
            builder.Property(u=>u.LastName)
                .HasColumnName("lastName")
                .HasColumnType("varchar (50)")
                .HasMaxLength(50)
                .IsRequired(false)
                .HasDefaultValue(null);
            //PhoneNumber
            //builder.Property(u => u.PhoneNumber)
            //    .HasColumnName("mobile")
            //    .HasColumnType("varchar (15)")
            //    .HasMaxLength(15)
            //    .IsRequired(false);
            //Email
            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar (50)")
                .HasMaxLength(50)
                .IsRequired(false);
            ////Password Hash
            //builder.Property(u => u.PasswordHash)
            //    .HasColumnName("passwordHash")
            //    .HasColumnType("varchar (32)")
            //    .HasMaxLength(32)
            //    .IsRequired(true);
            //Register At
            builder.Property(u => u.RegisteredAt)
                .HasColumnName("registeredAt")
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasDefaultValueSql("getdate()");
            //Last Login
            builder.Property(u => u.LastLogin)
                .HasColumnName("lastLogin")
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasDefaultValue(null);
            //Intro
            builder.Property(u => u.Intro)
                .HasColumnName("intro")
                .HasColumnType("nvarchar (255)")
                .HasMaxLength(255)
                .IsRequired(false)
                .HasDefaultValue(null);
            //Profile
            builder.Property(u => u.Profile)
                .HasColumnName("profile")
                .HasColumnType("text")
                .IsRequired(false)
                .HasDefaultValue(null);
            //Profile
            builder.Property(u => u.Avatar)
                .HasColumnName("avatar")
                .HasColumnType("text");
            #endregion

            #region Indexes Config
            //INDEX
            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique()
                .HasName("uq_mobile");

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasName("uq_email");

            builder.HasIndex(u => u.LockoutEnabled)
                .HasName("idx_user_lock");
            #endregion

            #region Relationships Config

            #endregion //None
        }
        public UserConfiguration() { }
    }
}
