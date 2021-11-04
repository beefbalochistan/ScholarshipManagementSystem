using DAL.Models.Domain.ScholarshipSetup;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Scheme", Schema = "master")]
    public class Scheme
    {
        [Key]
        public int SchemeId { get; set; }
        [ForeignKey("Scholarship")]
        [Display(Name = "Scholarship")]
        public int ScholarshipId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Scholarship Scholarship { get; set; }
    }
}
