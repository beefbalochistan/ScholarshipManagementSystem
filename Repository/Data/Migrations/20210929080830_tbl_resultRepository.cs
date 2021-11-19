using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_resultRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "master",
                table: "ColumnLabel",
                newName: "C9");

            migrationBuilder.AddColumn<string>(
                name: "C1",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C10",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C11",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C12",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C13",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C14",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C15",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C2",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C3",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C4",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C5",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C6",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C7",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C8",
                schema: "master",
                table: "ColumnLabel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResultRepository",
                schema: "master",
                columns: table => new
                {
                    ResultRepositoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resultFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultRepository", x => x.ResultRepositoryId);
                    table.ForeignKey(
                        name: "FK_ResultRepository_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultRepository_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultRepository_SchemeLevelId",
                schema: "master",
                table: "ResultRepository",
                column: "SchemeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRepository_ScholarshipFiscalYearId",
                schema: "master",
                table: "ResultRepository",
                column: "ScholarshipFiscalYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultRepository",
                schema: "master");

            migrationBuilder.DropColumn(
                name: "C1",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C10",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C11",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C12",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C13",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C14",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C15",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C2",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C3",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C4",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C5",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C6",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C7",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.DropColumn(
                name: "C8",
                schema: "master",
                table: "ColumnLabel");

            migrationBuilder.RenameColumn(
                name: "C9",
                schema: "master",
                table: "ColumnLabel",
                newName: "Name");
        }
    }
}
