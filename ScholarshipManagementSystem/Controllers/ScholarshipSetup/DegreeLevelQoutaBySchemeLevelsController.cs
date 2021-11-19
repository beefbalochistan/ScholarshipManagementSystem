using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    public class DegreeLevelQoutaBySchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DegreeLevelQoutaBySchemeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DegreeLevelQoutaBySchemeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DegreeLevelQoutaBySchemeLevel.Include(d => d.DegreeScholarshipLevel).Include(d => d.SRCForum).Include(d => d.SchemeLevelPolicy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DegreeLevelQoutaBySchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevelQoutaBySchemeLevel = await _context.DegreeLevelQoutaBySchemeLevel
                .Include(d => d.DegreeScholarshipLevel)
                .Include(d => d.SRCForum)
                .Include(d => d.SchemeLevelPolicy)
                .FirstOrDefaultAsync(m => m.DegreeLevelQoutaBySchemeLevelId == id);
            if (degreeLevelQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(degreeLevelQoutaBySchemeLevel);
        }

        // GET: DegreeLevelQoutaBySchemeLevels/Create
        public IActionResult Create()
        {
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy, "SchemeLevelPolicyId", "SchemeLevelPolicyId");
            return View();
        }

        // POST: DegreeLevelQoutaBySchemeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DegreeLevelQoutaBySchemeLevelId,DegreeScholarshipLevelId,ClassEnrollment,SlotAllocate,AdditionalSlotAllocate,StipendAmount,Threshold,PolicySRCForumId,SchemeLevelPolicyId")] DegreeLevelQoutaBySchemeLevel degreeLevelQoutaBySchemeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degreeLevelQoutaBySchemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId", degreeLevelQoutaBySchemeLevel.DegreeScholarshipLevelId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name", degreeLevelQoutaBySchemeLevel.PolicySRCForumId);
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy, "SchemeLevelPolicyId", "SchemeLevelPolicyId", degreeLevelQoutaBySchemeLevel.SchemeLevelPolicyId);
            return View(degreeLevelQoutaBySchemeLevel);
        }

        // GET: DegreeLevelQoutaBySchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevelQoutaBySchemeLevel = await _context.DegreeLevelQoutaBySchemeLevel.FindAsync(id);
            if (degreeLevelQoutaBySchemeLevel == null)
            {
                return NotFound();
            }
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId", degreeLevelQoutaBySchemeLevel.DegreeScholarshipLevelId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Name", degreeLevelQoutaBySchemeLevel.PolicySRCForumId);
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy, "SchemeLevelPolicyId", "SchemeLevelPolicyId", degreeLevelQoutaBySchemeLevel.SchemeLevelPolicyId);
            return View(degreeLevelQoutaBySchemeLevel);
        }

        // POST: DegreeLevelQoutaBySchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int degreePolicyId, int degreeSelectedValue, /*float populationSlot, float MPISlot,*/ float degreeAdditionalSlot)
        {
            if (degreeSelectedValue == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var degreeLevelQoutaBySchemeLevel = await _context.DegreeLevelQoutaBySchemeLevel.FindAsync(degreeSelectedValue);
                    if (degreeLevelQoutaBySchemeLevel.AdditionalSlotAllocate != degreeAdditionalSlot)
                    {
                        var Obj = new DegreeLevelQoutaBySchemeLevel();
                        Obj = degreeLevelQoutaBySchemeLevel;
                        Obj.AdditionalSlotAllocate = degreeAdditionalSlot;
                        //_context.Update(districtQoutaBySchemeLevel);
                        _context.Entry(degreeLevelQoutaBySchemeLevel).CurrentValues.SetValues(Obj);
                        await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("ViewPolicy", "DistrictQoutaBySchemeLevels", new { id = degreePolicyId });
            }
            return View();
        }

        // GET: DegreeLevelQoutaBySchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeLevelQoutaBySchemeLevel = await _context.DegreeLevelQoutaBySchemeLevel
                .Include(d => d.DegreeScholarshipLevel)
                .Include(d => d.SRCForum)
                .Include(d => d.SchemeLevelPolicy)
                .FirstOrDefaultAsync(m => m.DegreeLevelQoutaBySchemeLevelId == id);
            if (degreeLevelQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(degreeLevelQoutaBySchemeLevel);
        }

        // POST: DegreeLevelQoutaBySchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degreeLevelQoutaBySchemeLevel = await _context.DegreeLevelQoutaBySchemeLevel.FindAsync(id);
            _context.DegreeLevelQoutaBySchemeLevel.Remove(degreeLevelQoutaBySchemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeLevelQoutaBySchemeLevelExists(int id)
        {
            return _context.DegreeLevelQoutaBySchemeLevel.Any(e => e.DegreeLevelQoutaBySchemeLevelId == id);
        }
    }
}
