using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class Remarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Winners",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "TicketProgress",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Teams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Suppliers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Schedules",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "ScheduleDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Sales",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "SaleItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Salaries",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Routes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "RemoteCollections",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "RatingTemplates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Ratings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Rates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Raffles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Prizes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerRates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerConsumptions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerBills",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Payrolls",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "PayrollAdjustments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageTemplates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageOut",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageLog",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageIn",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Ledgers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Employers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Documents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Discounts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Designations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Dependents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Customers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Contributions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Contacts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Calendars",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "Catalog",
                table: "Brands",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Barcodes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Barangays",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Areas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Adjustments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Accounts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "TicketProgress");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "RemoteCollections");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "RatingTemplates");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Raffles");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Prizes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerRates");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerConsumptions");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "PowerBills");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageOut");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageLog");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "MessageIn");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeePayrollDetails");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "EmployeeAdjustments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "Catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Barangays");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Accounts");
        }
    }
}
