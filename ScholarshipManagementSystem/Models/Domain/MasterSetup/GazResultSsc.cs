using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("GazResultSsc", Schema = "master")]
    public class GazResultSsc
    {
        [Key]
        public int GazResultSscId { get; set; } 
        public string Roll_NO { get; set; } 
        public string REG_NO { get; set; }
        public string Name { get; set; } 
        public string Father_Name { get; set; }
        public string Institute { get; set; }
        public string Group { get; set; }
        public string candidate_district { get; set; }
        public string institute_district { get; set; }
        public string Marks_ { get; set; } 
        public string Pass_Fail { get; set; }
        public string Remarks { get; set; }

    }
}
