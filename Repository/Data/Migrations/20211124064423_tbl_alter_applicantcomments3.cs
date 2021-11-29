using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcomments3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_SeverityLevelId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "SeverityLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantStudent_SeverityLevel_SeverityLevelId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "SeverityLevelId",
                principalSchema: "Master",
                principalTable: "SeverityLevel",
                principalColumn: "SeverityLevelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_SeverityLevel_SeverityLevelId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantStudent_SeverityLevelId",
                schema: "Student",
                table: "ApplicantStudent");
        }
    }
}
