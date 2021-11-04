using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Discipline", Schema = "master")]
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }
        [Required]
        public string Name { get; set; }        
        [Required]
        [MaxLength(5, ErrorMessage = "Code cannot exceeding from 5 digit")]
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
