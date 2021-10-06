using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.Student;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applicant.Include(a => a.DegreeScholarshipLevel).Include(a => a.District).Include(a => a.Provience).Include(a => a.SchemeLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .Include(a => a.DegreeScholarshipLevel)
                .Include(a => a.District)
                .Include(a => a.Provience)
                .Include(a => a.SchemeLevel)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code");
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Code");
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantId,Name,FatherName,DateOfBirth,BFormCNIC,FatherCareTakerCNIC,StudentMobile,FatherMobile,RelationWithCareTaker,Religion,HomeAddress,DistrictId,ProvienceId,SchemeLevelId,DegreeScholarshipLevelId,ApplicantReferenceNo,TehsilName,Gender,Email,Year,CurrentInsituteName,CurrentInsituteHOD,CurrentInsituteFocalPerson,CurrentInsituteFocalDesignation,CurrentInsituteFocalMobile,CurrentInsituteFocalEmail,CurrentInsitutePhone,CurrentInsituteFax,CurrentInsituteAddress,RollNumber,TotalMarks,TotalGPA,ReceivedMarks,ReceivedCGPA,OldInstitudeNameAddress,NameBoardUniversity,TelephoneWithCode,Picture,ScanDocument,ScanOtherDocument,SelectionStatus,SelectedMethod")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId", applicant.DegreeScholarshipLevelId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", applicant.DistrictId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Code", applicant.ProvienceId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", applicant.SchemeLevelId);
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId", applicant.DegreeScholarshipLevelId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", applicant.DistrictId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Code", applicant.ProvienceId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", applicant.SchemeLevelId);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,Name,FatherName,DateOfBirth,BFormCNIC,FatherCareTakerCNIC,StudentMobile,FatherMobile,RelationWithCareTaker,Religion,HomeAddress,DistrictId,ProvienceId,SchemeLevelId,DegreeScholarshipLevelId,ApplicantReferenceNo,TehsilName,Gender,Email,Year,CurrentInsituteName,CurrentInsituteHOD,CurrentInsituteFocalPerson,CurrentInsituteFocalDesignation,CurrentInsituteFocalMobile,CurrentInsituteFocalEmail,CurrentInsitutePhone,CurrentInsituteFax,CurrentInsituteAddress,RollNumber,TotalMarks,TotalGPA,ReceivedMarks,ReceivedCGPA,OldInstitudeNameAddress,NameBoardUniversity,TelephoneWithCode,Picture,ScanDocument,ScanOtherDocument,SelectionStatus,SelectedMethod")] Applicant applicant)
        {
            if (id != applicant.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantId))
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
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId", applicant.DegreeScholarshipLevelId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", applicant.DistrictId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Code", applicant.ProvienceId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", applicant.SchemeLevelId);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .Include(a => a.DegreeScholarshipLevel)
                .Include(a => a.District)
                .Include(a => a.Provience)
                .Include(a => a.SchemeLevel)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            _context.Applicant.Remove(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.ApplicantId == id);
        }
    }
}
