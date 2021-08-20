using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagementSystem.Data.Migrations
{
    public partial class master_scholar_tbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "scholar");

            migrationBuilder.CreateTable(
                name: "Discipline",
                schema: "master",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "DistrictDetail",
                schema: "master",
                columns: table => new
                {
                    DistrictDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    MaleRatio = table.Column<float>(type: "real", nullable: false),
                    FemaleRatio = table.Column<float>(type: "real", nullable: false),
                    CensesYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowthRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictDetail", x => x.DistrictDetailId);
                    table.ForeignKey(
                        name: "FK_DistrictDetail_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "master",
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitudeType",
                schema: "master",
                columns: table => new
                {
                    InstitudeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitudeType", x => x.InstitudeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "QualificationLevel",
                schema: "master",
                columns: table => new
                {
                    QualificationLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationLevel", x => x.QualificationLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Scholarship",
                schema: "scholar",
                columns: table => new
                {
                    ScholarshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholarship", x => x.ScholarshipId);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipFiscalYear",
                schema: "scholar",
                columns: table => new
                {
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipFiscalYear", x => x.ScholarshipFiscalYearId);
                });

            migrationBuilder.CreateTable(
                name: "Institude",
                schema: "master",
                columns: table => new
                {
                    InstitudeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitudeTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<int>(type: "int", nullable: false),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    FaxNo = table.Column<int>(type: "int", nullable: false),
                    ProvienceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<int>(type: "int", nullable: false),
                    FocalPersonName = table.Column<int>(type: "int", nullable: false),
                    FocalPersonEmail = table.Column<int>(type: "int", nullable: false),
                    FocalPersonPhoneNo = table.Column<int>(type: "int", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institude", x => x.InstitudeId);
                    table.ForeignKey(
                        name: "FK_Institude_InstitudeType_InstitudeTypeId",
                        column: x => x.InstitudeTypeId,
                        principalSchema: "master",
                        principalTable: "InstitudeType",
                        principalColumn: "InstitudeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                schema: "master",
                columns: table => new
                {
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ResultSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.DegreeId);
                    table.ForeignKey(
                        name: "FK_Degree_QualificationLevel_QualificationLevelId",
                        column: x => x.QualificationLevelId,
                        principalSchema: "master",
                        principalTable: "QualificationLevel",
                        principalColumn: "QualificationLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scheme",
                schema: "master",
                columns: table => new
                {
                    SchemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarshipId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheme", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_Scheme_Scholarship_ScholarshipId",
                        column: x => x.ScholarshipId,
                        principalSchema: "scholar",
                        principalTable: "Scholarship",
                        principalColumn: "ScholarshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistrictQoutaBySchemeLevel",
                schema: "scholar",
                columns: table => new
                {
                    DistrictQoutaBySchemeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    CurrentYearPopulation = table.Column<int>(type: "int", nullable: false),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    MPI = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictQoutaBySchemeLevel", x => x.DistrictQoutaBySchemeLevelId);
                    table.ForeignKey(
                        name: "FK_DistrictQoutaBySchemeLevel_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "master",
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictQoutaBySchemeLevel_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitudeDepartment",
                schema: "master",
                columns: table => new
                {
                    InstitudeDepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitudeId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitudeDepartment", x => x.InstitudeDepartmentId);
                    table.ForeignKey(
                        name: "FK_InstitudeDepartment_Discipline_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "master",
                        principalTable: "Discipline",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstitudeDepartment_Institude_InstitudeId",
                        column: x => x.InstitudeId,
                        principalSchema: "master",
                        principalTable: "Institude",
                        principalColumn: "InstitudeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeLevel",
                schema: "master",
                columns: table => new
                {
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeId = table.Column<int>(type: "int", nullable: false),
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevel", x => x.SchemeLevelId);
                    table.ForeignKey(
                        name: "FK_SchemeLevel_Degree_DegreeId",
                        column: x => x.DegreeId,
                        principalSchema: "master",
                        principalTable: "Degree",
                        principalColumn: "DegreeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemeLevel_Scheme_SchemeId",
                        column: x => x.SchemeId,
                        principalSchema: "master",
                        principalTable: "Scheme",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeLevelPayment",
                schema: "scholar",
                columns: table => new
                {
                    SchemeLevelPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemeLevelId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipFiscalYearId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeLevelPayment", x => x.SchemeLevelPaymentId);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPayment_SchemeLevel_SchemeLevelId",
                        column: x => x.SchemeLevelId,
                        principalSchema: "master",
                        principalTable: "SchemeLevel",
                        principalColumn: "SchemeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemeLevelPayment_ScholarshipFiscalYear_ScholarshipFiscalYearId",
                        column: x => x.ScholarshipFiscalYearId,
                        principalSchema: "scholar",
                        principalTable: "ScholarshipFiscalYear",
                        principalColumn: "ScholarshipFiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Degree_QualificationLevelId",
                schema: "master",
                table: "Degree",
                column: "QualificationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictDetail_DistrictId",
                schema: "master",
                table: "DistrictDetail",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictQoutaBySchemeLevel_DistrictId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictQoutaBySchemeLevel_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "DistrictQoutaBySchemeLevel",
                column: "ScholarshipFiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Institude_InstitudeTypeId",
                schema: "master",
                table: "Institude",
                column: "InstitudeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitudeDepartment_DisciplineId",
                schema: "master",
                table: "InstitudeDepartment",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitudeDepartment_InstitudeId",
                schema: "master",
                table: "InstitudeDepartment",
                column: "InstitudeId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheme_ScholarshipId",
                schema: "master",
                table: "Scheme",
                column: "ScholarshipId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevel_DegreeId",
                schema: "master",
                table: "SchemeLevel",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevel_SchemeId",
                schema: "master",
                table: "SchemeLevel",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPayment_SchemeLevelId",
                schema: "scholar",
                table: "SchemeLevelPayment",
                column: "SchemeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeLevelPayment_ScholarshipFiscalYearId",
                schema: "scholar",
                table: "SchemeLevelPayment",
                column: "ScholarshipFiscalYearId");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistrictDetail",
                schema: "master");

            migrationBuilder.DropTable(
                name: "DistrictQoutaBySchemeLevel",
                schema: "scholar");

            migrationBuilder.DropTable(
                name: "InstitudeDepartment",
                schema: "master");

            migrationBuilder.DropTable(
                name: "SchemeLevelPayment",
                schema: "scholar");            

            migrationBuilder.DropTable(
                name: "Discipline",
                schema: "master");

            migrationBuilder.DropTable(
                name: "Institude",
                schema: "master");

            migrationBuilder.DropTable(
                name: "ScholarshipFiscalYear",
                schema: "scholar");

            migrationBuilder.DropTable(
                name: "SchemeLevel",
                schema: "master");

            migrationBuilder.DropTable(
                name: "InstitudeType",
                schema: "master");

            migrationBuilder.DropTable(
                name: "Degree",
                schema: "master");

            migrationBuilder.DropTable(
                name: "Scheme",
                schema: "master");

            migrationBuilder.DropTable(
                name: "QualificationLevel",
                schema: "master");

            migrationBuilder.DropTable(
                name: "Scholarship",
                schema: "scholar");
        }
    }
}
