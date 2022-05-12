using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_payment_disbursement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDisbursement",
                columns: table => new
                {
                    PaymentDisbursementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheDocumentId = table.Column<int>(type: "int", nullable: false),
                    ApplicantReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDReceiverCNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDRelationWithScholar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDReceiverContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCnic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisbursementAmount = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionAmount = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectAgentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    DDScannedCopy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeScannedCopy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDocumentScannedCopy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDisbursement", x => x.PaymentDisbursementId);
                    table.ForeignKey(
                        name: "FK_PaymentDisbursement_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "Student",
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PaymentDisbursement_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.NoAction);
                });
           

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDisbursement_ApplicantId",
                table: "PaymentDisbursement",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDisbursement_PaymentMethodId",
                table: "PaymentDisbursement",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDisbursement");
        
        }
    }
}
