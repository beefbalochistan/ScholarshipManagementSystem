using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class applicant_comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantReferenceNo",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicantCurrentStatus",
                schema: "Student",
                columns: table => new
                {
                    ApplicantCurrentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantCurrentStatus", x => x.ApplicantCurrentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "master",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSectionHead = table.Column<bool>(type: "bit", nullable: false),
                    BEEFSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_BEEFSection_BEEFSectionId",
                        column: x => x.BEEFSectionId,
                        principalSchema: "master",
                        principalTable: "BEEFSection",
                        principalColumn: "BEEFSectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantStudent",
                schema: "Student",
                columns: table => new
                {
                    ApplicantStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    SelectionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicantReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeniorityLevel = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantStudent", x => x.ApplicantStudentId);
                    table.ForeignKey(
                        name: "FK_ApplicantStudent_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "Student",
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantStudent_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "master",
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_ApplicantId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantStudent_EmployeeId",
                schema: "Student",
                table: "ApplicantStudent",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BEEFSectionId",
                schema: "master",
                table: "Employee",
                column: "BEEFSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantCurrentStatus",
                schema: "Student");

            migrationBuilder.DropTable(
                name: "ApplicantStudent",
                schema: "Student");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "master");

            migrationBuilder.DropColumn(
                name: "ApplicantCurrentStatusId",
                schema: "Student",
                table: "Applicant");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantReferenceNo",
                schema: "Student",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
