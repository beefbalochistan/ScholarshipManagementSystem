using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_tranch_Doc_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "int",
                nullable: false,
                defaultValue: 1);

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
