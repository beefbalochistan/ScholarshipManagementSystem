using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_labelcolumntemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnLabelTemp",
                schema: "ImportResult",
                columns: table => new
                {
                    ColumnLabelTempId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ResultRepositoryTempId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnLabelTemp", x => x.ColumnLabelTempId);
                    table.ForeignKey(
                        name: "FK_ColumnLabelTemp_ResultRepositoryTemp_ResultRepositoryTempId",
                        column: x => x.ResultRepositoryTempId,
                        principalSchema: "ImportResult",
                        principalTable: "ResultRepositoryTemp",
                        principalColumn: "ResultRepositoryTempId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnLabelTemp_ResultRepositoryTempId",
                schema: "ImportResult",
                table: "ColumnLabelTemp",
                column: "ResultRepositoryTempId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnLabelTemp",
                schema: "ImportResult");
        }
    }
}
