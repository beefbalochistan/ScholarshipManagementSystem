using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_selectionCriteriaGeneral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectionCriteriaGeneral_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectionCriteriaGeneral_ExcelColumnName_ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectionCriteriaGeneral_Operator_OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropIndex(
                name: "IX_SelectionCriteriaGeneral_DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropIndex(
                name: "IX_SelectionCriteriaGeneral_ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropIndex(
                name: "IX_SelectionCriteriaGeneral_OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropColumn(
                name: "DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropColumn(
                name: "ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteriaGeneral_DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "DegreeScholarshipLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteriaGeneral_ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "ExcelColumnNameId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteriaGeneral_OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionCriteriaGeneral_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "DegreeScholarshipLevelId",
                principalSchema: "master",
                principalTable: "DegreeScholarshipLevel",
                principalColumn: "DegreeScholarshipLevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionCriteriaGeneral_ExcelColumnName_ExcelColumnNameId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "ExcelColumnNameId",
                principalSchema: "ImportResult",
                principalTable: "ExcelColumnName",
                principalColumn: "ExcelColumnNameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionCriteriaGeneral_Operator_OperatorId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "OperatorId",
                principalSchema: "master",
                principalTable: "Operator",
                principalColumn: "OperatorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
