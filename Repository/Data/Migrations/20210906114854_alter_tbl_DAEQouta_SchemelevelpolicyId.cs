using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_DAEQouta_SchemelevelpolicyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DAEInstituteQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                table: "DAEInstituteQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelId",
                table: "DAEInstituteQoutaBySchemeLevel",
                newName: "SchemeLevelPolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_DAEInstituteQoutaBySchemeLevel_SchemeLevelId",
                table: "DAEInstituteQoutaBySchemeLevel",
                newName: "IX_DAEInstituteQoutaBySchemeLevel_SchemeLevelPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DAEInstituteQoutaBySchemeLevel_SchemeLevelPolicy_SchemeLevelPolicyId",
                table: "DAEInstituteQoutaBySchemeLevel",
                column: "SchemeLevelPolicyId",
                principalSchema: "scholar",
                principalTable: "SchemeLevelPolicy",
                principalColumn: "SchemeLevelPolicyId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DAEInstituteQoutaBySchemeLevel_SchemeLevelPolicy_SchemeLevelPolicyId",
                table: "DAEInstituteQoutaBySchemeLevel");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelPolicyId",
                table: "DAEInstituteQoutaBySchemeLevel",
                newName: "SchemeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_DAEInstituteQoutaBySchemeLevel_SchemeLevelPolicyId",
                table: "DAEInstituteQoutaBySchemeLevel",
                newName: "IX_DAEInstituteQoutaBySchemeLevel_SchemeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DAEInstituteQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                table: "DAEInstituteQoutaBySchemeLevel",
                column: "SchemeLevelId",
                principalSchema: "master",
                principalTable: "SchemeLevel",
                principalColumn: "SchemeLevelId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
