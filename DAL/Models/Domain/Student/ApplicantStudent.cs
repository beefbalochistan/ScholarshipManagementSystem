using DAL.Models.Domain.MasterSetup;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string ApplicantReferenceId { get; set; }
        public int SeniorityLevel { get; set; }
        public string Comments { get; set; }
        public string Attachment { get; set; }
        [ForeignKey("Employee")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Applicant Applicant { get; set; }        
    }
}
