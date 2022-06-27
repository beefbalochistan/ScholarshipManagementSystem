using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    [Table("DefaultComment", Schema = "master")]
    public class DefaultComment
    {
        public int DefaultCommentId {get;set;}
        public string ForwardCaseDefaultComment {get;set;}
    }
}
