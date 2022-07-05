using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_applicantstudent_to_from : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {                          

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "62b1ee79-4b55-4302-a2f4-fd5007ed0e21",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "master",
                table: "UserAccessToForward",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "62b1ee79-4b55-4302-a2f4-fd5007ed0e21",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessToSchemeLevel_UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessToForward_UserId",
                schema: "master",
                table: "UserAccessToForward",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessToForward_AspNetUsers_UserId",
                schema: "master",
                table: "UserAccessToForward",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessToSchemeLevel_AspNetUsers_UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessToForward_AspNetUsers_UserId",
                schema: "master",
                table: "UserAccessToForward");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessToSchemeLevel_AspNetUsers_UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel");

            migrationBuilder.DropIndex(
                name: "IX_UserAccessToSchemeLevel_UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel");

            migrationBuilder.DropIndex(
                name: "IX_UserAccessToForward_UserId",
                schema: "master",
                table: "UserAccessToForward");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "master",
                table: "UserAccessToSchemeLevel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "master",
                table: "UserAccessToForward",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
          
        }
    }
}
