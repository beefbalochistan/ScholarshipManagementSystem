using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_change_to_slot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Threshold",
                schema: "master",
                table: "DegreeScholarshipLevel",
                newName: "Slot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slot",
                schema: "master",
                table: "DegreeScholarshipLevel",
                newName: "Threshold");
        }
    }
}
