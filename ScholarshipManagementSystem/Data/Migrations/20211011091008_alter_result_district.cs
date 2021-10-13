using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alter_result_district : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainer_DistrictId",
                schema: "ImportResult",
                table: "ResultContainer",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultContainer_District_DistrictId",
                schema: "ImportResult",
                table: "ResultContainer",
                column: "DistrictId",
                principalSchema: "master",
                principalTable: "District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultContainer_District_DistrictId",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropIndex(
                name: "IX_ResultContainer_DistrictId",
                schema: "ImportResult",
                table: "ResultContainer");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "ImportResult",
                table: "ResultContainer");
        }
    }
}
