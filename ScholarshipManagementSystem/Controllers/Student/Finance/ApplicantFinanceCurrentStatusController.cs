using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.Student.Finance;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.Student.Finance
{
    public class ApplicantFinanceCurrentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantFinanceCurrentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantFinanceCurrentStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantFinanceCurrentStatus.ToListAsync());
        }
        public async Task<IActionResult> _Index(int applicantFinanceCurrentStatusId)
        {            
            ViewBag.CurrenrPoint = _context.ApplicantFinanceCurrentStatus.Find(applicantFinanceCurrentStatusId).VisibleStateNo;
            return PartialView(await _context.ApplicantFinanceCurrentStatus.Where(a => a.Visibility == true).ToListAsync());
        }
        // GET: ApplicantFinanceCurrentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantFinanceCurrentStatus = await _context.ApplicantFinanceCurrentStatus
                .FirstOrDefaultAsync(m => m.ApplicantFinanceCurrentStatusId == id);
            if (applicantFinanceCurrentStatus == null)
            {
                return NotFound();
            }

            return View(applicantFinanceCurrentStatus);
        }

        // GET: ApplicantFinanceCurrentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantFinanceCurrentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantFinanceCurrentStatusId,ProcessState,ProcessValue,VisibleStateNo,VisibleStateText,Visibility")] ApplicantFinanceCurrentStatus applicantFinanceCurrentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantFinanceCurrentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicantFinanceCurrentStatus);
        }

        // GET: ApplicantFinanceCurrentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantFinanceCurrentStatus = await _context.ApplicantFinanceCurrentStatus.FindAsync(id);
            if (applicantFinanceCurrentStatus == null)
            {
                return NotFound();
            }
            return View(applicantFinanceCurrentStatus);
        }

        // POST: ApplicantFinanceCurrentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantFinanceCurrentStatusId,ProcessState,ProcessValue,VisibleStateNo,VisibleStateText,Visibility")] ApplicantFinanceCurrentStatus applicantFinanceCurrentStatus)
        {
            if (id != applicantFinanceCurrentStatus.ApplicantFinanceCurrentStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantFinanceCurrentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantFinanceCurrentStatusExists(applicantFinanceCurrentStatus.ApplicantFinanceCurrentStatusId))
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
            return View(applicantFinanceCurrentStatus);
        }

        // GET: ApplicantFinanceCurrentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantFinanceCurrentStatus = await _context.ApplicantFinanceCurrentStatus
                .FirstOrDefaultAsync(m => m.ApplicantFinanceCurrentStatusId == id);
            if (applicantFinanceCurrentStatus == null)
            {
                return NotFound();
            }

            return View(applicantFinanceCurrentStatus);
        }

        // POST: ApplicantFinanceCurrentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantFinanceCurrentStatus = await _context.ApplicantFinanceCurrentStatus.FindAsync(id);
            _context.ApplicantFinanceCurrentStatus.Remove(applicantFinanceCurrentStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantFinanceCurrentStatusExists(int id)
        {
            return _context.ApplicantFinanceCurrentStatus.Any(e => e.ApplicantFinanceCurrentStatusId == id);
        }
    }
}
