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
    public class ProviencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProviencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proviences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provience.ToListAsync());
        }

        // GET: Proviences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provience = await _context.Provience
                .FirstOrDefaultAsync(m => m.ProvienceId == id);
            if (provience == null)
            {
                return NotFound();
            }

            return View(provience);
        }

        // GET: Proviences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proviences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvienceId,Name,Code")] Provience provience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provience);
        }

        // GET: Proviences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provience = await _context.Provience.FindAsync(id);
            if (provience == null)
            {
                return NotFound();
            }
            return View(provience);
        }

        // POST: Proviences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProvienceId,Name,Code")] Provience provience)
        {
            if (id != provience.ProvienceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvienceExists(provience.ProvienceId))
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
            return View(provience);
        }

        // GET: Proviences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provience = await _context.Provience
                .FirstOrDefaultAsync(m => m.ProvienceId == id);
            if (provience == null)
            {
                return NotFound();
            }

            return View(provience);
        }

        // POST: Proviences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provience = await _context.Provience.FindAsync(id);
            _context.Provience.Remove(provience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvienceExists(int id)
        {
            return _context.Provience.Any(e => e.ProvienceId == id);
        }
    }
}
