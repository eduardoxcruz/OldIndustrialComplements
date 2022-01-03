using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Password = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DebugCode = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Enrollment = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    MountingTechnology = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    EncapsulationType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ShortDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsUsingInventory = table.Column<bool>(type: "bit", nullable: true),
                    CurrentAmount = table.Column<int>(type: "int", nullable: true),
                    MinAmount = table.Column<int>(type: "int", nullable: true),
                    MaxAmount = table.Column<int>(type: "int", nullable: true),
                    Container = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    Location = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    BranchOffice = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Rack = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Shelf = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BuyPrice = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    UnitType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Manufacturer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TypeOfStock = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsManualProfit = table.Column<bool>(type: "bit", nullable: true),
                    PercentageOfProfit = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    PercentageOfDiscount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    SalePriceWithoutDiscount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    SalePriceWithDiscount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    ProfitWithoutDiscount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    ProfitWithDiscount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    FullDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Memo = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Type = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Status = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductRequests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsForBuy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RequestedAmount = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsForBuy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsForBuy_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductsForBuy_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsOfProductMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    PreviousAmount = table.Column<int>(type: "int", nullable: true),
                    NewAmount = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    Provider = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ProductFullDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    EmployeeName = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsOfProductMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordsOfProductMovements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RecordsOfProductMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_EmployeeId",
                table: "ProductRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_ProductId",
                table: "ProductRequests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsForBuy_EmployeeId",
                table: "ProductsForBuy",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsForBuy_ProductId",
                table: "ProductsForBuy",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsOfProductMovements_EmployeeId",
                table: "RecordsOfProductMovements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsOfProductMovements_ProductId",
                table: "RecordsOfProductMovements",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRequests");

            migrationBuilder.DropTable(
                name: "ProductsForBuy");

            migrationBuilder.DropTable(
                name: "RecordsOfProductMovements");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
