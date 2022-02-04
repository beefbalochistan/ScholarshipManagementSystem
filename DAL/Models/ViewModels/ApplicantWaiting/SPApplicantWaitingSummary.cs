using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels.ApplicantWaiting
{
    public class SPApplicantWaitingSummary
    {
        [Key]
        public int SchemeLevelId { get; set; }
        public int SchemeId { get; set; }
        public string Scheme { get; set; }
        public string SchemeLevel { get; set; }
        public string Qualification { get; set; }
        public int Applicant { get; set; }
    }
}
