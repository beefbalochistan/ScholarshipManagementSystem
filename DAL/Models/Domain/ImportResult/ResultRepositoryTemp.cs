using DAL.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ImportResult
{
    [Table("ResultRepositoryTemp", Schema = "ImportResult")]
    public class ResultRepositoryTemp
    {
        [Key]
        public int ResultRepositoryTempId { get; set; }
        [Display(Name = "Provience")]
        public string resultFilePath { get; set; }
        public string resultScannedFilePath { get; set; }
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }
        [Display(Name = "Scheme Level")]
        public int SchemeLevelPolicyId { get; set; }
        public int DegreeScholarshipLevelId { get; set; }
        [Display(Name = "Uploaded Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }     

        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
    }
}
