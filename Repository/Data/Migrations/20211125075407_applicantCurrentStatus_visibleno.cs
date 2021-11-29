using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class applicantCurrentStatus_visibleno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisibleStateNo",
                schema: "Student",
                table: "ApplicantCurrentStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisibleStateNo",
                schema: "Student",
                table: "ApplicantCurrentStatus");
        }
    }
}
