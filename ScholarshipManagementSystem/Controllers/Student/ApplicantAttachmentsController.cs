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
    public class ApplicantAttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantAttachmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantAttachments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicantAttachment.Include(a => a.Applicant);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            //var applicationDbContext = _context.ApplicantAttachment.Include(a=>a.Applicant).Where(a=>a.ApplicantId == id).Select(a=> new ApplicantAttachment { Title = a.Title, AttachmentPath = a.AttachmentPath, UploadedOn = a.UploadedOn, ApplicantId = a.ApplicantId, Applicant = new Applicant { ScanDocument = a.Applicant.ScanDocument } });
            var applicationDbContext = _context.ApplicantAttachment.Where(a=>a.ApplicantId == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _IndexCompare(int id)
        {
            var applicationDbContext = await _context.ApplicantAttachment.Where(a=>a.ApplicantId == id).ToListAsync();            
            ViewData["ApplicantAttachmentId"] = new SelectList(_context.ApplicantAttachment.Where(a => a.ApplicantId == id), "AttachmentPath", "Title");
            return PartialView(applicationDbContext);
        }
        public async Task<IActionResult> _ApplicantViewer(int id)
        {
            var applicationDbContext = await _context.Applicant.Include(a=>a.SelectionMethod).Where(a=>a.ApplicantId == id).FirstOrDefaultAsync();
            return PartialView(applicationDbContext);
        }
        // GET: ApplicantAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantAttachment = await _context.ApplicantAttachment
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.ApplicantAttachmentId == id);
            if (applicantAttachment == null)
            {
                return NotFound();
            }

            return View(applicantAttachment);
        }

        // GET: ApplicantAttachments/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo");
            return View();
        }

        // POST: ApplicantAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantAttachmentId,Title,AttachmentPath,UploadedOn,ApplicantId")] ApplicantAttachment applicantAttachment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantAttachment.ApplicantId);
            return View(applicantAttachment);
        }

        // GET: ApplicantAttachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantAttachment = await _context.ApplicantAttachment.FindAsync(id);
            if (applicantAttachment == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantAttachment.ApplicantId);
            return View(applicantAttachment);
        }

        // POST: ApplicantAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantAttachmentId,Title,AttachmentPath,UploadedOn,ApplicantId")] ApplicantAttachment applicantAttachment)
        {
            if (id != applicantAttachment.ApplicantAttachmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantAttachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantAttachmentExists(applicantAttachment.ApplicantAttachmentId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantAttachment.ApplicantId);
            return View(applicantAttachment);
        }

        // GET: ApplicantAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantAttachment = await _context.ApplicantAttachment
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.ApplicantAttachmentId == id);
            if (applicantAttachment == null)
            {
                return NotFound();
            }

            return View(applicantAttachment);
        }

        // POST: ApplicantAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantAttachment = await _context.ApplicantAttachment.FindAsync(id);
            _context.ApplicantAttachment.Remove(applicantAttachment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantAttachmentExists(int id)
        {
            return _context.ApplicantAttachment.Any(e => e.ApplicantAttachmentId == id);
        }
    }
}
