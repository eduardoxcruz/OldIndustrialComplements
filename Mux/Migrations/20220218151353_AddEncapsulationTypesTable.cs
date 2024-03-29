﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Migrations
{
    public partial class AddEncapsulationTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncapsulationTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EncapsulationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    BodyWidth = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[Name] + ', ' + [BodyWidth]", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncapsulationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_EncapsulationTypeId",
                table: "Products",
                column: "EncapsulationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_EncapsulationTypes_EncapsulationTypeId",
                table: "Products",
                column: "EncapsulationTypeId",
                principalTable: "EncapsulationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_EncapsulationTypes_EncapsulationTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "EncapsulationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_EncapsulationTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EncapsulationTypeId",
                table: "Products");
        }
    }
}
