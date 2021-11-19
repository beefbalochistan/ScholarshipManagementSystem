using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class ExcelColumnNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExcelColumnNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExcelColumnNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExcelColumnName.ToListAsync());
        }

        // GET: ExcelColumnNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excelColumnName = await _context.ExcelColumnName
                .FirstOrDefaultAsync(m => m.ExcelColumnNameId == id);
            if (excelColumnName == null)
            {
                return NotFound();
            }

            return View(excelColumnName);
        }

        // GET: ExcelColumnNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExcelColumnNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExcelColumnNameId,Name")] ExcelColumnName excelColumnName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excelColumnName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(excelColumnName);
        }

        // GET: ExcelColumnNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excelColumnName = await _context.ExcelColumnName.FindAsync(id);
            if (excelColumnName == null)
            {
                return NotFound();
            }
            return View(excelColumnName);
        }

        // POST: ExcelColumnNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExcelColumnNameId,Name")] ExcelColumnName excelColumnName)
        {
            if (id != excelColumnName.ExcelColumnNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excelColumnName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcelColumnNameExists(excelColumnName.ExcelColumnNameId))
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
            return View(excelColumnName);
        }

        // GET: ExcelColumnNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excelColumnName = await _context.ExcelColumnName
                .FirstOrDefaultAsync(m => m.ExcelColumnNameId == id);
            if (excelColumnName == null)
            {
                return NotFound();
            }

            return View(excelColumnName);
        }

        // POST: ExcelColumnNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excelColumnName = await _context.ExcelColumnName.FindAsync(id);
            _context.ExcelColumnName.Remove(excelColumnName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcelColumnNameExists(int id)
        {
            return _context.ExcelColumnName.Any(e => e.ExcelColumnNameId == id);
        }
    }
}
