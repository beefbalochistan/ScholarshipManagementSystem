using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_temp_result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultRepositoryTemp",
                schema: "ImportResult",
                columns: table => new
                {
                    ResultRepositoryTempId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resultFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resultScannedFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelPolicyId = table.Column<int>(type: "int", nullable: false),
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultRepositoryTemp", x => x.ResultRepositoryTempId);
                    table.ForeignKey(
                        name: "FK_ResultRepositoryTemp_SchemeLevelPolicy_SchemeLevelPolicyId",
                        column: x => x.SchemeLevelPolicyId,
                        principalSchema: "scholar",
                        principalTable: "SchemeLevelPolicy",
                        principalColumn: "SchemeLevelPolicyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ResultRepositoryTemp_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SPDocumentViewerReport",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLL_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFind = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPDocumentViewerReport", x => x.SrNo);
                });

            migrationBuilder.CreateTable(
                name: "ResultContainerTemp",
                schema: "ImportResult",
                columns: table => new
                {
                    ResultContainerTempId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roll_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REG_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Father_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Candidate_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institute_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks_ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pass_Fail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CGPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    ResultRepositoryTempId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultContainerTemp", x => x.ResultContainerTempId);
                    table.ForeignKey(
                        name: "FK_ResultContainerTemp_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "master",
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ResultContainerTemp_ResultRepositoryTemp_ResultRepositoryTempId",
                        column: x => x.ResultRepositoryTempId,
                        principalSchema: "ImportResult",
                        principalTable: "ResultRepositoryTemp",
                        principalColumn: "ResultRepositoryTempId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainerTemp_DistrictId",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainerTemp_ResultRepositoryTempId",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                column: "ResultRepositoryTempId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRepositoryTemp_SchemeLevelPolicyId",
                schema: "ImportResult",
                table: "ResultRepositoryTemp",
                column: "SchemeLevelPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRepositoryTemp_ScholarshipFiscalYearId",
                schema: "ImportResult",
                table: "ResultRepositoryTemp",
                column: "ScholarshipFiscalYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultContainerTemp",
                schema: "ImportResult");

            migrationBuilder.DropTable(
                name: "SPDocumentViewerReport");

            migrationBuilder.DropTable(
                name: "ResultRepositoryTemp",
                schema: "ImportResult");
        }
    }
}
