using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_Section_Comments2 : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BEEFSectionId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionComment_BEEFSectionId",
                schema: "master",
                table: "SectionComment",
                column: "BEEFSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionComment",
                schema: "master");
        }
    }
}
