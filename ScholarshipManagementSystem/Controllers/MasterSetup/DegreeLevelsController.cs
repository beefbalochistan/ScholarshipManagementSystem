using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class DegreeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DegreeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DegreeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DegreeLevel.Include(d => d.Degree);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DegreeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevel = await _context.DegreeLevel
                .Include(d => d.Degree)
                .FirstOrDefaultAsync(m => m.DegreeLevelId == id);
            if (degreeLevel == null)
            {
                return NotFound();
            }

            return View(degreeLevel);
        }

        // GET: DegreeLevels/Create
        public IActionResult Create()
        {
            ViewData["DegreeId"] = new SelectList(_context.Degree, "DegreeId", "Name");
            return View();
        }

        // POST: DegreeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DegreeLevelId,Name,Code,Year,DegreeId")] DegreeLevel degreeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degreeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeId"] = new SelectList(_context.Degree, "DegreeId", "Name", degreeLevel.DegreeId);
            return View(degreeLevel);
        }

        // GET: DegreeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevel = await _context.DegreeLevel.FindAsync(id);
            if (degreeLevel == null)
            {
                return NotFound();
            }
            ViewData["DegreeId"] = new SelectList(_context.Degree, "DegreeId", "Name", degreeLevel.DegreeId);
            return View(degreeLevel);
        }

        // POST: DegreeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DegreeLevelId,Name,Year,Code,DegreeId")] DegreeLevel degreeLevel)
        {
            if (id != degreeLevel.DegreeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degreeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeLevelExists(degreeLevel.DegreeLevelId))
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
            ViewData["DegreeId"] = new SelectList(_context.Degree, "DegreeId", "Name", degreeLevel.DegreeId);
            return View(degreeLevel);
        }

        // GET: DegreeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevel = await _context.DegreeLevel
                .Include(d => d.Degree)
                .FirstOrDefaultAsync(m => m.DegreeLevelId == id);
            if (degreeLevel == null)
            {
                return NotFound();
            }

            return View(degreeLevel);
        }

        // POST: DegreeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degreeLevel = await _context.DegreeLevel.FindAsync(id);
            _context.DegreeLevel.Remove(degreeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeLevelExists(int id)
        {
            return _context.DegreeLevel.Any(e => e.DegreeLevelId == id);
        }
    }
}
