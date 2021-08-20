using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("InstitudeDepartment", Schema = "master")]
    public class InstitudeDepartment
    {
        [Key]
        public int InstitudeDepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Institude")]
        [Display(Name = "Institude")]
        public int InstitudeId { get; set; }
        [ForeignKey("Discipline")]
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }        
        [Required]
        //[MaxLength(3, ErrorMessage = "Code cannot exceeding from 3 digit")]
        public string Code { get; set; }
        public string Description { get; set; }             
        public virtual Institude Institude { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
