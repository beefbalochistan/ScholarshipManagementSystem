using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Religion", Schema = "master")]
    public class Religion
    {
        [Key]     
        [Required]
        public string Name { get; set; }
        public int Sno { get; set; }
    }
}
