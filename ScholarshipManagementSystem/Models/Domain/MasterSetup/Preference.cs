
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("Preference", Schema = "master")]
    public class Preference
    {
        [Key]
        public int PreferenceId { get; set; }
        [Required]
        public int SchemeMatrictStipend { get; set; }
        public int SchemeIntermediateStipend { get; set; }
        public int SchemeDAEStipend { get; set; }
        public int SchemeSimpleGraduationStipend { get; set; }
        public int SchemeBacholarStipend { get; set; }
        public int SchemeMasterStipend { get; set; }
        public int SchemeMSStipend { get; set; }        
        public int DistrictThreshold { get; set; }        
        public int InstitudeThreshold { get; set; }
        [Display(Name = "IOMS Institude Qouta")]
        public int IOMSInstitudeQouta { get; set; }
        [Display(Name = "POMS Board Qouta")]
        public int POMSIBoardQouta { get; set; }
        [Display(Name = "DOMS Institude Qouta")]
        public int DOMSInstitudeQouta { get; set; }
        [Display(Name = "DOMS Board Qouta")]
        public int DOMSBoardQouta { get; set; }

        [Display(Name = "SQSOMS Qouta")]
        public int SQSOMSQouta { get; set; }
        [Display(Name = "SQS-EVI Qouta")]
        public int SQSEVIQouta { get; set; }            
    }
}
