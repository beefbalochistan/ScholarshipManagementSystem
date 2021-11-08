using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{   
        [Table("ApplicantCurrentStatus", Schema = "Student")]
        public class ApplicantCurrentStatus
        {
            [Key]
            public int ApplicantCurrentStatusId { get; set; }           
            public string Value { get; set; }
            public bool IsActive { get; set; } = true;
        }
}
