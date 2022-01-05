using Microsoft.EntityFrameworkCore;

namespace DAL.Models.ViewModels
{
    [Keyless]
    public class SPAssistDocumentViewer
    {        
        public int SrNo { get; set; }
        public string ColumnValue { get; set; }
        public int TotalFind { get; set; }
    } 
}
