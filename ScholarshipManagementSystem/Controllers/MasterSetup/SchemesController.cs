using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class SchemesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchemesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schemes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Scheme.Include(s => s.Scholarship);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheme = await _context.Scheme
                .Include(s => s.Scholarship)
                .FirstOrDefaultAsync(m => m.SchemeId == id);
            if (scheme == null)
            {
                return NotFound();
            }

            return View(scheme);
        }

        // GET: Schemes/Create
        public IActionResult Create()
        {
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarship, "ScholarshipId", "Name");
            return View();
        }

        // POST: Schemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchemeId,ScholarshipId,Name,Description")] Scheme scheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarship, "ScholarshipId", "Name", scheme.ScholarshipId);
            return View(scheme);
        }

        // GET: Schemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheme = await _context.Scheme.FindAsync(id);
            if (scheme == null)
            {
                return NotFound();
            }
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarship, "ScholarshipId", "Name", scheme.ScholarshipId);
            return View(scheme);
        }

        // POST: Schemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchemeId,ScholarshipId,Name,Description")] Scheme scheme)
        {
            if (id != scheme.SchemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchemeExists(scheme.SchemeId))
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
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarship, "ScholarshipId", "Name", scheme.ScholarshipId);
            return View(scheme);
        }

        // GET: Schemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheme = await _context.Scheme
                .Include(s => s.Scholarship)
                .FirstOrDefaultAsync(m => m.SchemeId == id);
            if (scheme == null)
            {
                return NotFound();
            }

            return View(scheme);
        }

        // POST: Schemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheme = await _context.Scheme.FindAsync(id);
            _context.Scheme.Remove(scheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchemeExists(int id)
        {
            return _context.Scheme.Any(e => e.SchemeId == id);
        }
    }
}
