using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("BudgetLevel", Schema = "master")]
    public class BudgetLevel
    {
        [Key]
        public int BudgetLevelId { get; set; }
        public string BudgetLevelName { get; set; }
        public int AnualStipend { get; set; }
        [ForeignKey("PaymentMethod")]
        [Display(Name = "PaymentMethod")]
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

    }
}
