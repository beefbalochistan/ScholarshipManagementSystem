using DAL.Models.Domain.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Models.Domain.MasterSetup
{
    [Table("UserAccessToForward", Schema = "master")]
    public class UserAccessToForward
    {
        [Key]
        public int UserAccessToForwardId { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }        
        [ForeignKey("ApplicantCurrentStatus")]
        [Display(Name = "ApplicantCurrentStatus")]
        public int ApplicantCurrentStatusId { get; set; }
        public bool IsDefault { get; set; } = false;
        public virtual ApplicantCurrentStatus ApplicantCurrentStatus { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
