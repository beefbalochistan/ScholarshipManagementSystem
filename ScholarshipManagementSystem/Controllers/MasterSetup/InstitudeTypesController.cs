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
    public class InstitudeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstitudeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstitudeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstitudeType.ToListAsync());
        }

        // GET: InstitudeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeType = await _context.InstitudeType
                .FirstOrDefaultAsync(m => m.InstitudeTypeId == id);
            if (institudeType == null)
            {
                return NotFound();
            }

            return View(institudeType);
        }

        // GET: InstitudeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstitudeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstitudeTypeId,Name,Description")] InstitudeType institudeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institudeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institudeType);
        }

        // GET: InstitudeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeType = await _context.InstitudeType.FindAsync(id);
            if (institudeType == null)
            {
                return NotFound();
            }
            return View(institudeType);
        }

        // POST: InstitudeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstitudeTypeId,Name,Description")] InstitudeType institudeType)
        {
            if (id != institudeType.InstitudeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institudeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitudeTypeExists(institudeType.InstitudeTypeId))
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
            return View(institudeType);
        }

        // GET: InstitudeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeType = await _context.InstitudeType
                .FirstOrDefaultAsync(m => m.InstitudeTypeId == id);
            if (institudeType == null)
            {
                return NotFound();
            }

            return View(institudeType);
        }

        // POST: InstitudeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institudeType = await _context.InstitudeType.FindAsync(id);
            _context.InstitudeType.Remove(institudeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitudeTypeExists(int id)
        {
            return _context.InstitudeType.Any(e => e.InstitudeTypeId == id);
        }
    }
}
