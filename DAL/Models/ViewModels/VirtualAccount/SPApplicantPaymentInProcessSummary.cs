using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels.VirtualAccount
{
    public class SPApplicantPaymentInProcessSummary
    {
        [Key]
        public int TrancheId { get; set; }        
        public string Name { get; set; }        
        public int Applicant { get; set; }
    }
}
