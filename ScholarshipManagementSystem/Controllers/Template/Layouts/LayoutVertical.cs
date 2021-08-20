﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Layouts
{
    [AllowAnonymous]
    public class LayoutVertical : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
