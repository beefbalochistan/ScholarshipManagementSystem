using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("PolicySRCForum", Schema = "scholar")]
    public class PolicySRCForum
    {
        [Key]
        public int PolicySRCForumId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsEndorse { get; set; }
        public string SRCMinutesAttachmentPath { get; set; }
        public string PolicyDocumentAttachmentPath { get; set; }
        public string OtherAttachment { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Scholarship Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }       
        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
    }
}
