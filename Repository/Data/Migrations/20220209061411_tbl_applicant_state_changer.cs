using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_applicant_state_changer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicantStateChanger",
                schema: "Student",
                columns: table => new
                {
                    ApplicantStateChangerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ApplicantSelectionStatusId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantStateChanger", x => x.ApplicantStateChangerId);
                    table.ForeignKey(
                        name: "FK_ApplicantStateChanger_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "Student",
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicantStateChanger_ApplicantSelectionStatus_ApplicantSelectionStatusId",
                        column: x => x.ApplicantSelectionStatusId,
                        principalSchema: "Student",
                        principalTable: "ApplicantSelectionStatus",
                        principalColumn: "ApplicantSelectionStatusId",
                        onDelete: ReferentialAction.NoAction);
                });
            

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStateChanger_ApplicantId",
                schema: "Student",
                table: "ApplicantStateChanger",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStateChanger_ApplicantSelectionStatusId",
                schema: "Student",
                table: "ApplicantStateChanger",
                column: "ApplicantSelectionStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantStateChanger",
                schema: "Student");
            
        }
    }
}
