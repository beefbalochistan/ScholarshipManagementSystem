using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchemeLevelAccess { get; set; }
        public int BEEFSectionId { get; set; }
        public int ApplicantCurrentStatusId { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public bool IsSectionHead { get; set; } = false;
    }
}
