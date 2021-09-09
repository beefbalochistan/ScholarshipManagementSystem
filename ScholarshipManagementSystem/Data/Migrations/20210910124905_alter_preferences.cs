using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_preferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MasterSlot",
                schema: "master",
                table: "Preference",
                newName: "MasterThreshold");

            migrationBuilder.RenameColumn(
                name: "Master1stYThreshold",
                schema: "master",
                table: "Preference",
                newName: "Master2ndYSlot");

            migrationBuilder.AddColumn<float>(
                name: "Master1stYSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "SlotMSCalculationMethod",
                schema: "master",
                table: "Preference",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SlotMasterCalculationMethod",
                schema: "master",
                table: "Preference",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master1stYSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SlotMSCalculationMethod",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SlotMasterCalculationMethod",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "MasterThreshold",
                schema: "master",
                table: "Preference",
                newName: "MasterSlot");

            migrationBuilder.RenameColumn(
                name: "Master2ndYSlot",
                schema: "master",
                table: "Preference",
                newName: "Master1stYThreshold");
        }
    }
}
