using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class delete_schemelevel_tbl_Tranche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchemeLevelId",
                schema: "VirtualAccount",
                table: "Tranche");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchemeLevelId",
                schema: "VirtualAccount",
                table: "Tranche",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
