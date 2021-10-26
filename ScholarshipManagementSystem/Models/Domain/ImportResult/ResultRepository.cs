using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ResultRepository", Schema = "ImportResult")]
    public class ResultRepository
    {
        [Key]
        public int ResultRepositoryId { get; set; }
        [Display(Name = "Provience")]
        public string resultFilePath { get; set; }
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }
        [Display(Name = "Scheme Level")]
        public int SchemeLevelPolicyId { get; set; }
        [Display(Name = "Uploaded Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        public bool IsSelctionCriteriaApplied { get; set; } = false;
        public bool IsDataCleaned { get; set; } = false;
        public bool IsMeritListGenerated { get; set; } = false;
        public int currentCounter { get; set; }

        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
    }
}
