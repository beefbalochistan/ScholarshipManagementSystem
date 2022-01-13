using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_SL_GPA_decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradingSystem",
                schema: "master",
                table: "SchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGPA",
                schema: "Student",
                table: "Applicant",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReceivedCGPA",
                schema: "Student",
                table: "Applicant",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropColumn(
                name: "GradingSystem",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.AlterColumn<float>(
                name: "TotalGPA",
                schema: "Student",
                table: "Applicant",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<float>(
                name: "ReceivedCGPA",
                schema: "Student",
                table: "Applicant",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
