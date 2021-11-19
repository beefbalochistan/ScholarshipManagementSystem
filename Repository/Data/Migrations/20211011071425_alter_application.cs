using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_application : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant",
                column: "DegreeScholarshipLevelId",
                principalSchema: "master",
                principalTable: "DegreeScholarshipLevel",
                principalColumn: "DegreeScholarshipLevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant",
                column: "DegreeScholarshipLevelId",
                principalSchema: "master",
                principalTable: "DegreeScholarshipLevel",
                principalColumn: "DegreeScholarshipLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
