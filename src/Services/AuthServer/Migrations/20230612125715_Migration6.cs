using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ed8f019-3c82-4731-ba9c-51d9752a4cc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89712991-a145-4555-8827-6abe5a0584ce");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d60d8e58-93d2-4b03-94fc-d8b3a5c1a40f", "2ed596aa-8886-40dd-82f3-7225abc2bec7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60d8e58-93d2-4b03-94fc-d8b3a5c1a40f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ed596aa-8886-40dd-82f3-7225abc2bec7");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { "3ed8f019-3c82-4731-ba9c-51d9752a4cc2", "3", "Manager", "MANAGER" },
                    { "89712991-a145-4555-8827-6abe5a0584ce", "2", "Engineer", "ENGINEER" },
                    { "d60d8e58-93d2-4b03-94fc-d8b3a5c1a40f", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2ed596aa-8886-40dd-82f3-7225abc2bec7", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAENGl2kCYYUJjvIv55e+4jrNTQ6MAB7DbJTqrt8zibwaAkdOIAzhxi9qD5gUsXgL1OA==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "4155f184-461a-4739-8f59-cea1b7191cb1", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d60d8e58-93d2-4b03-94fc-d8b3a5c1a40f", "2ed596aa-8886-40dd-82f3-7225abc2bec7" });
        }
    }
}
