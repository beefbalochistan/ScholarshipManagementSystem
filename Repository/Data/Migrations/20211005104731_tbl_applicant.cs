using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_applicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Student");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                schema: "ImportResult",
                table: "ResultContainer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Applicant",
                schema: "Student",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BFormCNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherCareTakerCNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationWithCareTaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    ProvienceId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    DegreeScholarshipLevelId = table.Column<int>(type: "int", nullable: false),
                    ApplicantReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TehsilName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteHOD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteFocalPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteFocalDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteFocalMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteFocalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsitutePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInsituteAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalGPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedCGPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldInstitudeNameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameBoardUniversity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneWithCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ScanDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScanOtherDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedMethod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.ApplicantId);
                    table.ForeignKey(
                        name: "FK_Applicant_DegreeScholarshipLevel_DegreeScholarshipLevelId",
                        column: x => x.DegreeScholarshipLevelId,
                        principalSchema: "master",
                        principalTable: "DegreeScholarshipLevel",
                        principalColumn: "DegreeScholarshipLevelId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Applicant_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "master",
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_Provience_ProvienceId",
                        column: x => x.ProvienceId,
                        principalSchema: "master",
                        principalTable: "Provience",
                        principalColumn: "ProvienceId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Applicant_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_DegreeScholarshipLevelId",
                schema: "Student",
                table: "Applicant",
                column: "DegreeScholarshipLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_DistrictId",
                schema: "Student",
                table: "Applicant",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ProvienceId",
                schema: "Student",
                table: "Applicant",
                column: "ProvienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_SchemeLevelId",
                schema: "Student",
                table: "Applicant",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant",
                schema: "Student");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                schema: "ImportResult",
                table: "ResultContainer");
        }
    }
}
