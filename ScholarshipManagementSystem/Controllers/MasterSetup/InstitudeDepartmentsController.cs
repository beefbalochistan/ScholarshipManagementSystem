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
    public class InstitudeDepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstitudeDepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstitudeDepartments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InstitudeDepartment.Include(i => i.Discipline).Include(i => i.Institude);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InstitudeDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeDepartment = await _context.InstitudeDepartment
                .Include(i => i.Discipline)
                .Include(i => i.Institude)
                .FirstOrDefaultAsync(m => m.InstitudeDepartmentId == id);
            if (institudeDepartment == null)
            {
                return NotFound();
            }

            return View(institudeDepartment);
        }

        // GET: InstitudeDepartments/Create
        public IActionResult Create()
        {
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name");
            ViewData["InstitudeId"] = new SelectList(_context.Institude, "InstitudeId", "Name");
            return View();
        }

        // POST: InstitudeDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstitudeDepartmentId,Name,InstitudeId,DisciplineId,Code,Description")] InstitudeDepartment institudeDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institudeDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", institudeDepartment.DisciplineId);
            ViewData["InstitudeId"] = new SelectList(_context.Institude, "InstitudeId", "Name", institudeDepartment.InstitudeId);
            return View(institudeDepartment);
        }

        // GET: InstitudeDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeDepartment = await _context.InstitudeDepartment.FindAsync(id);
            if (institudeDepartment == null)
            {
                return NotFound();
            }
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", institudeDepartment.DisciplineId);
            ViewData["InstitudeId"] = new SelectList(_context.Institude, "InstitudeId", "Name", institudeDepartment.InstitudeId);
            return View(institudeDepartment);
        }

        // POST: InstitudeDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstitudeDepartmentId,Name,InstitudeId,DisciplineId,Code,Description")] InstitudeDepartment institudeDepartment)
        {
            if (id != institudeDepartment.InstitudeDepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institudeDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitudeDepartmentExists(institudeDepartment.InstitudeDepartmentId))
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
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", institudeDepartment.DisciplineId);
            ViewData["InstitudeId"] = new SelectList(_context.Institude, "InstitudeId", "Name", institudeDepartment.InstitudeId);
            return View(institudeDepartment);
        }

        // GET: InstitudeDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institudeDepartment = await _context.InstitudeDepartment
                .Include(i => i.Discipline)
                .Include(i => i.Institude)
                .FirstOrDefaultAsync(m => m.InstitudeDepartmentId == id);
            if (institudeDepartment == null)
            {
                return NotFound();
            }

            return View(institudeDepartment);
        }

        // POST: InstitudeDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institudeDepartment = await _context.InstitudeDepartment.FindAsync(id);
            _context.InstitudeDepartment.Remove(institudeDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitudeDepartmentExists(int id)
        {
            return _context.InstitudeDepartment.Any(e => e.InstitudeDepartmentId == id);
        }
    }
}
