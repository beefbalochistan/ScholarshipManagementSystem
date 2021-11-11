using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSService.Models.Domain.AutoSMSApi
{
    [Table("SMSAPIServiceAuditTrail", Schema = "sms")]
    public class SMSAPIServiceAuditTrail
    {
        public int SMSAPIServiceAuditTrailId { get; set; }
        public string MessageFor { get; set; }
        public string UserId { get; set; }
        public string ReferenceId { get; set; }
        public int ApplicantId { get; set; }
        public string Text { get; set; }
        [Display(Name = "Text Length")]
        public int TextLength { get; set; }
        [Display(Name = "Destination Number")]
        public string DestinationNumber { get; set; }        
        public string Language { get; set; }
        [Display(Name = "Response Type")]
        public string ResponseType { get; set; }
        [Display(Name = "Response Message")]
        public string ResponseMessage { get; set; }
        [Display(Name = "Send On")]
        public DateTime SendOn { get; set; } = DateTime.Now;
        public string SendBy { get; set; } = "System";
    }
}
