
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("SchemeLevel", Schema = "master")]
    public class SchemeLevel
    {
        [Key]
        public int SchemeLevelId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description1 { get; set; }
        public string Code { get; set; }
        [ForeignKey("Scheme")]
        [Display(Name = "Scheme")]
        public int SchemeId { get; set; }
        [ForeignKey("Institute")]
        [Display(Name = "Institute")]
        public int InstituteId { get; set; }
        [ForeignKey("QualificationLevel")]
        [Display(Name = "QualificationLevel")]
        public int QualificationLevelId { get; set; }
        public int OrderBy { get; set; }
        public bool IsActive { get; set; } = true;
        [Display(Name = "Total Marks/GPA")]
        public decimal TotalMarks_GPA { get; set; }
        public int GradingSystem { get; set; }
        public virtual Scheme Scheme { get; set; }
        public virtual ICollection<DegreeScholarshipLevel> DegreeScholarshipLevels { get; set; }
        public virtual QualificationLevel QualificationLevel { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
