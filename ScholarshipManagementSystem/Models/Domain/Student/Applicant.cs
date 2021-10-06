using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.Student
{
    [Table("Applicant", Schema = "Student")]
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }       
        public string Name { get; set; }        
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BFormCNIC { get; set; }
        public string FatherCareTakerCNIC { get; set; }
        public string StudentMobile { get; set; }
        public string FatherMobile { get; set; }
        public string RelationWithCareTaker { get; set; }
        public string Religion { get; set; }
        public string HomeAddress { get; set; }
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [ForeignKey("Provience")]
        [Display(Name = "Provience")]
        public int ProvienceId { get; set; }
        [ForeignKey("SchemeLevel")]
        [Display(Name = "SchemeLevel")]
        public int SchemeLevelId { get; set; }
        [ForeignKey("DegreeScholarshipLevel")]
        [Display(Name = "DegreeScholarshipLevel")]
        public int DegreeScholarshipLevelId { get; set; }
        public string ApplicantReferenceNo { get; set; }        
        public string TehsilName { get; set; }        
        public string Gender { get; set; }        
        public string Email { get; set; }        
        public string Year { get; set; }        
        public string CurrentInsituteName { get; set; }        
        public string CurrentInsituteHOD { get; set; }        
        public string CurrentInsituteFocalPerson { get; set; }        
        public string CurrentInsituteFocalDesignation { get; set; }        
        public string CurrentInsituteFocalMobile { get; set; }        
        public string CurrentInsituteFocalEmail { get; set; }        
        public string CurrentInsitutePhone { get; set; }        
        public string CurrentInsituteFax { get; set; }        
        public string CurrentInsituteAddress { get; set; }        
        public string RollNumber { get; set; }        
        public int TotalMarks { get; set; }        
        public float TotalGPA { get; set; }        
        public int ReceivedMarks { get; set; }        
        public float ReceivedCGPA { get; set; }        
        public string OldInstitudeNameAddress { get; set; }        
        public string NameBoardUniversity { get; set; }        
        public string TelephoneWithCode { get; set; }
        public byte[] Picture { get; set; }
        public string ScanDocument { get; set; }
        public string ScanOtherDocument { get; set; }
        public string SelectionStatus { get; set; }
        public string SelectedMethod { get; set; }
        public virtual District District { get; set; }
        public virtual SchemeLevel SchemeLevel { get; set; }
        public virtual Provience Provience { get; set; }
        public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }
    }
}
