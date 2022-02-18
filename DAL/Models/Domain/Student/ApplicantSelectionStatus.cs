using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("ApplicantSelectionStatus", Schema = "Student")]
    public class ApplicantSelectionStatus
    {
        [Key]
        public int ApplicantSelectionStatusId { get; set; }
        public string SelectionStatus { get; set; }       
    }
}
