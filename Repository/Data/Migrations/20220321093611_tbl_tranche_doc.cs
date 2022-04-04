using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_tranche_doc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrancheDocument",
                schema: "VirtualAccount",
                columns: table => new
                {
                    TrancheDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheId = table.Column<int>(type: "int", nullable: false),
                    CSVAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSVAttachmentOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsGPGGenerated = table.Column<bool>(type: "bit", nullable: false),
                    GPGAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPGGeneratedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GPGKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsSendToServer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrancheDocument", x => x.TrancheDocumentId);
                    table.ForeignKey(
                        name: "FK_TrancheDocument_Tranche_TrancheId",
                        column: x => x.TrancheId,
                        principalSchema: "VirtualAccount",
                        principalTable: "Tranche",
                        principalColumn: "TrancheId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrancheDocument_TrancheId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                column: "TrancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrancheDocument",
                schema: "VirtualAccount");
        }
    }
}
