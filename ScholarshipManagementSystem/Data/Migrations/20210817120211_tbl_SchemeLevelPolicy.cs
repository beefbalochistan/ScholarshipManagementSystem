using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_SchemeLevelPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchemeLevelPolicy",
                schema: "scholar",
                columns: table => new
                {
                    SchemeLevelPolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ScholarshipQouta = table.Column<int>(type: "int", nullable: false),
                    POMS = table.Column<int>(type: "int", nullable: false),
                    DOMS = table.Column<int>(type: "int", nullable: false),
                    SQSOMS = table.Column<int>(type: "int", nullable: false),
                    SQSEVIs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevelPolicy", x => x.SchemeLevelPolicyId);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPolicy_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPolicy_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPolicy_SchemeLevelId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "SchemeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPolicy_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "ScholarshipFiscalYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemeLevelPolicy",
                schema: "scholar");
        }
    }
}
