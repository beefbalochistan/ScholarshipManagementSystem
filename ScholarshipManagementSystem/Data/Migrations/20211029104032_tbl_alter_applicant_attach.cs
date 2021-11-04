using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_alter_applicant_attach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AttachPicture",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Affidavit",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_CNIC_BForm",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_DMC_Transcript",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Father_Death_Certificate",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Father_Mother_Guardian_CNIC",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Minority_Certificate",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Payslip",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Attach_Picture",
                schema: "Student",
                table: "Applicant",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachPicture",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Affidavit",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_CNIC_BForm",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_DMC_Transcript",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Father_Death_Certificate",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Father_Mother_Guardian_CNIC",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Minority_Certificate",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Payslip",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Attach_Picture",
                schema: "Student",
                table: "Applicant");
        }
    }
}
