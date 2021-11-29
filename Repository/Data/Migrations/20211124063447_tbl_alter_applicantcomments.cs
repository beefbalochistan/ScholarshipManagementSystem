using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "userAccessToForwardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantStudent_UserAccessToForward_userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "userAccessToForwardId",
                principalSchema: "master",
                principalTable: "UserAccessToForward",
                principalColumn: "UserAccessToForwardId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_UserAccessToForward_userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantStudent_userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropColumn(
                name: "userAccessToForwardId",
                schema: "Student",
                table: "ApplicantStudent");
        }
    }
}
