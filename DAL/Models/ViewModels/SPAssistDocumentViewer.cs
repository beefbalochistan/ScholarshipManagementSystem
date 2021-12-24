using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class SPAssistDocumentViewer
    {
        [Key]
        public int SrNo { get; set; }
        public string ColumnValue { get; set; }
        public int TotalFind { get; set; }
    } 
}
