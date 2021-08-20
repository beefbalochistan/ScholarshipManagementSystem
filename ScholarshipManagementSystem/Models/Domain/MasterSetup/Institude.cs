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
        [Display(Name = "InstitudeType")]
        public int InstitudeTypeId { get; set; }
        public string Name { get; set; }
        public string NameAbbreviation { get; set; }
        public string Website { get; set; }
        public int Email { get; set; }
        public int PhoneNo { get; set; }
        public int FaxNo { get; set; }
        public string ProvienceId { get; set; }        
        public int Address { get; set; }
        public int FocalPersonName { get; set; }
        public int FocalPersonEmail { get; set; }
        public int FocalPersonPhoneNo { get; set; }
        public string LogoPath { get; set; }
        public virtual InstitudeType InstitudeType { get; set; }
    }
}
