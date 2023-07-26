using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPatient.Migrations
{
    /// <inheritdoc />
    public partial class LogsModelDeleteReceivedAtAddSended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedAt",
                table: "PatientLogs");

            migrationBuilder.AddColumn<byte>(
                name: "Sended",
                table: "PatientLogs",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sended",
                table: "PatientLogs");

            migrationBuilder.AddColumn<string>(
                name: "ReceivedAt",
                table: "PatientLogs",
                type: "text",
                nullable: true);
        }
    }
}
