using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.Student;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.VirtualAccount
{
    public class PaymentDisbursement
    {
        public int PaymentDisbursementId { get; set; }
        public int TrancheDocumentId { get; set; }
        public string ApplicantReferenceNo { get; set; }
        public string DDNo { get; set; }
        public string ChequeNo { get; set; }
        public string DDReceiver { get; set; }
        public string DDReceiverCNIC { get; set; }
        public string DDRelationWithScholar { get; set; }
        public string DDReceiverContactNo { get; set; }
        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCnic { get; set; }
        public string MobileNumber { get; set; }
        public string District  { get; set; }
        public int DisbursementAmount { get; set; }
        public string TransactionType { get; set; }
        public int TransactionAmount    { get; set; }
        public string TransactionDate   { get; set; }
        public string DirectAgentId { get; set; }
        public string AgentName { get; set; }
        public string City  { get; set; }        
        public int ApplicantId { get; set; }        
        public int PaymentMethodId  { get; set; }
        public string DDScannedCopy  { get; set; }
        [NotMapped]
        public IFormFile scannedcopy { get; set; }
        public string ChequeScannedCopy  { get; set; }       
        public string OtherDocumentScannedCopy  { get; set; }       
        public virtual Applicant Applicant { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
