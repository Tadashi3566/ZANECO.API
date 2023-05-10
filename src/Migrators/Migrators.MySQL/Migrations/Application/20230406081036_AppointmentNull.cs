using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class AppointmentNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceRule",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceException",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CssClass",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Appointments",
                keyColumn: "RecurrenceRule",
                keyValue: null,
                column: "RecurrenceRule",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceRule",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Appointments",
                keyColumn: "RecurrenceException",
                keyValue: null,
                column: "RecurrenceException",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "RecurrenceException",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Appointments",
                keyColumn: "Location",
                keyValue: null,
                column: "Location",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "Appointments",
                keyColumn: "CssClass",
                keyValue: null,
                column: "CssClass",
                value: string.Empty);

            migrationBuilder.AlterColumn<string>(
                name: "CssClass",
                schema: "ZANECO",
                table: "Appointments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}