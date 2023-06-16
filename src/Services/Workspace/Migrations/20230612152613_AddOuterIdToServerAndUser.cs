﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Migrations
{
    /// <inheritdoc />
    public partial class AddOuterIdToServerAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OuterId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OuterId",
                table: "Servers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OuterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OuterId",
                table: "Servers");
        }
    }
}
