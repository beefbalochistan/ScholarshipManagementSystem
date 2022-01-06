using DAL.Models.Domain.ImportResult;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("DocumentAssistGeneral", Schema = "master")]
    public class DocumentAssistGeneral
    {
        [Key]
        public int DocumentAssistGeneralId { get; set; }
        [ForeignKey("ExcelColumnName")]
        [Display(Name = "ExcelColumnName")]
        public int ExcelColumnNameId { get; set; }
        [ForeignKey("DocumentAssist")]
        [Display(Name = "Document Assist")]
        public int DocumentAssistId { get; set; }
        [ForeignKey("SchemeLevel")]
        [Display(Name = "Scheme Level")]
        public int SchemeLevelId { get; set; }
        [ForeignKey("DegreeScholarshipLevel")]
        [Display(Name = "Degree Level")]
        public int? DegreeScholarshipLevelId { get; set; }
        public int TotalFind { get; set; } 
        public bool IsActive { get; set; } = true;
        public virtual ExcelColumnName ExcelColumnName { get; set; }
        public virtual DocumentAssist DocumentAssist { get; set; }        
        public virtual SchemeLevel SchemeLevel { get; set; }        
        public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }        
    }
}