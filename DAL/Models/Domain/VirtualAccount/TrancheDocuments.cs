using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.VirtualAccount
{
    [Table("TrancheDocument", Schema = "VirtualAccount")]
    public class TrancheDocument
    {
        [Key]
        public int TrancheDocumentId { get; set; }
        [ForeignKey("Tranche")]
        [Display(Name = "Tranche")]
        public int TrancheId { get; set; }
        [Display(Name = "Tranche Document Name")]
        public string TrancheDocumentName { get; set; }
        [Display(Name = "CSV File")]
        public string CSVAttachment { get; set; }
        [Display(Name = "CSV On")]
        [DataType(DataType.Date)]
        public DateTime CSVAttachmentOn { get; set; }
        public bool IsPGPGenerated { get; set; }
        [Display(Name = "PGP File")]
        public string PGPAttachment { get; set; }
        [Display(Name = "PGP On")]
        [DataType(DataType.Date)]
        public DateTime PGPGeneratedOn { get; set; }
        public string PGPKey { get; set; }
        [Display(Name = "Email")]
        public bool IsEmail { get; set; }
        [Display(Name = "SFTP")]
        public bool IsSendToServer { get; set; }
        [Display(Name = "Is Execute")]
        public int IsExecuteSuccessfully { get; set; } = 0;
        public int IsAutoDisbursement { get; set; } = 0;
        public string SuccessList { get; set; }

        public virtual Tranche Tranche { get; set; }
    }
}
