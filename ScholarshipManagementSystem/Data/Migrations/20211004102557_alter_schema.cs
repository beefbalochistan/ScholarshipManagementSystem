using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ImportResult");

            migrationBuilder.RenameTable(
                name: "ResultRepository",
                schema: "master",
                newName: "ResultRepository",
                newSchema: "ImportResult");

            migrationBuilder.RenameTable(
                name: "ResultContainer",
                schema: "master",
                newName: "ResultContainer",
                newSchema: "ImportResult");

            migrationBuilder.RenameTable(
                name: "ExcelColumnName",
                schema: "master",
                newName: "ExcelColumnName",
                newSchema: "ImportResult");

            migrationBuilder.RenameTable(
                name: "ColumnLabel",
                schema: "master",
                newName: "ColumnLabel",
                newSchema: "ImportResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ResultRepository",
                schema: "ImportResult",
                newName: "ResultRepository",
                newSchema: "master");

            migrationBuilder.RenameTable(
                name: "ResultContainer",
                schema: "ImportResult",
                newName: "ResultContainer",
                newSchema: "master");

            migrationBuilder.RenameTable(
                name: "ExcelColumnName",
                schema: "ImportResult",
                newName: "ExcelColumnName",
                newSchema: "master");

            migrationBuilder.RenameTable(
                name: "ColumnLabel",
                schema: "ImportResult",
                newName: "ColumnLabel",
                newSchema: "master");
        }
    }
}
