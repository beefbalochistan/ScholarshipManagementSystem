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
    [Table("SchemeLevelMandatoryColumn", Schema = "master")]
    public class SchemeLevelMandatoryColumn        
    {
        [Key]
        public int SchemeLevelMandatoryColumnId { get; set; }                
        [ForeignKey("ExcelColumnName")]        
        public int ExcelColumnNameId { get; set; }
        [ForeignKey("SchemeLevel")]
        public int SchemeLevelId { get; set; }
        public virtual SchemeLevel SchemeLevel { get; set; }
        public virtual ExcelColumnName ExcelColumnName { get; set; }
    }
}
