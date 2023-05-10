using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class AuditableWithApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "TicketProgress");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "TicketProgress");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "TicketProgress");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "RemoteCollections");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "RemoteCollections");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "RemoteCollections");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "RatingTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "RatingTemplates");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "RatingTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Raffles");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Raffles");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Raffles");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Prizes");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Prizes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Prizes");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerRates");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerRates");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "PowerRates");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerConsumptions");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerConsumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "PowerConsumptions");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerBills");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerBills");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "PowerBills");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageOut");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageOut");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "MessageOut");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageLog");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageLog");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "MessageLog");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageIn");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageIn");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "MessageIn");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeePayrollDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeePayrollDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeePayrollDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeeAdjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeeAdjustments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeeAdjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Barangays");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Barangays");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Barangays");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ZANECO",
                table: "Accounts");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "TimeLogs",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "VARCHAR(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Tickets",
                type: "VARCHAR(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldMaxLength: 16,
                oldNullable: true,
                oldDefaultValue: "PENDING")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Tickets",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Tickets",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Loans",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Loans",
                type: "VARCHAR(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Loans",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Loans",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Attendance",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Attendance",
                type: "VARCHAR(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Attendance",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Attendance",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Appointments",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Appointments",
                type: "VARCHAR(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Appointments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Appointments",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "RecommendedBy",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RecommendedOn",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Winners",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Winners",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Winners",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "VARCHAR(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Tickets",
                type: "VARCHAR(16)",
                maxLength: 16,
                nullable: true,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldMaxLength: 16,
                oldDefaultValue: "PENDING")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "TicketProgress",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "TicketProgress",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "TicketProgress",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Suppliers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Suppliers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Suppliers",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Schedules",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Schedules",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Schedules",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "ScheduleDetails",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "ScheduleDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "ScheduleDetails",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Sales",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Sales",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "SaleItems",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "SaleItems",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "SaleItems",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Salaries",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Salaries",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Salaries",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Routes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Routes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Routes",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "RemoteCollections",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "RemoteCollections",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "RemoteCollections",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "RatingTemplates",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "RatingTemplates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "RatingTemplates",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Ratings",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Ratings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Ratings",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Rates",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Rates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Rates",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Raffles",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Raffles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Raffles",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Products",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Products",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Prizes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Prizes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Prizes",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerRates",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerRates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "PowerRates",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerConsumptions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerConsumptions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "PowerConsumptions",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PowerBills",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PowerBills",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "PowerBills",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Payrolls",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Payrolls",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Payrolls",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "PayrollAdjustments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "PayrollAdjustments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "PayrollAdjustments",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageTemplates",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageTemplates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "MessageTemplates",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageOut",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageOut",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "MessageOut",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageLog",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageLog",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "MessageLog",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "MessageIn",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "MessageIn",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "MessageIn",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Members",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Members",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Members",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Loans",
                type: "VARCHAR(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Ledgers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Ledgers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Ledgers",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Groups",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Groups",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Groups",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Employers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Employers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Employers",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Employees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Employees",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Employees",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Documents",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Documents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Documents",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Discounts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Discounts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Discounts",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Designations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Designations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Designations",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Dependents",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Dependents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Dependents",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Customers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Customers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Customers",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Contributions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Contributions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Contributions",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Contacts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Contacts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Contacts",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Calendars",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Calendars",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Calendars",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Brands",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Brands",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Brands",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Barcodes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Barcodes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Barcodes",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Barangays",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Barangays",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Barangays",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Attendance",
                type: "VARCHAR(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Areas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Areas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Areas",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Appointments",
                type: "VARCHAR(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Adjustments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Adjustments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Adjustments",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "ZANECO",
                table: "Accounts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                schema: "ZANECO",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "ZANECO",
                table: "Accounts",
                type: "VARCHAR(16)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}