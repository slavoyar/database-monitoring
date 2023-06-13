using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71ac5c1b-aa44-4703-9dba-5efb8e42d4e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6731e5d-7191-48e5-bf4c-714775267364");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "efa19287-b4e2-4838-823b-e56670c5df84", "54608683-0654-4c0d-a006-87b02332dee4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efa19287-b4e2-4838-823b-e56670c5df84");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54608683-0654-4c0d-a006-87b02332dee4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15f7bbe1-0395-4a47-bec8-b7426bdc5e2a", "1", "Admin", "ADMIN" },
                    { "6eeb9117-31d1-4a1e-8a43-23518e9818cd", "2", "Engineer", "ENGINEER" },
                    { "8545f06f-0340-4af1-837c-ec2d7f7ba6b1", "3", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "06680121-c4b0-4ccf-b676-443bd780a77f", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAEHfGr5foRsSTZE2l8kUcvmVVQWZUpAA5Wnx3lfvOYk5pqB4G2kGqWMXU6bHXoNivRg==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "7e11493c-6981-4a86-9be4-824429085189", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "15f7bbe1-0395-4a47-bec8-b7426bdc5e2a", "06680121-c4b0-4ccf-b676-443bd780a77f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eeb9117-31d1-4a1e-8a43-23518e9818cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8545f06f-0340-4af1-837c-ec2d7f7ba6b1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "15f7bbe1-0395-4a47-bec8-b7426bdc5e2a", "06680121-c4b0-4ccf-b676-443bd780a77f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15f7bbe1-0395-4a47-bec8-b7426bdc5e2a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "06680121-c4b0-4ccf-b676-443bd780a77f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71ac5c1b-aa44-4703-9dba-5efb8e42d4e6", "3", "Manager", "MANAGER" },
                    { "a6731e5d-7191-48e5-bf4c-714775267364", "2", "Engineer", "ENGINEER" },
                    { "efa19287-b4e2-4838-823b-e56670c5df84", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "54608683-0654-4c0d-a006-87b02332dee4", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAENCBK9Ipgzaziz3apCdEu5jhdgGqX3CMa77NwrOGjWfJzx+4KnBv3+eXVsbfRbVGCQ==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "13fd5a03-8759-42fd-9c16-7b2ba56d66a8", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "efa19287-b4e2-4838-823b-e56670c5df84", "54608683-0654-4c0d-a006-87b02332dee4" });
        }
    }
}
