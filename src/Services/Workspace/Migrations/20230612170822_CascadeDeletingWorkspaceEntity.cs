using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletingWorkspaceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceId",
                table: "Servers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceId",
                table: "Servers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
