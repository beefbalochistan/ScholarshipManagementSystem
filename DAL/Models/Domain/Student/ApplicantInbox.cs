using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{
    [Table("ApplicantInbox", Schema = "Student")]
    public class ApplicantInbox
    {
        [Key]
        public int ApplicantInboxId { get; set; }
        [Required]
        public string Name { get; set; }        
    }
}
