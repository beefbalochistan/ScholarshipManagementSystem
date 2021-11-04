using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_alter_applicant_IsSubmitted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachPicture",
                schema: "Student",
                table: "Applicant",
                newName: "IsFormSubmitted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFormSubmitted",
                schema: "Student",
                table: "Applicant",
                newName: "AttachPicture");
        }
    }
}
