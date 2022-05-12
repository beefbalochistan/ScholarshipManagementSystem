using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Repository.Data;
using DAL.Models;
using System;
using System.Diagnostics;
using System.Linq;
using ScholarshipManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Controllers
{
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Index()
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
        public async Task<JsonResult> ReloadInbox()
        {
            int MaxFYId = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Max(a => a.ScholarshipFiscalYearId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            int applicantCurrentStatusId = currentUser.ApplicantCurrentStatusId;
           /* var InProcessSummary = await _context.SPApplicantInProcessSummary.FromSqlRaw("exec [Student].[ApplicantInProcessSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            var RejectedSummary = await _context.SPApplicantRejectedSummary.FromSqlRaw("exec [Student].[ApplicantRejectedSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            var WaitingSummary = await _context.SPApplicantWaitingSummary.FromSqlRaw("exec [Student].[ApplicantWaitingSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            MyStaticClass.SetInProcessFile(InProcessSummary.Sum(a => a.Applicant));
            MyStaticClass.SetRejectedFile(RejectedSummary.Sum(a => a.Applicant));
            MyStaticClass.SetWaitingFile(WaitingSummary.Sum(a => a.Applicant));*/

            return Json(new { isValid = true, inProcessValue = MyStaticClass.GetInProcessFile().Result, waitingValue = MyStaticClass.GetWaitingFile().Result, rejectedValue = MyStaticClass.GetRejectedFile().Result });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}