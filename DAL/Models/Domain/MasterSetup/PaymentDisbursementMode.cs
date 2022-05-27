using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("PaymentDisbursementMode", Schema = "master")]
    public class PaymentDisbursementMode
    {
        [Key]
        public int PaymentDisbursementModeId { get; set; }
        public string Name { get; set; }
    }
}
