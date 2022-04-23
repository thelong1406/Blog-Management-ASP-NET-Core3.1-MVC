using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EditPostCommentProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "[blog].[post_comment]",
                type: "varchar (100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar (100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "publishAt",
                table: "[blog].[post_comment]",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "[blog].[post_comment]",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "[blog].[post_comment]",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "[blog].[post_comment]",
                type: "varchar (100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar (100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "publishAt",
                table: "[blog].[post_comment]",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "[blog].[post_comment]",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "[blog].[post_comment]",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "62093e36-600c-4821-a4ad-b70b3b38a2c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "dc0f8ba2-4e36-4335-ad2d-bf9dc0c945bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "933ab2e3-8e72-47a7-b8b4-7a0e71816631", "AQAAAAEAACcQAAAAEFDxNZUS6wuxYfDePHKRaqfCbFzR/+XslS2Wa8RM7TB7kHW7E0byhAIhCeZaAP4ZqA==", "052c20ad-adea-4b40-a825-3bc822efd40f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73d31091-1c69-4615-81c3-4803bd9510ae", "AQAAAAEAACcQAAAAEDOrX8MJQ0ihBct9PIgkdbCbkMglMr30045bXB5OENmmNbn8cE5WXiK3gnO8NydQHg==", "6e80a4b7-ae4c-4e72-8923-a646ccff4a48" });
        }
    }
}
