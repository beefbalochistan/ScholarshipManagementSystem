using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_districtdetail_districtqouta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchemeSimpleGraduationStipend",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MPIScore",
                schema: "master",
                table: "DistrictDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchemeSimpleGraduationStipend",
                schema: "master",
                table: "Preference");

            migrationBuilder.DropColumn(
                name: "MPIScore",
                schema: "master",
                table: "DistrictDetail");
        }
    }
}
