using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("SchemeLevelPayment", Schema = "scholar")]
    public class SchemeLevelPayment
    {
        [Key]
        public int SchemeLevelPaymentId { get; set; }           
        public string Name { get; set; }           
        [ForeignKey("SchemeLevel")]
        [Display(Name = "SchemeLevel")]
        public int SchemeLevelId { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "ScholarshipFiscalYear")]
        public int ScholarshipFiscalYearId { get; set; }
        [Display(Name = "Stipend")]
        public int Amount { get; set; }
        public int ScholarshipQouta { get; set; }
        [Display(Name = "POMS/IOMS")]
        public int POMS { get; set; }
        [Display(Name = "DOMS")]
        public int DOMS { get; set; }
        [Display(Name = "SQS-OMS")]
        public int SQSOMS { get; set; }
        [Display(Name = "SQS-EVIs")]
        public int SQSEVIs { get; set; }
        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
        public virtual SchemeLevel SchemeLevel { get; set; }
    }
}
