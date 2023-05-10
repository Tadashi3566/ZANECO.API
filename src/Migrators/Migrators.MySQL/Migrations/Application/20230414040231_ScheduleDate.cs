using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class ScheduleDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduleDate",
                schema: "ZANECO",
                table: "MessageTemplates",
                newName: "Schedule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Schedule",
                schema: "ZANECO",
                table: "MessageTemplates",
                newName: "ScheduleDate");
        }
    }
}