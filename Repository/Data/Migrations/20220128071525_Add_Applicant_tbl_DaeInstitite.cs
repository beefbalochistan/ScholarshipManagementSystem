using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class Add_Applicant_tbl_DaeInstitite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DAEInstituteId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_DAEInstituteId",
                schema: "Student",
                table: "Applicant",
                column: "DAEInstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_DAEInstitute_DAEInstituteId",
                schema: "Student",
                table: "Applicant",
                column: "DAEInstituteId",
                principalSchema: "master",
                principalTable: "DAEInstitute",
                principalColumn: "DAEInstituteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_DAEInstitute_DAEInstituteId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_DAEInstituteId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "DAEInstituteId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
