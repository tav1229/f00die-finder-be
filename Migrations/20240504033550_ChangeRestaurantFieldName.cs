using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f00die_finder_be.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRestaurantFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceRange",
                table: "Restaurants",
                newName: "PriceRangePerPerson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceRangePerPerson",
                table: "Restaurants",
                newName: "PriceRange");
        }
    }
}
