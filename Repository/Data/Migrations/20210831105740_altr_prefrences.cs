using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class altr_prefrences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POMSDOMSInstituteQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSIBoardQouta");

            migrationBuilder.RenameColumn(
                name: "POMSDOMSBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "IOMSInstituteQouta");

            migrationBuilder.AddColumn<int>(
                name: "DOMSBoardQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSInstituteQouta",
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
                name: "DOMSInstituteQouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "POMSIBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSDOMSInstituteQouta");

            migrationBuilder.RenameColumn(
                name: "IOMSInstituteQouta",
                schema: "master",
                table: "Preference",
                newName: "POMSDOMSBoardQouta");
        }
    }
}
