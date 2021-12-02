
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.ViewModels.UserManagement
{
    public class GetUserSchemeLevelAccess
    {
        [Key]
        public string UserId { get; set; }
        public string SchemeLevel { get; set; }
    }
}
