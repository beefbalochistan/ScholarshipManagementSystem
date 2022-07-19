using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_useraccesstopaymentmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {           
           
            migrationBuilder.CreateTable(
                name: "UserAccessToPaymentMethod",
                schema: "master",
                columns: table => new
                {
                    UserAccessToPaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessToPaymentMethod", x => x.UserAccessToPaymentMethodId);
                    table.ForeignKey(
                        name: "FK_UserAccessToPaymentMethod_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserAccessToPaymentMethod_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessToPaymentMethod_PaymentMethodId",
                schema: "master",
                table: "UserAccessToPaymentMethod",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessToPaymentMethod_UserId",
                schema: "master",
                table: "UserAccessToPaymentMethod",
                column: "UserId");      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.DropTable(
                name: "UserAccessToPaymentMethod",
                schema: "master");                   
        }
    }
}
