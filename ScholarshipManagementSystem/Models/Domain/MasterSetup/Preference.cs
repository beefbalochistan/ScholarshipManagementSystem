
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
        public int SchemeMatrict { get; set; }
        public int SchemeIntermediate { get; set; }
        public int SchemeBacholar { get; set; }
        public int SchemeMaster { get; set; }
        public int SchemeMS { get; set; }        
        public int DistrictThreshold { get; set; }        
        public int InstitudeThreshold { get; set; }
        [Display(Name = "POMS/IOMS InstitudeQouta")]
        public int POMSDOMSBoardQouta { get; set; }
        [Display(Name = "POMS/IOMS InstitudeQouta")]
        public int POMSDOMSInstitudeQouta { get; set; }

        [Display(Name = "SQSOMS Qouta")]
        public int SQSOMSQouta { get; set; }
        [Display(Name = "SQS-EVI Qouta")]
        public int SQSEVIQouta { get; set; }            
    }
}
