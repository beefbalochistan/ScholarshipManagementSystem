using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_paymentMethodMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicKeyFilePath",
                table: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "SFTP_Username",
                table: "PaymentMethod",
                newName: "MobileNo");

            migrationBuilder.RenameColumn(
                name: "SFTP_Port",
                table: "PaymentMethod",
                newName: "FocalPerson");

            migrationBuilder.RenameColumn(
                name: "SFTP_Password",
                table: "PaymentMethod",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "SFTP_IP",
                table: "PaymentMethod",
                newName: "Designation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNo",
                table: "PaymentMethod",
                newName: "SFTP_Username");

            migrationBuilder.RenameColumn(
                name: "FocalPerson",
                table: "PaymentMethod",
                newName: "SFTP_Port");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "PaymentMethod",
                newName: "SFTP_Password");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "PaymentMethod",
                newName: "SFTP_IP");

            migrationBuilder.AddColumn<string>(
                name: "PublicKeyFilePath",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
