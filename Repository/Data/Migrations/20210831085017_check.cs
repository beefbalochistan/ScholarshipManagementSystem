using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QoutaMetric",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotMetric");

            migrationBuilder.RenameColumn(
                name: "QoutaFAFSc2Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotFAFSc2Y");

            migrationBuilder.RenameColumn(
                name: "QoutaFAFSc1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotFAFSc1Y");

            migrationBuilder.RenameColumn(
                name: "QoutaDAE3Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotDAE3Y");

            migrationBuilder.RenameColumn(
                name: "QoutaDAE2Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotDAE2Y");

            migrationBuilder.RenameColumn(
                name: "QoutaDAE1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotDAE1Y");

            migrationBuilder.RenameColumn(
                name: "QoutaBacholar1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "SlotBacholar1Y");

            migrationBuilder.RenameColumn(
                name: "MasterQouta",
                schema: "master",
                table: "PreferencesSlot",
                newName: "MasterSlot");

            migrationBuilder.RenameColumn(
                name: "BacholarQouta",
                schema: "master",
                table: "PreferencesSlot",
                newName: "BacholarSlot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SlotMetric",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaMetric");

            migrationBuilder.RenameColumn(
                name: "SlotFAFSc2Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaFAFSc2Y");

            migrationBuilder.RenameColumn(
                name: "SlotFAFSc1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaFAFSc1Y");

            migrationBuilder.RenameColumn(
                name: "SlotDAE3Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaDAE3Y");

            migrationBuilder.RenameColumn(
                name: "SlotDAE2Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaDAE2Y");

            migrationBuilder.RenameColumn(
                name: "SlotDAE1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaDAE1Y");

            migrationBuilder.RenameColumn(
                name: "SlotBacholar1Y",
                schema: "master",
                table: "PreferencesSlot",
                newName: "QoutaBacholar1Y");

            migrationBuilder.RenameColumn(
                name: "MasterSlot",
                schema: "master",
                table: "PreferencesSlot",
                newName: "MasterQouta");

            migrationBuilder.RenameColumn(
                name: "BacholarSlot",
                schema: "master",
                table: "PreferencesSlot",
                newName: "BacholarQouta");
        }
    }
}
