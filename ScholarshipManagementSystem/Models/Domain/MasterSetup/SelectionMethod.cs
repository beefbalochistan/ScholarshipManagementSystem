using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("SelectionMethod", Schema = "master")]
    public class SelectionMethod
    {
        [Key]
        public int SelectionMethodId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
