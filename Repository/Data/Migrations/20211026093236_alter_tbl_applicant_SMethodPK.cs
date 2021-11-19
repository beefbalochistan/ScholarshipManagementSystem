using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_applicant_SMethodPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applicant_SelectionMethodId",
                schema: "Student",
                table: "Applicant",
                column: "SelectionMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_SelectionMethod_SelectionMethodId",
                schema: "Student",
                table: "Applicant",
                column: "SelectionMethodId",
                principalSchema: "master",
                principalTable: "SelectionMethod",
                principalColumn: "SelectionMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_SelectionMethod_SelectionMethodId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_SelectionMethodId",
                schema: "Student",
                table: "Applicant");
        }
    }
}
