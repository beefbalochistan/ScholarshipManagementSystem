using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_tbl_finance_current_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicantFinanceCurrentStatus",
                schema: "Student",
                columns: table => new
                {
                    ApplicantFinanceCurrentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessValue = table.Column<int>(type: "int", nullable: false),
                    VisibleStateNo = table.Column<int>(type: "int", nullable: false),
                    VisibleStateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visibility = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantFinanceCurrentStatus", x => x.ApplicantFinanceCurrentStatusId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantFinanceCurrentStatus",
                schema: "Student");
        }
    }
}
