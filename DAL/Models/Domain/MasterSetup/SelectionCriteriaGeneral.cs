using DAL.Models.Domain.ImportResult;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{ 
        [Table("SelectionCriteriaGeneral", Schema = "master")]
        public class SelectionCriteriaGeneral
        {
            [Key]
            public int SelectionCriteriaGeneralId { get; set; }                        
            [ForeignKey("SchemeLevel")]
            [Display(Name = "Scheme Level")]
            public int SchemeLevelId { get; set; }            
            public string Expression { get; set; }            
            public virtual SchemeLevel SchemeLevel { get; set; }            
        }
    }