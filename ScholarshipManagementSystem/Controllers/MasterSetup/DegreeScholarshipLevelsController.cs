using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class DegreeScholarshipLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DegreeScholarshipLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DegreeScholarshipLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DegreeScholarshipLevel.Include(d => d.DegreeLevel).Include(d => d.SchemeLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DegreeScholarshipLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeScholarshipLevel = await _context.DegreeScholarshipLevel
                .Include(d => d.DegreeLevel)
                .Include(d => d.SchemeLevel)
                .FirstOrDefaultAsync(m => m.DegreeScholarshipLevelId == id);
            if (degreeScholarshipLevel == null)
            {
                return NotFound();
            }

            return View(degreeScholarshipLevel);
        }

        // GET: DegreeScholarshipLevels/Create
        public IActionResult Create()
        {
            ViewData["DegreeLevelId"] = new SelectList(_context.DegreeLevel, "DegreeLevelId", "Name");
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            return View();
        }

        // POST: DegreeScholarshipLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DegreeScholarshipLevelId,Name,SchemeLevelId,IsActive,Enrollment,Slot,DegreeLevelId")] DegreeScholarshipLevel degreeScholarshipLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degreeScholarshipLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeLevelId"] = new SelectList(_context.DegreeLevel, "DegreeLevelId", "Name", degreeScholarshipLevel.DegreeLevelId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", degreeScholarshipLevel.SchemeLevelId);
            return View(degreeScholarshipLevel);
        }

        // GET: DegreeScholarshipLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeScholarshipLevel = await _context.DegreeScholarshipLevel.FindAsync(id);
            if (degreeScholarshipLevel == null)
            {
                return NotFound();
            }
            ViewData["DegreeLevelId"] = new SelectList(_context.DegreeLevel, "DegreeLevelId", "Name", degreeScholarshipLevel.DegreeLevelId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", degreeScholarshipLevel.SchemeLevelId);
            return View(degreeScholarshipLevel);
        }

        // POST: DegreeScholarshipLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DegreeScholarshipLevelId,Name,SchemeLevelId,IsActive,Enrollment,Slot,DegreeLevelId")] DegreeScholarshipLevel degreeScholarshipLevel)
        {
            if (id != degreeScholarshipLevel.DegreeScholarshipLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degreeScholarshipLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeScholarshipLevelExists(degreeScholarshipLevel.DegreeScholarshipLevelId))
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
            ViewData["DegreeLevelId"] = new SelectList(_context.DegreeLevel, "DegreeLevelId", "Name", degreeScholarshipLevel.DegreeLevelId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", degreeScholarshipLevel.SchemeLevelId);
            return View(degreeScholarshipLevel);
        }

        // GET: DegreeScholarshipLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeScholarshipLevel = await _context.DegreeScholarshipLevel
                .Include(d => d.DegreeLevel)
                .Include(d => d.SchemeLevel)
                .FirstOrDefaultAsync(m => m.DegreeScholarshipLevelId == id);
            if (degreeScholarshipLevel == null)
            {
                return NotFound();
            }

            return View(degreeScholarshipLevel);
        }

        // POST: DegreeScholarshipLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degreeScholarshipLevel = await _context.DegreeScholarshipLevel.FindAsync(id);
            _context.DegreeScholarshipLevel.Remove(degreeScholarshipLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeScholarshipLevelExists(int id)
        {
            return _context.DegreeScholarshipLevel.Any(e => e.DegreeScholarshipLevelId == id);
        }
    }
}
