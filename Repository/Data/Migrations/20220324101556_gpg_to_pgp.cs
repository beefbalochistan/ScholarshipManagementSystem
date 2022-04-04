using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class gpg_to_pgp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGPGGenerated",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "IsPGPGenerated");

            migrationBuilder.RenameColumn(
                name: "GPGKey",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "PGPKey");

            migrationBuilder.RenameColumn(
                name: "GPGGeneratedOn",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "PGPGeneratedOn");

            migrationBuilder.RenameColumn(
                name: "GPGAttachment",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "PGPAttachment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PGPKey",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "GPGKey");

            migrationBuilder.RenameColumn(
                name: "PGPGeneratedOn",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "GPGGeneratedOn");

            migrationBuilder.RenameColumn(
                name: "PGPAttachment",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "GPGAttachment");

            migrationBuilder.RenameColumn(
                name: "IsPGPGenerated",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                newName: "IsGPGGenerated");
        }
    }
}
