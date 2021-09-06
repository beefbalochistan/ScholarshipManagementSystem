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
    public class DAEInstituteQoutaBySchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DAEInstituteQoutaBySchemeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DAEInstituteQoutaBySchemeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DAEInstituteQoutaBySchemeLevel.Include(d => d.DAEInstitute).Include(d => d.SRCForum).Include(d => d.SchemeLevelPolicy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DAEInstituteQoutaBySchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstituteQoutaBySchemeLevel = await _context.DAEInstituteQoutaBySchemeLevel
                .Include(d => d.DAEInstitute)
                .Include(d => d.SRCForum)
                .Include(d => d.SchemeLevelPolicy)
                .FirstOrDefaultAsync(m => m.DAEInstituteQoutaBySchemeLevelId == id);
            if (dAEInstituteQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(dAEInstituteQoutaBySchemeLevel);
        }

        // GET: DAEInstituteQoutaBySchemeLevels/Create
        public IActionResult Create()
        {
            ViewData["DAEInstituteId"] = new SelectList(_context.DAEInstitute, "DAEInstituteId", "Name");
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevel, "SchemeLevelPolicyId", "Name");
            return View();
        }

        // POST: DAEInstituteQoutaBySchemeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DAEInstituteQoutaBySchemeLevelPolicyId,DAEInstituteId,ClassEnrollment,SlotAllocate,StipendAmount,Threshold,PolicySRCForumId,SchemeLevelPolicyId,InstituteAdditionalSlot")] DAEInstituteQoutaBySchemeLevel dAEInstituteQoutaBySchemeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dAEInstituteQoutaBySchemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DAEInstituteId"] = new SelectList(_context.DAEInstitute, "DAEInstituteId", "Name", dAEInstituteQoutaBySchemeLevel.DAEInstituteId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name", dAEInstituteQoutaBySchemeLevel.PolicySRCForumId);
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy, "SchemeLevelPolicyId", "Name", dAEInstituteQoutaBySchemeLevel.SchemeLevelPolicyId);
            return View(dAEInstituteQoutaBySchemeLevel);
        }

        // GET: DAEInstituteQoutaBySchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstituteQoutaBySchemeLevel = await _context.DAEInstituteQoutaBySchemeLevel.FindAsync(id);
            if (dAEInstituteQoutaBySchemeLevel == null)
            {
                return NotFound();
            }
            ViewData["DAEInstituteId"] = new SelectList(_context.DAEInstitute, "DAEInstituteId", "Name", dAEInstituteQoutaBySchemeLevel.DAEInstituteId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name", dAEInstituteQoutaBySchemeLevel.PolicySRCForumId);
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevel, "SchemeLevelPolicyId", "Name", dAEInstituteQoutaBySchemeLevel.SchemeLevelPolicyId);
            return View(dAEInstituteQoutaBySchemeLevel);
        }

        // POST: DAEInstituteQoutaBySchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DAEInstituteQoutaBySchemeLevelPolicyId,DAEInstituteId,ClassEnrollment,SlotAllocate,StipendAmount,Threshold,PolicySRCForumId,SchemeLevelPolicyId,InstituteAdditionalSlot")] DAEInstituteQoutaBySchemeLevel dAEInstituteQoutaBySchemeLevel)
        {
            if (id != dAEInstituteQoutaBySchemeLevel.DAEInstituteQoutaBySchemeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dAEInstituteQoutaBySchemeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DAEInstituteQoutaBySchemeLevelExists(dAEInstituteQoutaBySchemeLevel.DAEInstituteQoutaBySchemeLevelId))
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
            ViewData["DAEInstituteId"] = new SelectList(_context.DAEInstitute, "DAEInstituteId", "Name", dAEInstituteQoutaBySchemeLevel.DAEInstituteId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name", dAEInstituteQoutaBySchemeLevel.PolicySRCForumId);
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy, "SchemeLevelPolicyId", "Name", dAEInstituteQoutaBySchemeLevel.SchemeLevelPolicyId);
            return View(dAEInstituteQoutaBySchemeLevel);
        }

        // GET: DAEInstituteQoutaBySchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstituteQoutaBySchemeLevel = await _context.DAEInstituteQoutaBySchemeLevel
                .Include(d => d.DAEInstitute)
                .Include(d => d.SRCForum)
                .Include(d => d.SchemeLevelPolicy)
                .FirstOrDefaultAsync(m => m.DAEInstituteQoutaBySchemeLevelId == id);
            if (dAEInstituteQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(dAEInstituteQoutaBySchemeLevel);
        }

        // POST: DAEInstituteQoutaBySchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dAEInstituteQoutaBySchemeLevel = await _context.DAEInstituteQoutaBySchemeLevel.FindAsync(id);
            _context.DAEInstituteQoutaBySchemeLevel.Remove(dAEInstituteQoutaBySchemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DAEInstituteQoutaBySchemeLevelExists(int id)
        {
            return _context.DAEInstituteQoutaBySchemeLevel.Any(e => e.DAEInstituteQoutaBySchemeLevelId == id);
        }
    }
}
