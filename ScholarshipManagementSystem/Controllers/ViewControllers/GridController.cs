using Microsoft.AspNetCore.Mvc;
using ScholarshipManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Controllers.ViewControllers
{
    public class GridController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GridController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult InlineEditing()
        {
            var order = _context.District;
            ViewBag.datasource = order;
            ViewBag.ddDataSource = new string[] { "Top", "Bottom" };
            return View();
        }
    }
}
