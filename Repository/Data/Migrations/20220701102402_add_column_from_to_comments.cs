using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_column_from_to_comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromUserId",
                schema: "Student",
                table: "ApplicantStudent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                schema: "Student",
                table: "ApplicantStudent",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserId",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                schema: "Student",
                table: "ApplicantStudent");
        }
    }
}
