
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models.Domain.ImportResult
{
    [Table("ColumnLabelTemp", Schema = "ImportResult")]
    public class ColumnLabelTemp
    {
        [Key]
        public int ColumnLabelTempId { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        public string C4 { get; set; }
        public string C5 { get; set; }
        public string C6 { get; set; }
        public string C7 { get; set; }
        public string C8 { get; set; }
        public string C9 { get; set; }
        public string C10 { get; set; }
        public string C11 { get; set; }
        public string C12 { get; set; }
        public string C13 { get; set; }
        public string C14 { get; set; }
        public string C15 { get; set; }
        public string C16 { get; set; }
        public bool IsActive { get; set; }
        public int ResultRepositoryTempId { get; set; }
        public virtual ResultRepositoryTemp ResultRepositoryTemp { get; set; }
    }
}
