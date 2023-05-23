using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class TimeLogDeviceSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Device",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "SyncDateTime",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "Device",
                schema: "ZANECO",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "SyncDateTime",
                schema: "ZANECO",
                table: "TimeLogs");
        }
    }
}
