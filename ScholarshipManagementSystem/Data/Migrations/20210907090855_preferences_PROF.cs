using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class preferences_PROF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BacholarSlot",
                schema: "master",
                table: "Preference",
                newName: "GraduationPROF5thYSlot");

            migrationBuilder.AddColumn<float>(
                name: "GraduationPROF1stYSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GraduationPROF2ndYSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GraduationPROF3rdYSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GraduationPROF4thYSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduationPROF1stYSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "GraduationPROF2ndYSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "GraduationPROF3rdYSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "GraduationPROF4thYSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "GraduationPROF5thYSlot",
                schema: "master",
                table: "Preference",
                newName: "BacholarSlot");
        }
    }
}
