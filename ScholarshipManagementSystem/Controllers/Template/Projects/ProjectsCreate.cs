using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.Projects
{
    [AllowAnonymous]
    public class ProjectsCreate : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("projects-create")]
        public ActionResult projectscreate()
        {
            return View();
        }

    }
}
