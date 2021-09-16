using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class add_tbl_degree_qouta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DegreeLevelQoutaBySchemeLevel",
                schema: "scholar",
                columns: table => new
                {
                    DegreeLevelQoutaBySchemeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: false),
                    ClassEnrollment = table.Column<int>(type: "int", nullable: false),
                    SlotAllocate = table.Column<float>(type: "real", nullable: false),
                    AdditionalSlotAllocate = table.Column<float>(type: "real", nullable: false),
                    StipendAmount = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    PolicySRCForumId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelPolicyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeLevelQoutaBySchemeLevel", x => x.DegreeLevelQoutaBySchemeLevelId);
                    table.ForeignKey(
                        name: "FK_DegreeLevelQoutaBySchemeLevel_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                        column: x => x.DegreeScholarshipLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeScholarshipLevel",
                        principalColumn: "DegreeScholarshipLevelId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DegreeLevelQoutaBySchemeLevel_PolicySRCForum_PolicySRCForumId",
                        column: x => x.PolicySRCForumId,
                        principalSchema: "scholar",
                        principalTable: "PolicySRCForum",
                        principalColumn: "PolicySRCForumId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DegreeLevelQoutaBySchemeLevel_SchemeLevelPolicy_SchemeLevelPolicyId",
                        column: x => x.SchemeLevelPolicyId,
                        principalSchema: "scholar",
                        principalTable: "SchemeLevelPolicy",
                        principalColumn: "SchemeLevelPolicyId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DegreeLevelQoutaBySchemeLevel_DegreeScholarshipLevelId",
                schema: "scholar",
                table: "DegreeLevelQoutaBySchemeLevel",
                column: "DegreeScholarshipLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DegreeLevelQoutaBySchemeLevel_PolicySRCForumId",
                schema: "scholar",
                table: "DegreeLevelQoutaBySchemeLevel",
                column: "PolicySRCForumId");

            migrationBuilder.CreateIndex(
                name: "IX_DegreeLevelQoutaBySchemeLevel_SchemeLevelPolicyId",
                schema: "scholar",
                table: "DegreeLevelQoutaBySchemeLevel",
                column: "SchemeLevelPolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DegreeLevelQoutaBySchemeLevel",
                schema: "scholar");
        }
    }
}
