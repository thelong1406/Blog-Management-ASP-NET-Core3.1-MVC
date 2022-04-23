using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddNewAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "2aa5c2c9-5766-4797-9e94-b5ac932f92a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "fa649501-1061-4ec5-bb0b-3b3f3cdf2fee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a96323cc-2812-4148-a2e8-c8cec2ec4306", "AQAAAAEAACcQAAAAED3Xt3+Pd5C2l7JUGpJLW09ORhbf65Mz6rP9h5jIsQ2ioda8pmWgotluBRhRwVOfQQ==", "014ea835-0567-4074-b7ec-1674d938209e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8b98c4a-6a02-411f-bc00-2736ce399ccf", "AQAAAAEAACcQAAAAEGRP4GVoWEG8xmNmbFY1eIDW61GIPGRJ0+3CklON0Z9Bbx81sWiwLHXDmOZF7GEqnA==", "5f4412b6-eaee-4916-a4cc-c4b62be9dfdc" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "avatar", "ConcurrencyStamp", "email", "EmailConfirmed", "firstName", "lastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3f4631bd-f907-4409-b416-b3356312e659", 0, null, "4ee57e5e-cebf-4038-b9fe-fc734c9400ee", "antei.fa@gmail.com", true, "Admin", "New", false, null, "antei.fa@gmail.com", "antei.fa@gmail.com", "AQAAAAEAACcQAAAAEB52E17h0/Dw8Tc+G3tvN+LeD5ablJv5BTu4Y3fov2OogTr0fqM3U+xWT+Scz3xYEA==", null, false, "5a573308-e057-45a8-9975-d14fb422b554", false, "antei" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "3f4631bd-f907-4409-b416-b3356312e659", "cac43a6e-f7bb-4448-baaf-1add431ccbbf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "3f4631bd-f907-4409-b416-b3356312e659", "cac43a6e-f7bb-4448-baaf-1add431ccbbf" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-b3356312e659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "b2e2146c-5428-4337-977c-b9d1d8b7202e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "60eebc25-4bba-486d-9238-c5086576a61b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "724af9e6-bde3-4ae1-ae19-2ec4f0493d62", "AQAAAAEAACcQAAAAEDqdgpKzOchPFZJJ2TeNvrmJoiYsZM3XdVWcPqObOZdu0HFfKcD8sqKynH7ZuMJ5pw==", "336fa4bc-a08e-4510-8e3d-8e09d18941e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e402e2f6-ec15-4e7a-84e4-87069b1cc805", "AQAAAAEAACcQAAAAEC4AnOgpy7IgiNy7p85vdquhB+fMca2OLrsdow6+4SDsABo7zsnmrUAvIkQG9zldgQ==", "0790cecd-9519-4d9a-a75b-925527fa95ea" });
        }
    }
}
