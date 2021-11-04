using DAL.Models.Domain.MasterSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Domain.MasterSetup
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
        public int Population { get; set; }
        [Display(Name = "Male Ratio")]
        public float MaleRatio { get; set; }
        [Display(Name = "Female Ratio")]
        public float FemaleRatio { get; set; }
        [Display(Name = "Censes Year")]
        [Required]
        public string CensesYear { get; set; }
        [Display(Name = "Growth Rate")]
        public float GrowthRate { get; set; }
        [Display(Name = "MPI Score")]
        public float MPIScore { get; set; }
        public float MPIDifferenceFromStatndard { get; set; }

        public virtual District District { get; set; }
    }
}
