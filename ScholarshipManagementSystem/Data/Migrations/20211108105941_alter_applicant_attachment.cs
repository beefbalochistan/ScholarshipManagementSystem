using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_applicant_attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantAttachment_ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantAttachment_Applicant_ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment",
                column: "ApplicantId",
                principalSchema: "Student",
                principalTable: "Applicant",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantAttachment_Applicant_ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantAttachment_ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                schema: "Student",
                table: "ApplicantAttachment");
        }
    }
}
