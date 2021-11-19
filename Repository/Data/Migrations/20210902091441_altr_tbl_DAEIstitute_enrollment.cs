using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class altr_tbl_DAEIstitute_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DAEThreshold",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Enrollment1stY",
                schema: "master",
                table: "DAEInstitute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Enrollment2ndY",
                schema: "master",
                table: "DAEInstitute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Enrollment3rdY",
                schema: "master",
                table: "DAEInstitute",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DAEThreshold",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "Enrollment1stY",
                schema: "master",
                table: "DAEInstitute");

            migrationBuilder.DropColumn(
                name: "Enrollment2ndY",
                schema: "master",
                table: "DAEInstitute");

            migrationBuilder.DropColumn(
                name: "Enrollment3rdY",
                schema: "master",
                table: "DAEInstitute");
        }
    }
}
