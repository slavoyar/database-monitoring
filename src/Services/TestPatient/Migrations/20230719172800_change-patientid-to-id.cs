using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPatient.Migrations
{
    /// <inheritdoc />
    public partial class changepatientidtoid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PatientLogs",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PatientLogs",
                newName: "PatientId");
        }
    }
}
