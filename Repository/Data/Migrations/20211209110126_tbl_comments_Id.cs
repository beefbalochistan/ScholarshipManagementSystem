using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_comments_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_UserAccessToForward_UserAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantStudent_UserAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");                    

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_ApplicantCurrentStatusId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "ApplicantCurrentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantStudent_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "ApplicantCurrentStatusId",
                principalSchema: "Student",
                principalTable: "ApplicantCurrentStatus",
                principalColumn: "ApplicantCurrentStatusId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "Student",
                table: "ApplicantStudent");
           
            migrationBuilder.DropIndex(
                name: "IX_ApplicantStudent_ApplicantCurrentStatusId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_UserAccessToForwardId",
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
    }
}
