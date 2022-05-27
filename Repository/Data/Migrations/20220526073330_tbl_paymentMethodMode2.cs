using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_paymentMethodMode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethodMode",
                schema: "VirtualAccount",
                columns: table => new
                {
                    PaymentDisbursementModeId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    PublicKeyFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SFTP_IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SFTP_Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SFTP_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SFTP_Port = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodMode", x => new { x.PaymentDisbursementModeId, x.PaymentMethodId });
                    table.ForeignKey(
                        name: "FK_PaymentMethodMode_PaymentDisbursementMode_PaymentDisbursementModeId",
                        column: x => x.PaymentDisbursementModeId,
                        principalSchema: "master",
                        principalTable: "PaymentDisbursementMode",
                        principalColumn: "PaymentDisbursementModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentMethodMode_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodMode_PaymentMethodId",
                schema: "VirtualAccount",
                table: "PaymentMethodMode",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethodMode",
                schema: "VirtualAccount");
        }
    }
}
