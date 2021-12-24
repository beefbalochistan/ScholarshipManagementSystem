using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_docAssistOperator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAssistIndicator",
                schema: "ImportResult",
                columns: table => new
                {
                    DocumentAssistIndicatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false),
                    DocumentAssistId = table.Column<int>(type: "int", nullable: false),
                    ResultRepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAssistIndicator", x => x.DocumentAssistIndicatorId);
                    table.ForeignKey(
                        name: "FK_DocumentAssistIndicator_DocumentAssist_DocumentAssistId",
                        column: x => x.DocumentAssistId,
                        principalSchema: "ImportResult",
                        principalTable: "DocumentAssist",
                        principalColumn: "DocumentAssistId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DocumentAssistIndicator_ExcelColumnName_ExcelColumnNameId",
                        column: x => x.ExcelColumnNameId,
                        principalSchema: "ImportResult",
                        principalTable: "ExcelColumnName",
                        principalColumn: "ExcelColumnNameId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DocumentAssistIndicator_ResultRepository_ResultRepositoryId",
                        column: x => x.ResultRepositoryId,
                        principalSchema: "ImportResult",
                        principalTable: "ResultRepository",
                        principalColumn: "ResultRepositoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistIndicator_DocumentAssistId",
                schema: "ImportResult",
                table: "DocumentAssistIndicator",
                column: "DocumentAssistId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistIndicator_ExcelColumnNameId",
                schema: "ImportResult",
                table: "DocumentAssistIndicator",
                column: "ExcelColumnNameId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistIndicator_ResultRepositoryId",
                schema: "ImportResult",
                table: "DocumentAssistIndicator",
                column: "ResultRepositoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAssistIndicator",
                schema: "ImportResult");
        }
    }
}
