using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Winners",
                schema: "Catalog",
                newName: "Winners",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "TimeLogs",
                schema: "Catalog",
                newName: "TimeLogs",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Tickets",
                schema: "Catalog",
                newName: "Tickets",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "TicketProgress",
                schema: "Catalog",
                newName: "TicketProgress",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                schema: "Catalog",
                newName: "Suppliers",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Schedules",
                schema: "Catalog",
                newName: "Schedules",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "ScheduleDetails",
                schema: "Catalog",
                newName: "ScheduleDetails",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Sales",
                schema: "Catalog",
                newName: "Sales",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "SaleItems",
                schema: "Catalog",
                newName: "SaleItems",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Salaries",
                schema: "Catalog",
                newName: "Salaries",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Routes",
                schema: "Catalog",
                newName: "Routes",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "RemoteCollections",
                schema: "Catalog",
                newName: "RemoteCollections",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "RatingTemplates",
                schema: "Catalog",
                newName: "RatingTemplates",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Ratings",
                schema: "Catalog",
                newName: "Ratings",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Rates",
                schema: "Catalog",
                newName: "Rates",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Raffles",
                schema: "Catalog",
                newName: "Raffles",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Prizes",
                schema: "Catalog",
                newName: "Prizes",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "PowerRates",
                schema: "Catalog",
                newName: "PowerRates",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "PowerConsumptions",
                schema: "Catalog",
                newName: "PowerConsumptions",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "PowerBills",
                schema: "Catalog",
                newName: "PowerBills",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Payrolls",
                schema: "Catalog",
                newName: "Payrolls",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "PayrollAdjustments",
                schema: "Catalog",
                newName: "PayrollAdjustments",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "MessageTemplates",
                schema: "Catalog",
                newName: "MessageTemplates",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "MessageOut",
                schema: "Catalog",
                newName: "MessageOut",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "MessageLog",
                schema: "Catalog",
                newName: "MessageLog",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "MessageIn",
                schema: "Catalog",
                newName: "MessageIn",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Members",
                schema: "Catalog",
                newName: "Members",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Loans",
                schema: "Catalog",
                newName: "Loans",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Ledgers",
                schema: "Catalog",
                newName: "Ledgers",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Groups",
                schema: "Catalog",
                newName: "Groups",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Employers",
                schema: "Catalog",
                newName: "Employers",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "Catalog",
                newName: "Employees",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "EmployeePayrolls",
                schema: "Catalog",
                newName: "EmployeePayrolls",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "EmployeePayrollDetails",
                schema: "Catalog",
                newName: "EmployeePayrollDetails",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "EmployeeAdjustments",
                schema: "Catalog",
                newName: "EmployeeAdjustments",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "Catalog",
                newName: "Documents",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Discounts",
                schema: "Catalog",
                newName: "Discounts",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Designations",
                schema: "Catalog",
                newName: "Designations",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Dependents",
                schema: "Catalog",
                newName: "Dependents",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Catalog",
                newName: "Customers",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Contributions",
                schema: "Catalog",
                newName: "Contributions",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Contacts",
                schema: "Catalog",
                newName: "Contacts",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Calendars",
                schema: "Catalog",
                newName: "Calendars",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Barcodes",
                schema: "Catalog",
                newName: "Barcodes",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Barangays",
                schema: "Catalog",
                newName: "Barangays",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Attendance",
                schema: "Catalog",
                newName: "Attendance",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Areas",
                schema: "Catalog",
                newName: "Areas",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Appointments",
                schema: "Catalog",
                newName: "Appointments",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Adjustments",
                schema: "Catalog",
                newName: "Adjustments",
                newSchema: "ZANECO");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "Catalog",
                newName: "Accounts",
                newSchema: "ZANECO");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "ScheduleDetails",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Salaries",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "RatingTemplates",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Ratings",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Rates",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Payrolls",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "PayrollAdjustments",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "MessageTemplates",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "MessageLog",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Loans",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Employers",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Documents",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Designations",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Dependents",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Contributions",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Contacts",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Calendars",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Attendance",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Appointments",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "ZANECO",
                table: "Adjustments",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: string.Empty)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "RatingTemplates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "MessageLog");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeePayrollDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "EmployeeAdjustments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "ZANECO",
                table: "Adjustments");

            migrationBuilder.RenameTable(
                name: "Winners",
                schema: "ZANECO",
                newName: "Winners",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "TimeLogs",
                schema: "ZANECO",
                newName: "TimeLogs",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Tickets",
                schema: "ZANECO",
                newName: "Tickets",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "TicketProgress",
                schema: "ZANECO",
                newName: "TicketProgress",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                schema: "ZANECO",
                newName: "Suppliers",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Schedules",
                schema: "ZANECO",
                newName: "Schedules",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "ScheduleDetails",
                schema: "ZANECO",
                newName: "ScheduleDetails",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Sales",
                schema: "ZANECO",
                newName: "Sales",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "SaleItems",
                schema: "ZANECO",
                newName: "SaleItems",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Salaries",
                schema: "ZANECO",
                newName: "Salaries",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Routes",
                schema: "ZANECO",
                newName: "Routes",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "RemoteCollections",
                schema: "ZANECO",
                newName: "RemoteCollections",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "RatingTemplates",
                schema: "ZANECO",
                newName: "RatingTemplates",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Ratings",
                schema: "ZANECO",
                newName: "Ratings",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Rates",
                schema: "ZANECO",
                newName: "Rates",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Raffles",
                schema: "ZANECO",
                newName: "Raffles",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Prizes",
                schema: "ZANECO",
                newName: "Prizes",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "PowerRates",
                schema: "ZANECO",
                newName: "PowerRates",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "PowerConsumptions",
                schema: "ZANECO",
                newName: "PowerConsumptions",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "PowerBills",
                schema: "ZANECO",
                newName: "PowerBills",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Payrolls",
                schema: "ZANECO",
                newName: "Payrolls",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "PayrollAdjustments",
                schema: "ZANECO",
                newName: "PayrollAdjustments",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "MessageTemplates",
                schema: "ZANECO",
                newName: "MessageTemplates",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "MessageOut",
                schema: "ZANECO",
                newName: "MessageOut",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "MessageLog",
                schema: "ZANECO",
                newName: "MessageLog",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "MessageIn",
                schema: "ZANECO",
                newName: "MessageIn",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Members",
                schema: "ZANECO",
                newName: "Members",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Loans",
                schema: "ZANECO",
                newName: "Loans",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Ledgers",
                schema: "ZANECO",
                newName: "Ledgers",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Groups",
                schema: "ZANECO",
                newName: "Groups",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Employers",
                schema: "ZANECO",
                newName: "Employers",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "ZANECO",
                newName: "Employees",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "EmployeePayrolls",
                schema: "ZANECO",
                newName: "EmployeePayrolls",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "EmployeePayrollDetails",
                schema: "ZANECO",
                newName: "EmployeePayrollDetails",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "EmployeeAdjustments",
                schema: "ZANECO",
                newName: "EmployeeAdjustments",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "ZANECO",
                newName: "Documents",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Discounts",
                schema: "ZANECO",
                newName: "Discounts",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Designations",
                schema: "ZANECO",
                newName: "Designations",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Dependents",
                schema: "ZANECO",
                newName: "Dependents",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "ZANECO",
                newName: "Customers",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Contributions",
                schema: "ZANECO",
                newName: "Contributions",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Contacts",
                schema: "ZANECO",
                newName: "Contacts",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Calendars",
                schema: "ZANECO",
                newName: "Calendars",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Barcodes",
                schema: "ZANECO",
                newName: "Barcodes",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Barangays",
                schema: "ZANECO",
                newName: "Barangays",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Attendance",
                schema: "ZANECO",
                newName: "Attendance",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Areas",
                schema: "ZANECO",
                newName: "Areas",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Appointments",
                schema: "ZANECO",
                newName: "Appointments",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Adjustments",
                schema: "ZANECO",
                newName: "Adjustments",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "ZANECO",
                newName: "Accounts",
                newSchema: "Catalog");
        }
    }
}