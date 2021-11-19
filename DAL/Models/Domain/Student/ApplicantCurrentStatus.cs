﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.Student
{   
        [Table("ApplicantCurrentStatus", Schema = "Student")]
        public class ApplicantCurrentStatus
        {
            [Key]
            public int ApplicantCurrentStatusId { get; set; }   
            [Display(Name = "Process State")]
            public string ProcessState { get; set; }
            public int ProcessValue { get; set; }
            public bool IsActive { get; set; } = true;
        }
}
