using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_clumn_applicant_Tranche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrancheId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_TrancheId",
                schema: "Student",
                table: "Applicant",
                column: "TrancheId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Tranche_TrancheId",
                schema: "Student",
                table: "Applicant",
                column: "TrancheId",
                principalSchema: "VirtualAccount",
                principalTable: "Tranche",
                principalColumn: "TrancheId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Tranche_TrancheId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_TrancheId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "TrancheId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
