using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_SchemeLevelDegreeLevel_DegreeLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DegreeLevel",
                schema: "master",
                columns: table => new
                {
                    DegreeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeLevel", x => x.DegreeLevelId);
                    table.ForeignKey(
                        name: "FK_DegreeLevel_Degree_DegreeId",
                        column: x => x.DegreeId,
                        principalSchema: "master",
                        principalTable: "Degree",
                        principalColumn: "DegreeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DegreeScholarshipLevel",
                schema: "scholar",
                columns: table => new
                {
                    SchemeLevelDegreeLevelId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_DegreeLevel_DegreeId",
                schema: "master",
                table: "DegreeLevel",
                column: "DegreeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DegreeScholarshipLevel",
                schema: "scholar");

            migrationBuilder.DropTable(
                name: "DegreeLevel",
                schema: "master");
        }
    }
}
