using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class changeadminguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUserWorkspaces");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2204be7c-6e3d-4b38-bdc2-d49e0eff2910");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df88ba4b-c8e1-4003-8a54-cf4b5d6d0a08");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0913c32a-abc0-498e-b5be-6ca166bb5a48", "a21c54d7-d571-42e2-b3d3-d78ab5cd7f6a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0913c32a-abc0-498e-b5be-6ca166bb5a48");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a21c54d7-d571-42e2-b3d3-d78ab5cd7f6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2804a1f3-da44-4c8c-a418-3e8f6f75016f", "2", "Engineer", "ENGINEER" },
                    { "5430d357-b85f-4ad2-9125-ff84b6eeb80b", "3", "Manager", "MANAGER" },
                    { "78f8db3f-fa66-466a-9649-339364d6358f", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7499844d-efea-4494-9612-39138922c9db", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAENuPLxJqm8t7v2Z8PDZbCytPBXj7fIR1poB58O3SdBMVNeXIdeAjRA4arzIFBCj9Ng==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "b87bd3ab-ee0b-49cc-90ca-9956bd4b359e", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "78f8db3f-fa66-466a-9649-339364d6358f", "7499844d-efea-4494-9612-39138922c9db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2804a1f3-da44-4c8c-a418-3e8f6f75016f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5430d357-b85f-4ad2-9125-ff84b6eeb80b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "78f8db3f-fa66-466a-9649-339364d6358f", "7499844d-efea-4494-9612-39138922c9db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78f8db3f-fa66-466a-9649-339364d6358f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7499844d-efea-4494-9612-39138922c9db");

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    WorkspacesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.WorkspacesId);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserWorkspaces",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserWorkspaces", x => new { x.UsersId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_AuthUserWorkspaces_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthUserWorkspaces_Workspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "Workspaces",
                        principalColumn: "WorkspacesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0913c32a-abc0-498e-b5be-6ca166bb5a48", "1", "Admin", "ADMIN" },
                    { "2204be7c-6e3d-4b38-bdc2-d49e0eff2910", "2", "Engineer", "ENGINEER" },
                    { "df88ba4b-c8e1-4003-8a54-cf4b5d6d0a08", "3", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a21c54d7-d571-42e2-b3d3-d78ab5cd7f6a", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAEMKvATDYYU2rrjnM7pq/Ljt6vXhtXtqz7MR4XFKdZ+GGmzIn4kuXJy6R8gRHgy2Ckg==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "8be37c9d-2662-4a4d-85aa-9e8591206962", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0913c32a-abc0-498e-b5be-6ca166bb5a48", "a21c54d7-d571-42e2-b3d3-d78ab5cd7f6a" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserWorkspaces_WorkspacesId",
                table: "AuthUserWorkspaces",
                column: "WorkspacesId");
        }
    }
}
