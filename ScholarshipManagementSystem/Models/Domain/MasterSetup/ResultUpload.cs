using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ResultUpload", Schema = "master")]
    public class ResultUpload
    {
        [Key]
        public int FilesUploadId { get; set; }
        [ForeignKey("ResultUploadType")]
        [Display(Name = "Result Upload Type")]
        public int ResultUploadTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] VFile { get; set; }  
        public virtual ResultUploadType ResultUploadType { get; set; } 
    }
}
