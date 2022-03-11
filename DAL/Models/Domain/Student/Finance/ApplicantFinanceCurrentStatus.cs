
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models.Domain.Student.Finance
{   
        [Table("ApplicantFinanceCurrentStatus", Schema = "Student")]
        public class ApplicantFinanceCurrentStatus
    {
            [Key]
            public int ApplicantFinanceCurrentStatusId { get; set; }   
            [Display(Name = "Process State")]
            public string ProcessState { get; set; }
            [Display(Name = "Process Value")]
            public int ProcessValue { get; set; }
            [Display(Name = "Process No")]
            public int VisibleStateNo { get; set; }
            public string VisibleStateText { get; set; }
            public bool Visibility { get; set; } = true;            
    }
}
