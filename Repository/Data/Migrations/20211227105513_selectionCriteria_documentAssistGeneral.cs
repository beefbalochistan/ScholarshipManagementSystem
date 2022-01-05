using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class selectionCriteria_documentAssistGeneral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAssistGeneral",
                schema: "master",
                columns: table => new
                {
                    DocumentAssistGeneralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false),
                    DocumentAssistId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAssistGeneral", x => x.DocumentAssistGeneralId);
                    table.ForeignKey(
                        name: "FK_DocumentAssistGeneral_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                        column: x => x.DegreeScholarshipLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeScholarshipLevel",
                        principalColumn: "DegreeScholarshipLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentAssistGeneral_DocumentAssist_DocumentAssistId",
                        column: x => x.DocumentAssistId,
                        principalSchema: "ImportResult",
                        principalTable: "DocumentAssist",
                        principalColumn: "DocumentAssistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAssistGeneral_ExcelColumnName_ExcelColumnNameId",
                        column: x => x.ExcelColumnNameId,
                        principalSchema: "ImportResult",
                        principalTable: "ExcelColumnName",
                        principalColumn: "ExcelColumnNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAssistGeneral_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectionCriteriaGeneral",
                schema: "master",
                columns: table => new
                {
                    SelectionCriteriaGeneralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionCriteriaGeneral", x => x.SelectionCriteriaGeneralId);
                    table.ForeignKey(
                        name: "FK_SelectionCriteriaGeneral_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                        column: x => x.DegreeScholarshipLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeScholarshipLevel",
                        principalColumn: "DegreeScholarshipLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectionCriteriaGeneral_ExcelColumnName_ExcelColumnNameId",
                        column: x => x.ExcelColumnNameId,
                        principalSchema: "ImportResult",
                        principalTable: "ExcelColumnName",
                        principalColumn: "ExcelColumnNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectionCriteriaGeneral_Operator_OperatorId",
                        column: x => x.OperatorId,
                        principalSchema: "master",
                        principalTable: "Operator",
                        principalColumn: "OperatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectionCriteriaGeneral_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistGeneral_DegreeScholarshipLevelId",
                schema: "master",
                table: "DocumentAssistGeneral",
                column: "DegreeScholarshipLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistGeneral_DocumentAssistId",
                schema: "master",
                table: "DocumentAssistGeneral",
                column: "DocumentAssistId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistGeneral_ExcelColumnNameId",
                schema: "master",
                table: "DocumentAssistGeneral",
                column: "ExcelColumnNameId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAssistGeneral_SchemeLevelId",
                schema: "master",
                table: "DocumentAssistGeneral",
                column: "SchemeLevelId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SelectionCriteriaGeneral_SchemeLevelId",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAssistGeneral",
                schema: "master");

            migrationBuilder.DropTable(
                name: "SelectionCriteriaGeneral",
                schema: "master");
        }
    }
}
