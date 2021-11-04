using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class InstituteTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstituteTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstituteTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstituteType.ToListAsync());
        }

        // GET: InstituteTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteType = await _context.InstituteType
                .FirstOrDefaultAsync(m => m.InstituteTypeId == id);
            if (InstituteType == null)
            {
                return NotFound();
            }

            return View(InstituteType);
        }

        // GET: InstituteTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstituteTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteTypeId,Name,Description")] InstituteType InstituteType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(InstituteType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(InstituteType);
        }

        // GET: InstituteTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteType = await _context.InstituteType.FindAsync(id);
            if (InstituteType == null)
            {
                return NotFound();
            }
            return View(InstituteType);
        }

        // POST: InstituteTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteTypeId,Name,Description")] InstituteType InstituteType)
        {
            if (id != InstituteType.InstituteTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(InstituteType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteTypeExists(InstituteType.InstituteTypeId))
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
            return View(InstituteType);
        }

        // GET: InstituteTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteType = await _context.InstituteType
                .FirstOrDefaultAsync(m => m.InstituteTypeId == id);
            if (InstituteType == null)
            {
                return NotFound();
            }

            return View(InstituteType);
        }

        // POST: InstituteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var InstituteType = await _context.InstituteType.FindAsync(id);
            _context.InstituteType.Remove(InstituteType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteTypeExists(int id)
        {
            return _context.InstituteType.Any(e => e.InstituteTypeId == id);
        }
    }
}
