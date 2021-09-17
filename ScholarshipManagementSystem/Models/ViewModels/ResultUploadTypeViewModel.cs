using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ScholarshipManagementSystem.Constants.Permissions;

namespace ScholarshipManagementSystem.Models.ViewModels
{
    public class ResultUploadTypeViewModel
    {
         


        [Display(Name = "Result Upload Type")]
        [Required]
        public string ResultUploadTypeId { get; set; }  
        [Required(ErrorMessage = "Please choose File")]
        [Display(Name = "Excel File")]
        public IFormFile ExcelFile { get; set; } 
        public SelectList ResultUploadType { get; set; }

    }
}
