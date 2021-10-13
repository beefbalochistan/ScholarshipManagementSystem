using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ResultContainer", Schema = "ImportResult")]
    public class ResultContainer
    {
        [Key]
        public int ResultContainerId { get; set; }       
        public string Roll_NO { get; set; }
        public string REG_NO { get; set; }
        public string Name { get; set; }
        public string Father_Name { get; set; }
        public string Institute { get; set; }
        public string Group { get; set; }
        public string Candidate_District { get; set; }
        public string Institute_District { get; set; }                
        public string Marks_ { get; set; }
        public string Pass_Fail { get; set; }
        public string Remarks { get; set; }        
        public string CNIC { get; set; }        
        public string CGPA { get; set; }        
        public int DistrictId { get; set; }
        [ForeignKey("ResultRepository")]
        [Display(Name = "ResultRepository")]
        public int ResultRepositoryId { get; set; }
        [ForeignKey("ColumnLabel")]
        [Display(Name = "ColumnLabel")]
        //public int ColumnLabelId { get; set; }
        public bool IsOnCriteria { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public virtual ResultRepository ResultRepository { get; set; }
        //public virtual ColumnLabel ColumnLabel { get; set; }
        public virtual District District { get; set; }
    }
}
