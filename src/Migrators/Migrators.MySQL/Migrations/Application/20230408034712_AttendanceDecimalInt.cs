using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class AttendanceDecimalInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOutDate",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.AlterColumn<int>(
                name: "UnderTimeMinutes",
                schema: "ZANECO",
                table: "Attendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Decimal(12,2");

            migrationBuilder.AlterColumn<int>(
                name: "LateMinutes",
                schema: "ZANECO",
                table: "Attendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(12,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnderTimeMinutes",
                schema: "ZANECO",
                table: "Attendance",
                type: "Decimal(12,2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "LateMinutes",
                schema: "ZANECO",
                table: "Attendance",
                type: "Decimal(12,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOutDate",
                schema: "ZANECO",
                table: "Attendance",
                type: "date",
                nullable: true);
        }
    }
}