using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.AutoSMSApi
{
    [Table("SMSAPIService", Schema = "sms")]
    public class SMSAPIService
    {
        [Key]
        public int SMSAPIServiceId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mask { get; set; }
        public string Description { get; set; }
    }
}
