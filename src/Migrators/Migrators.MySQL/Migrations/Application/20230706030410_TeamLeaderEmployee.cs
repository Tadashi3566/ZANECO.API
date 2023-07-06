using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class TeamLeaderEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "ZANECO",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "MemberName",
                schema: "ZANECO",
                table: "Teams",
                newName: "LeaderName");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                schema: "ZANECO",
                table: "Teams",
                newName: "LeaderId");

            migrationBuilder.RenameColumn(
                name: "ManagerName",
                schema: "ZANECO",
                table: "Teams",
                newName: "EmployeeName");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                schema: "ZANECO",
                table: "Teams",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Teams",
                column: "EmployeeId",
                principalSchema: "ZANECO",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "LeaderName",
                schema: "ZANECO",
                table: "Teams",
                newName: "MemberName");

            migrationBuilder.RenameColumn(
                name: "LeaderId",
                schema: "ZANECO",
                table: "Teams",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                schema: "ZANECO",
                table: "Teams",
                newName: "ManagerName");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                schema: "ZANECO",
                table: "Teams",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                schema: "ZANECO",
                table: "Teams",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employees_EmployeeId",
                schema: "ZANECO",
                table: "Teams",
                column: "EmployeeId",
                principalSchema: "ZANECO",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
