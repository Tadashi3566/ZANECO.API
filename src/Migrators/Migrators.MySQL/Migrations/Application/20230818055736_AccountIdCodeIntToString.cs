using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class AccountIdCodeIntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdCode",
                schema: "ZANECO",
                table: "Ledgers",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "IdCode",
                schema: "ZANECO",
                table: "Accounts",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdCode",
                schema: "ZANECO",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "IdCode",
                schema: "ZANECO",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
