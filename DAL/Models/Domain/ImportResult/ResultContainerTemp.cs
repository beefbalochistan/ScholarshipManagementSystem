using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.ImportResult
{
    [Table("ResultContainerTemp", Schema = "ImportResult")]
    public class ResultContainerTemp
    {
        [Key]
        public int ResultContainerTempId { get; set; }       
        public string Roll_NO { get; set; }
        public string REG_NO { get; set; }
        public string Name { get; set; }
        public string Father_Name { get; set; }
        public string Institute { get; set; }
        public string Group { get; set; }
        public string Candidate_District { get; set; }
        public string Institute_District { get; set; }                
        public int Marks_ { get; set; }
        public string Pass_Fail { get; set; }
        public string Remarks { get; set; }        
        public string CNIC { get; set; }        
        public decimal CGPA { get; set; }
        public decimal TotalGPA { get; set; }
        public decimal TotalMarks_ { get; set; }
        public string Department { get; set; }        
        public int DistrictId { get; set; }
        [ForeignKey("ResultRepository")]
        [Display(Name = "ResultRepository")]
        public int ResultRepositoryTempId { get; set; }      
        public virtual ResultRepositoryTemp ResultRepositoryTemp { get; set; }        
        public virtual District District { get; set; }
    }
}
