using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("ApplicantStudent", Schema = "Student")]
    public class ApplicantStudent
    {
        [Key]
        public int ApplicantStudentId { get; set; }
        [ForeignKey("Applicant")]
        [Display(Name = "Applicant")]
        public int ApplicantId { get; set; }
        public string SelectionStatus { get; set; }
        public string Comments { get; set; }
        public string Attachment { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
