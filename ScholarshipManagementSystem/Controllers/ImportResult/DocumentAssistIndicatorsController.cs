using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.ImportResult;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.ImportResult
{
    public class DocumentAssistIndicatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentAssistIndicatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DocumentAssistIndicators
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DocumentAssistIndicator.Include(d => d.DocumentAssist).Include(d => d.ExcelColumnName).Include(d => d.ResultRepository);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DocumentAssistIndicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .Include(d => d.ResultRepository)
                .FirstOrDefaultAsync(m => m.DocumentAssistIndicatorId == id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }

            return View(documentAssistIndicator);
        }

        // GET: DocumentAssistIndicators/Create
        public IActionResult Create()
        {
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId");
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId");
            return View();
        }

        // POST: DocumentAssistIndicators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentAssistIndicatorId,ExcelColumnNameId,DocumentAssistId,ResultRepositoryId")] DocumentAssistIndicator documentAssistIndicator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentAssistIndicator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // GET: DocumentAssistIndicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator.FindAsync(id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // POST: DocumentAssistIndicators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentAssistIndicatorId,ExcelColumnNameId,DocumentAssistId,ResultRepositoryId")] DocumentAssistIndicator documentAssistIndicator)
        {
            if (id != documentAssistIndicator.DocumentAssistIndicatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentAssistIndicator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentAssistIndicatorExists(documentAssistIndicator.DocumentAssistIndicatorId))
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
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // GET: DocumentAssistIndicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .Include(d => d.ResultRepository)
                .FirstOrDefaultAsync(m => m.DocumentAssistIndicatorId == id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }

            return View(documentAssistIndicator);
        }

        // POST: DocumentAssistIndicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentAssistIndicator = await _context.DocumentAssistIndicator.FindAsync(id);
            _context.DocumentAssistIndicator.Remove(documentAssistIndicator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentAssistIndicatorExists(int id)
        {
            return _context.DocumentAssistIndicator.Any(e => e.DocumentAssistIndicatorId == id);
        }
    }
}
