using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("DistrictDetail", Schema = "master")]
    public class DistrictDetail
    {
        [Key]
        public int DistrictDetailId { get; set; }
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Required]
        public Int64 Population { get; set; }  
        public float MaleRatio { get; set; }
        public float FemaleRatio { get; set; }
        [Required]
        public string CensesYear { get; set; }
        public float GrowthRate { get; set; }

        public virtual District District { get; set; }
    }
}
