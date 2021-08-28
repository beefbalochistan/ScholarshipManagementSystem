using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_schemeLevel_SRC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_SchemeLevelPolicy_SchemeLevel_PolicySRCForumId",
                schema: "scholar",
                table: "SchemeLevelPolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchemeLevel",
                schema: "scholar",
                table: "SchemeLevel");

            migrationBuilder.RenameTable(
                name: "SchemeLevel",
                schema: "scholar",
                newName: "PolicySRCForum",
                newSchema: "scholar");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "master",
                table: "SchemeLevel",
                newName: "Description1");

            migrationBuilder.RenameIndex(
                name: "IX_SchemeLevel_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "PolicySRCForum",
                newName: "IX_PolicySRCForum_ScholarshipFiscalYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicySRCForum",
                schema: "scholar",
                table: "PolicySRCForum",
                column: "PolicySRCForumId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicySRCForum_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "PolicySRCForum",
                column: "ScholarshipFiscalYearId",
                principalSchema: "scholar",
                principalTable: "ScholarshipFiscalYear",
                principalColumn: "ScholarshipFiscalYearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeLevelPolicy_PolicySRCForum_PolicySRCForumId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "PolicySRCForumId",
                principalSchema: "scholar",
                principalTable: "PolicySRCForum",
                principalColumn: "PolicySRCForumId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicySRCForum_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "PolicySRCForum");

            migrationBuilder.DropForeignKey(
                name: "FK_SchemeLevelPolicy_PolicySRCForum_PolicySRCForumId",
                schema: "scholar",
                table: "SchemeLevelPolicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicySRCForum",
                schema: "scholar",
                table: "PolicySRCForum");

            migrationBuilder.RenameTable(
                name: "PolicySRCForum",
                schema: "scholar",
                newName: "SchemeLevel",
                newSchema: "scholar");

            migrationBuilder.RenameColumn(
                name: "Description1",
                schema: "master",
                table: "SchemeLevel",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_PolicySRCForum_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevel",
                newName: "IX_SchemeLevel_ScholarshipFiscalYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchemeLevel",
                schema: "scholar",
                table: "SchemeLevel",
                column: "PolicySRCForumId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevel",
                column: "ScholarshipFiscalYearId",
                principalSchema: "scholar",
                principalTable: "ScholarshipFiscalYear",
                principalColumn: "ScholarshipFiscalYearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeLevelPolicy_SchemeLevel_PolicySRCForumId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "PolicySRCForumId",
                principalSchema: "scholar",
                principalTable: "SchemeLevel",
                principalColumn: "PolicySRCForumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
