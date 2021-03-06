using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using SMSService.Models.Domain.AutoSMSApi;

namespace ScholarshipManagementSystem.Controllers.AutoSMSApi
{
    public class SMSAPIServiceAuditTrailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSAPIServiceAuditTrailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMSAPIServiceAuditTrails
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSAPIServiceAuditTrail.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            return PartialView(await _context.SMSAPIServiceAuditTrail.Where(a=>a.ApplicantId == id).OrderByDescending(a=>a.SMSAPIServiceAuditTrailId).ToListAsync());
        }

        // GET: SMSAPIServiceAuditTrails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAPIServiceAuditTrail = await _context.SMSAPIServiceAuditTrail
                .FirstOrDefaultAsync(m => m.SMSAPIServiceAuditTrailId == id);
            if (sMSAPIServiceAuditTrail == null)
            {
                return NotFound();
            }

            return View(sMSAPIServiceAuditTrail);
        }
    }
}
