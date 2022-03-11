using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_clumn_applicant_finance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantFinanceCurrentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantFinanceCurrentStatus_ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantFinanceCurrentStatusId",
                principalSchema: "Student",
                principalTable: "ApplicantFinanceCurrentStatus",
                principalColumn: "ApplicantFinanceCurrentStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantFinanceCurrentStatus_ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "ApplicantFinanceCurrentStatusId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
