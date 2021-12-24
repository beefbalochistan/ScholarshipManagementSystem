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
    public class DocumentAssistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentAssistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DocumentAssists
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentAssist.ToListAsync());
        }

        // GET: DocumentAssists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssist = await _context.DocumentAssist
                .FirstOrDefaultAsync(m => m.DocumentAssistId == id);
            if (documentAssist == null)
            {
                return NotFound();
            }

            return View(documentAssist);
        }

        // GET: DocumentAssists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentAssists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentAssistId,ConditionalOperator")] DocumentAssist documentAssist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentAssist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentAssist);
        }

        // GET: DocumentAssists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssist = await _context.DocumentAssist.FindAsync(id);
            if (documentAssist == null)
            {
                return NotFound();
            }
            return View(documentAssist);
        }

        // POST: DocumentAssists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentAssistId,ConditionalOperator")] DocumentAssist documentAssist)
        {
            if (id != documentAssist.DocumentAssistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentAssist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentAssistExists(documentAssist.DocumentAssistId))
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
            return View(documentAssist);
        }

        // GET: DocumentAssists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssist = await _context.DocumentAssist
                .FirstOrDefaultAsync(m => m.DocumentAssistId == id);
            if (documentAssist == null)
            {
                return NotFound();
            }

            return View(documentAssist);
        }

        // POST: DocumentAssists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentAssist = await _context.DocumentAssist.FindAsync(id);
            _context.DocumentAssist.Remove(documentAssist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentAssistExists(int id)
        {
            return _context.DocumentAssist.Any(e => e.DocumentAssistId == id);
        }
    }
}
