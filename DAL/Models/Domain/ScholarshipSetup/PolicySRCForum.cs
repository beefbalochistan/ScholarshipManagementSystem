using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ScholarshipSetup
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
        [Display(Name = "Is Endorse")]
        public bool IsEndorse { get; set; }
        public bool IsFreez { get; set; } = false;
        [Display(Name = "SRC Minutes")]
        public string SRCMinutesAttachmentPath { get; set; }
        [Display(Name = "Policy Document")]
        public string PolicyDocumentAttachmentPath { get; set; }
        [Display(Name = "Other Attachment")]
        public string OtherAttachment { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }       
        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
    }
}
