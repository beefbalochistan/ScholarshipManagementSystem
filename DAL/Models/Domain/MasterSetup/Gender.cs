using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Gender", Schema = "master")]
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Required]
        public string Name { get; set; }        
    }
}
