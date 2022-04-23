using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Persistence.SeedData
{
    public class SeedExampleUserRole : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                    UserId = "408aa945-3d84-4421-8342-7269ec64d949"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                    UserId = "3f4631bd-f907-4409-b416-ba356312e659"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                    UserId = "3f4631bd-f907-4409-b416-b3356312e659"
                }
            );
        }

        public SeedExampleUserRole() { }
    }
}
