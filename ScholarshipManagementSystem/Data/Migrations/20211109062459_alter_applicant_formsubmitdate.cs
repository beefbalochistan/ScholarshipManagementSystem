using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_applicant_formsubmitdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FormSubmittedOnDate",
                schema: "Student",
                table: "Applicant",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormSubmittedOnDate",
                schema: "Student",
                table: "Applicant");
        }
    }
}
