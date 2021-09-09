using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("DegreeLevel", Schema = "master")]
    public class DegreeLevel
    {
        [Key]
        public int DegreeLevelId { get; set; }        
        public string Name { get; set; }
        public string Code { get; set; }
        public int Year { get; set; }

        [ForeignKey("Degree")]
        [Display(Name = "Degree")]
        public int DegreeId { get; set; }


        public virtual Degree Degree { get; set; }
    }
}
