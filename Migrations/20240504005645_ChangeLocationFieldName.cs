using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f00die_finder_be.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLocationFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Locations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WardId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
