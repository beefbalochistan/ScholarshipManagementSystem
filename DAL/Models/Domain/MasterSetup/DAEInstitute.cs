using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("DAEInstitute", Schema = "master")]
    public class DAEInstitute
    {
        [Key]
        public int DAEInstituteId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]                
        public string NameAbbreviation { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone#")]
        public string PhoneNo { get; set; }
        [Display(Name = "Fax#")]
        public string FaxNo { get; set; }
        [ForeignKey("Provience")]
        [Display(Name = "Provience")]
        public int ProvienceId { get; set; }
        public string Address { get; set; }
        [Display(Name = "Focal Person Name")]
        public string FocalPersonName { get; set; }
        [Display(Name = "Focal Person Email")]
        public string FocalPersonEmail { get; set; }
        [Display(Name = "Focal Person Phone#")]
        public string FocalPersonPhoneNo { get; set; }
        public bool IsActive { get; set; } = true;
        public int Enrollment1stY { get; set; }
        public int Enrollment2ndY { get; set; }
        public int Enrollment3rdY { get; set; }
        public string DAEYear { get; set; }
        [ForeignKey("District")]
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        public float PercentageSlots { get; set; }
        public virtual District District { get; set; }
    }
}
