using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class WithApprovalRemarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Tickets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Loans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Attendance",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "ZANECO",
                table: "Appointments",
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
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "ZANECO",
                table: "Appointments");
        }
    }
}
