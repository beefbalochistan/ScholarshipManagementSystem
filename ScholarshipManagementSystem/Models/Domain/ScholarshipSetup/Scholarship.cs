using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("Scholarship", Schema = "scholar")]
    public class Scholarship
    {
        [Key]
        public int ScholarshipId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public string Code { get; set; }
    }
}
