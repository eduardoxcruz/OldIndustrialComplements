using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class AddedCountPropertiesToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountAdjustments",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Devolutions",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Egresses",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Entrys",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceAdjustments",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountAdjustments",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Devolutions",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Egresses",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Entrys",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceAdjustments",
                table: "Products");
        }
    }
}
