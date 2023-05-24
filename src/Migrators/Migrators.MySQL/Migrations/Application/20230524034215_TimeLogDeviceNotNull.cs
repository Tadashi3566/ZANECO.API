using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class TimeLogDeviceNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ZANECO",
                table: "TimeLogs",
                keyColumn: "Device",
                keyValue: null,
                column: "Device",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsVatable",
                schema: "Catalog",
                table: "Products",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                schema: "Catalog",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsVatable",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Specification",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                schema: "ZANECO",
                table: "TimeLogs",
                type: "varchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
