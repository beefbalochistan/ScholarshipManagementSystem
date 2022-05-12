using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models
{
    public class OmniDisbursement
    {
        public string CustomerName { get; set; }
        public string CustomerCnic { get; set; }
        public string MobileNumber { get; set; }
        public string District  { get; set; }
    }

    public sealed class DataRecordMap : ClassMap<OmniDisbursement>
    {
        public DataRecordMap()
        {
            Map(m => m.CustomerName).Name("Customer Name");
            Map(m => m.CustomerCnic).Name("Customer Cnic");
            Map(m => m.MobileNumber).Name("Mobile Number");
            Map(m => m.District).Name("District");           
        }
    }
}
