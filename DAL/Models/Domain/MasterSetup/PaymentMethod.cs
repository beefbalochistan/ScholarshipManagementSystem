using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Payment Through")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string FocalPerson { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }        
        public bool IsActive { get; set; }                
    }
}
