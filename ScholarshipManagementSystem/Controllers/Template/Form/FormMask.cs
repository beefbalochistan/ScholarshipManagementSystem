using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.Form
{
    [AllowAnonymous]
    public class FormMask : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("form-mask")]
        public ActionResult formmask()
        {
            return View();
        }
    }
}
