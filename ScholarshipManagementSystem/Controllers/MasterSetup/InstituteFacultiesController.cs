using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class InstituteFacultiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstituteFacultiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstituteFaculties
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.InstituteName = _context.Institute.Find(id).Name;
            var applicationDbContext = _context.InstituteFaculty.Include(i => i.Faculty).Include(i => i.Institute).Where(a=>a.InstituteId == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexInstitute()
        {
            var applicationDbContext = _context.Institute;
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: InstituteFaculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteFaculty = await _context.InstituteFaculty
                .Include(i => i.Faculty)
                .Include(i => i.Institute)
                .FirstOrDefaultAsync(m => m.InstituteFacultyId == id);
            if (instituteFaculty == null)
            {
                return NotFound();
            }

            return View(instituteFaculty);
        }

        // GET: InstituteFaculties/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "FacultyId");
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "InstituteId");
            return View();
        }

        // POST: InstituteFaculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteFacultyId,Name,InstituteId,FocalPersonName,FocalPersonContactNo,FocalPersonEmail,FocalPersonDesignation,FacultyId,InstituteId")] InstituteFaculty instituteFaculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instituteFaculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "Name", instituteFaculty.FacultyId);
            ViewData["Institute"] = new SelectList(_context.Institute, "Institute", "Name", instituteFaculty.InstituteId);
            return View(instituteFaculty);
        }

        // GET: InstituteFaculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteFaculty = await _context.InstituteFaculty.FindAsync(id);
            if (instituteFaculty == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "Name", instituteFaculty.FacultyId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "Name", instituteFaculty.InstituteId);
            return View(instituteFaculty);
        }

        // POST: InstituteFaculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteFacultyId,InstituteId,Name,FocalPersonName,FocalPersonContactNo,FocalPersonEmail,FocalPersonDesignation,FacultyId,InstituteId")] InstituteFaculty instituteFaculty)
        {
            if (id != instituteFaculty.InstituteFacultyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteFaculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteFacultyExists(instituteFaculty.InstituteFacultyId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "FacultyId", instituteFaculty.FacultyId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "InstituteId", instituteFaculty.InstituteId);
            return View(instituteFaculty);
        }

        // GET: InstituteFaculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteFaculty = await _context.InstituteFaculty
                .Include(i => i.Faculty)
                .Include(i => i.Institute)
                .FirstOrDefaultAsync(m => m.InstituteFacultyId == id);
            if (instituteFaculty == null)
            {
                return NotFound();
            }

            return View(instituteFaculty);
        }

        // POST: InstituteFaculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instituteFaculty = await _context.InstituteFaculty.FindAsync(id);
            _context.InstituteFaculty.Remove(instituteFaculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteFacultyExists(int id)
        {
            return _context.InstituteFaculty.Any(e => e.InstituteFacultyId == id);
        }
    }
}
