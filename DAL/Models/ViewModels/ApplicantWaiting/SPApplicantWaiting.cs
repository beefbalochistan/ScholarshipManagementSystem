using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels.ApplicantWaiting
{
    public class SPApplicantWaiting
    {
        [Key]
        public int ApplicantId { get; set; }
        public string ApplicantReferenceNo { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public string SchemeLevel { get; set; }
        public int Round { get; set; }
    }
}
