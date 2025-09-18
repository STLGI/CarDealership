using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealership.Migrations
{
    /// <inheritdoc />
    public partial class DealershipDbCarCompRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sName",
                table: "Companies",
                newName: "SName");

            migrationBuilder.RenameColumn(
                name: "img",
                table: "Companies",
                newName: "Img");

            migrationBuilder.RenameColumn(
                name: "pics",
                table: "Cars",
                newName: "Pics");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ManufacturerId",
                table: "Cars",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars",
                column: "ManufacturerId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ManufacturerId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "SName",
                table: "Companies",
                newName: "sName");

            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Companies",
                newName: "img");

            migrationBuilder.RenameColumn(
                name: "Pics",
                table: "Cars",
                newName: "pics");
        }
    }
}
