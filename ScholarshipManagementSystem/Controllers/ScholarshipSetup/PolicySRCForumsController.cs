using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    public class PolicySRCForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicySRCForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PolicySRCForums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PolicySRCForum.Include(p => p.ScholarshipFiscalYear);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PolicySRCForums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum
                .Include(p => p.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.PolicySRCForumId == id);
            if (policySRCForum == null)
            {
                return NotFound();
            }

            return View(policySRCForum);
        }

        // GET: PolicySRCForums/Create
        public IActionResult Create()
        {
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code");
            return View();
        }

        // POST: PolicySRCForums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicySRCForumId,Name,Description,Code,IsEndorse,SRCMinutesAttachmentPath,PolicyDocumentAttachmentPath,OtherAttachment,CreatedOn,ScholarshipFiscalYearId")] PolicySRCForum policySRCForum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policySRCForum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }

        // GET: PolicySRCForums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum.FindAsync(id);
            if (policySRCForum == null)
            {
                return NotFound();
            }
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }

        // POST: PolicySRCForums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicySRCForumId,Name,Description,Code,IsEndorse,SRCMinutesAttachmentPath,PolicyDocumentAttachmentPath,OtherAttachment,CreatedOn,ScholarshipFiscalYearId")] PolicySRCForum policySRCForum)
        {
            if (id != policySRCForum.PolicySRCForumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policySRCForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicySRCForumExists(policySRCForum.PolicySRCForumId))
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
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }

        // GET: PolicySRCForums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum
                .Include(p => p.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.PolicySRCForumId == id);
            if (policySRCForum == null)
            {
                return NotFound();
            }

            return View(policySRCForum);
        }

        // POST: PolicySRCForums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policySRCForum = await _context.PolicySRCForum.FindAsync(id);
            _context.PolicySRCForum.Remove(policySRCForum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicySRCForumExists(int id)
        {
            return _context.PolicySRCForum.Any(e => e.PolicySRCForumId == id);
        }
    }
}
