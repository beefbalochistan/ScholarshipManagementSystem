using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class tbl_SchemeLevelPolicy_preferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DOMS",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POMS",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSEVIs",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SQSOMS",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScholarshipQouta",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOMS",
                schema: "scholar",
                table: "SchemeLevelPayment");

            migrationBuilder.DropColumn(
                name: "POMS",
                schema: "scholar",
                table: "SchemeLevelPayment");

            migrationBuilder.DropColumn(
                name: "SQSEVIs",
                schema: "scholar",
                table: "SchemeLevelPayment");

            migrationBuilder.DropColumn(
                name: "SQSOMS",
                schema: "scholar",
                table: "SchemeLevelPayment");

            migrationBuilder.DropColumn(
                name: "ScholarshipQouta",
                schema: "scholar",
                table: "SchemeLevelPayment");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "scholar",
                table: "SchemeLevelPayment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
