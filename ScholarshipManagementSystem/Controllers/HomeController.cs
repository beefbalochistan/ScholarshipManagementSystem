using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace ScholarshipManagementSystem.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            ViewData["Years"] = new SelectList(_context.ScholarshipFiscalYear.OrderByDescending(a=>a.Code), "Code", "Code");
            ViewBag.value = _context.ScholarshipFiscalYear.Max(a=>a.Description);
            return View();
        }
        public IActionResult Graph(string year)
        {
            ViewBag.value = _context.ScholarshipFiscalYear.Where(a=>a.Code == year).Select(a => a.Description).FirstOrDefault();
            return PartialView();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}