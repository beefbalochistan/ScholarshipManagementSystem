using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    [Keyless]
    public class SPDocumentViewerReport
    {        
        public int SrNo { get; set; }
        public string ROLL_NO { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ColumnValue { get; set; }        
        public string TotalFind { get; set; }        
    }
}
