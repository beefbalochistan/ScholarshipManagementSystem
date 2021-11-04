using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ScholarshipSetup
{
    [Table("ScholarshipFiscalYear", Schema = "scholar")]
    public class ScholarshipFiscalYear
    {
        [Key]
        [Display(Name = "Fiscal Year")]
        public int ScholarshipFiscalYearId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
