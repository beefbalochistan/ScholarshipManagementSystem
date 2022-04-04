using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models
{
    public class CSVModel
    {
        [CsvHelper.Configuration.Attributes.Name("S#")]
        public int Sno { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Customer Name")]
        public string Name { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Customer CNIC")]
        public string CNIC { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Mobile Number")]
        public string StudentMobile { get; set; }
        [CsvHelper.Configuration.Attributes.Name("District")]
        public string District { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Amount")]
        public int Amount { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Start Date")]        
        public string StartDate { get; set; }
        [CsvHelper.Configuration.Attributes.Name("End Date")]
        public string EndDate { get; set; }
        [CsvHelper.Configuration.Attributes.Name("Remarks")]
        public string ApplicantReferenceNo { get; set; }
    }
}
