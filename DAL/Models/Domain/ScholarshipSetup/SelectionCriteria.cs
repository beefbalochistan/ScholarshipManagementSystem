using DAL.Models.Domain.ImportResult;
using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ScholarshipSetup
{
    [Table("SelectionCriteria", Schema = "ScholarshipSetup")]
    public class SelectionCriteria
    {
        [Key]
        public int SelectionCriteriaId { get; set; }
        [Required]
        [ForeignKey("ResultRepository")]
        [Display(Name = "Result Repository")]
        public int ResultRepositoryId { get; set; }        
        [ForeignKey("Operator")]
        [Display(Name = "Operator")]
        public int OperatorId { get; set; }
        [Required]
        [ForeignKey("ExcelColumnName")]
        [Display(Name = "Column")]
        public int ExcelColumnNameId { get; set; }
        public string Condition { get; set; }
        public virtual Operator Operator { get; set; }
        public virtual ResultRepository ResultRepository { get; set; }
        public virtual ExcelColumnName ExcelColumnName { get; set; }
    }
}
