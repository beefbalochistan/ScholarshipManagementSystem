using CsvHelper.Configuration;


namespace ScholarshipManagementSystem.Models.Mappers
{
    public sealed class CSVModelMap : ClassMap<CSVModel>
    {
        public CSVModelMap()
        {
            Map(x => x.ApplicantReferenceNo).Name("ReferenceNo");
            Map(x => x.Name).Name("Customer Name");
            Map(x => x.CNIC).Name("Customer CNIC");
            Map(x => x.StudentMobile).Name("Mobile Number");
            Map(x => x.District).Name("District");
            Map(x => x.Amount).Name("Amount");
            Map(x => x.StartDate).Name("Start Date");
            Map(x => x.EndDate).Name("End Date");
        }
    }
}