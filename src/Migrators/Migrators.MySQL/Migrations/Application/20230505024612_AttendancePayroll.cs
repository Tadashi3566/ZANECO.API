using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.migrations.application
{
    public partial class AttendancePayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "ZANECO",
                table: "Attendance",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PayrollId",
                schema: "ZANECO",
                table: "Attendance",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "PayrollName",
                schema: "ZANECO",
                table: "Attendance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_PayrollId",
                schema: "ZANECO",
                table: "Attendance",
                column: "PayrollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Payrolls_PayrollId",
                schema: "ZANECO",
                table: "Attendance",
                column: "PayrollId",
                principalSchema: "ZANECO",
                principalTable: "Payrolls",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Payrolls_PayrollId",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_PayrollId",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "PayrollName",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Employees",
                keyColumn: "MiddleName",
                keyValue: null,
                column: "MiddleName",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Employees",
                keyColumn: "Extension",
                keyValue: null,
                column: "Extension",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                schema: "ZANECO",
                table: "Employees",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}