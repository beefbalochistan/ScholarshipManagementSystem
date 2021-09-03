using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_preferences_qouta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bachelorThreshold",
                schema: "master",
                table: "Preference",
                newName: "BSProfThresholdForClass");

            migrationBuilder.AddColumn<int>(
                name: "BSProfDistrictThresholdFor1stY",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "DistrictAdditionalSlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StipendAmount",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DistrictQoutaBySchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "SchemeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "SchemeLevelId",
                principalSchema: "master",
                principalTable: "SchemeLevel",
                principalColumn: "SchemeLevelId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropIndex(
                name: "IX_DistrictQoutaBySchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "BSProfDistrictThresholdFor1stY",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "DistrictAdditionalSlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "StipendAmount",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "BSProfThresholdForClass",
                schema: "master",
                table: "Preference",
                newName: "bachelorThreshold");
        }
    }
}
