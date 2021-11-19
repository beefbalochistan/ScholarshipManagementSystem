using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class rename_column_calculat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SlotCalculationMethod",
                schema: "master",
                table: "Preference",
                newName: "SlotGraduationPROFCalculationMethod");

            migrationBuilder.RenameColumn(
                name: "MasterThreshold",
                schema: "master",
                table: "Preference",
                newName: "Master1stYThreshold");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SlotGraduationPROFCalculationMethod",
                schema: "master",
                table: "Preference",
                newName: "SlotCalculationMethod");

            migrationBuilder.RenameColumn(
                name: "Master1stYThreshold",
                schema: "master",
                table: "Preference",
                newName: "MasterThreshold");
        }
    }
}
