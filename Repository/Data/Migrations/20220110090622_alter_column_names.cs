using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_column_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMarks_CGPA",
                schema: "master",
                table: "SchemeLevel",
                newName: "TotalMarks_GPA");

            migrationBuilder.RenameColumn(
                name: "TotalCGPA",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "TotalGPA");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGPA",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalMarks_",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGPA",
                schema: "ImportResult",
                table: "ResultContainerTemp");

            migrationBuilder.DropColumn(
                name: "TotalMarks_",
                schema: "ImportResult",
                table: "ResultContainerTemp");

            migrationBuilder.RenameColumn(
                name: "TotalMarks_GPA",
                schema: "master",
                table: "SchemeLevel",
                newName: "TotalMarks_CGPA");

            migrationBuilder.RenameColumn(
                name: "TotalGPA",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "TotalCGPA");
        }
    }
}
