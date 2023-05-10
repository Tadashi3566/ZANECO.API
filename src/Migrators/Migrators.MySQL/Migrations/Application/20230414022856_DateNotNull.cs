using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class DateNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "DateEffectivityEnd",
            //    schema: "ZANECO",
            //    table: "Contributions",
            //    newName: "EndDate");

            //migrationBuilder.RenameColumn(
            //    name: "DateEffectivityStart",
            //    schema: "ZANECO",
            //    table: "Contributions",
            //    newName: "StartDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RaffleDate",
                schema: "ZANECO",
                table: "Raffles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalSalary",
                schema: "ZANECO",
                table: "Payrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalNet",
                schema: "ZANECO",
                table: "Payrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGross",
                schema: "ZANECO",
                table: "Payrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeduction",
                schema: "ZANECO",
                table: "Payrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAdditional",
                schema: "ZANECO",
                table: "Payrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Payrolls",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReleased",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostingDate",
                schema: "ZANECO",
                table: "Ledgers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Employers",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                schema: "ZANECO",
                table: "Employers",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "ZANECO",
                table: "Employers",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "ZANECO",
                table: "Employees",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Net",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gross",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Deduction",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Additional",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Relation",
                schema: "ZANECO",
                table: "Dependents",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Dependents",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "ZANECO",
                table: "Dependents",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "ZANECO",
                table: "Dependents",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeStart",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeEnd",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EmployerContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EmployeeContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "Decimal(12,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "Contributions",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "Contributions",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "DateEffectivityEnd",
               schema: "ZANECO",
               table: "Contributions",
               newName: "EndDate");

            migrationBuilder.RenameColumn(
               name: "DateEffectivityStart",
               schema: "ZANECO",
               table: "Contributions",
               newName: "DateStart");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RaffleDate",
                schema: "ZANECO",
                table: "Raffles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalSalary",
                schema: "ZANECO",
                table: "Payrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalNet",
                schema: "ZANECO",
                table: "Payrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGross",
                schema: "ZANECO",
                table: "Payrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeduction",
                schema: "ZANECO",
                table: "Payrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAdditional",
                schema: "ZANECO",
                table: "Payrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Payrolls",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "Payrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReleased",
                schema: "ZANECO",
                table: "Loans",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostingDate",
                schema: "ZANECO",
                table: "Ledgers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Employers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldMaxLength: 1024)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                schema: "ZANECO",
                table: "Employers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "ZANECO",
                table: "Employers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldMaxLength: 1024)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "ZANECO",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "Net",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Gross",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "Deduction",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Additional",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PayrollDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ZANECO",
                table: "EmployeePayrollDetails",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ZANECO",
                table: "EmployeeAdjustments",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Relation",
                schema: "ZANECO",
                table: "Dependents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ZANECO",
                table: "Dependents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldMaxLength: 1024)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "ZANECO",
                table: "Dependents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "ZANECO",
                table: "Dependents",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeStart",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "RangeEnd",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "EmployerContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "EmployeeContribution",
                schema: "ZANECO",
                table: "Contributions",
                type: "decimal(65,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEffectivityEnd",
                schema: "ZANECO",
                table: "Contributions",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEffectivityStart",
                schema: "ZANECO",
                table: "Contributions",
                type: "date",
                nullable: true);
        }
    }
}