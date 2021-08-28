using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.AutoSMSApi
{
    [Table("SMSAPIServiceAuditTrail", Schema = "sms")]
    public class SMSAPIServiceAuditTrail
    {
        public int SMSAPIServiceAuditTrailId { get; set; }
        public string Text { get; set; }
        public int TextLength { get; set; }
        public string DestinationNumber { get; set; }
        public string Language { get; set; }
        public string ResponseType { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime SendOn { get; set; } = DateTime.Now;
    }
}
