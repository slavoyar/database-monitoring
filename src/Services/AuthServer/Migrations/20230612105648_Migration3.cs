using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
