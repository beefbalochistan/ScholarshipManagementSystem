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
        [Table("SelectionCriteriaGeneral", Schema = "master")]
        public class SelectionCriteriaGeneral
        {
            [Key]
            public int SelectionCriteriaGeneralId { get; set; }            
            [ForeignKey("Operator")]
            [Display(Name = "Operator")]
            public int OperatorId { get; set; }
            [Required]
            [ForeignKey("ExcelColumnName")]
            [Display(Name = "Column")]
            public int ExcelColumnNameId { get; set; }
            [ForeignKey("SchemeLevel")]
            [Display(Name = "Scheme Level")]
            public int SchemeLevelId { get; set; }
            [ForeignKey("DegreeScholarshipLevel")]
            [Display(Name = "Degree Level")]
            public int? DegreeScholarshipLevelId { get; set; }
            public string Condition { get; set; }
            public virtual Operator Operator { get; set; }           
            public virtual ExcelColumnName ExcelColumnName { get; set; }
            public virtual SchemeLevel SchemeLevel { get; set; }
            public virtual DegreeScholarshipLevel DegreeScholarshipLevel { get; set; }
        }
    }