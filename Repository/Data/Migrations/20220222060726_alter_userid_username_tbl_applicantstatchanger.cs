using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_userid_username_tbl_applicantstatchanger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Student",
                table: "ApplicantStateChanger",
                newName: "UserName");
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "Student",
                table: "ApplicantStateChanger",
                newName: "UserId");
        }
    }
}
