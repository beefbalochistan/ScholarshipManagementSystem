using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels.VirtualAccount
{
    public class SPApplicantPaymentInProcess
    {
        [Key]
        public int ApplicantId { get; set; }
        public int TrancheId { get; set; }
        public string TrancheName { get; set; }
        [Display(Name = "Reference No")]
        public string ApplicantReferenceNo { get; set; }
        [Display(Name = "Roll No")]
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public string SchemeLevel { get; set; }
        public string District { get; set; }
        public string FatherName { get; set; }
        public string? Institute { get; set; }
        public int? TrancheDocumentId { get; set; }
        public string PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }        
    }
}
