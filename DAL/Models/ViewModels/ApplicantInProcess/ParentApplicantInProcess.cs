using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels.ApplicantInProcess
{
    public class ParentApplicantInProcess
    {
        public IEnumerable<SPApplicantInProcess> SPApplicantInProcessList { get; set; }
        public IEnumerable<SPApplicantInProcessSummary> SPApplicantInProcessSummaryList { get; set; }
    }
}
