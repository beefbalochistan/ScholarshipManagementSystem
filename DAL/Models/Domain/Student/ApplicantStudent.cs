using DAL.Models.Domain.MasterSetup;
using System;
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
        public string Comments { get; set; }
        [ForeignKey("ApplicantCurrentStatus")]
        public int ApplicantCurrentStatusId { get; set; }
        public string Attachment { get; set; }
        public string AttachFileName { get; set; }        
        public string AttachFileType { get; set; }
        [MaxLength]
        public byte[] AttachFileData { get; set; }
        public string UserName { get; set; }
        [ForeignKey("ApplicationUserFrom")]
        public string FromUserId { get; set; }
        [ForeignKey("ApplicationUserTo")]
        public string ToUserId { get; set; }
        public string ForwardToUserName { get; set; }
        public DateTime CreatedOn  { get; set; }        
        public virtual Applicant Applicant { get; set; }        
        public virtual ApplicantCurrentStatus ApplicantCurrentStatus { get; set; }        
        public virtual ApplicationUser ApplicationUserFrom { get; set; }        
        public virtual ApplicationUser ApplicationUserTo { get; set; }        
    }
}
