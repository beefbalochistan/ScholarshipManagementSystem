using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_Pre_QPre_SLP_SRC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preference",
                schema: "master",
                columns: table => new
                {
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeMatrict = table.Column<int>(type: "int", nullable: false),
                    SchemeIntermediate = table.Column<int>(type: "int", nullable: false),
                    SchemeBacholar = table.Column<int>(type: "int", nullable: false),
                    SchemeMaster = table.Column<int>(type: "int", nullable: false),
                    SchemeMS = table.Column<int>(type: "int", nullable: false),
                    DistrictThreshold = table.Column<int>(type: "int", nullable: false),
                    InstituteThreshold = table.Column<int>(type: "int", nullable: false),
                    POMSDOMSBoardQouta = table.Column<int>(type: "int", nullable: false),
                    POMSDOMSInstituteQouta = table.Column<int>(type: "int", nullable: false),
                    SQSOMSQouta = table.Column<int>(type: "int", nullable: false),
                    SQSEVIQouta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preference", x => x.PreferenceId);
                });

            migrationBuilder.CreateTable(
                name: "PreferencesSlot",
                schema: "master",
                columns: table => new
                {
                    PreferencesSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QoutaMetric = table.Column<int>(type: "int", nullable: false),
                    QoutaFAFSc1Y = table.Column<int>(type: "int", nullable: false),
                    QoutaFAFSc2Y = table.Column<int>(type: "int", nullable: false),
                    QoutaDAE1Y = table.Column<int>(type: "int", nullable: false),
                    QoutaDAE2Y = table.Column<int>(type: "int", nullable: false),
                    QoutaDAE3Y = table.Column<int>(type: "int", nullable: false),
                    QoutaBacholar1Y = table.Column<int>(type: "int", nullable: false),
                    BacholarQouta = table.Column<int>(type: "int", nullable: false),
                    MasterQouta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencesSlot", x => x.PreferencesSlotId);
                });

            migrationBuilder.CreateTable(
                name: "SchemeLevel",
                schema: "scholar",
                columns: table => new
                {
                    PolicySRCForumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEndorse = table.Column<bool>(type: "bit", nullable: false),
                    SRCMinutesAttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyDocumentAttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevel", x => x.PolicySRCForumId);
                    table.ForeignKey(
                        name: "FK_SchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeLevelPolicy",
                schema: "scholar",
                columns: table => new
                {
                    SchemeLevelPolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    PolicySRCForumId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ScholarshipQouta = table.Column<int>(type: "int", nullable: false),
                    POMS = table.Column<int>(type: "int", nullable: false),
                    DOMS = table.Column<int>(type: "int", nullable: false),
                    SQSOMS = table.Column<int>(type: "int", nullable: false),
                    SQSEVIs = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevelPolicy", x => x.SchemeLevelPolicyId);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPolicy_SchemeLevel_PolicySRCForumId",
                        column: x => x.PolicySRCForumId,
                        principalSchema: "scholar",
                        principalTable: "SchemeLevel",
                        principalColumn: "PolicySRCForumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPolicy_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevel_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevel",
                column: "ScholarshipFiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPolicy_PolicySRCForumId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "PolicySRCForumId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPolicy_SchemeLevelId",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preference",
                schema: "master");

            migrationBuilder.DropTable(
                name: "PreferencesSlot",
                schema: "master");

            migrationBuilder.DropTable(
                name: "SchemeLevelPolicy",
                schema: "scholar");

            migrationBuilder.DropTable(
                name: "SchemeLevel",
                schema: "scholar");
        }
    }
}
