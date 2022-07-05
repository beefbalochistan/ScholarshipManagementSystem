using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("UserAccessToSchemeLevel", Schema = "master")]
    public class UserAccessToSchemeLevel
    {
        [Key]
        public int UserAccessToSchemeLevelId { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("SchemeLevel")]
        [Display(Name = "Scheme")]
        public int SchemeLevelId { get; set; }
        public virtual SchemeLevel SchemeLevel { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
