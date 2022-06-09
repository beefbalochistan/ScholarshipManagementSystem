using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_religion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Religion",
                schema: "master",
                table: "Religion");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                schema: "master",
                table: "Religion");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "master",
                table: "Religion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Religion",
                schema: "master",
                table: "Religion",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Religion",
                schema: "master",
                table: "Religion");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "master",
                table: "Religion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                schema: "master",
                table: "Religion",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Religion",
                schema: "master",
                table: "Religion",
                column: "ReligionId");
        }
    }
}
