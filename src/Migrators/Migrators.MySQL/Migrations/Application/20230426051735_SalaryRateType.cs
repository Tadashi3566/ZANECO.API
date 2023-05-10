using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class SalaryRateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayType",
                schema: "ZANECO",
                table: "Salaries",
                newName: "RateType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RateType",
                schema: "ZANECO",
                table: "Salaries",
                newName: "PayType");
        }
    }
}