using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("SpecialQuotaCategory", Schema = "master")]
    public class SpecialQuotaCategory
    {
        [Key]
        public int SpecialQuotaCategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName  { get; set; }
        [Display(Name = "Type")]
        public string CategoryType  { get; set; }
        public decimal PercentageValue   { get; set; }
    }
}
