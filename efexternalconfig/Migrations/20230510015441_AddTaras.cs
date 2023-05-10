using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efexternalconfig.Migrations
{
    /// <inheritdoc />
    public partial class AddTaras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Taras",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taras",
                table: "People");
        }
    }
}
