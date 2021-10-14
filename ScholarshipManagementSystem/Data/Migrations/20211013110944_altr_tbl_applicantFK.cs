using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_tbl_applicantFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applicant_SchemeLevelPolicyId",
                schema: "Student",
                table: "Applicant",
                column: "SchemeLevelPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "Student",
                table: "Applicant",
                column: "SchemeLevelPolicyId",
                principalSchema: "scholar",
                principalTable: "SchemeLevelPolicy",
                principalColumn: "SchemeLevelPolicyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_SchemeLevelPolicyId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
