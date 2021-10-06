using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("Operator", Schema = "master")]
    public class Operator
    {
        [Key]
        public int OperatorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string OperatorType { get; set; }
    }
}
