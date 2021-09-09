using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("DegreeLevelQoutaBySchemeLevel", Schema = "scholar")]
    public class DegreeLevelQoutaBySchemeLevel
    {
        [Key]
        public int DegreeLevelQoutaBySchemeLevelId { get; set; }
        [ForeignKey("DegreeScholarshipLevel")]
        [Display(Name = "DegreeScholarshipLevel")]
        public int DegreeScholarshipLevelId { get; set; }
        public int ClassEnrollment { get; set; }        
        public float SlotAllocate { get; set; }
        public float AdditionalSlotAllocate { get; set; }
        public int StipendAmount { get; set; }
        public float Threshold { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Policy SRC Forum")]
        public int PolicySRCForumId { get; set; }
        [ForeignKey("SchemeLevelPolicy")]
        [Display(Name = "Scheme Level Policy")]
        public int SchemeLevelPolicyId { get; set; }                
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
        public virtual PolicySRCForum SRCForum { get; set; }
        public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }
    }
}
