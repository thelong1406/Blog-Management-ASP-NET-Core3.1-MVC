using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class REMOVEKEYOFVOTE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_[blog].[vote]",
                table: "[blog].[vote]");

            migrationBuilder.DropIndex(
                name: "IX_[blog].[vote]_userId",
                table: "[blog].[vote]");

            migrationBuilder.DropColumn(
                name: "voteId",
                table: "[blog].[vote]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_[blog].[vote]",
                table: "[blog].[vote]",
                columns: new[] { "userId", "postId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_[blog].[vote]",
                table: "[blog].[vote]");

            migrationBuilder.AddColumn<long>(
                name: "voteId",
                table: "[blog].[vote]",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_[blog].[vote]",
                table: "[blog].[vote]",
                column: "voteId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "5421e3b4-ac05-4926-906d-56e7487585c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "d833cd15-1375-457e-8cca-3edc0ae8df8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fb481d3-505d-4593-9fd1-719be9d60ab1", "AQAAAAEAACcQAAAAELiz6dIu7JLxJRwFn62vCmPdHjnl3ZG9RsZQR3xC4v2deVNE2dwql3pyi0YPyeSvMw==", "b98d79c0-125c-4410-8c0d-8d2e5561a405" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7645c0f6-6f00-426e-a2d5-4eedb421ff89", "AQAAAAEAACcQAAAAEPsWt1g1t7IfzGl5c3p+cMZNrmDn4zO9PyaC6dJ9PAFiyySqOgzyLa8BFeM2IgypjA==", "6a4ca39b-523a-4c54-aff3-6a20bf593ddd" });

            migrationBuilder.CreateIndex(
                name: "IX_[blog].[vote]_userId",
                table: "[blog].[vote]",
                column: "userId");
        }
    }
}
