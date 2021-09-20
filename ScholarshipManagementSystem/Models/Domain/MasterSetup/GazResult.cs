using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("GazResult", Schema = "master")]
    public class GazResult
    {
        [Key]
        public int GazResultId { get; set; } 
        public string Roll_NO { get; set; } 
        public string REG_NO { get; set; }
        public string Name { get; set; } 
        public string Father_Name { get; set; }
        public string Institute { get; set; }
        public string Institute_District { get; set; }
        public string Group { get; set; }
        public string Candidate_District { get; set; }
        public string Marks_ { get; set; }
        public string Type { get; set; }
        public string Pass_Fail { get; set; }
        public string Remarks { get; set; }

    }
}
