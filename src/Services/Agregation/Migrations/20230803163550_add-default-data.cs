using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agregation.Migrations
{
    /// <inheritdoc />
    public partial class adddefaultdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PingStatus = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    IdAddress = table.Column<string>(type: "text", nullable: false),
                    LastSuccessLog = table.Column<string>(type: "text", nullable: false),
                    IconId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPatients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CriticalStatus = table.Column<string>(type: "text", nullable: false),
                    ErrorState = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<string>(type: "text", nullable: false),
                    RecievedAt = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ServerPatientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_ServerPatients_ServerPatientId",
                        column: x => x.ServerPatientId,
                        principalTable: "ServerPatients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ServerPatients",
                columns: new[] { "Id", "ConnectionStatus", "IconId", "IdAddress", "LastSuccessLog", "Name", "PingStatus", "Status" },
                values: new object[,]
                {
                    { new Guid("8d8a6029-676a-4e09-91c5-32c56602f67f"), true, "1", "testpatient-3", "2023-08-03 19:35:50", "testpatient3", true, "Working" },
                    { new Guid("d13920a2-4961-43cc-bd22-12187b19f512"), true, "1", "testpatient-2", "2023-08-03 19:35:50", "testpatient2", true, "Working" },
                    { new Guid("d69cd87f-1f08-4b12-af16-980b003cdc5f"), true, "1", "testpatient-1", "2023-08-03 19:35:50", "testpatient1", true, "Working" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ServerPatientId",
                table: "Logs",
                column: "ServerPatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ServerPatients");
        }
    }
}
