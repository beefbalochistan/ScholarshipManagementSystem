using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_column_expression : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Condition",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                newName: "Expression");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expression",
                schema: "master",
                table: "SelectionCriteriaGeneral",
                newName: "Condition");
        }
    }
}
