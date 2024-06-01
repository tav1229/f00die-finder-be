using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f00die_finder_be.Migrations
{
    /// <inheritdoc />
    public partial class addiconurlforcuisinetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "CuisineTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "CuisineTypes");
        }
    }
}
