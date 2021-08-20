using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("InstitudeType", Schema = "master")]
    public class InstitudeType
    {
        [Key]
        public int InstitudeTypeId { get; set; }
        [Required]
        public string Name { get; set; }        
        public string Description { get; set; }        
    }
}
