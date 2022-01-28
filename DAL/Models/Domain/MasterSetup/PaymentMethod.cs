﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Domain.MasterSetup
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }        
        public bool IsActive { get; set; }        
    }
}