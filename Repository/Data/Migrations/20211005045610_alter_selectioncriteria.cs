using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_selectioncriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteria_OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionCriteria_Operator_OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria",
                column: "OperatorId",
                principalSchema: "master",
                principalTable: "Operator",
                principalColumn: "OperatorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectionCriteria_Operator_OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria");

            migrationBuilder.DropIndex(
                name: "IX_SelectionCriteria_OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria");
        }
    }
}
