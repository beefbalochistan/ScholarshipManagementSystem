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
    public class BEEFSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BEEFSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BEEFSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.BEEFSection.ToListAsync());
        }

        // GET: BEEFSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bEEFSection = await _context.BEEFSection
                .FirstOrDefaultAsync(m => m.BEEFSectionId == id);
            if (bEEFSection == null)
            {
                return NotFound();
            }

            return View(bEEFSection);
        }

        // GET: BEEFSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BEEFSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BEEFSectionId,Name,Description")] BEEFSection bEEFSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bEEFSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bEEFSection);
        }

        // GET: BEEFSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bEEFSection = await _context.BEEFSection.FindAsync(id);
            if (bEEFSection == null)
            {
                return NotFound();
            }
            return View(bEEFSection);
        }

        // POST: BEEFSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BEEFSectionId,Name,Description")] BEEFSection bEEFSection)
        {
            if (id != bEEFSection.BEEFSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bEEFSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BEEFSectionExists(bEEFSection.BEEFSectionId))
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
            return View(bEEFSection);
        }

        // GET: BEEFSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bEEFSection = await _context.BEEFSection
                .FirstOrDefaultAsync(m => m.BEEFSectionId == id);
            if (bEEFSection == null)
            {
                return NotFound();
            }

            return View(bEEFSection);
        }

        // POST: BEEFSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bEEFSection = await _context.BEEFSection.FindAsync(id);
            _context.BEEFSection.Remove(bEEFSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BEEFSectionExists(int id)
        {
            return _context.BEEFSection.Any(e => e.BEEFSectionId == id);
        }
    }
}
