using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class RenamedTableRecordOfProductMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordsOfProductMovements_Employees_EmployeeId",
                table: "RecordsOfProductMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordsOfProductMovements_Products_ProductId",
                table: "RecordsOfProductMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordsOfProductMovements",
                table: "RecordsOfProductMovements");

            migrationBuilder.RenameTable(
                name: "RecordsOfProductMovements",
                newName: "ProductChangeLogs");

            migrationBuilder.RenameIndex(
                name: "IX_RecordsOfProductMovements_ProductId",
                table: "ProductChangeLogs",
                newName: "IX_ProductChangeLogs_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordsOfProductMovements_EmployeeId",
                table: "ProductChangeLogs",
                newName: "IX_ProductChangeLogs_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductChangeLogs",
                table: "ProductChangeLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductChangeLogs_Employees_EmployeeId",
                table: "ProductChangeLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductChangeLogs_Products_ProductId",
                table: "ProductChangeLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductChangeLogs_Employees_EmployeeId",
                table: "ProductChangeLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductChangeLogs_Products_ProductId",
                table: "ProductChangeLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductChangeLogs",
                table: "ProductChangeLogs");

            migrationBuilder.RenameTable(
                name: "ProductChangeLogs",
                newName: "RecordsOfProductMovements");

            migrationBuilder.RenameIndex(
                name: "IX_ProductChangeLogs_ProductId",
                table: "RecordsOfProductMovements",
                newName: "IX_RecordsOfProductMovements_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductChangeLogs_EmployeeId",
                table: "RecordsOfProductMovements",
                newName: "IX_RecordsOfProductMovements_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordsOfProductMovements",
                table: "RecordsOfProductMovements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordsOfProductMovements_Employees_EmployeeId",
                table: "RecordsOfProductMovements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordsOfProductMovements_Products_ProductId",
                table: "RecordsOfProductMovements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
