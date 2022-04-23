using DataAccess.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Persistence.SeedData
{
    public class SeedExampleUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Seed Data
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                 new User
                 {
                     Id = "408aa945-3d84-4421-8342-7269ec64d949",
                     Email = "admin@lc.com",
                     NormalizedEmail = "ADMIN@LC.COM",
                     NormalizedUserName = "ADMIN@LC.COM",
                     UserName = "admin",
                     FirstName = "System",
                     LastName = "Admin",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true
                 },
                 new User
                 {
                     Id = "3f4631bd-f907-4409-b416-ba356312e659",
                     Email = "blogger@lc.com",
                     NormalizedEmail = "BLOGGER@LC.COM",
                     NormalizedUserName = "BLOGGER@LC.COM",
                     UserName = "blogger",
                     FirstName = "Blogger",
                     LastName = "First",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true
                 },
                 new User
                 {
                     Id = "3f4631bd-f907-4409-b416-b3356312e659",
                     Email = "antei.fa@gmail.com",
                     NormalizedEmail = "antei.fa@gmail.com",
                     NormalizedUserName = "antei.fa@gmail.com",
                     UserName = "antei",
                     FirstName = "Admin",
                     LastName = "New",
                     PasswordHash = hasher.HashPassword(null, "1"),
                     EmailConfirmed = true
                 }
            );
            #endregion
        }
        public SeedExampleUser() { }
    }
}
