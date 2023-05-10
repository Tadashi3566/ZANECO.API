using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class AppointmentDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "ZANECO",
                table: "Appointments",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                schema: "ZANECO",
                table: "Appointments",
                newName: "EndDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                schema: "ZANECO",
                table: "Appointments",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                schema: "ZANECO",
                table: "Appointments",
                newName: "EndTime");
        }
    }
}