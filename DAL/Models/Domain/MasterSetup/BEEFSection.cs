using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("BEEFSection", Schema = "master")]
    public class BEEFSection
    {
        [Key]
        public int BEEFSectionId { get; set; }
        [Required]
        public string Name { get; set; }       
        public string Description { get; set; }
    }
}
