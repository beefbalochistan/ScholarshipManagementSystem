using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.VirtualAccount
{
    [Table("PaymentMethodMode", Schema = "VirtualAccount")]
    public class PaymentMethodMode
    {               
        [Key]
        public int PaymentMethodModeId { get; set; }
        [ForeignKey("PaymentDisbursementMode")]
        public int PaymentDisbursementModeId { get; set; }        
        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }
        //---------------_----Via Cloud--------------------------
        public string PublicKeyFilePath { get; set; }
        public string SFTP_IP { get; set; }
        public string SFTP_Username { get; set; }
        public string SFTP_Password { get; set; }
        public string SFTP_Port { get; set; }        
        //---------------------Via API---------------------------
        /*public string PublicKeyFilePath { get; set; }
        public string SFTP_IP { get; set; }
        public string SFTP_Username { get; set; }
        public string SFTP_Password { get; set; }
        public string SFTP_Port { get; set; }*/
        //-------------------------------------------------------
        public string Description { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual PaymentDisbursementMode PaymentDisbursementMode { get; set; }
    }
}
