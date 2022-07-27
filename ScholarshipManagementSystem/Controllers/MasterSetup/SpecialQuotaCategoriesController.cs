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
    public class SpecialQuotaCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialQuotaCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpecialQuotaCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.SpecialQuotaCategory.ToListAsync());
        }

        // GET: SpecialQuotaCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SpecialQuotaCategory == null)
            {
                return NotFound();
            }

            var specialQuotaCategory = await _context.SpecialQuotaCategory
                .FirstOrDefaultAsync(m => m.SpecialQuotaCategoryId == id);
            if (specialQuotaCategory == null)
            {
                return NotFound();
            }

            return View(specialQuotaCategory);
        }

        // GET: SpecialQuotaCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialQuotaCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialQuotaCategoryId,CategoryName,CategoryType,PercentageValue")] SpecialQuotaCategory specialQuotaCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialQuotaCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialQuotaCategory);
        }

        // GET: SpecialQuotaCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SpecialQuotaCategory == null)
            {
                return NotFound();
            }

            var specialQuotaCategory = await _context.SpecialQuotaCategory.FindAsync(id);
            if (specialQuotaCategory == null)
            {
                return NotFound();
            }
            return View(specialQuotaCategory);
        }

        // POST: SpecialQuotaCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialQuotaCategoryId,CategoryName,CategoryType,PercentageValue")] SpecialQuotaCategory specialQuotaCategory)
        {
            if (id != specialQuotaCategory.SpecialQuotaCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialQuotaCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialQuotaCategoryExists(specialQuotaCategory.SpecialQuotaCategoryId))
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
            return View(specialQuotaCategory);
        }

        // GET: SpecialQuotaCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SpecialQuotaCategory == null)
            {
                return NotFound();
            }

            var specialQuotaCategory = await _context.SpecialQuotaCategory
                .FirstOrDefaultAsync(m => m.SpecialQuotaCategoryId == id);
            if (specialQuotaCategory == null)
            {
                return NotFound();
            }

            return View(specialQuotaCategory);
        }

        // POST: SpecialQuotaCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SpecialQuotaCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SpecialQuotaCategory'  is null.");
            }
            var specialQuotaCategory = await _context.SpecialQuotaCategory.FindAsync(id);
            if (specialQuotaCategory != null)
            {
                _context.SpecialQuotaCategory.Remove(specialQuotaCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialQuotaCategoryExists(int id)
        {
          return _context.SpecialQuotaCategory.Any(e => e.SpecialQuotaCategoryId == id);
        }
    }
}
