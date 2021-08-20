using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.contacts
{
    [AllowAnonymous]
    public class ContactsList : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("contacts-list")]
        public ActionResult contactslist()
        {
            return View();
        }
    }
}
