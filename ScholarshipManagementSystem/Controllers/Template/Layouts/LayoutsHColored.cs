using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.Layouts
{
    [AllowAnonymous]
    public class LayoutsHColored : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
