using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ResultUploadType", Schema = "master")]
    public class ResultUploadType
    {
        [Key]
        public int ResultUploadTypeId { get; set; }
       
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(3, ErrorMessage = "Code cannot exceeding from 3 digit")]
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; } 
    }
}
