using DAL.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("AnnualBudget", Schema = "master")]
    public class AnnualBudget
    {
        [Key]
        public int AnnualBudgetId { get; set; }
        [Display(Name = "Provincial Quota")]
        public decimal POMQuota { get; set; }
        [Display(Name = "Open Metrit Quota")]
        public decimal DOMSQuota { get; set; }
        [Display(Name = "Special Quota")]
        public decimal SpecialQuota { get; set; }
        [Display(Name = "Decline Quota")]
        public decimal DeclineQuota { get; set; }
        public string MeetingName { get; set; }
        public string MeetingReferancNo { get; set; }
        public string Description { get; set; }
        public DateTime OnDate { get; set; }
        public string UserId { get; set; }
        public string BudgetType { get; set; }

        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }
        [ForeignKey("BudgetLevel")]
        [Display(Name = "Budget Level")]
        public int BudgetLevelId { get; set; }
        public virtual BudgetLevel BudgetLevel { get; set; }
        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
    }
}
