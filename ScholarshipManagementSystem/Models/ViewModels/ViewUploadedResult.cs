using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.ViewModels
{
    public class ViewUploadedResult
    {
        public List<ResultContainer> resultContainerList { get; set; }
        public List<int> compileResult { get; set; }
        public ColumnLabel columnLabel { get; set; }
    }
}
