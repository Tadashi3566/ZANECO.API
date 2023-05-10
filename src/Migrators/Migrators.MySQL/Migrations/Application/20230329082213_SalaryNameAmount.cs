using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class SalaryNameAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryAmount",
                schema: "ZANECO",
                table: "Salaries",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "SalaryRank",
                schema: "ZANECO",
                table: "Employees",
                newName: "SalaryNumber");

            migrationBuilder.RenameColumn(
                name: "SalaryRank",
                schema: "ZANECO",
                table: "Designations",
                newName: "SalaryNumber");

            migrationBuilder.RenameColumn(
                name: "SalaryAmount",
                schema: "ZANECO",
                table: "Designations",
                newName: "Amount");

            migrationBuilder.AddColumn<string>(
                name: "SalaryName",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SalaryName",
                schema: "ZANECO",
                table: "Designations",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryName",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryName",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "ZANECO",
                table: "Salaries",
                newName: "SalaryAmount");

            migrationBuilder.RenameColumn(
                name: "SalaryNumber",
                schema: "ZANECO",
                table: "Employees",
                newName: "SalaryRank");

            migrationBuilder.RenameColumn(
                name: "SalaryNumber",
                schema: "ZANECO",
                table: "Designations",
                newName: "SalaryRank");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "ZANECO",
                table: "Designations",
                newName: "SalaryAmount");
        }
    }
}