using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_tbl_columnlabel_repId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ColumnLabel_ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel",
                column: "ResultRepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnLabel_ResultRepository_ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel",
                column: "ResultRepositoryId",
                principalSchema: "ImportResult",
                principalTable: "ResultRepository",
                principalColumn: "ResultRepositoryId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnLabel_ResultRepository_ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel");

            migrationBuilder.DropIndex(
                name: "IX_ColumnLabel_ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "ResultRepositoryId",
                schema: "ImportResult",
                table: "ColumnLabel");
        }
    }
}
