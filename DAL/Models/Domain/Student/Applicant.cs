using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.ScholarshipSetup;
using DAL.Models.Domain.Student.Finance;
using DAL.Models.Domain.VirtualAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("Applicant", Schema = "Student")]
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Father Name")]        
        public string FatherName { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "B-Form/CNIC")]
        public string BFormCNIC { get; set; }
        [Display(Name = "Father/CareTaker CNIC")]
        public string FatherCareTakerCNIC { get; set; }
        [Display(Name = "Disbursement CNIC")]
        public string DisbursementCNIC { get; set; }
        [Display(Name = "Student Mobile#1")]
        public string StudentMobile { get; set; }
        [Display(Name = "Student Mobile#2")]
        public string StudentMobile2 { get; set; }
        [Display(Name = "Father Mobile")]
        public string FatherMobile { get; set; }
        [Display(Name = "Relation With Care Taker")]
        public string RelationWithCareTaker { get; set; }
        public string Religion { get; set; }
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }
        [Required]
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [ForeignKey("Provience")]
        [Display(Name = "Provience")]
        [Required]
        public int ProvienceId { get; set; }
        [ForeignKey("SchemeLevelPolicy")]
        [Required]
        [Display(Name = "Scheme Level Policy")]
        public int SchemeLevelPolicyId { get; set; }
        [ForeignKey("DegreeScholarshipLevel")]
        [Display(Name = "DegreeScholarshipLevel")]
        public int? DegreeScholarshipLevelId { get; set; }
        [ForeignKey("DAEInstitute")]
        [Display(Name = "DAE Institute")]
        public int? DAEInstituteId { get; set; }
        [Display(Name = "Reference#")]
        [Required]
        public string ApplicantReferenceNo { get; set; }
        [Display(Name = "Tehsil Name")]
        public string TehsilName { get; set; }        
        public string Gender { get; set; }        
        public string Email { get; set; }        
        public string Year { get; set; }
        [Display(Name = "Insitute Name")]
        public string CurrentInsituteName { get; set; }
        [Display(Name = "Insitute HOD")]
        public string CurrentInsituteHOD { get; set; }
        [Display(Name = "Focal Person Name")]
        public string CurrentInsituteFocalPerson { get; set; }
        [Display(Name = "Focal Person Designation")]
        public string CurrentInsituteFocalDesignation { get; set; }
        [Display(Name = "Focal Person Mobile")]
        public string CurrentInsituteFocalMobile { get; set; }
        [Display(Name = "Focal Person Email")]
        public string CurrentInsituteFocalEmail { get; set; }
        [Display(Name = "Insitute Phone")]
        public string CurrentInsitutePhone { get; set; }
        [Display(Name = "Insitute Fax")]
        public string CurrentInsituteFax { get; set; }
        [Display(Name = "Insitute Address")]
        public string CurrentInsituteAddress { get; set; }
        [Display(Name = "Roll No")]
        public string RollNumber { get; set; }
        [Display(Name = "Registeration#")]
        public string RegisterationNumber { get; set; }
        [Display(Name = "Total Marks")]
        public decimal TotalMarks { get; set; }        
        [Display(Name = "Total GPA")]
        public decimal TotalGPA { get; set; }
        [Display(Name = "Received Marks")]
        public decimal ReceivedMarks { get; set; }
        [Display(Name = "Received CGPA")]
        public decimal ReceivedCGPA { get; set; }
        [Display(Name = "Old Institude Name With Address")]
        public string OldInstitudeNameAddress { get; set; }
        [Display(Name = "Board/University")]
        public string NameBoardUniversity { get; set; }
        [Display(Name = "Telephone")]
        public string TelephoneWithCode { get; set; }        
        public byte[] Picture { get; set; }
        [Display(Name = "Scan Document")]
        public string ScanDocument { get; set; }
        [Display(Name = "Scanned Other Document")]
        public string ScanOtherDocument { get; set; }
        [Display(Name = "Selection Status")]
        public string SelectionStatus { get; set; }
        [ForeignKey("ApplicantSelectionStatus")]
        public int ApplicantSelectionStatusId { get; set; }
        [Display(Name = "Selected Method")]
        public int SelectionMethodId { get; set; }
        [Display(Name = "Entry Through")]
        public string EntryThrough { get; set; }
        public bool Attach_Picture { get; set; } = false;        
        public bool Attach_DMC_Transcript { get; set; } = false;        
        public bool Attach_CNIC_BForm { get; set; } = false;        
        public bool Attach_Father_Mother_Guardian_CNIC { get; set; } = false;        
        public bool Attach_Father_Death_Certificate { get; set; } = false;                     
        public bool Attach_Payslip { get; set; } = false;        
        public bool IsFormSubmitted { get; set; } = false;        
        public bool IsFormEntered { get; set; } = false;        
        public DateTime? FormSubmittedOnDate { get; set; }     
        public bool Attach_Affidavit { get; set; } = false;        
        public bool Attach_Minority_Certificate { get; set; } = false;
        [ForeignKey("ApplicantCurrentStatus")]
        [Display(Name = "ApplicantCurrentStatus")]
        public int ApplicantCurrentStatusId { get; set; }
        [ForeignKey("ApplicantInbox")]
        public int ApplicantInboxId { get; set; } = 1;
        [ForeignKey("ApplicantFinanceCurrentStatus")]
        [Display(Name = "Finance Current Status")]
        public int? ApplicantFinanceCurrentStatusId { get; set; }
        [ForeignKey("Tranche")]        
        public int? TrancheId { get; set; }
        public int? TrancheDocumentId { get; set; }
        public bool IsPaymentInProcess { get; set; } = false;
        public bool IsDisbursed { get; set; } = false;
        public virtual District District { get; set; }
        public virtual SchemeLevelPolicy SchemeLevelPolicy { get; set; }
        public virtual Provience Provience { get; set; }
        public virtual SelectionMethod SelectionMethod { get; set; }
        public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }
        public virtual DAEInstitute DAEInstitute { get; set; }
        public virtual ApplicantCurrentStatus ApplicantCurrentStatus { get; set; }
        public virtual ApplicantSelectionStatus ApplicantSelectionStatus { get; set; }
        public virtual ApplicantFinanceCurrentStatus ApplicantFinanceCurrentStatus { get; set; }
        public virtual Tranche Tranche { get; set; }
        public virtual ApplicantInbox ApplicantInbox { get; set; }
    }
}
