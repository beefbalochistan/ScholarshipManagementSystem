using DAL.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{   
        [Table("ApplicantCurrentStatus", Schema = "Student")]
        public class ApplicantCurrentStatus
        {
            [Key]
            public int ApplicantCurrentStatusId { get; set; }   
            [Display(Name = "Process State")]
            public string ProcessState { get; set; }
            [Display(Name = "Process Value")]
            public int ProcessValue { get; set; }
            [Display(Name = "Process No")]
            public int VisibleStateNo { get; set; }
            public bool Visibility { get; set; } = true;
            public bool IsActive { get; set; } = true;
            [ForeignKey("BEEFSection")]
            [Display(Name = "Section")]
            public int BEEFSectionId { get; set; }
            public virtual BEEFSection BEEFSection { get; set; }
    }
}
