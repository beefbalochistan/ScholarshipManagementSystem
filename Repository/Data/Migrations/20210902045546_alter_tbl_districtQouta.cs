using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_districtQouta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "ScholarshipFiscalYearId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "PolicySRCForumId");

            migrationBuilder.RenameIndex(
                name: "IX_DistrictQoutaBySchemeLevel_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "IX_DistrictQoutaBySchemeLevel_PolicySRCForumId");

            migrationBuilder.AddColumn<float>(
                name: "DistrictMPISlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DistrictPopulationSlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<int>(
                name: "Population",
                schema: "master",
                table: "DistrictDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_PolicySRCForum_PolicySRCForumId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "PolicySRCForumId",
                principalSchema: "scholar",
                principalTable: "PolicySRCForum",
                principalColumn: "PolicySRCForumId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_PolicySRCForum_PolicySRCForumId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "DistrictMPISlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "DistrictPopulationSlot",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "PolicySRCForumId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "ScholarshipFiscalYearId");

            migrationBuilder.RenameIndex(
                name: "IX_DistrictQoutaBySchemeLevel_PolicySRCForumId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "IX_DistrictQoutaBySchemeLevel_ScholarshipFiscalYearId");

            migrationBuilder.AlterColumn<long>(
                name: "Population",
                schema: "master",
                table: "DistrictDetail",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "ScholarshipFiscalYearId",
                principalSchema: "scholar",
                principalTable: "ScholarshipFiscalYear",
                principalColumn: "ScholarshipFiscalYearId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
