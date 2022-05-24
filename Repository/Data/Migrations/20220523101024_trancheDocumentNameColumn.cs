using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class trancheDocumentNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrancheDocumentName",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrancheDocumentName",
                schema: "VirtualAccount",
                table: "TrancheDocument");
        }
    }
}
