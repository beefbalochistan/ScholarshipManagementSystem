using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcurrentstatus_add_section : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantCurrentStatus_BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                column: "BEEFSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantCurrentStatus_BEEFSection_BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                column: "BEEFSectionId",
                principalSchema: "master",
                principalTable: "BEEFSection",
                principalColumn: "BEEFSectionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantCurrentStatus_BEEFSection_BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantCurrentStatus_BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus");

            migrationBuilder.DropColumn(
                name: "BEEFSectionId",
                schema: "Student",
                table: "ApplicantCurrentStatus");
        }
    }
}
