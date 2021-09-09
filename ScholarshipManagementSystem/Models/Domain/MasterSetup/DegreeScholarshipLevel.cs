using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("DegreeScholarshipLevel", Schema = "master")]
    public class DegreeScholarshipLevel
    {
        [Key]
        public int DegreeScholarshipLevelId { get; set; }
        public string Name { get; set; }
        [ForeignKey("SchemeLevel")]
        [Display(Name = "SchemeLevel")]
        public int SchemeLevelId { get; set; }
        public bool IsActive { get; set; } = true;
        public int Enrollment { get; set; }
        public float Slot { get; set; }
        [ForeignKey("DegreeLevel")]
        [Display(Name = "DegreeLevel")]
        public int DegreeLevelId { get; set; }

        public virtual SchemeLevel SchemeLevel { get; set; }
        public virtual DegreeLevel DegreeLevel { get; set; }
    }
}
