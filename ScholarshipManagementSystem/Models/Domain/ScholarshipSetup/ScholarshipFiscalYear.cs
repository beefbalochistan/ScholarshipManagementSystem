﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models.Domain.ScholarshipSetup
{
    [Table("ScholarshipFiscalYear", Schema = "scholar")]
    public class ScholarshipFiscalYear
    {
        [Key]
        public int ScholarshipFiscalYearId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
