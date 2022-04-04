using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tranchdocument_isexecuted_isauto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsAutoDisbursement",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsExecuteSuccessfully",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAutoDisbursement",
                schema: "VirtualAccount",
                table: "TrancheDocument");

            migrationBuilder.DropColumn(
                name: "IsExecuteSuccessfully",
                schema: "VirtualAccount",
                table: "TrancheDocument");
        }
    }
}
