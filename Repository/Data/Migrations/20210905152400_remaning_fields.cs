using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class remaning_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "IOMSMasterQoutaPER");

            migrationBuilder.RenameColumn(
                name: "POMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "IOMSMSQoutaPER");

            migrationBuilder.RenameColumn(
                name: "POMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "IOMSGraduationQoutaPER");

            migrationBuilder.RenameColumn(
                name: "POMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "IOMSBachelorQoutaPER");

            migrationBuilder.AddColumn<int>(
                name: "DistrictSlotMPIPer",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictSlotPopulationPer",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Threshold",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "MPIDifferenceFromStatndard",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MPIDifferenceFromStatndard",
                schema: "master",
                table: "DistrictDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "master",
                table: "District",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<float>(
                name: "PercentageSlots",
                schema: "master",
                table: "DAEInstitute",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictSlotMPIPer",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DistrictSlotPopulationPer",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MPIDifferenceFromStatndard",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "MPIDifferenceFromStatndard",
                schema: "master",
                table: "DistrictDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "master",
                table: "District");

            migrationBuilder.RenameColumn(
                name: "IOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "POMSMasterQoutaPER");

            migrationBuilder.RenameColumn(
                name: "IOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "POMSMSQoutaPER");

            migrationBuilder.RenameColumn(
                name: "IOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "POMSGraduationQoutaPER");

            migrationBuilder.RenameColumn(
                name: "IOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                newName: "POMSBachelorQoutaPER");

            migrationBuilder.AlterColumn<int>(
                name: "Threshold",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "PercentageSlots",
                schema: "master",
                table: "DAEInstitute",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
