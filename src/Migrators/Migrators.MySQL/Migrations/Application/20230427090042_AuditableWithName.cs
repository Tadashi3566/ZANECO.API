using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class AuditableWithName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Tickets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Tickets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Loans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Loans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "TotalHours",
                schema: "ZANECO",
                table: "Attendance",
                type: "Double(12,2",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Decimal(12,2");

            migrationBuilder.AlterColumn<double>(
                name: "PaidHours",
                schema: "ZANECO",
                table: "Attendance",
                type: "Double(12,2",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Decimal(12,2");

            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Attendance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Attendance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CalendarId",
                schema: "ZANECO",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverName",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "ApproverName",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RecommenderName",
                schema: "ZANECO",
                table: "Appointments");

            migrationBuilder.AlterColumn<double>(
                name: "TotalHours",
                schema: "ZANECO",
                table: "Attendance",
                type: "Decimal(12,2",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Double(12,2");

            migrationBuilder.AlterColumn<double>(
                name: "PaidHours",
                schema: "ZANECO",
                table: "Attendance",
                type: "Decimal(12,2",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Double(12,2");

            migrationBuilder.AlterColumn<int>(
                name: "CalendarId",
                schema: "ZANECO",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}