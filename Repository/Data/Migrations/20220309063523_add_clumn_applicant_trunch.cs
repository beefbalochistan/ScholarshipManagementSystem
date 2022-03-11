using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_clumn_applicant_trunch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrunchId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_TrunchId",
                schema: "Student",
                table: "Applicant",
                column: "TrunchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Trunch_TrunchId",
                schema: "Student",
                table: "Applicant",
                column: "TrunchId",
                principalSchema: "VirtualAccount",
                principalTable: "Trunch",
                principalColumn: "TrunchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Trunch_TrunchId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_TrunchId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "TrunchId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
