using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ExcelColumnName", Schema = "ImportResult")]
    public class ExcelColumnName
    {
        [Key]
        public int ExcelColumnNameId { get; set; }
        public string Name { get; set; }
    }
}
