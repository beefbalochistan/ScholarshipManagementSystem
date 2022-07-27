using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class AnnualBudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnualBudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnnualBudgets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnnualBudget.Include(a => a.BudgetLevel).Include(a => a.ScholarshipFiscalYear);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AnnualBudgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnnualBudget == null)
            {
                return NotFound();
            }

            var annualBudget = await _context.AnnualBudget
                .Include(a => a.BudgetLevel)
                .Include(a => a.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.AnnualBudgetId == id);
            if (annualBudget == null)
            {
                return NotFound();
            }

            return View(annualBudget);
        }

        // GET: AnnualBudgets/Create
        public IActionResult Create()
        {
            ViewData["BudgetLevelId"] = new SelectList(_context.BudgetLevel, "BudgetLevelId", "BudgetLevelId");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code");
            return View();
        }

        // POST: AnnualBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnnualBudgetId,POMQuota,DOMSQuota,SpecialQuota,DeclineQuota,MeetingName,MeetingReferancNo,Description,OnDate,UserId,BudgetType,ScholarshipFiscalYearId,BudgetLevelId")] AnnualBudget annualBudget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(annualBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetLevelId"] = new SelectList(_context.BudgetLevel, "BudgetLevelId", "BudgetLevelId", annualBudget.BudgetLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", annualBudget.ScholarshipFiscalYearId);
            return View(annualBudget);
        }

        // GET: AnnualBudgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnnualBudget == null)
            {
                return NotFound();
            }

            var annualBudget = await _context.AnnualBudget.FindAsync(id);
            if (annualBudget == null)
            {
                return NotFound();
            }
            ViewData["BudgetLevelId"] = new SelectList(_context.BudgetLevel, "BudgetLevelId", "BudgetLevelId", annualBudget.BudgetLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", annualBudget.ScholarshipFiscalYearId);
            return View(annualBudget);
        }

        // POST: AnnualBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnnualBudgetId,POMQuota,DOMSQuota,SpecialQuota,DeclineQuota,MeetingName,MeetingReferancNo,Description,OnDate,UserId,BudgetType,ScholarshipFiscalYearId,BudgetLevelId")] AnnualBudget annualBudget)
        {
            if (id != annualBudget.AnnualBudgetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annualBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnualBudgetExists(annualBudget.AnnualBudgetId))
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
            ViewData["BudgetLevelId"] = new SelectList(_context.BudgetLevel, "BudgetLevelId", "BudgetLevelId", annualBudget.BudgetLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", annualBudget.ScholarshipFiscalYearId);
            return View(annualBudget);
        }

        // GET: AnnualBudgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnnualBudget == null)
            {
                return NotFound();
            }

            var annualBudget = await _context.AnnualBudget
                .Include(a => a.BudgetLevel)
                .Include(a => a.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.AnnualBudgetId == id);
            if (annualBudget == null)
            {
                return NotFound();
            }

            return View(annualBudget);
        }

        // POST: AnnualBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnnualBudget == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnnualBudget'  is null.");
            }
            var annualBudget = await _context.AnnualBudget.FindAsync(id);
            if (annualBudget != null)
            {
                _context.AnnualBudget.Remove(annualBudget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnualBudgetExists(int id)
        {
          return _context.AnnualBudget.Any(e => e.AnnualBudgetId == id);
        }
    }
}
