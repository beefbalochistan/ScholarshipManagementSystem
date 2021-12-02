using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class SRC_ISFreez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFreez",
                schema: "scholar",
                table: "PolicySRCForum",
                type: "bit",
                nullable: false,
                defaultValue: false);            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.DropColumn(
                name: "IsFreez",
                schema: "scholar",
                table: "PolicySRCForum");
        }
    }
}
