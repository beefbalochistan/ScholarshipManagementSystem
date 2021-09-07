using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("InstituteType", Schema = "master")]
    public class InstituteType
    {
        [Key]
        public int InstituteTypeId { get; set; }
        [Required]
        public string Name { get; set; }        
        public string Description { get; set; }        
    }
}
