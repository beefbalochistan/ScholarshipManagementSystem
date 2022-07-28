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
    public class BudgetLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BudgetLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BudgetLevel.Include(b => b.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BudgetLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BudgetLevel == null)
            {
                return NotFound();
            }

            var budgetLevel = await _context.BudgetLevel
                .Include(b => b.PaymentMethod)
                .FirstOrDefaultAsync(m => m.BudgetLevelId == id);
            if (budgetLevel == null)
            {
                return NotFound();
            }

            return View(budgetLevel);
        }

        // GET: BudgetLevels/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");
            return View();
        }

        // POST: BudgetLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetLevelId,BudgetLevelName,AnualStipend,PaymentMethodId")] BudgetLevel budgetLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", budgetLevel.PaymentMethodId);
            return View(budgetLevel);
        }

        // GET: BudgetLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BudgetLevel == null)
            {
                return NotFound();
            }

            var budgetLevel = await _context.BudgetLevel.FindAsync(id);
            if (budgetLevel == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", budgetLevel.PaymentMethodId);
            return View(budgetLevel);
        }

        // POST: BudgetLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetLevelId,BudgetLevelName,AnualStipend,PaymentMethodId")] BudgetLevel budgetLevel)
        {
            if (id != budgetLevel.BudgetLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetLevelExists(budgetLevel.BudgetLevelId))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", budgetLevel.PaymentMethodId);
            return View(budgetLevel);
        }

        // GET: BudgetLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BudgetLevel == null)
            {
                return NotFound();
            }

            var budgetLevel = await _context.BudgetLevel
                .Include(b => b.PaymentMethod)
                .FirstOrDefaultAsync(m => m.BudgetLevelId == id);
            if (budgetLevel == null)
            {
                return NotFound();
            }

            return View(budgetLevel);
        }

        // POST: BudgetLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BudgetLevel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BudgetLevel'  is null.");
            }
            var budgetLevel = await _context.BudgetLevel.FindAsync(id);
            if (budgetLevel != null)
            {
                _context.BudgetLevel.Remove(budgetLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetLevelExists(int id)
        {
          return _context.BudgetLevel.Any(e => e.BudgetLevelId == id);
        }
    }
}
