using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_ApplicantInbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicantInbox",
                schema: "Student",
                columns: table => new
                {
                    ApplicantInboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantInbox", x => x.ApplicantInboxId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantInboxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantInboxId",
                principalSchema: "Student",
                principalTable: "ApplicantInbox",
                principalColumn: "ApplicantInboxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropTable(
                name: "ApplicantInbox",
                schema: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_ApplicantInboxId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "ApplicantInboxId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
