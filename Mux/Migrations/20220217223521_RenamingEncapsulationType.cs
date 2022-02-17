using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class RenamingEncapsulationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncapsulationType",
                table: "Products",
                newName: "OldEncapsulationType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldEncapsulationType",
                table: "Products",
                newName: "EncapsulationType");
        }
    }
}
