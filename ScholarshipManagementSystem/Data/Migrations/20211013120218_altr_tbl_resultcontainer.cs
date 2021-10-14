using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_tbl_resultcontainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultRepository_SchemeLevel_SchemeLevelId",
                schema: "ImportResult",
                table: "ResultRepository");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelId",
                schema: "ImportResult",
                table: "ResultRepository",
                newName: "SchemeLevelPolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultRepository_SchemeLevelId",
                schema: "ImportResult",
                table: "ResultRepository",
                newName: "IX_ResultRepository_SchemeLevelPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultRepository_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "ImportResult",
                table: "ResultRepository",
                column: "SchemeLevelPolicyId",
                principalSchema: "scholar",
                principalTable: "SchemeLevelPolicy",
                principalColumn: "SchemeLevelPolicyId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultRepository_SchemeLevelPolicy_SchemeLevelPolicyId",
                schema: "ImportResult",
                table: "ResultRepository");

            migrationBuilder.RenameColumn(
                name: "SchemeLevelPolicyId",
                schema: "ImportResult",
                table: "ResultRepository",
                newName: "SchemeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultRepository_SchemeLevelPolicyId",
                schema: "ImportResult",
                table: "ResultRepository",
                newName: "IX_ResultRepository_SchemeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultRepository_SchemeLevel_SchemeLevelId",
                schema: "ImportResult",
                table: "ResultRepository",
                column: "SchemeLevelId",
                principalSchema: "master",
                principalTable: "SchemeLevel",
                principalColumn: "SchemeLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
