using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("ApplicantAttachment", Schema = "Student")]
    public class ApplicantAttachment
    {
        [Key]
        public int ApplicantAttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime UploadedOn { get; set; }
        [ForeignKey("Applicant")]
        [Display(Name = "Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
