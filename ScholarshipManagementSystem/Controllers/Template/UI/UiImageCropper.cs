using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.UI
{
    [AllowAnonymous]
    public class UiImageCropper : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("ui-image-cropper")]
        public ActionResult uiimagecropper()
        {
            return View();
        }

    }
}
