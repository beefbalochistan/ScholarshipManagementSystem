using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.ViewModels
{   
    public class PolicyView
    {
        public int SchemeId { get; set; }
        public string Scheme { get; set; }
        [Key]
        public int SchemeLevelId { get; set; }
        public string SchemeLevel { get; set; }
        public string SchemeLevelCode { get; set; }
        public int QualificationLevelId { get; set; }
        public string QualificationLevel { get; set; }
        public int OrderBy { get; set; }
        public int SchemeLevelPolicyId { get; set; }
        [Display(Name = "Annual Stipend")]
        public int Amount { get; set; }
        public float DOMS { get; set; }
        public float POMS { get; set; }
        [Display(Name = "Total Slots")]
        public float ScholarshipSlot { get; set; }
        public float SQSOMS { get; set; }
        public float SQSEVIs { get; set; }
        public int PolicySRCForumId { get; set; }        
        public string PolicyForum { get; set; }
        public string PolicyYear { get; set; }
        public double DistrictAdditionalSlot { get; set; }
        public int InstituteId { get; set; }
        public int Year { get; set; }
        public double DAEAdditionalSlot { get; set; }
        public double DegreeAdditionalSlot { get; set; }
        public ICollection<PolicyDetailView> DistrictPolicyDetailViewList { get;set;}
        public ICollection<DAEPolicyDetailView> DAEPolicyDetailViewList { get;set;}
        [NotMapped]
        public List<DegreeSecondLevel> DegreeSecondLevelList { get;set;}
    }

    public class PolicyDetailView
    {
        [Key]
        public int DistrictQoutaBySchemeLevelId { get; set; }
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }        
        public float Threshold { get; set; }
        [Display(Name = "Population")]
        public int CurrentYearPopulation { get; set; }
        public int PolicySRSForumId { get; set; }
        public float MPI { get; set; }
        [Display(Name = "MPI Slot")]
        public float DistrictMPISlot { get; set; }
        [Display(Name = "Population Slot")]
        public float DistrictPopulationSlot { get; set; }
        [Display(Name = "Additional Slot")]
        public float DistrictAdditionalSlot { get; set; }
        public int PolicySRCForumId { get; set; }
        public int SchemeLevelPolicyId { get; set; }        
        public int StipendAmount { get; set; }
        [Display(Name = "MPI Difference")]
        public float MPIDifferenceFromStatndard { get; set; }                  
        public virtual District District { get; set; }        
    }

    public class DAEPolicyDetailView
    {
        [Key]
        public int DAEInstituteQoutaBySchemeLevelId { get; set; }
        [ForeignKey("DAEInstitute")]
        [Display(Name = "DAEInstitute")]
        public int DAEInstituteId { get; set; }
        public string DAEInstituteName { get; set; }
        public int ClassEnrollment { get; set; }
        public string Year { get; set; }
        public float SlotAllocate { get; set; }
        public int StipendAmount { get; set; }
        public float Threshold { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Policy SRC Forum")]
        public int PolicySRCForumId { get; set; }
        [ForeignKey("SchemeLevelPolicy")]
        [Display(Name = "Scheme Level Policy")]
        public int SchemeLevelPolicyId { get; set; }
        public float InstituteAdditionalSlot { get; set; }
        public virtual DAEInstitute DAEInstitute { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
        public virtual PolicySRCForum SRCForum { get; set; }
    }

    public partial class DegreeSecondLevel
    {        
        public int SchemeLevelId { get; set; }
        public int SchemeId { get; set; }
        public int InstituteId { get; set; }
        public int CurrentYear { get; set; }
        public string Scheme { get; set; }
        public string SchemeLevel { get; set; }
        public int DegreeLevel { get; set; }
        [NotMapped]
        public List<DegreeThirdLevel> DegreeThirdLevelList { get; set; }
    }

    public partial class DegreeThirdLevel
    {

        public int DegreeLevelQoutaBySchemeLevelId { get; set; }
        public int DegreeScholarshipLevelId { get; set; }
        public string DegreeScholarshipLevel { get; set; }
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public int SchemeId { get; set; }
        public int CurrentYear { get; set; }
        public int ClassEnrollment { get; set; }
        public float SlotAllocate { get; set; }
        public float Threshold { get; set; }
        public int StipendAmount { get; set; }
        public float AdditionalSlotAllocate { get; set; }
    }
}
