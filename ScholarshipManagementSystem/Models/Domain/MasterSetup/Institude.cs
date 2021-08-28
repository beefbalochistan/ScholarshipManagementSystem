using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("Institude", Schema = "master")]
    public class Institude
    {
        [Key]
        public int InstitudeId { get; set; }
        [ForeignKey("InstitudeType")]
        [Display(Name = "Institude Type")]
        public int InstitudeTypeId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Abbreviation")]
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
        [Display(Name = "Logo")]
        public string LogoPath { get; set; }
        public virtual InstitudeType InstitudeType { get; set; }
        public virtual Provience Provience { get; set; }
    }
}
