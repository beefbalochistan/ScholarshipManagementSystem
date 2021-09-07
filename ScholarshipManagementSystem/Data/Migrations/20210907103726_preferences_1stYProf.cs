using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class preferences_1stYProf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SQSOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSOMSBachelorClassQoutaPER");

            migrationBuilder.RenameColumn(
                name: "SQSEVIBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSOMSBachelor1stYQoutaPER");

            migrationBuilder.RenameColumn(
                name: "IOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSEVIBachelorClassQoutaPER");

            migrationBuilder.RenameColumn(
                name: "DOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSEVIBachelor1stYQoutaPER");

            migrationBuilder.AddColumn<float>(
                name: "DOMSBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DOMSBachelorClassQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IOMSBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IOMSBachelorClassQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOMSBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSBachelorClassQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "IOMSBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "IOMSBachelorClassQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "SQSOMSBachelorClassQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSOMSBachelorQoutaPER");

            migrationBuilder.RenameColumn(
                name: "SQSOMSBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "SQSEVIBachelorQoutaPER");

            migrationBuilder.RenameColumn(
                name: "SQSEVIBachelorClassQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "IOMSBachelorQoutaPER");

            migrationBuilder.RenameColumn(
                name: "SQSEVIBachelor1stYQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "DOMSBachelorQoutaPER");
        }
    }
}
