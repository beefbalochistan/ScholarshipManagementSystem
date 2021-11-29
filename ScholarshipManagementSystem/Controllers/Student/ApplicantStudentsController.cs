using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.Student;
using Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostEnvironment hostingEnv;

        public ApplicantStudentsController(ApplicationDbContext context, IHostEnvironment env)
        {
            this.hostingEnv = env;
            _context = context;
        }

        // GET: ApplicantStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicantStudent.Include(a => a.Applicant);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {            
            var applicationDbContext = _context.ApplicantStudent.Include(a => a.UserAccessToForward.ApplicantCurrentStatus.BEEFSection).Where(a=>a.ApplicantId == id);
            return PartialView(await applicationDbContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.ApplicantStudentId == id);
            if (applicantStudent == null)
            {
                return NotFound();
            }

            return View(applicantStudent);
        }
        
        public async Task<JsonResult> CommentSubmit(int applicantId, string applicantRefNo, string comment, int sectionCommentId, int severityLevelId, int userAccessToForwardId, int applicantCurrentStatusId)
        {
            var applicantInfo = await _context.Applicant.FindAsync(applicantId);
            var file = Request.Form.Files;
            if (applicantId != 0 && applicantRefNo != "" && comment != "" && userAccessToForwardId != 0)
            {
                ApplicantStudent obj = new ApplicantStudent();
                obj.ApplicantId = applicantId;
                obj.ApplicantReferenceId = applicantRefNo;
                obj.Comments = comment;
                obj.CreatedOn = DateTime.Now;
                if(sectionCommentId == 0)
                {
                    obj.SeverityLevelId = severityLevelId;
                }
                else
                {
                    obj.SeverityLevelId = _context.SectionComment.Find(sectionCommentId).SeverityLevelId;
                }             
                obj.ApplicantCurrentStatusId = applicantCurrentStatusId;
                obj.UserName = User.Identity.Name;                
                obj.UserAccessToForwardId = userAccessToForwardId;
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true });
            }
            return Json(new { isValid = false });
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
        public async Task<IActionResult> Create([Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeverityLevelId,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);            
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
            return View(applicantStudent);
        }

        // POST: ApplicantStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeverityLevelId,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
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

        [HttpPost]
        public async Task<string> Upload_File(int applicantId, string applicantRefNo, string comment, int sectionCommentId, int severityLevelId, int userAccessToForwardId, int applicantCurrentStatusId)
        {
            var applicantInfo = await _context.Applicant.FindAsync(applicantId);
            var file = Request.Form.Files;
            if (applicantId != 0 && applicantRefNo != "" && comment != "" && userAccessToForwardId != 0)
            {
                ApplicantStudent obj = new ApplicantStudent();
                obj.ApplicantId = applicantId;
                obj.ApplicantReferenceId = applicantRefNo;
                obj.Comments = comment;
                obj.CreatedOn = DateTime.Now;
                if (sectionCommentId == 0)
                {
                    obj.SeverityLevelId = severityLevelId;
                }
                else
                {
                    obj.SeverityLevelId = _context.SectionComment.Find(sectionCommentId).SeverityLevelId;
                }
                obj.ApplicantCurrentStatusId = applicantCurrentStatusId;
                obj.UserName = User.Identity.Name;
                obj.UserAccessToForwardId = userAccessToForwardId;
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return "Uploaded Successfully!";
            }
            return "Failed to Upload!";
        }
    }
}
