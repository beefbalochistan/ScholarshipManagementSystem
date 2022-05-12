using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.VirtualAccount
{
    [Table("Tranche", Schema = "VirtualAccount")]
    public class Tranche
    {
        [Key]
        public int TrancheId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PaymentMethod")]
        [Display(Name = "Payment Method")]
        public int PaymentMethodId { get; set; }       
        public bool IsOpen { get; set; } = true;
        public bool IsClose { get; set; } = false;
        public bool IsLock { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Today.Date;
        [DataType(DataType.Date)]
        public DateTime ApprovedOn { get; set; }
        public string ApprovedAttachment { get; set; }
        public decimal CurrentCommittedAmount { get; set; }
        public decimal ApprovedAmount { get; set; } = 0;
        public decimal DisbursedAmount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDisbursementInProcess { get; set; } = false;
        public int ApplicantCount { get; set; } = 0;
        public virtual PaymentMethod PaymentMethod { get; set; }                     
    }
}
