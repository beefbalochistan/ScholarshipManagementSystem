using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("Degree", Schema = "master")]
    public class Degree
    {
        [Key]
        public int DegreeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(12, ErrorMessage = "Code cannot exceeding from 12 digit")]
        public string Code { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Result System")]
        public string ResultSystem { get; set; }
        [ForeignKey("QualificationLevel")]
        [Display(Name = "Qualification Level")]
        public int QualificationLevelId { get; set; }
        [ForeignKey("Discipline")]
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }
        public virtual QualificationLevel QualificationLevel { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
