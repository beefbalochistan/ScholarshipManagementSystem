using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ScholarshipSetup
{
    public class DAEInstituteQoutaBySchemeLevel
    {
        [Key]
        public int DAEInstituteQoutaBySchemeLevelId { get; set; }
        [ForeignKey("DAEInstitute")]
        [Display(Name = "DAEInstitute")]
        public int DAEInstituteId { get; set; }
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
}
