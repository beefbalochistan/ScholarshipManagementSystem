using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    [AllowAnonymous]
    public class SchemeLevelPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchemeLevelPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchemeLevelPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchemeLevelPayment.Include(s => s.SchemeLevel).Include(s => s.ScholarshipFiscalYear);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchemeLevelPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPayment = await _context.SchemeLevelPayment
                .Include(s => s.SchemeLevel)
                .Include(s => s.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.SchemeLevelPaymentId == id);
            if (schemeLevelPayment == null)
            {
                return NotFound();
            }

            return View(schemeLevelPayment);
        }

        // GET: SchemeLevelPayments/Create
        public IActionResult Create()
        {
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code");
            return View();
        }

        // POST: SchemeLevelPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchemeLevelPaymentId,Name,SchemeLevelId,ScholarshipFiscalYearId,Amount")] SchemeLevelPayment schemeLevelPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schemeLevelPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPayment.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", schemeLevelPayment.ScholarshipFiscalYearId);
            return View(schemeLevelPayment);
        }

        // GET: SchemeLevelPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPayment = await _context.SchemeLevelPayment.FindAsync(id);
            if (schemeLevelPayment == null)
            {
                return NotFound();
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPayment.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", schemeLevelPayment.ScholarshipFiscalYearId);
            return View(schemeLevelPayment);
        }

        // POST: SchemeLevelPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchemeLevelPaymentId,Name,SchemeLevelId,ScholarshipFiscalYearId,Amount")] SchemeLevelPayment schemeLevelPayment)
        {
            if (id != schemeLevelPayment.SchemeLevelPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schemeLevelPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchemeLevelPaymentExists(schemeLevelPayment.SchemeLevelPaymentId))
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
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPayment.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", schemeLevelPayment.ScholarshipFiscalYearId);
            return View(schemeLevelPayment);
        }

        // GET: SchemeLevelPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPayment = await _context.SchemeLevelPayment
                .Include(s => s.SchemeLevel)
                .Include(s => s.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.SchemeLevelPaymentId == id);
            if (schemeLevelPayment == null)
            {
                return NotFound();
            }

            return View(schemeLevelPayment);
        }

        // POST: SchemeLevelPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schemeLevelPayment = await _context.SchemeLevelPayment.FindAsync(id);
            _context.SchemeLevelPayment.Remove(schemeLevelPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchemeLevelPaymentExists(int id)
        {
            return _context.SchemeLevelPayment.Any(e => e.SchemeLevelPaymentId == id);
        }
    }
}
