using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class AddedProductChangeCountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductChangeCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entrys = table.Column<int>(type: "int", nullable: false),
                    Devolutions = table.Column<int>(type: "int", nullable: false),
                    Egresses = table.Column<int>(type: "int", nullable: false),
                    AmountAdjustments = table.Column<int>(type: "int", nullable: false),
                    PriceAdjustments = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductChangeCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductChangeCounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductChangeCounts_ProductId",
                table: "ProductChangeCounts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductChangeCounts");
        }
    }
}
