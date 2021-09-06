using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("DistrictQoutaBySchemeLevel", Schema = "scholar")]
    public class DistrictQoutaBySchemeLevel
    {
        [Key]
        public int DistrictQoutaBySchemeLevelId { get; set; }
        [Required]
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Required]
        [ForeignKey("SchemeLevelPolicy")]
        [Display(Name = "Scheme Level Policy")]
        public int SchemeLevelPolicyId { get; set; }
        public float Threshold { get; set; }
        public int StipendAmount { get; set; }
        public int CurrentYearPopulation { get; set; }
        [Required]
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "Policy SRC Forum")]
        public int PolicySRCForumId { get; set; }
        [ForeignKey("MPI Score")]
        public float MPI { get; set; }
        public float MPIDifferenceFromStatndard { get; set; }
        public float DistrictPopulationSlot { get; set; }
        public float DistrictMPISlot { get; set; }
        public float DistrictAdditionalSlot { get; set; }
        public virtual District District { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
        public virtual PolicySRCForum SRCForum { get; set; }
    }
}
