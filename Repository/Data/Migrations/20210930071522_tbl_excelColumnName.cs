using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_excelColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "ExcelColumnName",
                schema: "master",
                columns: table => new
                {
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelColumnName", x => x.ExcelColumnNameId);
                });

            migrationBuilder.CreateTable(
                name: "ResultContainer",
                schema: "master",
                columns: table => new
                {
                    ResultContainerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultRepositoryId = table.Column<int>(type: "int", nullable: false),
                    ColumnLabelId = table.Column<int>(type: "int", nullable: false),
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
                    C15 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultContainer", x => x.ResultContainerId);
                    table.ForeignKey(
                        name: "FK_ResultContainer_ColumnLabel_ColumnLabelId",
                        column: x => x.ColumnLabelId,
                        principalSchema: "master",
                        principalTable: "ColumnLabel",
                        principalColumn: "ColumnLabelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultContainer_ResultRepository_ResultRepositoryId",
                        column: x => x.ResultRepositoryId,
                        principalSchema: "master",
                        principalTable: "ResultRepository",
                        principalColumn: "ResultRepositoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainer_ColumnLabelId",
                schema: "master",
                table: "ResultContainer",
                column: "ColumnLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultContainer_ResultRepositoryId",
                schema: "master",
                table: "ResultContainer",
                column: "ResultRepositoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelColumnName",
                schema: "master");

            migrationBuilder.DropTable(
                name: "ResultContainer",
                schema: "master");           
        }
    }
}
