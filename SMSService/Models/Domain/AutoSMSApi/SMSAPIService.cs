
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SMSService.Models.Domain.AutoSMSApi
{
    [Table("SMSAPIService", Schema = "sms")]
    public class SMSAPIService
    {
        [Key]
        public int SMSAPIServiceId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
        public string SendSMSURL { get; set; }
        public string BalanceEnquiryURL { get; set; }
        public string PackageExpiryURL { get; set; }
        public string Mask { get; set; }
        public string Description { get; set; }
    }
}
