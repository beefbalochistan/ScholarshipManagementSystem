using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_alter_SMSTRAIL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                schema: "sms",
                table: "SMSAPIServiceAuditTrail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendBy",
                schema: "sms",
                table: "SMSAPIServiceAuditTrail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "sms",
                table: "SMSAPIServiceAuditTrail");

            migrationBuilder.DropColumn(
                name: "SendBy",
                schema: "sms",
                table: "SMSAPIServiceAuditTrail");
        }
    }
}
