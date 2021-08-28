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
    public class SMSAPIServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSAPIServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMSAPIServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSAPIService.ToListAsync());
        }

        // GET: SMSAPIServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAPIService = await _context.SMSAPIService
                .FirstOrDefaultAsync(m => m.SMSAPIServiceId == id);
            if (sMSAPIService == null)
            {
                return NotFound();
            }

            return View(sMSAPIService);
        }

        // GET: SMSAPIServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSAPIServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SMSAPIServiceId,Username,Password,Mask,Description")] SMSAPIService sMSAPIService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSAPIService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSAPIService);
        }

        // GET: SMSAPIServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAPIService = await _context.SMSAPIService.FindAsync(id);
            if (sMSAPIService == null)
            {
                return NotFound();
            }
            return View(sMSAPIService);
        }

        // POST: SMSAPIServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SMSAPIServiceId,Username,Password,Mask,Description")] SMSAPIService sMSAPIService)
        {
            if (id != sMSAPIService.SMSAPIServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSAPIService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSAPIServiceExists(sMSAPIService.SMSAPIServiceId))
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
            return View(sMSAPIService);
        }

        // GET: SMSAPIServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAPIService = await _context.SMSAPIService
                .FirstOrDefaultAsync(m => m.SMSAPIServiceId == id);
            if (sMSAPIService == null)
            {
                return NotFound();
            }

            return View(sMSAPIService);
        }

        // POST: SMSAPIServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sMSAPIService = await _context.SMSAPIService.FindAsync(id);
            _context.SMSAPIService.Remove(sMSAPIService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSAPIServiceExists(int id)
        {
            return _context.SMSAPIService.Any(e => e.SMSAPIServiceId == id);
        }
    }
}
