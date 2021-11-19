using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class changes_institute_range : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProvienceId",
                schema: "master",
                table: "Institute",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "master",
                table: "Degree",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ProvienceId",
                schema: "master",
                table: "Institute",
                column: "ProvienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Provience_ProvienceId",
                schema: "master",
                table: "Institute",
                column: "ProvienceId",
                principalSchema: "master",
                principalTable: "Provience",
                principalColumn: "ProvienceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Provience_ProvienceId",
                schema: "master",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_ProvienceId",
                schema: "master",
                table: "Institute");

            migrationBuilder.AlterColumn<string>(
                name: "ProvienceId",
                schema: "master",
                table: "Institute",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "master",
                table: "Degree",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);
        }
    }
}
