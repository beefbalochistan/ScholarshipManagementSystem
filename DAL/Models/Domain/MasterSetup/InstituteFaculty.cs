using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("InstituteFaculty", Schema = "master")]
    public class InstituteFaculty
    {
        [Key]
        public int InstituteFacultyId { get; set; }        
        public string Name { get; set; }
        public string FocalPersonName { get; set; }
        public string FocalPersonContactNo { get; set; }
        public string FocalPersonEmail { get; set; }
        public string FocalPersonDesignation { get; set; }        
        [ForeignKey("Faculty")]
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }
        [ForeignKey("Institute")]
        [Display(Name = "Institute")]
        public int InstituteId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
