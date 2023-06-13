using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76a7048b-dae7-4c97-b862-2f560ca54609");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c83f18fa-4dd4-419d-b879-ee51d679c217");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5a241444-d151-489f-a117-ce33ae058d1b", "5f13ff5b-28e2-451e-9286-aa470f27695d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a241444-d151-489f-a117-ce33ae058d1b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f13ff5b-28e2-451e-9286-aa470f27695d");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "5a241444-d151-489f-a117-ce33ae058d1b", "1", "Admin", "ADMIN" },
                    { "76a7048b-dae7-4c97-b862-2f560ca54609", "3", "Manager", "MANAGER" },
                    { "c83f18fa-4dd4-419d-b879-ee51d679c217", "2", "Engineer", "ENGINEER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullUserName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5f13ff5b-28e2-451e-9286-aa470f27695d", 0, null, "admin@admin", false, "Admin Adminovich", false, null, "ADMIN@ADMIN", "ADMIN@ADMIN", "Qwe123!@#", "AQAAAAIAAYagAAAAEJPevY+fJZ177mXdpJX9U/FV05YSahqYg8xt6mUtNfPfJ8KiUj9GCuNpNeHFO66IoA==", "1234567890", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "61a63eaf-4e39-439d-9d8f-51cdea48dd9e", false, "admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5a241444-d151-489f-a117-ce33ae058d1b", "5f13ff5b-28e2-451e-9286-aa470f27695d" });
        }
    }
}
