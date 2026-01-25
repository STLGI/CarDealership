using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealership.Migrations
{
    /// <inheritdoc />
    public partial class carImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars",
                column: "ManufacturerId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Companies_ManufacturerId",
                table: "Cars",
                column: "ManufacturerId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
