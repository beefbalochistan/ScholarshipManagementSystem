using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("ApplicantStateChanger", Schema = "Student")]
    public class ApplicantStateChanger
    {
        [Key]
        public int ApplicantStateChangerId { get; set; }
        [ForeignKey("Applicant")]
        [Display(Name = "Applicant")]
        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantSelectionStatus")]
        [Display(Name = "Selection Status")]
        public int ApplicantSelectionStatusId { get; set; }
        public string CurrentState { get; set; }
        public string PreviousState { get; set; }
        public string Notes { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime OnDate { get; set; }
        public string UserName { get; set; }
        public virtual Applicant Applicant { get; set; }
        public virtual ApplicantSelectionStatus ApplicantSelectionStatus { get; set; }
    }
}
