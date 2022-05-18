using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class letter_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLetterGenerated",
                schema: "VirtualAccount",
                table: "Tranche",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LetterAttachment",
                schema: "VirtualAccount",
                table: "Tranche",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLetterGenerated",
                schema: "VirtualAccount",
                table: "Tranche");

            migrationBuilder.DropColumn(
                name: "LetterAttachment",
                schema: "VirtualAccount",
                table: "Tranche");
        }
    }
}
