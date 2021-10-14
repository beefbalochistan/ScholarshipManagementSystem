using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_tbl_applicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_SchemeLevel_SchemeLevelId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_SchemeLevelId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelId",
                schema: "Student",
                table: "Applicant",
                newName: "SchemeLevelPolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchemeLevelPolicyId",
                schema: "Student",
                table: "Applicant",
                newName: "SchemeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_SchemeLevelId",
                schema: "Student",
                table: "Applicant",
                column: "SchemeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_SchemeLevel_SchemeLevelId",
                schema: "Student",
                table: "Applicant",
                column: "SchemeLevelId",
                principalSchema: "master",
                principalTable: "SchemeLevel",
                principalColumn: "SchemeLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
