using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    public partial class applicant_user_IsSectionHead_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetLevelId",
                schema: "master",
                table: "SchemeLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSectionHead",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BudgetLevel",
                schema: "master",
                columns: table => new
                {
                    BudgetLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnualStipend = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLevel", x => x.BudgetLevelId);
                    table.ForeignKey(
                        name: "FK_BudgetLevel_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialQuotaCategory",
                schema: "master",
                columns: table => new
                {
                    SpecialQuotaCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentageValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialQuotaCategory", x => x.SpecialQuotaCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AnnualBudget",
                schema: "master",
                columns: table => new
                {
                    AnnualBudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POMQuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DOMSQuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialQuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeclineQuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeetingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingReferancNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    BudgetLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualBudget", x => x.AnnualBudgetId);
                    table.ForeignKey(
                        name: "FK_AnnualBudget_BudgetLevel_BudgetLevelId",
                        column: x => x.BudgetLevelId,
                        principalSchema: "master",
                        principalTable: "BudgetLevel",
                        principalColumn: "BudgetLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualBudget_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualBudget_BudgetLevelId",
                schema: "master",
                table: "AnnualBudget",
                column: "BudgetLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualBudget_ScholarshipFiscalYearId",
                schema: "master",
                table: "AnnualBudget",
                column: "ScholarshipFiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLevel_PaymentMethodId",
                schema: "master",
                table: "BudgetLevel",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualBudget",
                schema: "master");

            migrationBuilder.DropTable(
                name: "SpecialQuotaCategory",
                schema: "master");

            migrationBuilder.DropTable(
                name: "BudgetLevel",
                schema: "master");

            migrationBuilder.DropColumn(
                name: "BudgetLevelId",
                schema: "master",
                table: "SchemeLevel");

            migrationBuilder.DropColumn(
                name: "IsSectionHead",
                table: "AspNetUsers");
        }
    }
}
