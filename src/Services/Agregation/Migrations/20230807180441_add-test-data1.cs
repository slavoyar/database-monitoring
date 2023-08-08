using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agregation.Migrations
{
    /// <inheritdoc />
    public partial class addtestdata1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("8d8a6029-676a-4e09-91c5-32c56602f67f"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:04:41");

            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("d13920a2-4961-43cc-bd22-12187b19f512"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:04:41");

            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("d69cd87f-1f08-4b12-af16-980b003cdc5f"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:04:41");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("8d8a6029-676a-4e09-91c5-32c56602f67f"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:03:40");

            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("d13920a2-4961-43cc-bd22-12187b19f512"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:03:40");

            migrationBuilder.UpdateData(
                table: "ServerPatients",
                keyColumn: "Id",
                keyValue: new Guid("d69cd87f-1f08-4b12-af16-980b003cdc5f"),
                column: "LastSuccessLog",
                value: "2023-08-07 21:03:40");
        }
    }
}
