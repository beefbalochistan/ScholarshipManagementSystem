using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class schemelevelmandatorycolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchemeLevelMandatoryColumn",
                schema: "master",
                columns: table => new
                {
                    SchemeLevelMandatoryColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelColumnNameId = table.Column<int>(type: "int", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevelMandatoryColumn", x => x.SchemeLevelMandatoryColumnId);
                    table.ForeignKey(
                        name: "FK_SchemeLevelMandatoryColumn_ExcelColumnName_ExcelColumnNameId",
                        column: x => x.ExcelColumnNameId,
                        principalSchema: "ImportResult",
                        principalTable: "ExcelColumnName",
                        principalColumn: "ExcelColumnNameId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SchemeLevelMandatoryColumn_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelMandatoryColumn_ExcelColumnNameId",
                schema: "master",
                table: "SchemeLevelMandatoryColumn",
                column: "ExcelColumnNameId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelMandatoryColumn_SchemeLevelId",
                schema: "master",
                table: "SchemeLevelMandatoryColumn",
                column: "SchemeLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemeLevelMandatoryColumn",
                schema: "master");
        }
    }
}
