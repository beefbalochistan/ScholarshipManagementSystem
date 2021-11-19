using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_DAEQoutaSLP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DAEInstituteQoutaBySchemeLevel",
                columns: table => new
                {
                    DAEInstituteQoutaBySchemeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DAEInstituteId = table.Column<int>(type: "int", nullable: false),
                    ClassEnrollment = table.Column<int>(type: "int", nullable: false),
                    SlotAllocate = table.Column<float>(type: "real", nullable: false),
                    StipendAmount = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    PolicySRCForumId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    InstituteAdditionalSlot = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DAEInstituteQoutaBySchemeLevel", x => x.DAEInstituteQoutaBySchemeLevelId);
                    table.ForeignKey(
                        name: "FK_DAEInstituteQoutaBySchemeLevel_DAEInstitute_DAEInstituteId",
                        column: x => x.DAEInstituteId,
                        principalSchema: "master",
                        principalTable: "DAEInstitute",
                        principalColumn: "DAEInstituteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DAEInstituteQoutaBySchemeLevel_PolicySRCForum_PolicySRCForumId",
                        column: x => x.PolicySRCForumId,
                        principalSchema: "scholar",
                        principalTable: "PolicySRCForum",
                        principalColumn: "PolicySRCForumId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DAEInstituteQoutaBySchemeLevel_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DAEInstituteQoutaBySchemeLevel_DAEInstituteId",
                table: "DAEInstituteQoutaBySchemeLevel",
                column: "DAEInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_DAEInstituteQoutaBySchemeLevel_PolicySRCForumId",
                table: "DAEInstituteQoutaBySchemeLevel",
                column: "PolicySRCForumId");

            migrationBuilder.CreateIndex(
                name: "IX_DAEInstituteQoutaBySchemeLevel_SchemeLevelId",
                table: "DAEInstituteQoutaBySchemeLevel",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DAEInstituteQoutaBySchemeLevel");
        }
    }
}
