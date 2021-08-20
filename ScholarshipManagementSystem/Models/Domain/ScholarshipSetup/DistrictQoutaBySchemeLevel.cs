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
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        public int Threshold { get; set; }
        public int CurrentYearPopulation { get; set; }
        [ForeignKey("ScholarshipFiscalYear")]
        [Display(Name = "ScholarshipFiscalYear")]
        public int ScholarshipFiscalYearId { get; set; }
        public float MPI { get; set; }

        public virtual District District { get; set; }
        public virtual ScholarshipFiscalYear ScholarshipFiscalYear { get; set; }
    }
}
