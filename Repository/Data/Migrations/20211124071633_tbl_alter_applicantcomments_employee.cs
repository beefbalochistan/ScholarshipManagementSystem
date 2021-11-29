using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_applicantcomments_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantStudent_Employee_EmployeeId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicantStudent_EmployeeId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "Student",
                table: "ApplicantStudent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                schema: "Student",
                table: "ApplicantStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_EmployeeId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantStudent_Employee_EmployeeId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "EmployeeId",
                principalSchema: "master",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
