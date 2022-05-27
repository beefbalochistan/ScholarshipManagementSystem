using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class fk_remove_tranchedoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrancheDocument_PaymentDisbursementMode_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");

            migrationBuilder.DropIndex(
                name: "IX_TrancheDocument_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");

            migrationBuilder.DropColumn(
                name: "PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrancheDocument_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                column: "PaymentDisbursementModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrancheDocument_PaymentDisbursementMode_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                column: "PaymentDisbursementModeId",
                principalSchema: "master",
                principalTable: "PaymentDisbursementMode",
                principalColumn: "PaymentDisbursementModeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
