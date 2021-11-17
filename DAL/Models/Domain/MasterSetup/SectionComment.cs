using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("SectionComment", Schema = "master")]
    public class SectionComment
    {
        [Key]
        public int SectionCommentId { get; set; }
        [Required]        
        public string Comment { get; set; }                      
        [ForeignKey("BEEFSection")]
        [Display(Name = "Section")]
        public int BEEFSectionId { get; set; }
        [ForeignKey("SeverityLevel")]
        [Display(Name = "Severity Level")]
        public int SeverityLevelId { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual BEEFSection BEEFSection { get; set; }
        public virtual SeverityLevel SeverityLevel { get; set; }
    }
}
