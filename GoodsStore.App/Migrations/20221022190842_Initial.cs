using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsStore.App.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "IAM_MainMenu",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_MainMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IAM_Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IAM_User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCM_Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCM_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IAM_Menu",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainMenuId = table.Column<int>(type: "int", nullable: false),
                    DtRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_Menu_IAM_MainMenu_MainMenuId",
                        column: x => x.MainMenuId,
                        principalSchema: "dbo",
                        principalTable: "IAM_MainMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_RoleClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_RoleClaim_IAM_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_UserClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_UserClaim_IAM_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IAM_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_UserLogin",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IAM_UserLogin_IAM_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IAM_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_UserRole",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IAM_UserRole_IAM_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IAM_UserRole_IAM_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IAM_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_UserToken",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IAM_UserToken_IAM_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IAM_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCM_Customer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCM_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCM_Customer_IAM_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IAM_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_Module",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    DtRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_Module_IAM_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCM_Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCM_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCM_Order_OCM_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "OCM_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_Feature",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    DtRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_Feature_IAM_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCM_OrderItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCM_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCM_OrderItem_OCM_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "OCM_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OCM_OrderItem_OCM_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "OCM_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAM_RoleFeature",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAM_RoleFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IAM_RoleFeature_IAM_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IAM_RoleFeature_IAM_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "IAM_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "IAM_Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d5a5c22-d6e7-4636-81b3-ad2060b8a3fd", "ae4472d8-598b-4218-9662-de0fed0c2cbf", "Operator", "OPERATOR" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "IAM_Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65721dc7-aa7d-45f7-8b74-907ecf7f748f", "d7d2120a-ca94-408a-9dd9-78d30d294107", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "IAM_Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d45be9d8-9511-4f5c-bedc-d39ddaebb4ee", "5f58ef37-7f92-46aa-b196-2be56381c837", "Customer", "VISITOR" });

            migrationBuilder.CreateIndex(
                name: "IX_IAM_Feature_ModuleId",
                schema: "dbo",
                table: "IAM_Feature",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_Menu_MainMenuId",
                schema: "dbo",
                table: "IAM_Menu",
                column: "MainMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_Module_MenuId",
                schema: "dbo",
                table: "IAM_Module",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "IAM_Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_RoleClaim_RoleId",
                schema: "dbo",
                table: "IAM_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_RoleFeature_FeatureId",
                schema: "dbo",
                table: "IAM_RoleFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_RoleFeature_RoleId",
                schema: "dbo",
                table: "IAM_RoleFeature",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "IAM_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "IAM_User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_UserClaim_UserId",
                schema: "dbo",
                table: "IAM_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_UserLogin_UserId",
                schema: "dbo",
                table: "IAM_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IAM_UserRole_RoleId",
                schema: "dbo",
                table: "IAM_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OCM_Customer_UserId",
                schema: "dbo",
                table: "OCM_Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OCM_Order_CustomerId",
                schema: "dbo",
                table: "OCM_Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OCM_OrderItem_OrderId",
                schema: "dbo",
                table: "OCM_OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OCM_OrderItem_ProductId",
                schema: "dbo",
                table: "OCM_OrderItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IAM_RoleClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_RoleFeature",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_UserClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_UserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_UserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_UserToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OCM_OrderItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_Feature",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OCM_Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OCM_Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_Module",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OCM_Customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_Menu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IAM_MainMenu",
                schema: "dbo");
        }
    }
}
