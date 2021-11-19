using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_result_IsClean_IsSCriteriaApplied : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDataCleaned",
                schema: "ImportResult",
                table: "ResultRepository",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelctionCriteriaApplied",
                schema: "ImportResult",
                table: "ResultRepository",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDataCleaned",
                schema: "ImportResult",
                table: "ResultRepository");

            migrationBuilder.DropColumn(
                name: "IsSelctionCriteriaApplied",
                schema: "ImportResult",
                table: "ResultRepository");
        }
    }
}
