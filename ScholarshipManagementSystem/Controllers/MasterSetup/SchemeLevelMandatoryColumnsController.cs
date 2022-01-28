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
    public class SchemeLevelMandatoryColumnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchemeLevelMandatoryColumnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchemeLevelMandatoryColumns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchemeLevelMandatoryColumn.Include(s => s.ExcelColumnName).Include(s => s.SchemeLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchemeLevelMandatoryColumns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelMandatoryColumn = await _context.SchemeLevelMandatoryColumn
                .Include(s => s.ExcelColumnName)
                .Include(s => s.SchemeLevel)
                .FirstOrDefaultAsync(m => m.SchemeLevelMandatoryColumnId == id);
            if (schemeLevelMandatoryColumn == null)
            {
                return NotFound();
            }

            return View(schemeLevelMandatoryColumn);
        }

        // GET: SchemeLevelMandatoryColumns/Create
        public IActionResult Create()
        {
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "Name");
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            return View();
        }

        // POST: SchemeLevelMandatoryColumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchemeLevelMandatoryColumnId,ExcelColumnNameId,SchemeLevelId")] SchemeLevelMandatoryColumn schemeLevelMandatoryColumn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schemeLevelMandatoryColumn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnName", schemeLevelMandatoryColumn.ExcelColumnNameId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelMandatoryColumn.SchemeLevelId);
            return View(schemeLevelMandatoryColumn);
        }

        // GET: SchemeLevelMandatoryColumns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelMandatoryColumn = await _context.SchemeLevelMandatoryColumn.FindAsync(id);
            if (schemeLevelMandatoryColumn == null)
            {
                return NotFound();
            }
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnName", schemeLevelMandatoryColumn.ExcelColumnNameId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelMandatoryColumn.SchemeLevelId);
            return View(schemeLevelMandatoryColumn);
        }

        // POST: SchemeLevelMandatoryColumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchemeLevelMandatoryColumnId,ExcelColumnNameId,SchemeLevelId")] SchemeLevelMandatoryColumn schemeLevelMandatoryColumn)
        {
            if (id != schemeLevelMandatoryColumn.SchemeLevelMandatoryColumnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schemeLevelMandatoryColumn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchemeLevelMandatoryColumnExists(schemeLevelMandatoryColumn.SchemeLevelMandatoryColumnId))
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
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnName", schemeLevelMandatoryColumn.ExcelColumnNameId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelMandatoryColumn.SchemeLevelId);
            return View(schemeLevelMandatoryColumn);
        }

        // GET: SchemeLevelMandatoryColumns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelMandatoryColumn = await _context.SchemeLevelMandatoryColumn
                .Include(s => s.ExcelColumnName)
                .Include(s => s.SchemeLevel)
                .FirstOrDefaultAsync(m => m.SchemeLevelMandatoryColumnId == id);
            if (schemeLevelMandatoryColumn == null)
            {
                return NotFound();
            }

            return View(schemeLevelMandatoryColumn);
        }

        // POST: SchemeLevelMandatoryColumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schemeLevelMandatoryColumn = await _context.SchemeLevelMandatoryColumn.FindAsync(id);
            _context.SchemeLevelMandatoryColumn.Remove(schemeLevelMandatoryColumn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchemeLevelMandatoryColumnExists(int id)
        {
            return _context.SchemeLevelMandatoryColumn.Any(e => e.SchemeLevelMandatoryColumnId == id);
        }
    }
}
