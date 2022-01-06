using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_column_department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "ImportResult",
                table: "ResultContainerTemp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                schema: "ImportResult",
                table: "ResultContainerTemp");

            migrationBuilder.DropColumn(
                name: "Department",
                schema: "ImportResult",
                table: "ResultContainer");

            
        }
    }
}
