using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class paymentMethod_filezila : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicKeyFilePath",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SFTP_IP",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SFTP_Password",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SFTP_Port",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SFTP_Username",
                table: "PaymentMethod",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicKeyFilePath",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "SFTP_IP",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "SFTP_Password",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "SFTP_Port",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "SFTP_Username",
                table: "PaymentMethod");
        }
    }
}
