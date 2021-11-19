using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class prefrences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScholarshipQouta",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                newName: "ScholarshipSlot");

            migrationBuilder.RenameColumn(
                name: "SchemeMatrict",
                schema: "master",
                table: "Preference",
                newName: "SchemeMatrictStipend");

            migrationBuilder.RenameColumn(
                name: "SchemeMaster",
                schema: "master",
                table: "Preference",
                newName: "SchemeMasterStipend");

            migrationBuilder.RenameColumn(
                name: "SchemeMS",
                schema: "master",
                table: "Preference",
                newName: "SchemeMSStipend");

            migrationBuilder.RenameColumn(
                name: "SchemeIntermediate",
                schema: "master",
                table: "Preference",
                newName: "SchemeIntermediateStipend");

            migrationBuilder.RenameColumn(
                name: "SchemeBacholar",
                schema: "master",
                table: "Preference",
                newName: "SchemeDAEStipend");

            migrationBuilder.AddColumn<int>(
                name: "SchemeBacholarStipend",
                schema: "master",
                table: "Preference",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchemeBacholarStipend",
                schema: "master",
                table: "Preference");

            migrationBuilder.RenameColumn(
                name: "ScholarshipSlot",
                schema: "scholar",
                table: "SchemeLevelPolicy",
                newName: "ScholarshipQouta");

            migrationBuilder.RenameColumn(
                name: "SchemeMatrictStipend",
                schema: "master",
                table: "Preference",
                newName: "SchemeMatrict");

            migrationBuilder.RenameColumn(
                name: "SchemeMasterStipend",
                schema: "master",
                table: "Preference",
                newName: "SchemeMaster");

            migrationBuilder.RenameColumn(
                name: "SchemeMSStipend",
                schema: "master",
                table: "Preference",
                newName: "SchemeMS");

            migrationBuilder.RenameColumn(
                name: "SchemeIntermediateStipend",
                schema: "master",
                table: "Preference",
                newName: "SchemeIntermediate");

            migrationBuilder.RenameColumn(
                name: "SchemeDAEStipend",
                schema: "master",
                table: "Preference",
                newName: "SchemeBacholar");
        }
    }
}
