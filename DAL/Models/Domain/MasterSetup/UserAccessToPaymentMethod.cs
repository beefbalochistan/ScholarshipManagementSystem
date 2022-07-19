using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("UserAccessToPaymentMethod", Schema = "master")]
    public class UserAccessToPaymentMethod
    {
        [Key]
        public int UserAccessToPaymentMethodId { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("PaymentMethod")]
        [Display(Name = "Payment Method")]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
