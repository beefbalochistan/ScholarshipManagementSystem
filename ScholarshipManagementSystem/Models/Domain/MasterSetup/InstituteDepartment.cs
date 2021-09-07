using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("InstituteDepartment", Schema = "master")]
    public class InstituteDepartment
    {
        [Key]
        public int InstituteDepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public string Code { get; set; }
        public string Description { get; set; }
        public string FocalPersonName { get; set; }
        public string FocalPersonContactNo { get; set; }
        public string FocalPersonEmail { get; set; }
        public string FocalPersonDesignation { get; set; }
        [ForeignKey("InstituteFaculty")]
        [Display(Name = "Institute Faculty")]
        public int InstituteFacultyId { get; set; }
        [ForeignKey("Discipline")]
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }                         
        public virtual InstituteFaculty InstituteFaculty { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
