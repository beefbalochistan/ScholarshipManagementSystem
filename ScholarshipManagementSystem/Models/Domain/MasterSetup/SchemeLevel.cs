
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
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
        [ForeignKey("InstitudeDepartment")]
        [Display(Name = "Institude Department")]
        public int InstitudeDepartmentId { get; set; }
        [ForeignKey("Degree")]
        [Display(Name = "Degree")]
        public int DegreeId { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Scheme Scheme { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual InstitudeDepartment InstitudeDepartment { get; set; }
    }
}
