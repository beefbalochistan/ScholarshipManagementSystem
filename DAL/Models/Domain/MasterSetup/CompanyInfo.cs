using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("CompanyInfo", Schema = "master")]
    public class CompanyInfo
    {
        [Key]
        public int CompanyInfoId { get; set; }
        [Required]
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string AuthorizeThrough { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Singatory1Name { get; set; }
        public string Singatory2Name { get; set; }
        public string Singatory3Name { get; set; }
        public string Singatory1Designation { get; set; }
        public string Singatory2Designation { get; set; }
        public string Singatory3Designation { get; set; }
    }
}
