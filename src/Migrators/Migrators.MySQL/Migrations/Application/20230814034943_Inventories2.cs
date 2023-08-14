using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class Inventories2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                schema: "ZANECO",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                schema: "ZANECO",
                newName: "Inventories",
                newSchema: "ZANECO");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_EmployeeId",
                schema: "ZANECO",
                table: "Inventories",
                newName: "IX_Inventories_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                schema: "ZANECO",
                table: "Inventories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Inventories",
                column: "EmployeeId",
                principalSchema: "ZANECO",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                schema: "ZANECO",
                table: "Inventories");

            migrationBuilder.RenameTable(
                name: "Inventories",
                schema: "ZANECO",
                newName: "Inventory",
                newSchema: "ZANECO");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_EmployeeId",
                schema: "ZANECO",
                table: "Inventory",
                newName: "IX_Inventory_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                schema: "ZANECO",
                table: "Inventory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Inventory",
                column: "EmployeeId",
                principalSchema: "ZANECO",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
