using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "[blog].[category]",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "varchar (75)", maxLength: 75, nullable: false),
                    metaTitle = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: true),
                    slug = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[category]", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_parent",
                        column: x => x.parentId,
                        principalTable: "[blog].[category]",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[product]",
                columns: table => new
                {
                    productId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar (150)", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    onSalePrice = table.Column<decimal>(type: "money", nullable: true),
                    thumbnail = table.Column<byte[]>(nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[product]", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "[blog].[tag]",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar (75)", maxLength: 75, nullable: false),
                    metaTitle = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: true),
                    slug = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[tag]", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "varchar (50)", maxLength: 50, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    firstName = table.Column<string>(type: "varchar (50)", maxLength: 50, nullable: true),
                    middleName = table.Column<string>(type: "varchar (50)", maxLength: 50, nullable: true),
                    lastName = table.Column<string>(type: "varchar (50)", maxLength: 50, nullable: true),
                    registeredAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    lastLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    intro = table.Column<string>(type: "nvarchar (255)", maxLength: 255, nullable: true),
                    profile = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "[blog].[cart]",
                columns: table => new
                {
                    CartId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(nullable: true),
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "Getdate()"),
                    isPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[cart]", x => x.CartId);
                    table.ForeignKey(
                        name: "fk_user_cart",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product_cart",
                        column: x => x.productId,
                        principalTable: "[blog].[product]",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "[blog].[invoice]",
                columns: table => new
                {
                    invoiceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    paidDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    totalPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[invoice]", x => x.invoiceId);
                    table.ForeignKey(
                        name: "pfk_User_Invoice",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[post]",
                columns: table => new
                {
                    postId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<long>(type: "bigint", nullable: true),
                    authorId = table.Column<string>(nullable: false),
                    title = table.Column<string>(type: "varchar (75)", maxLength: 75, nullable: false),
                    metaTitle = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: true),
                    slug = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: false),
                    summary = table.Column<string>(type: "varchar (255)", maxLength: 255, nullable: true),
                    published = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    publishedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    isVideoPost = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    minutesSpentForReading = table.Column<double>(type: "float", nullable: true),
                    thumbnail = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[post]", x => x.postId);
                    table.ForeignKey(
                        name: "fk_post_parent",
                        column: x => x.parentId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "fk_post_user",
                        column: x => x.authorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "[blog].[invoice_product]",
                columns: table => new
                {
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    invoiceId = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    currentPice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[invoice_product]", x => new { x.invoiceId, x.productId });
                    table.ForeignKey(
                        name: "fk_invoice_invoiceproduct",
                        column: x => x.invoiceId,
                        principalTable: "[blog].[invoice]",
                        principalColumn: "invoiceId");
                    table.ForeignKey(
                        name: "fk_product_invoiceproduct",
                        column: x => x.productId,
                        principalTable: "[blog].[product]",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[post_category]",
                columns: table => new
                {
                    postId = table.Column<long>(type: "bigint", nullable: false),
                    categoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[post_category]", x => new { x.postId, x.categoryId });
                    table.ForeignKey(
                        name: "fk_pc_category",
                        column: x => x.categoryId,
                        principalTable: "[blog].[category]",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pc_post",
                        column: x => x.postId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[post_comment]",
                columns: table => new
                {
                    postCommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    postId = table.Column<long>(type: "bigint", nullable: false),
                    parentId = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "varchar (100)", maxLength: 100, nullable: true),
                    published = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    publishAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    isHidedByAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[post_comment]", x => x.postCommentId);
                    table.ForeignKey(
                        name: "fk_comment_parent",
                        column: x => x.parentId,
                        principalTable: "[blog].[post_comment]",
                        principalColumn: "postCommentId");
                    table.ForeignKey(
                        name: "fk_comment_post",
                        column: x => x.postId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "fk_comment_user",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "[blog].[post_meta]",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postId = table.Column<long>(type: "bigint", nullable: false),
                    key = table.Column<string>(type: "varchar (50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[post_meta]", x => x.id);
                    table.ForeignKey(
                        name: "fk_meta_post",
                        column: x => x.postId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[post_tag]",
                columns: table => new
                {
                    postId = table.Column<long>(type: "bigint", nullable: false),
                    tagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[post_tag]", x => new { x.postId, x.tagId });
                    table.ForeignKey(
                        name: "fk_pt_post",
                        column: x => x.postId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "fk_pt_tag",
                        column: x => x.tagId,
                        principalTable: "[blog].[tag]",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "[blog].[vote]",
                columns: table => new
                {
                    voteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(nullable: false),
                    postId = table.Column<long>(nullable: false),
                    rate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[blog].[vote]", x => x.voteId);
                    table.CheckConstraint("check_vote_rate", "rate between 0 and 5");
                    table.ForeignKey(
                        name: "fpk_vote_post",
                        column: x => x.postId,
                        principalTable: "[blog].[post]",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "fpk_vote_user",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "5421e3b4-ac05-4926-906d-56e7487585c1", "Administrator", "ADMINISTRATOR" },
                    { "cac43a7e-f7cb-4148-baaf-1acb431eabbf", "d833cd15-1375-457e-8cca-3edc0ae8df8a", "Blogger", "BLOGGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "avatar", "ConcurrencyStamp", "email", "EmailConfirmed", "firstName", "lastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, null, "7645c0f6-6f00-426e-a2d5-4eedb421ff89", "admin@lc.com", true, "System", "Admin", false, null, "ADMIN@LC.COM", "ADMIN@LC.COM", "AQAAAAEAACcQAAAAEPsWt1g1t7IfzGl5c3p+cMZNrmDn4zO9PyaC6dJ9PAFiyySqOgzyLa8BFeM2IgypjA==", null, false, "6a4ca39b-523a-4c54-aff3-6a20bf593ddd", false, "admin" },
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, null, "4fb481d3-505d-4593-9fd1-719be9d60ab1", "blogger@lc.com", true, "Blogger", "First", false, null, "BLOGGER@LC.COM", "BLOGGER@LC.COM", "AQAAAAEAACcQAAAAELiz6dIu7JLxJRwFn62vCmPdHjnl3ZG9RsZQR3xC4v2deVNE2dwql3pyi0YPyeSvMw==", null, false, "b98d79c0-125c-4410-8c0d-8d2e5561a405", false, "blogger" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "408aa945-3d84-4421-8342-7269ec64d949", "cac43a6e-f7bb-4448-baaf-1add431ccbbf" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "3f4631bd-f907-4409-b416-ba356312e659", "cac43a7e-f7cb-4148-baaf-1acb431eabbf" });

            migrationBuilder.CreateIndex(
                name: "idx_cart_user_userId",
                table: "[blog].[cart]",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_[blog].[cart]_productId",
                table: "[blog].[cart]",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "idx_category_parent",
                table: "[blog].[category]",
                column: "parentId");

            migrationBuilder.CreateIndex(
                name: "idx_Invoice_PaidDate",
                table: "[blog].[invoice]",
                column: "paidDate");

            migrationBuilder.CreateIndex(
                name: "idx_Invoice_UserId",
                table: "[blog].[invoice]",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_invoice_product_invoiceId",
                table: "[blog].[invoice_product]",
                column: "invoiceId");

            migrationBuilder.CreateIndex(
                name: "idx_invoice_product_productId",
                table: "[blog].[invoice_product]",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "idx_post_isDeleted",
                table: "[blog].[post]",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "idx_post_parent",
                table: "[blog].[post]",
                column: "parentId");

            migrationBuilder.CreateIndex(
                name: "idx_post_published",
                table: "[blog].[post]",
                column: "published");

            migrationBuilder.CreateIndex(
                name: "idx_post_datepublish",
                table: "[blog].[post]",
                column: "publishedAt");

            migrationBuilder.CreateIndex(
                name: "uq_slug",
                table: "[blog].[post]",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_post_title",
                table: "[blog].[post]",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "idx_post_user",
                table: "[blog].[post]",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "idx_pc_category",
                table: "[blog].[post_category]",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "idx_pc_post",
                table: "[blog].[post_category]",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "idx_comment_isDeleted",
                table: "[blog].[post_comment]",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "idx_comment_parent",
                table: "[blog].[post_comment]",
                column: "parentId");

            migrationBuilder.CreateIndex(
                name: "idx_comment_post",
                table: "[blog].[post_comment]",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_[blog].[post_comment]_UserId",
                table: "[blog].[post_comment]",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_meta_post",
                table: "[blog].[post_meta]",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "uq_post_meta",
                table: "[blog].[post_meta]",
                columns: new[] { "postId", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pt_post",
                table: "[blog].[post_tag]",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "idx_pt_tag",
                table: "[blog].[post_tag]",
                column: "tagId");

            migrationBuilder.CreateIndex(
                name: "idx_product_isDeleted",
                table: "[blog].[product]",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "idx_tag_isDeleted",
                table: "[blog].[tag]",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "idx_vote_postId",
                table: "[blog].[vote]",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_[blog].[vote]_userId",
                table: "[blog].[vote]",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "uq_email",
                table: "AspNetUsers",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "idx_user_lock",
                table: "AspNetUsers",
                column: "LockoutEnabled");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uq_mobile",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "[blog].[cart]");

            migrationBuilder.DropTable(
                name: "[blog].[invoice_product]");

            migrationBuilder.DropTable(
                name: "[blog].[post_category]");

            migrationBuilder.DropTable(
                name: "[blog].[post_comment]");

            migrationBuilder.DropTable(
                name: "[blog].[post_meta]");

            migrationBuilder.DropTable(
                name: "[blog].[post_tag]");

            migrationBuilder.DropTable(
                name: "[blog].[vote]");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "[blog].[invoice]");

            migrationBuilder.DropTable(
                name: "[blog].[product]");

            migrationBuilder.DropTable(
                name: "[blog].[category]");

            migrationBuilder.DropTable(
                name: "[blog].[tag]");

            migrationBuilder.DropTable(
                name: "[blog].[post]");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
