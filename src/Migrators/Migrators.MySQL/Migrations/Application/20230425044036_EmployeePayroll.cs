using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MySQL.Migrations.Application
{
    public partial class EmployeePayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrolls",
                schema: "ZANECO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PayrollId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PayrollName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    PayrollDate = table.Column<DateTime>(type: "date", nullable: false),
                    Salary = table.Column<decimal>(type: "Decimal(12,2)", nullable: false, defaultValue: 0m),
                    Additional = table.Column<decimal>(type: "Decimal(12,2)", nullable: false, defaultValue: 0m),
                    Gross = table.Column<decimal>(type: "Decimal(12,2)", nullable: false, defaultValue: 0m),
                    Deduction = table.Column<decimal>(type: "Decimal(12,2)", nullable: false, defaultValue: 0m),
                    Net = table.Column<decimal>(type: "Decimal(12,2)", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(16)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApprovedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePayrolls_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "ZANECO",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrolls_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalSchema: "ZANECO",
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrolls_EmployeeId",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrolls_PayrollId",
                schema: "ZANECO",
                table: "EmployeePayrolls",
                column: "PayrollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrolls",
                schema: "ZANECO");
        }
    }
}