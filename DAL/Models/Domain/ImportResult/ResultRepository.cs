using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ImportResult
{
    [Table("ResultRepository", Schema = "ImportResult")]
    public class ResultRepository
    {
        [Key]
        public int ResultRepositoryId { get; set; }
        [Display(Name = "Provience")]
        public string resultFilePath { get; set; }
        public string resultScannedFilePath { get; set; }
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }
        [Display(Name = "Scheme Level")]
        public int SchemeLevelPolicyId { get; set; }
        public int? DegreeScholarshipLevelId { get; set; }
        public int? DAEInstituteId { get; set; }
        [Display(Name = "Uploaded Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        public bool IsSelctionCriteriaApplied { get; set; } = false;
        public bool IsDataCleaned { get; set; } = false;
        public bool IsMeritListGenerated { get; set; } = false;
        public int currentCounter { get; set; }

        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
        public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }
        public virtual DAEInstitute DAEInstitute { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
    }
}
