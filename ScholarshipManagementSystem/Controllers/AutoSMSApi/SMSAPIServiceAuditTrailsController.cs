using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.AutoSMSApi;

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

        // GET: SMSAPIServiceAuditTrails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSAPIServiceAuditTrails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SMSAPIServiceAuditTrailId,Text,TextLength,DestinationNumber,Language,ResponseType,ResponseMessage,SendOn")] SMSAPIServiceAuditTrail sMSAPIServiceAuditTrail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSAPIServiceAuditTrail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSAPIServiceAuditTrail);
        }

        // GET: SMSAPIServiceAuditTrails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAPIServiceAuditTrail = await _context.SMSAPIServiceAuditTrail.FindAsync(id);
            if (sMSAPIServiceAuditTrail == null)
            {
                return NotFound();
            }
            return View(sMSAPIServiceAuditTrail);
        }

        // POST: SMSAPIServiceAuditTrails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SMSAPIServiceAuditTrailId,Text,TextLength,DestinationNumber,Language,ResponseType,ResponseMessage,SendOn")] SMSAPIServiceAuditTrail sMSAPIServiceAuditTrail)
        {
            if (id != sMSAPIServiceAuditTrail.SMSAPIServiceAuditTrailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSAPIServiceAuditTrail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSAPIServiceAuditTrailExists(sMSAPIServiceAuditTrail.SMSAPIServiceAuditTrailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sMSAPIServiceAuditTrail);
        }

        // GET: SMSAPIServiceAuditTrails/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: SMSAPIServiceAuditTrails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sMSAPIServiceAuditTrail = await _context.SMSAPIServiceAuditTrail.FindAsync(id);
            _context.SMSAPIServiceAuditTrail.Remove(sMSAPIServiceAuditTrail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSAPIServiceAuditTrailExists(int id)
        {
            return _context.SMSAPIServiceAuditTrail.Any(e => e.SMSAPIServiceAuditTrailId == id);
        }
    }
}
