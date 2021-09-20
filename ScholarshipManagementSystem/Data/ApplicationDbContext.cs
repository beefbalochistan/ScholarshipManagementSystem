using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;
using ScholarshipManagementSystem.Models;
using ScholarshipManagementSystem.Models.Domain.AutoSMSApi;
using ScholarshipManagementSystem.Models.ViewModels;

namespace ScholarshipManagementSystem.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Provience> Provience { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Division> Division { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.District> District { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.DistrictDetail> DistrictDetail { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.InstituteType> InstituteType { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Institute> Institute { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.QualificationLevel> QualificationLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Discipline> Discipline { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Degree> Degree { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.InstituteDepartment> InstituteDepartment { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.Scholarship> Scholarship { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Scheme> Scheme { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.SchemeLevel> SchemeLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.ScholarshipFiscalYear> ScholarshipFiscalYear { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.SchemeLevelPayment> SchemeLevelPayment { get; set; }        
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.DistrictQoutaBySchemeLevel> DistrictQoutaBySchemeLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.ViewModels.PolicyView> PolicyView { get; set; }        
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.SchemeLevelPolicy> SchemeLevelPolicy { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.PolicySRCForum> PolicySRCForum { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Preference> Preference { get; set; }        
        public DbSet<ScholarshipManagementSystem.Models.Domain.AutoSMSApi.SMSAPIService> SMSAPIService { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.AutoSMSApi.SMSAPIServiceAuditTrail> SMSAPIServiceAuditTrail { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.SMSMassage> SMSMassage { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.DAEInstitute> DAEInstitute { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.InstituteFaculty> InstituteFaculty { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.DAEInstituteQoutaBySchemeLevel> DAEInstituteQoutaBySchemeLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.Faculty> Faculty { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.DegreeLevel> DegreeLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.MasterSetup.DegreeScholarshipLevel> DegreeScholarshipLevel { get; set; }
        public DbSet<ScholarshipManagementSystem.Models.Domain.ScholarshipSetup.DegreeLevelQoutaBySchemeLevel> DegreeLevelQoutaBySchemeLevel { get; set; }

        public DbSet<ResultUploadType> ResultUploadType { get; set; }
        public DbSet<ResultUpload> ResultUpload { get; set; }
        public DbSet<GazResult> GazResult { get; set; } 
    }
}