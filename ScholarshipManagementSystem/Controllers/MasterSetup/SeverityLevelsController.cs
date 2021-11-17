using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Data;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class SeverityLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeverityLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SeverityLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.SeverityLevel.ToListAsync());
        }

        // GET: SeverityLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var severityLevel = await _context.SeverityLevel
                .FirstOrDefaultAsync(m => m.SeverityLevelId == id);
            if (severityLevel == null)
            {
                return NotFound();
            }

            return View(severityLevel);
        }

        // GET: SeverityLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeverityLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeverityLevelId,Level,Meaning,Color")] SeverityLevel severityLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(severityLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(severityLevel);
        }

        // GET: SeverityLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var severityLevel = await _context.SeverityLevel.FindAsync(id);
            if (severityLevel == null)
            {
                return NotFound();
            }
            return View(severityLevel);
        }

        // POST: SeverityLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeverityLevelId,Level,Meaning,Color")] SeverityLevel severityLevel)
        {
            if (id != severityLevel.SeverityLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(severityLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeverityLevelExists(severityLevel.SeverityLevelId))
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
            return View(severityLevel);
        }

        // GET: SeverityLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var severityLevel = await _context.SeverityLevel
                .FirstOrDefaultAsync(m => m.SeverityLevelId == id);
            if (severityLevel == null)
            {
                return NotFound();
            }

            return View(severityLevel);
        }

        // POST: SeverityLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var severityLevel = await _context.SeverityLevel.FindAsync(id);
            _context.SeverityLevel.Remove(severityLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeverityLevelExists(int id)
        {
            return _context.SeverityLevel.Any(e => e.SeverityLevelId == id);
        }
    }
}
