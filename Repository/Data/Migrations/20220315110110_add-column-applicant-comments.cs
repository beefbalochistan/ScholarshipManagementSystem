using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class addcolumnapplicantcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment",
                type: "int",
                nullable: false,
                defaultValue: 1);
            

            migrationBuilder.CreateIndex(
                name: "IX_SectionComment_ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment",
                column: "ApplicantCurrentStatusId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_SectionComment_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment",
                column: "ApplicantCurrentStatusId",
                principalSchema: "Student",
                principalTable: "ApplicantCurrentStatus",
                principalColumn: "ApplicantCurrentStatusId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropForeignKey(
                name: "FK_SectionComment_ApplicantCurrentStatus_ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment");

            migrationBuilder.DropIndex(
                name: "IX_SectionComment_ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment");

           

            migrationBuilder.DropColumn(
                name: "ApplicantCurrentStatusId",
                schema: "master",
                table: "SectionComment");

            
        }
    }
}
