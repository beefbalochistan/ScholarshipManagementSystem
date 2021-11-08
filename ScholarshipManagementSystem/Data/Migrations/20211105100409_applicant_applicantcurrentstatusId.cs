using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class applicant_applicantcurrentstatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantCurrentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantCurrentStatusId",
                principalSchema: "Student",
                principalTable: "ApplicantCurrentStatus",
                principalColumn: "ApplicantCurrentStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
