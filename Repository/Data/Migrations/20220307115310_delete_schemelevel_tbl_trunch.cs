using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class delete_schemelevel_tbl_trunch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchemeLevelId",
                schema: "VirtualAccount",
                table: "Trunch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchemeLevelId",
                schema: "VirtualAccount",
                table: "Trunch",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
