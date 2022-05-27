using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class col_remove_CKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethodMode",
                schema: "VirtualAccount",
                table: "PaymentMethodMode");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "PaymentMethodMode",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethodMode",
                schema: "VirtualAccount",
                table: "PaymentMethodMode",
                column: "PaymentMethodModeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodMode_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "PaymentMethodMode",
                column: "PaymentDisbursementModeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethodMode",
                schema: "VirtualAccount",
                table: "PaymentMethodMode");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethodMode_PaymentDisbursementModeId",
                schema: "VirtualAccount",
                table: "PaymentMethodMode");

            migrationBuilder.DropColumn(
                name: "PaymentMethodModeId",
                schema: "VirtualAccount",
                table: "PaymentMethodMode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethodMode",
                schema: "VirtualAccount",
                table: "PaymentMethodMode",
                columns: new[] { "PaymentDisbursementModeId", "PaymentMethodId" });
        }
    }
}
