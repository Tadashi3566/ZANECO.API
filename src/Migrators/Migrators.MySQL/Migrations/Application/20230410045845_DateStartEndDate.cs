using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class DateStartEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "Salaries",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "Salaries",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "Payrolls",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "Payrolls",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "Loans",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "Loans",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                newName: "EndDate");

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                schema: "ZANECO",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "Salaries",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "Salaries",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "Payrolls",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "Payrolls",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "Loans",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "Loans",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                newName: "DateEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                newName: "DateEnd");
        }
    }
}