using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.Student;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantCurrentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantCurrentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantCurrentStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantCurrentStatus.ToListAsync());
        }

        // GET: ApplicantCurrentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantCurrentStatus = await _context.ApplicantCurrentStatus
                .FirstOrDefaultAsync(m => m.ApplicantCurrentStatusId == id);
            if (applicantCurrentStatus == null)
            {
                return NotFound();
            }

            return View(applicantCurrentStatus);
        }

        // GET: ApplicantCurrentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantCurrentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantCurrentStatusId,ProcessState,ProcessValue,IsActive")] ApplicantCurrentStatus applicantCurrentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantCurrentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicantCurrentStatus);
        }

        // GET: ApplicantCurrentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantCurrentStatus = await _context.ApplicantCurrentStatus.FindAsync(id);
            if (applicantCurrentStatus == null)
            {
                return NotFound();
            }
            return View(applicantCurrentStatus);
        }

        // POST: ApplicantCurrentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantCurrentStatusId,ProcessState,ProcessValue,IsActive")] ApplicantCurrentStatus applicantCurrentStatus)
        {
            if (id != applicantCurrentStatus.ApplicantCurrentStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantCurrentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantCurrentStatusExists(applicantCurrentStatus.ApplicantCurrentStatusId))
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
            return View(applicantCurrentStatus);
        }

        // GET: ApplicantCurrentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantCurrentStatus = await _context.ApplicantCurrentStatus
                .FirstOrDefaultAsync(m => m.ApplicantCurrentStatusId == id);
            if (applicantCurrentStatus == null)
            {
                return NotFound();
            }

            return View(applicantCurrentStatus);
        }

        // POST: ApplicantCurrentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantCurrentStatus = await _context.ApplicantCurrentStatus.FindAsync(id);
            _context.ApplicantCurrentStatus.Remove(applicantCurrentStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantCurrentStatusExists(int id)
        {
            return _context.ApplicantCurrentStatus.Any(e => e.ApplicantCurrentStatusId == id);
        }
    }
}
