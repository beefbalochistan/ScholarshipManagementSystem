using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class altr_rename_degreescholarshiplevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DegreeScholarshipLevel",
                schema: "scholar");

            migrationBuilder.CreateTable(
                name: "DegreeScholarshipLevel",
                schema: "master",
                columns: table => new
                {
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Enrollment = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<float>(type: "real", nullable: false),
                    DegreeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeScholarshipLevel", x => x.DegreeScholarshipLevelId);
                    table.ForeignKey(
                        name: "FK_DegreeScholarshipLevel_DegreeLevel_DegreeLevelId",
                        column: x => x.DegreeLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeLevel",
                        principalColumn: "DegreeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DegreeScholarshipLevel_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DegreeScholarshipLevel_DegreeLevelId",
                schema: "master",
                table: "DegreeScholarshipLevel",
                column: "DegreeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DegreeScholarshipLevel_SchemeLevelId",
                schema: "master",
                table: "DegreeScholarshipLevel",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DegreeScholarshipLevel",
                schema: "master");

            migrationBuilder.CreateTable(
                name: "DegreeScholarshipLevel",
                schema: "scholar",
                columns: table => new
                {
                    SchemeLevelDegreeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegreeLevelId = table.Column<int>(type: "int", nullable: false),
                    Enrollment = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevelDegreeLevel", x => x.SchemeLevelDegreeLevelId);
                    table.ForeignKey(
                        name: "FK_SchemeLevelDegreeLevel_DegreeLevel_DegreeLevelId",
                        column: x => x.DegreeLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeLevel",
                        principalColumn: "DegreeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemeLevelDegreeLevel_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelDegreeLevel_DegreeLevelId",
                schema: "scholar",
                table: "DegreeScholarshipLevel",
                column: "DegreeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelDegreeLevel_SchemeLevelId",
                schema: "scholar",
                table: "DegreeScholarshipLevel",
                column: "SchemeLevelId");
        }
    }
}
