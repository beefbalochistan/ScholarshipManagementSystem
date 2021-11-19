using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_useraccesstoforward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccessToForward",
                schema: "master",
                columns: table => new
                {
                    UserAccessToForwardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantCurrentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessToForward", x => x.UserAccessToForwardId);
                    table.ForeignKey(
                        name: "FK_UserAccessToForward_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                        column: x => x.ApplicantCurrentStatusId,
                        principalSchema: "Student",
                        principalTable: "ApplicantCurrentStatus",
                        principalColumn: "ApplicantCurrentStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessToForward_ApplicantCurrentStatusId",
                schema: "master",
                table: "UserAccessToForward",
                column: "ApplicantCurrentStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccessToForward",
                schema: "master");
        }
    }
}
