using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class TeamDepartmentPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "ZANECO",
                table: "Teams",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                schema: "ZANECO",
                table: "Teams",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                schema: "ZANECO",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Position",
                schema: "ZANECO",
                table: "Teams");
        }
    }
}
