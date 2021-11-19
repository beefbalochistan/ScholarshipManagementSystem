using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_sms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sms");

            migrationBuilder.CreateTable(
                name: "SMSAPIService",
                schema: "sms",
                columns: table => new
                {
                    SMSAPIServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mask = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSAPIService", x => x.SMSAPIServiceId);
                });

            migrationBuilder.CreateTable(
                name: "SMSAPIServiceAuditTrail",
                schema: "sms",
                columns: table => new
                {
                    SMSAPIServiceAuditTrailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextLength = table.Column<int>(type: "int", nullable: false),
                    DestinationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSAPIServiceAuditTrail", x => x.SMSAPIServiceAuditTrailId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSAPIService",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "SMSAPIServiceAuditTrail",
                schema: "sms");
        }
    }
}
