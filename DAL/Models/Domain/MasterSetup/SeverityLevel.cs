using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("SeverityLevel", Schema = "Master")]
    public class SeverityLevel
    {
        [Key]
        public int SeverityLevelId { get; set; }       
        public int Level { get; set; }
        public string Meaning { get; set; }
        public string Color { get; set; }
    }
}
