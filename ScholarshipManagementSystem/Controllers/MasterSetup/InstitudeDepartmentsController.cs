using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class InstituteDepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstituteDepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstituteDepartments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InstituteDepartment/*.Include(i => i.Discipline).Include(i => i.InstituteFaculty)*/;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InstituteDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteDepartment = await _context.InstituteDepartment
                .Include(i => i.Discipline)
                .Include(i => i.InstituteFaculty)
                .FirstOrDefaultAsync(m => m.InstituteDepartmentId == id);
            if (InstituteDepartment == null)
            {
                return NotFound();
            }

            return View(InstituteDepartment);
        }

        // GET: InstituteDepartments/Create
        public IActionResult Create()
        {
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name");
            ViewData["InstituteFacultyId"] = new SelectList(_context.InstituteFaculty, "InstituteFacultyId", "Name");
            return View();
        }

        // POST: InstituteDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteDepartmentId,Name,InstituteFacultyId,DisciplineId,Code,Description")] InstituteDepartment InstituteDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(InstituteDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", InstituteDepartment.DisciplineId);
            ViewData["InstituteFacultyId"] = new SelectList(_context.InstituteFaculty, "InstituteFacultyId", "Name", InstituteDepartment.InstituteFacultyId);
            return View(InstituteDepartment);
        }

        // GET: InstituteDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteDepartment = await _context.InstituteDepartment.FindAsync(id);
            if (InstituteDepartment == null)
            {
                return NotFound();
            }
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", InstituteDepartment.DisciplineId);
            ViewData["InstituteFacultyId"] = new SelectList(_context.InstituteFaculty, "InstituteFacultyId", "Name", InstituteDepartment.InstituteFacultyId);
            return View(InstituteDepartment);
        }

        // POST: InstituteDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteDepartmentId,Name,InstituteFacultyId,DisciplineId,Code,Description")] InstituteDepartment InstituteDepartment)
        {
            if (id != InstituteDepartment.InstituteDepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(InstituteDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteDepartmentExists(InstituteDepartment.InstituteDepartmentId))
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
            ViewData["DisciplineId"] = new SelectList(_context.Discipline, "DisciplineId", "Name", InstituteDepartment.DisciplineId);
            ViewData["InstituteFacultyId"] = new SelectList(_context.InstituteFaculty, "InstituteFacultyId", "Name", InstituteDepartment.InstituteFacultyId);
            return View(InstituteDepartment);
        }

        // GET: InstituteDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var InstituteDepartment = await _context.InstituteDepartment
                .Include(i => i.Discipline)
                .Include(i => i.InstituteFaculty)
                .FirstOrDefaultAsync(m => m.InstituteDepartmentId == id);
            if (InstituteDepartment == null)
            {
                return NotFound();
            }

            return View(InstituteDepartment);
        }

        // POST: InstituteDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var InstituteDepartment = await _context.InstituteDepartment.FindAsync(id);
            _context.InstituteDepartment.Remove(InstituteDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteDepartmentExists(int id)
        {
            return _context.InstituteDepartment.Any(e => e.InstituteDepartmentId == id);
        }
    }
}
