using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ImportResult
{
    [Table("DocumentAssistIndicator", Schema = "ImportResult")]
    public class DocumentAssistIndicator
    {
        [Key]
        public int DocumentAssistIndicatorId { get; set; }
        [ForeignKey("ExcelColumnName")]
        [Display(Name = "ExcelColumnName")]
        public int ExcelColumnNameId { get; set; }
        [ForeignKey("DocumentAssist")]
        [Display(Name = "Document Assist")]
        public int DocumentAssistId { get; set; }
        [ForeignKey("ResultRepository")]
        public int ResultRepositoryId { get; set; }
        public int TotalFind { get; set; }
        public virtual ExcelColumnName ExcelColumnName { get; set; }
        public virtual DocumentAssist DocumentAssist { get; set; }
        public virtual ResultRepository ResultRepository { get; set; }
    }
}
