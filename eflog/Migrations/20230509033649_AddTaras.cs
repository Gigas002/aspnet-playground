﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eflog.Migrations
{
    /// <inheritdoc />
    public partial class AddTaras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Taras",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taras",
                table: "Users");
        }
    }
}
