using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class altr_DAE_add_year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "DAEInstituteQoutaBySchemeLevel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DAEYear",
                schema: "master",
                table: "DAEInstitute",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "DAEInstituteQoutaBySchemeLevel");

            migrationBuilder.DropColumn(
                name: "DAEYear",
                schema: "master",
                table: "DAEInstitute");
        }
    }
}
