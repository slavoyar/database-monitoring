using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ff2a312-e799-4fa1-b5d9-7ea6816cbb4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5d172ea-41d4-4eaf-894c-f352c257d637");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1efe9143-4705-457f-b138-5e4106a3520d", "7f593a95-693d-439c-b5c0-ab75246c34db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1efe9143-4705-457f-b138-5e4106a3520d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f593a95-693d-439c-b5c0-ab75246c34db");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1efe9143-4705-457f-b138-5e4106a3520d", "1", "Admin", "ADMIN" },
                    { "8ff2a312-e799-4fa1-b5d9-7ea6816cbb4b", "3", "Manager", "MANAGER" },
                    { "d5d172ea-41d4-4eaf-894c-f352c257d637", "2", "Engineer", "ENGINEER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7f593a95-693d-439c-b5c0-ab75246c34db", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAEBUnD1gqo9+Qmp6lvvSwn5Bk/jdEAfvZNfgrlX4ktVPRF2fpic8NJ+M/fDk8cCRZ5A==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "9b5cb801-7197-4a50-8a16-f09db59cdb8a", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1efe9143-4705-457f-b138-5e4106a3520d", "7f593a95-693d-439c-b5c0-ab75246c34db" });
        }
    }
}
