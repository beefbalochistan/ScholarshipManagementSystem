
using DAL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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

            /*modelBuilder.Entity<DAL.Models.Domain.VirtualAccount.PaymentMethodMode>()
            .HasKey(c => new { c.PaymentDisbursementModeId, c.PaymentMethodId });*/
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
        public DbSet<DAL.Models.Domain.Student.Finance.ApplicantFinanceCurrentStatus> ApplicantFinanceCurrentStatus { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SchemeLevelMandatoryColumn> SchemeLevelMandatoryColumn { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.PaymentMethod> PaymentMethod { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.PaymentDisbursementMode> PaymentDisbursementMode { get; set; }
        public DbSet<DAL.Models.Domain.VirtualAccount.PaymentMethodMode> PaymentMethodMode { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.CompanyInfo> CompanyInfo { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.InstituteDepartment> InstituteDepartment { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.Scholarship> Scholarship { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Scheme> Scheme { get; set; }
        public DbSet<DAL.Models.Domain.VirtualAccount.Tranche> Tranche { get; set; }
        public DbSet<DAL.Models.Domain.VirtualAccount.TrancheDocument> TrancheDocument { get; set; }
        public DbSet<DAL.Models.Domain.VirtualAccount.PaymentDisbursement> PaymentDisbursement { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SchemeLevel> SchemeLevel { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SelectionCriteriaGeneral> SelectionCriteriaGeneral { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.DocumentAssistGeneral> DocumentAssistGeneral { get; set; }
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
        public DbSet<DAL.Models.Domain.MasterSetup.Gender> Gender { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Religion> Religion { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ColumnLabel> ColumnLabel { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ResultRepository> ResultRepository { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ResultContainer> ResultContainer { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ResultRepositoryTemp> ResultRepositoryTemp { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ResultContainerTemp> ResultContainerTemp { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ColumnLabelTemp> ColumnLabelTemp { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.ExcelColumnName> ExcelColumnName { get; set; }
        public DbSet<DAL.Models.Domain.ScholarshipSetup.SelectionCriteria> SelectionCriteria { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Operator> Operator { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.DocumentAssist> DocumentAssist { get; set; }
        public DbSet<DAL.Models.Domain.Student.Applicant> Applicant { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantSelectionStatus> ApplicantSelectionStatus { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantStateChanger> ApplicantStateChanger { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SelectionMethod> SelectionMethod { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.Employee> Employee { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantStudent> ApplicantStudent { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantCurrentStatus> ApplicantCurrentStatus { get; set; }
        public DbSet<DAL.Models.Domain.Student.ApplicantAttachment> ApplicantAttachment { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SectionComment> SectionComment { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.SeverityLevel> SeverityLevel { get; set; }
        public DbSet<DAL.Models.ViewModels.SPAssistDocumentViewer> SPAssistDocumentViewer { get; set; }
        public DbSet<DAL.Models.ViewModels.SPDocumentViewerReport> SPDocumentViewerReport { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.UserAccessToForward> userAccessToForward { get; set; }
        public DbSet<DAL.Models.Domain.MasterSetup.UserAccessToSchemeLevel> UserAccessToSchemeLevel { get; set; }
        public DbSet<DAL.Models.Domain.ImportResult.DocumentAssistIndicator> DocumentAssistIndicator { get; set; }
        [NotMapped]
        public DbSet<DAL.Models.ViewModels.ApplicantInProcess.SPApplicantInProcess> SPApplicantInProcess { get; set; }
        public DbSet<DAL.Models.ViewModels.VirtualAccount.SPApplicantPaymentInProcess> SPApplicantPaymentInProcess { get; set; }
        public DbSet<DAL.Models.ViewModels.ApplicantWaiting.SPApplicantWaiting> SPApplicantWaiting { get; set; }
        public DbSet<DAL.Models.ViewModels.ApplicantRejected.SPApplicantRejected> SPApplicantRejected { get; set; }
        public DbSet<DAL.Models.ViewModels.ApplicantInProcess.SPApplicantInProcessSummary> SPApplicantInProcessSummary { get; set; }
        public DbSet<DAL.Models.ViewModels.VirtualAccount.SPApplicantPaymentInProcessSummary> SPApplicantPaymentInProcessSummary { get; set; }
        public DbSet<DAL.Models.ViewModels.ApplicantWaiting.SPApplicantWaitingSummary> SPApplicantWaitingSummary { get; set; }
        public DbSet<DAL.Models.ViewModels.ApplicantRejected.SPApplicantRejectedSummary> SPApplicantRejectedSummary { get; set; }
        public DbSet<DAL.Models.ViewModels.UserManagement.GetUserSchemeLevelAccess> GetUserSchemeLevelAccess { get; set; }
    }
}