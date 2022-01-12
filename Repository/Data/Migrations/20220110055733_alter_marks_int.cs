using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_marks_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalMarks_CGPA",
                schema: "master",
                table: "SchemeLevel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);                                   

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCGPA",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalMarks_",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "C16",
                schema: "ImportResult",
                table: "ColumnLabelTemp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C16",
                schema: "ImportResult",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMarks_CGPA",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropColumn(
                name: "TotalCGPA",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropColumn(
                name: "TotalMarks_",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropColumn(
                name: "C16",
                schema: "ImportResult",
                table: "ColumnLabelTemp");

            migrationBuilder.DropColumn(
                name: "C16",
                schema: "ImportResult",
                table: "ColumnLabel");

            migrationBuilder.AlterColumn<string>(
                name: "Marks_",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CGPA",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Marks_",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CGPA",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
