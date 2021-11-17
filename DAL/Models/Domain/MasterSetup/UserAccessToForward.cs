using DAL.Models.Domain.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("UserAccessToForward", Schema = "master")]
    class UserAccessToForward
    {
        [Key]
        public int UserAccessToForwardId { get; set; }
        [Required]
        public string UserId { get; set; }        
        [ForeignKey("ApplicantCurrentStatus")]
        [Display(Name = "ApplicantCurrentStatus")]
        public int ApplicantCurrentStatusId { get; set; }
        public virtual ApplicantCurrentStatus ApplicantCurrentStatus { get; set; }
    }
}
