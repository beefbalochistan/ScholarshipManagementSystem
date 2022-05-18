using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_company_info_alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Singatory3",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory3Name");

            migrationBuilder.RenameColumn(
                name: "Singatory2",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory3Designation");

            migrationBuilder.RenameColumn(
                name: "Singatory1",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory2Name");

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                schema: "master",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Singatory1Designation",
                schema: "master",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Singatory1Name",
                schema: "master",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Singatory2Designation",
                schema: "master",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                schema: "master",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Singatory1Designation",
                schema: "master",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Singatory1Name",
                schema: "master",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Singatory2Designation",
                schema: "master",
                table: "CompanyInfo");

            migrationBuilder.RenameColumn(
                name: "Singatory3Name",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory3");

            migrationBuilder.RenameColumn(
                name: "Singatory3Designation",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory2");

            migrationBuilder.RenameColumn(
                name: "Singatory2Name",
                schema: "master",
                table: "CompanyInfo",
                newName: "Singatory1");
        }
    }
}
