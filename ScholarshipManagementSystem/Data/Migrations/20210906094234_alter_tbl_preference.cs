using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_preference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ScholarshipSlot",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIs",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "POMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotMetric",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotFAFSc2Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotFAFSc1Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotDAE3Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotDAE2Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotDAE1Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SlotBacholar1Y",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSIntermediateQouta",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSIntermediateEVIQouta",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "SQSEVIBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Qouta",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "POMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "POMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "MasterSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "MSSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "IOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "IOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "IOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "IOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "IOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "DOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "BacholarSlot",
                schema: "master",
                table: "Preference",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ScholarshipSlot",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIs",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "POMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMS",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotMetric",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotFAFSc2Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotFAFSc1Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotDAE3Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotDAE2Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotDAE1Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SlotBacholar1Y",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSIntermediateQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSIntermediateEVIQouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "SQSEVIBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Qouta",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "POMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "POMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "MasterSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "MSSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "IOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "IOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "IOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "IOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "IOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSMatricQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSMasterQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSMSQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSIntermediateQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSGraduationQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSDAEQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "DOMSBachelorQoutaPER",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "BacholarSlot",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
