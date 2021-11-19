using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class alter_tbl_SMSAPIService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                schema: "sms",
                table: "SMSAPIService",
                newName: "SendSMSURL");

            migrationBuilder.AddColumn<string>(
                name: "BalanceEnquiryURL",
                schema: "sms",
                table: "SMSAPIService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageExpiryURL",
                schema: "sms",
                table: "SMSAPIService",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceEnquiryURL",
                schema: "sms",
                table: "SMSAPIService");

            migrationBuilder.DropColumn(
                name: "PackageExpiryURL",
                schema: "sms",
                table: "SMSAPIService");

            migrationBuilder.RenameColumn(
                name: "SendSMSURL",
                schema: "sms",
                table: "SMSAPIService",
                newName: "URL");
        }
    }
}
