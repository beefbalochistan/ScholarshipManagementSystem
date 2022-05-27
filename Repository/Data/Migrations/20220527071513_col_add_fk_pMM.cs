using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class col_add_fk_pMM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_TrancheDocument_PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                column: "PaymentMethodModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrancheDocument_PaymentMethodMode_PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument",
                column: "PaymentMethodModeId",
                principalSchema: "VirtualAccount",
                principalTable: "PaymentMethodMode",
                principalColumn: "PaymentMethodModeId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrancheDocument_PaymentMethodMode_PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");

            migrationBuilder.DropIndex(
                name: "IX_TrancheDocument_PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");

            migrationBuilder.DropColumn(
                name: "PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "TrancheDocument");
        }
    }
}
