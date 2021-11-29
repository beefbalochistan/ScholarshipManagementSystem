using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Data.Migrations
{
    public partial class tbl_app_stu_bytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AttachFileData",
                schema: "Student",
                table: "ApplicantStudent",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachFileName",
                schema: "Student",
                table: "ApplicantStudent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachFileType",
                schema: "Student",
                table: "ApplicantStudent",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachFileData",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropColumn(
                name: "AttachFileName",
                schema: "Student",
                table: "ApplicantStudent");

            migrationBuilder.DropColumn(
                name: "AttachFileType",
                schema: "Student",
                table: "ApplicantStudent");
        }
    }
}
