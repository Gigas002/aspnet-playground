using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eflog.Migrations
{
    /// <inheritdoc />
    public partial class RmTaras2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taras",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Taras",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
