using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_DistrictQouta_SchemelevelpolicyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "SchemeLevelPolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_DistrictQoutaBySchemeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "IX_DistrictQoutaBySchemeLevel_SchemeLevelPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "SchemeLevelPolicyId",
                principalSchema: "scholar",
                principalTable: "SchemeLevelPolicy",
                principalColumn: "SchemeLevelPolicyId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictQoutaBySchemeLevel_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelPolicyId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "SchemeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_DistrictQoutaBySchemeLevel_SchemeLevelPolicyId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                newName: "IX_DistrictQoutaBySchemeLevel_SchemeLevelId");

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
    }
}
