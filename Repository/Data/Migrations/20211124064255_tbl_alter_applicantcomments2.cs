using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcomments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeverityLevel",
                schema: "Student",
                table: "ApplicantStudent",
                newName: "SeverityLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeverityLevelId",
                schema: "Student",
                table: "ApplicantStudent",
                newName: "SeverityLevel");
        }
    }
}
