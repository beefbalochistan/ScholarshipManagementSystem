using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class alterSchemeLevel_InstitudeDepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevel_InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel",
                column: "InstitudeDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeLevel_InstitudeDepartment_InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel",
                column: "InstitudeDepartmentId",
                principalSchema: "master",
                principalTable: "InstitudeDepartment",
                principalColumn: "InstitudeDepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchemeLevel_InstitudeDepartment_InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropIndex(
                name: "IX_SchemeLevel_InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropColumn(
                name: "InstitudeDepartmentId",
                schema: "master",
                table: "SchemeLevel");
        }
    }
}
