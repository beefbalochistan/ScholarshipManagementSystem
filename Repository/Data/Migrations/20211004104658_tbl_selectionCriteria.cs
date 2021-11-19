using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_selectionCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ScholarshipSetup");

            migrationBuilder.CreateTable(
                name: "SelectionCriteria",
                schema: "ScholarshipSetup",
                columns: table => new
                {
                    SelectionCriteriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultRepositoryId = table.Column<int>(type: "int", nullable: false),
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionCriteria", x => x.SelectionCriteriaId);
                    table.ForeignKey(
                        name: "FK_SelectionCriteria_ExcelColumnName_ExcelColumnNameId",
                        column: x => x.ExcelColumnNameId,
                        principalSchema: "ImportResult",
                        principalTable: "ExcelColumnName",
                        principalColumn: "ExcelColumnNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectionCriteria_ResultRepository_ResultRepositoryId",
                        column: x => x.ResultRepositoryId,
                        principalSchema: "ImportResult",
                        principalTable: "ResultRepository",
                        principalColumn: "ResultRepositoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteria_ExcelColumnNameId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria",
                column: "ExcelColumnNameId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteria_ResultRepositoryId",
                schema: "ScholarshipSetup",
                table: "SelectionCriteria",
                column: "ResultRepositoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectionCriteria",
                schema: "ScholarshipSetup");
        }
    }
}
