using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_applicant_SMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedMethod",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AddColumn<int>(
                name: "SelectionMethodId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectionMethodId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AddColumn<string>(
                name: "SelectedMethod",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
