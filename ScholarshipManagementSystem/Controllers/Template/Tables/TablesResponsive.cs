using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.Tables
{
    [AllowAnonymous]
    public class TablesResponsive : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("tables-responsive")]
        public ActionResult tablesresponsive()
        {
            return View();
        }
    }
}
