using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialQouta",
                schema: "master",
                table: "SchemeLevel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BEEFSectionId",
                table: "AspNetUsers",
                column: "BEEFSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BEEFSection_BEEFSectionId",
                table: "AspNetUsers",
                column: "BEEFSectionId",
                principalSchema: "master",
                principalTable: "BEEFSection",
                principalColumn: "BEEFSectionId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BEEFSection_BEEFSectionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BEEFSectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsSpecialQouta",
                schema: "master",
                table: "SchemeLevel");
           
        }
    }
}
