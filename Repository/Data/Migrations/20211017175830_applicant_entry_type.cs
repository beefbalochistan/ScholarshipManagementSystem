using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class applicant_entry_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryThrough",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterationNumber",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryThrough",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "RegisterationNumber",
                schema: "Student",
                table: "Applicant");
        }
    }
}
