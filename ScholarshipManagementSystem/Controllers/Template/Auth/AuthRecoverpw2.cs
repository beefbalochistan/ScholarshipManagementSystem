﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Auth
{
    [AllowAnonymous]
    public class AuthRecoverpw2 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
