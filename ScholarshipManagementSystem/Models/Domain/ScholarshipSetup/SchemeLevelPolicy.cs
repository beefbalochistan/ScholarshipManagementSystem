using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("SchemeLevelPolicy", Schema = "scholar")]
    public class SchemeLevelPolicy
    {
        [Key]
        public int SchemeLevelPolicyId { get; set; }
        [ForeignKey("SchemeLevel")]
        [Display(Name = "SchemeLevel")]
        public int SchemeLevelId { get; set; }
        [ForeignKey("PolicySRCForum")]
        [Display(Name = "PolicySRCForum")]
        public int PolicySRCForumId { get; set; }
        [Display(Name = "Stipend")]
        public int Amount { get; set; }
        public float ScholarshipSlot { get; set; }
        [Display(Name = "POMS/IOMS")]
        public float POMS { get; set; }
        [Display(Name = "DOMS")]
        public float DOMS { get; set; }
        [Display(Name = "SQS-OMS")]
        public float SQSOMS { get; set; }
        [Display(Name = "SQS-EVIs")]
        public float SQSEVIs { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual PolicySRCForum PolicySRCForum { get; set; }
        public virtual SchemeLevel SchemeLevel { get; set; }
    }
}
