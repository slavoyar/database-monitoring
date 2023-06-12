using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationalPropertiesToServerAndUserEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceEntityId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceEntityId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WorkspaceEntityId",
                table: "Users",
                newName: "WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_WorkspaceEntityId",
                table: "Users",
                newName: "IX_Users_WorkspaceId");

            migrationBuilder.RenameColumn(
                name: "WorkspaceEntityId",
                table: "Servers",
                newName: "WorkspaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_WorkspaceEntityId",
                table: "Servers",
                newName: "IX_Servers_WorkspaceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceId",
                table: "Servers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "Users",
                newName: "WorkspaceEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_WorkspaceId",
                table: "Users",
                newName: "IX_Users_WorkspaceEntityId");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "Servers",
                newName: "WorkspaceEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_WorkspaceId",
                table: "Servers",
                newName: "IX_Servers_WorkspaceEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Workspaces_WorkspaceEntityId",
                table: "Servers",
                column: "WorkspaceEntityId",
                principalTable: "Workspaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workspaces_WorkspaceEntityId",
                table: "Users",
                column: "WorkspaceEntityId",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
