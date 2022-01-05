using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_ApplicantSelectionStatusFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ApplicantSelectionStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantSelectionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantSelectionStatus_ApplicantSelectionStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantSelectionStatusId",
                principalSchema: "Student",
                principalTable: "ApplicantSelectionStatus",
                principalColumn: "ApplicantSelectionStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantSelectionStatus_ApplicantSelectionStatusId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_ApplicantSelectionStatusId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
