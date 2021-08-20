using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    public class QoutaPreference
    {
        public int QoutaMetric { get; set; }
        public int QoutaFAFSc1Y { get; set; }
        public int QoutaFAFSc2Y { get; set; }
        public int QoutaDAE1Y { get; set; }
        public int QoutaDAE2Y { get; set; }
        public int QoutaDAE3Y { get; set; }
        public int QoutaBacholar1Y { get; set; }
        public int BacholarQouta { get; set; }
        public int MasterQouta { get; set; }
    }
}
