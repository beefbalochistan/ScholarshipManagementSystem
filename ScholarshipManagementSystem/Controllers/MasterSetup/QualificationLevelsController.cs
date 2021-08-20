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
    public class QualificationLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QualificationLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QualificationLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.QualificationLevel.ToListAsync());
        }

        // GET: QualificationLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationLevel = await _context.QualificationLevel
                .FirstOrDefaultAsync(m => m.QualificationLevelId == id);
            if (qualificationLevel == null)
            {
                return NotFound();
            }

            return View(qualificationLevel);
        }

        // GET: QualificationLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QualificationLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QualificationLevelId,Name,Code,Description")] QualificationLevel qualificationLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualificationLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qualificationLevel);
        }

        // GET: QualificationLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationLevel = await _context.QualificationLevel.FindAsync(id);
            if (qualificationLevel == null)
            {
                return NotFound();
            }
            return View(qualificationLevel);
        }

        // POST: QualificationLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualificationLevelId,Name,Code,Description")] QualificationLevel qualificationLevel)
        {
            if (id != qualificationLevel.QualificationLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualificationLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationLevelExists(qualificationLevel.QualificationLevelId))
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
            return View(qualificationLevel);
        }

        // GET: QualificationLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualificationLevel = await _context.QualificationLevel
                .FirstOrDefaultAsync(m => m.QualificationLevelId == id);
            if (qualificationLevel == null)
            {
                return NotFound();
            }

            return View(qualificationLevel);
        }

        // POST: QualificationLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualificationLevel = await _context.QualificationLevel.FindAsync(id);
            _context.QualificationLevel.Remove(qualificationLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualificationLevelExists(int id)
        {
            return _context.QualificationLevel.Any(e => e.QualificationLevelId == id);
        }
    }
}
