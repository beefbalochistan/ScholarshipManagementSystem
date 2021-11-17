using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_Section_Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                newName: "ProcessState");

            migrationBuilder.AddColumn<int>(
                name: "ProcessValue",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessValue",
                schema: "Student",
                table: "ApplicantCurrentStatus");

            migrationBuilder.RenameColumn(
                name: "ProcessState",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                newName: "Value");
        }
    }
}
