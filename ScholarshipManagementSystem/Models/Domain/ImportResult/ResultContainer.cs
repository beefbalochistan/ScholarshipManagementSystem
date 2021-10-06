using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("ResultContainer", Schema = "ImportResult")]
    public class ResultContainer
    {
        [Key]
        public int ResultContainerId { get; set; }       
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
        [ForeignKey("ResultRepository")]
        [Display(Name = "ResultRepository")]
        public int ResultRepositoryId { get; set; }
        [ForeignKey("ColumnLabel")]
        [Display(Name = "ColumnLabel")]
        public int ColumnLabelId { get; set; }
        public bool IsOnCriteria { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public virtual ResultRepository ResultRepository { get; set; }
        public virtual ColumnLabel ColumnLabel { get; set; }
    }
}
