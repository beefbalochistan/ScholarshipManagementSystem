using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("Faculty", Schema = "master")]
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string Name { get; set; }        
        public string Code { get; set; }        
        public string Description { get; set; }        
    }
}
