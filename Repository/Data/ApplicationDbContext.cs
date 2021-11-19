﻿
using Microsoft.EntityFrameworkCore;
using DAL.Models.ViewModels;

namespace Repository.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DegreeSecondLevel>().HasNoKey();
            modelBuilder.Entity<DegreeThirdLevel>().HasNoKey();
        }
        public DbSet<DAL.Models.Domain.MasterSetup.Provience> Provience { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Division> Division { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.District> District { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.DistrictDetail> DistrictDetail { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.InstituteType> InstituteType { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Institute> Institute { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.QualificationLevel> QualificationLevel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Discipline> Discipline { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Degree> Degree { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.InstituteDepartment> InstituteDepartment { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.Scholarship> Scholarship { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Scheme> Scheme { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SchemeLevel> SchemeLevel { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.ScholarshipFiscalYear> ScholarshipFiscalYear { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.SchemeLevelPayment> SchemeLevelPayment { get; set; }        
        public DbSet<DAL.Models.Domain.ScholarshipSetup.DistrictQoutaBySchemeLevel> DistrictQoutaBySchemeLevel { get; set; }
        public DbSet<DAL.Models.ViewModels.PolicyView> PolicyView { get; set; }        
        public virtual DbSet<DAL.Models.ViewModels.DegreeSecondLevel> DegreeSecondLevel { get; set; }        
        public virtual DbSet<DAL.Models.ViewModels.DegreeThirdLevel> DegreeThirdLevel { get; set; }        
        public DbSet<DAL.Models.Domain.ScholarshipSetup.SchemeLevelPolicy> SchemeLevelPolicy { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.PolicySRCForum> PolicySRCForum { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Preference> Preference { get; set; }        
        public DbSet<SMSService.Models.Domain.AutoSMSApi.SMSAPIService> SMSAPIService { get; set; }
        public DbSet<SMSService.Models.Domain.AutoSMSApi.SMSAPIServiceAuditTrail> SMSAPIServiceAuditTrail { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SMSMassage> SMSMassage { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.DAEInstitute> DAEInstitute { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.InstituteFaculty> InstituteFaculty { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.DAEInstituteQoutaBySchemeLevel> DAEInstituteQoutaBySchemeLevel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Faculty> Faculty { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.DegreeLevel> DegreeLevel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.DegreeScholarshipLevel> DegreeScholarshipLevel { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.DegreeLevelQoutaBySchemeLevel> DegreeLevelQoutaBySchemeLevel { get; set; }
           
        public DbSet<DAL.Models.Domain.MasterSetup.BEEFSection> BEEFSection { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.ColumnLabel> ColumnLabel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.ResultRepository> ResultRepository { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.ResultContainer> ResultContainer { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.ExcelColumnName> ExcelColumnName { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.SelectionCriteria> SelectionCriteria { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Operator> Operator { get; set; }
        public DbSet<DAL.Models.Domain.Student.Applicant> Applicant { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SelectionMethod> SelectionMethod { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Employee> Employee { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantStudent> ApplicantStudent { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantCurrentStatus> ApplicantCurrentStatus { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantAttachment> ApplicantAttachment { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SectionComment> SectionComment { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SeverityLevel> SeverityLevel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.UserAccessToForward> userAccessToForward { get; set; }
    }
}