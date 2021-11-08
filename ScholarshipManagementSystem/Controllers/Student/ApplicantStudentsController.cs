using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.Student;
using ScholarshipManagementSystem.Data;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicantStudent.Include(a => a.Applicant).Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApplicantStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent
                .Include(a => a.Applicant)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ApplicantStudentId == id);
            if (applicantStudent == null)
            {
                return NotFound();
            }

            return View(applicantStudent);
        }

        // GET: ApplicantStudents/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            return View();
        }

        // POST: ApplicantStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeniorityLevel,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Name", applicantStudent.EmployeeId);
            return View(applicantStudent);
        }

        // GET: ApplicantStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent.FindAsync(id);
            if (applicantStudent == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Name", applicantStudent.EmployeeId);
            return View(applicantStudent);
        }

        // POST: ApplicantStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeniorityLevel,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
        {
            if (id != applicantStudent.ApplicantStudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantStudentExists(applicantStudent.ApplicantStudentId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Name", applicantStudent.EmployeeId);
            return View(applicantStudent);
        }

        // GET: ApplicantStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent
                .Include(a => a.Applicant)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ApplicantStudentId == id);
            if (applicantStudent == null)
            {
                return NotFound();
            }

            return View(applicantStudent);
        }

        // POST: ApplicantStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantStudent = await _context.ApplicantStudent.FindAsync(id);
            _context.ApplicantStudent.Remove(applicantStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantStudentExists(int id)
        {
            return _context.ApplicantStudent.Any(e => e.ApplicantStudentId == id);
        }
    }
}
