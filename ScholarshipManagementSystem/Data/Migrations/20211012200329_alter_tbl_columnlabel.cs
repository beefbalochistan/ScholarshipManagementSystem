using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_columnlabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultContainer_ColumnLabel_ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropIndex(
                name: "IX_ResultContainer_ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropColumn(
                name: "ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainer_ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer",
                column: "ColumnLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultContainer_ColumnLabel_ColumnLabelId",
                schema: "ImportResult",
                table: "ResultContainer",
                column: "ColumnLabelId",
                principalSchema: "ImportResult",
                principalTable: "ColumnLabel",
                principalColumn: "ColumnLabelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
