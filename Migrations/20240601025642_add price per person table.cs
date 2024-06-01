using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f00die_finder_be.Migrations
{
    /// <inheritdoc />
    public partial class addpriceperpersontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceRangePerPerson",
                table: "Restaurants");

            migrationBuilder.AddColumn<Guid>(
                name: "PriceRangePerPersonId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PriceRangePerPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRangePerPersons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_PriceRangePerPersonId",
                table: "Restaurants",
                column: "PriceRangePerPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_PriceRangePerPersons_PriceRangePerPersonId",
                table: "Restaurants",
                column: "PriceRangePerPersonId",
                principalTable: "PriceRangePerPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_PriceRangePerPersons_PriceRangePerPersonId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "PriceRangePerPersons");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_PriceRangePerPersonId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PriceRangePerPersonId",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "PriceRangePerPerson",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
