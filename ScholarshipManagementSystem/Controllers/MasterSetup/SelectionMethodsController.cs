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
    public class SelectionMethodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectionMethodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SelectionMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.SelectionMethod.ToListAsync());
        }

        // GET: SelectionMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionMethod = await _context.SelectionMethod
                .FirstOrDefaultAsync(m => m.SelectionMethodId == id);
            if (selectionMethod == null)
            {
                return NotFound();
            }

            return View(selectionMethod);
        }

        // GET: SelectionMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SelectionMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectionMethodId,Name")] SelectionMethod selectionMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selectionMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(selectionMethod);
        }

        // GET: SelectionMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionMethod = await _context.SelectionMethod.FindAsync(id);
            if (selectionMethod == null)
            {
                return NotFound();
            }
            return View(selectionMethod);
        }

        // POST: SelectionMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectionMethodId,Name")] SelectionMethod selectionMethod)
        {
            if (id != selectionMethod.SelectionMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectionMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectionMethodExists(selectionMethod.SelectionMethodId))
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
            return View(selectionMethod);
        }

        // GET: SelectionMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionMethod = await _context.SelectionMethod
                .FirstOrDefaultAsync(m => m.SelectionMethodId == id);
            if (selectionMethod == null)
            {
                return NotFound();
            }

            return View(selectionMethod);
        }

        // POST: SelectionMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selectionMethod = await _context.SelectionMethod.FindAsync(id);
            _context.SelectionMethod.Remove(selectionMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectionMethodExists(int id)
        {
            return _context.SelectionMethod.Any(e => e.SelectionMethodId == id);
        }
    }
}
