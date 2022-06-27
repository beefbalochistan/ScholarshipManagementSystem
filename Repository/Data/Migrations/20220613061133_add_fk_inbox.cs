using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_fk_inbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantInboxId",
                principalSchema: "Student",
                principalTable: "ApplicantInbox",
                principalColumn: "ApplicantInboxId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_ApplicantInbox_ApplicantInboxId",
                schema: "Student",
                table: "Applicant",
                column: "ApplicantInboxId",
                principalSchema: "Student",
                principalTable: "ApplicantInbox",
                principalColumn: "ApplicantInboxId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
