using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.ScholarshipSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class ViewUploadedResult
    {
        public List<ResultContainer> resultContainerList { get; set; }
        public List<int> compileResult { get; set; }
        public ColumnLabel columnLabel { get; set; }
        public SchemeLevelPolicy schemeLevelPolicy { get; set; }
    }
}
