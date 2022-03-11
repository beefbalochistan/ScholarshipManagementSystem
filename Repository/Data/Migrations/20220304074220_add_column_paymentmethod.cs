using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class add_column_paymentmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                schema: "master",
                table: "SchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevel_PaymentMethodId",
                schema: "master",
                table: "SchemeLevel",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchemeLevel_PaymentMethod_PaymentMethodId",
                schema: "master",
                table: "SchemeLevel",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchemeLevel_PaymentMethod_PaymentMethodId",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropIndex(
                name: "IX_SchemeLevel_PaymentMethodId",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                schema: "master",
                table: "SchemeLevel");
        }
    }
}
