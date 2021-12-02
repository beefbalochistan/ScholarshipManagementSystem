using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class SectionCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SectionComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SectionComment.Include(s => s.BEEFSection).Include(s => s.SeverityLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SectionComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionComment = await _context.SectionComment
                .Include(s => s.BEEFSection)
                .Include(s => s.SeverityLevel)
                .FirstOrDefaultAsync(m => m.SectionCommentId == id);
            if (sectionComment == null)
            {
                return NotFound();
            }

            return View(sectionComment);
        }

        // GET: SectionComments/Create
        public IActionResult Create()
        {
            ViewData["BEEFSectionId"] = new SelectList(_context.BEEFSection, "BEEFSectionId", "Name");
            ViewData["SeverityLevelId"] = _context.SeverityLevel;
            return View();
        }        
        // POST: SectionComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionCommentId,Comment,BEEFSectionId,SeverityLevelId,IsActive")] SectionComment sectionComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BEEFSectionId"] = new SelectList(_context.BEEFSection, "BEEFSectionId", "Name", sectionComment.BEEFSectionId);
            ViewData["SeverityLevelId"] = new SelectList(_context.SeverityLevel, "SeverityLevelId", "Meaning", sectionComment.SeverityLevelId);
            return View(sectionComment);
        }

        // GET: SectionComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionComment = await _context.SectionComment.FindAsync(id);
            if (sectionComment == null)
            {
                return NotFound();
            }
            ViewData["BEEFSectionId"] = new SelectList(_context.BEEFSection, "BEEFSectionId", "Name", sectionComment.BEEFSectionId);
            ViewData["SeverityLevelId"] = new SelectList(_context.SeverityLevel, "SeverityLevelId", "Meaning", sectionComment.SeverityLevelId);
            return View(sectionComment);
        }

        // POST: SectionComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionCommentId,Comment,BEEFSectionId,SeverityLevelId,IsActive")] SectionComment sectionComment)
        {
            if (id != sectionComment.SectionCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionCommentExists(sectionComment.SectionCommentId))
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
            ViewData["BEEFSectionId"] = new SelectList(_context.BEEFSection, "BEEFSectionId", "Name", sectionComment.BEEFSectionId);
            ViewData["SeverityLevelId"] = new SelectList(_context.SeverityLevel, "SeverityLevelId", "Meaning", sectionComment.SeverityLevelId);
            return View(sectionComment);
        }

        // GET: SectionComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectionComment = await _context.SectionComment
                .Include(s => s.BEEFSection)
                .Include(s => s.SeverityLevel)
                .FirstOrDefaultAsync(m => m.SectionCommentId == id);
            if (sectionComment == null)
            {
                return NotFound();
            }

            return View(sectionComment);
        }

        // POST: SectionComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectionComment = await _context.SectionComment.FindAsync(id);
            _context.SectionComment.Remove(sectionComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionCommentExists(int id)
        {
            return _context.SectionComment.Any(e => e.SectionCommentId == id);
        }
    }
}
