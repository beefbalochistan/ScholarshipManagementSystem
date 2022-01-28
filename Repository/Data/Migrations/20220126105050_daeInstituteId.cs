using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class daeInstituteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepositoryTemp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultRepository_DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository",
                column: "DAEInstituteId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_ResultRepository_DAEInstitute_DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository",
                column: "DAEInstituteId",
                principalSchema: "master",
                principalTable: "DAEInstitute",
                principalColumn: "DAEInstituteId",
                onDelete: ReferentialAction.Restrict);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultRepository_DAEInstitute_DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository");

           
            migrationBuilder.DropIndex(
                name: "IX_ResultRepository_DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository");

            

            migrationBuilder.DropColumn(
                name: "DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepositoryTemp");

            migrationBuilder.DropColumn(
                name: "DAEInstituteId",
                schema: "ImportResult",
                table: "ResultRepository");
        }
    }
}
