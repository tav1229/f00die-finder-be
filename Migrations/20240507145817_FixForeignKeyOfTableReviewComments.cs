using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f00die_finder_be.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyOfTableReviewComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Users_RestaurantId",
                table: "ReviewComments");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_UserId",
                table: "ReviewComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Users_UserId",
                table: "ReviewComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Users_UserId",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_UserId",
                table: "ReviewComments");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Users_RestaurantId",
                table: "ReviewComments",
                column: "RestaurantId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
