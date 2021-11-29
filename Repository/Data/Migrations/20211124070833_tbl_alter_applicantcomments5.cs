using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcomments5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "UserAccessToForwardId");            

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantStudent_UserAccessToForward_UserAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "UserAccessToForwardId",
                principalSchema: "master",
                principalTable: "UserAccessToForward",
                principalColumn: "UserAccessToForwardId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_UserAccessToForward_UserAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");           
        }
    }
}
