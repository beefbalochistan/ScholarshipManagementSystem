using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("SMSMassage", Schema = "master")]
    public class SMSMassage
    {
        [Key]
        public int SMSMassageTypeId { get; set; }
        public string SMSType { get; set; }
        public string Title { get; set; }
        public string Massage { get; set; }       
    }
}
