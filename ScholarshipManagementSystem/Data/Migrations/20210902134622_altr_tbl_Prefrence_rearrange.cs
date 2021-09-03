using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_tbl_Prefrence_rearrange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferencesSlot",
                schema: "master");

            migrationBuilder.RenameColumn(
                name: "SchemeSimpleGraduationStipend",
                schema: "master",
                table: "Preference",
                newName: "bachelorThreshold");

            migrationBuilder.RenameColumn(
                name: "SchemeMatrictStipend",
                schema: "master",
                table: "Preference",
                newName: "SlotMetric");

            migrationBuilder.RenameColumn(
                name: "SQSOMSQouta",
                schema: "master",
                table: "Preference",
                newName: "SlotFAFSc2Y");

            migrationBuilder.RenameColumn(
                name: "SQSEVIQouta",
                schema: "master",
                table: "Preference",
                newName: "SlotFAFSc1Y");

            migrationBuilder.RenameColumn(
                name: "POMSIBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "SlotDAE3Y");

            migrationBuilder.RenameColumn(
                name: "InstitudeThreshold",
                schema: "master",
                table: "Preference",
                newName: "SlotDAE2Y");

            migrationBuilder.RenameColumn(
                name: "IOMSInstitudeQouta",
                schema: "master",
                table: "Preference",
                newName: "SlotDAE1Y");

            migrationBuilder.RenameColumn(
                name: "DistrictThreshold",
                schema: "master",
                table: "Preference",
                newName: "SlotBacholar1Y");

            migrationBuilder.RenameColumn(
                name: "DOMSInstitudeQouta",
                schema: "master",
                table: "Preference",
                newName: "SchemeMatricStipend");

            migrationBuilder.RenameColumn(
                name: "DOMSBoardQouta",
                schema: "master",
                table: "Preference",
                newName: "SchemeGraduationStipend");

            migrationBuilder.AddColumn<int>(
                name: "BacholarSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GraduationThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntermediateThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MSSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MSThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasterSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasterThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatricThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhDThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSIntermediateEVIQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSIntermediateQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BacholarSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSDAEQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSMSQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSMasterQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DOMSMatricQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "GraduationThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "IOMSDAEQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "IntermediateThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MSSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MSThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MasterSlot",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MasterThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MatricThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSBachelorQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSGraduationQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSMSQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSMasterQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "POMSMatricQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "PhDThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "Qouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIBachelorQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIDAEQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIGraduationQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIMSQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIMasterQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSEVIMatricQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSIntermediateEVIQouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSDAEQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSIntermediateQouta",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSMSQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSMasterQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "SQSOMSMatricQoutaPER",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "bachelorThreshold",
                schema: "master",
                table: "Preference",
                newName: "SchemeSimpleGraduationStipend");

            migrationBuilder.RenameColumn(
                name: "SlotMetric",
                schema: "master",
                table: "Preference",
                newName: "SchemeMatrictStipend");

            migrationBuilder.RenameColumn(
                name: "SlotFAFSc2Y",
                schema: "master",
                table: "Preference",
                newName: "SQSOMSQouta");

            migrationBuilder.RenameColumn(
                name: "SlotFAFSc1Y",
                schema: "master",
                table: "Preference",
                newName: "SQSEVIQouta");

            migrationBuilder.RenameColumn(
                name: "SlotDAE3Y",
                schema: "master",
                table: "Preference",
                newName: "POMSIBoardQouta");

            migrationBuilder.RenameColumn(
                name: "SlotDAE2Y",
                schema: "master",
                table: "Preference",
                newName: "InstitudeThreshold");

            migrationBuilder.RenameColumn(
                name: "SlotDAE1Y",
                schema: "master",
                table: "Preference",
                newName: "IOMSInstitudeQouta");

            migrationBuilder.RenameColumn(
                name: "SlotBacholar1Y",
                schema: "master",
                table: "Preference",
                newName: "DistrictThreshold");

            migrationBuilder.RenameColumn(
                name: "SchemeMatricStipend",
                schema: "master",
                table: "Preference",
                newName: "DOMSInstitudeQouta");

            migrationBuilder.RenameColumn(
                name: "SchemeGraduationStipend",
                schema: "master",
                table: "Preference",
                newName: "DOMSBoardQouta");

            migrationBuilder.CreateTable(
                name: "PreferencesSlot",
                schema: "master",
                columns: table => new
                {
                    PreferencesSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BacholarSlot = table.Column<int>(type: "int", nullable: false),
                    MasterSlot = table.Column<int>(type: "int", nullable: false),
                    SlotBacholar1Y = table.Column<int>(type: "int", nullable: false),
                    SlotDAE1Y = table.Column<int>(type: "int", nullable: false),
                    SlotDAE2Y = table.Column<int>(type: "int", nullable: false),
                    SlotDAE3Y = table.Column<int>(type: "int", nullable: false),
                    SlotFAFSc1Y = table.Column<int>(type: "int", nullable: false),
                    SlotFAFSc2Y = table.Column<int>(type: "int", nullable: false),
                    SlotMetric = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencesSlot", x => x.PreferencesSlotId);
                });
        }
    }
}
