using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class RenamedTableProductsForBuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsForBuy_Employees_EmployeeId",
                table: "ProductsForBuy");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsForBuy_Products_ProductId",
                table: "ProductsForBuy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsForBuy",
                table: "ProductsForBuy");

            migrationBuilder.RenameTable(
                name: "ProductsForBuy",
                newName: "ShoppingCart");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsForBuy_ProductId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsForBuy_EmployeeId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Employees_EmployeeId",
                table: "ShoppingCart",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Employees_EmployeeId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                newName: "ProductsForBuy");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ProductsForBuy",
                newName: "IX_ProductsForBuy_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_EmployeeId",
                table: "ProductsForBuy",
                newName: "IX_ProductsForBuy_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsForBuy",
                table: "ProductsForBuy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsForBuy_Employees_EmployeeId",
                table: "ProductsForBuy",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsForBuy_Products_ProductId",
                table: "ProductsForBuy",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
