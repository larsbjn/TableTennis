using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultiLingualRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Rules",
                newName: "English");

            migrationBuilder.AddColumn<string>(
                name: "Danish",
                table: "Rules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Danish",
                table: "Rules");

            migrationBuilder.RenameColumn(
                name: "English",
                table: "Rules",
                newName: "Text");
        }
    }
}
