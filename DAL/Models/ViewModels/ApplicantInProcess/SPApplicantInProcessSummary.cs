
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.ViewModels.ApplicantInProcess
{
    public class SPApplicantInProcessSummary
    {
        [Key]
        public int SchemeId { get; set; }        
        public string Scheme { get; set; }
        public int SchemeLevelId { get; set; }
        public string SchemeLevel { get; set; }
        public string Qualification { get; set; }
        public int Applicant { get; set; }        
    }
}
