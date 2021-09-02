using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_prefrences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POMSDOMSInstitudeQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSIBoardQouta");

            migrationBuilder.RenameColumn(
                name: "POMSDOMSBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "IOMSInstitudeQouta");

            migrationBuilder.AddColumn<int>(
                name: "DOMSBoardQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSInstitudeQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOMSBoardQouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSInstitudeQouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "POMSIBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSDOMSInstitudeQouta");

            migrationBuilder.RenameColumn(
                name: "IOMSInstitudeQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSDOMSBoardQouta");
        }
    }
}
