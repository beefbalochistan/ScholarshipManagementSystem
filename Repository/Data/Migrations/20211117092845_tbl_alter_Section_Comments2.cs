using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_Section_Comments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SectionComment",
                schema: "master",
                columns: table => new
                {
                    SectionCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BEEFSectionId = table.Column<int>(type: "int", nullable: false),
                    SeverityLevelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionComment", x => x.SectionCommentId);
                    table.ForeignKey(
                        name: "FK_SectionComment_BEEFSection_BEEFSectionId",
                        column: x => x.BEEFSectionId,
                        principalSchema: "master",
                        principalTable: "BEEFSection",
                        principalColumn: "BEEFSectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionComment_SeverityLevel_SeverityLevelId",
                        column: x => x.SeverityLevelId,
                        principalSchema: "Master",
                        principalTable: "SeverityLevel",
                        principalColumn: "SeverityLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionComment_BEEFSectionId",
                schema: "master",
                table: "SectionComment",
                column: "BEEFSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionComment_SeverityLevelId",
                schema: "master",
                table: "SectionComment",
                column: "SeverityLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionComment",
                schema: "master");
        }
    }
}
