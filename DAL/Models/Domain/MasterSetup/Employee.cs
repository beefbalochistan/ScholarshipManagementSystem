using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Employee", Schema = "master")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsSectionHead { get; set; }
        [ForeignKey("BEEFSection")]
        [Display(Name = "Section")]
        public int BEEFSectionId { get; set; }
        public virtual BEEFSection BEEFSection { get; set; }
    }
}
