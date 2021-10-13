using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_resultContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "C1",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropColumn(
                name: "C10",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.RenameColumn(
                name: "C9",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Roll_NO");

            migrationBuilder.RenameColumn(
                name: "C8",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "C7",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "REG_NO");

            migrationBuilder.RenameColumn(
                name: "C6",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Pass_Fail");

            migrationBuilder.RenameColumn(
                name: "C5",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "C4",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Marks_");

            migrationBuilder.RenameColumn(
                name: "C3",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Institute_District");

            migrationBuilder.RenameColumn(
                name: "C2",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Institute");

            migrationBuilder.RenameColumn(
                name: "C15",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "C14",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Father_Name");

            migrationBuilder.RenameColumn(
                name: "C13",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "Candidate_District");

            migrationBuilder.RenameColumn(
                name: "C12",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "CNIC");

            migrationBuilder.RenameColumn(
                name: "C11",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "CGPA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Roll_NO",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C9");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C8");

            migrationBuilder.RenameColumn(
                name: "REG_NO",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C7");

            migrationBuilder.RenameColumn(
                name: "Pass_Fail",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C6");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C5");

            migrationBuilder.RenameColumn(
                name: "Marks_",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C4");

            migrationBuilder.RenameColumn(
                name: "Institute_District",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C3");

            migrationBuilder.RenameColumn(
                name: "Institute",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C2");

            migrationBuilder.RenameColumn(
                name: "Group",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C15");

            migrationBuilder.RenameColumn(
                name: "Father_Name",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C14");

            migrationBuilder.RenameColumn(
                name: "Candidate_District",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C13");

            migrationBuilder.RenameColumn(
                name: "CNIC",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C12");

            migrationBuilder.RenameColumn(
                name: "CGPA",
                schema: "ImportResult",
                table: "ResultContainer",
                newName: "C11");

            migrationBuilder.AddColumn<string>(
                name: "C1",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C10",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
