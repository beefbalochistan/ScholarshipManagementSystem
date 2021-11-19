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
        public async Task<IActionResult> Edit(int daePolicyId, int daeSelectedValue, /*float populationSlot, float MPISlot,*/ float daeAdditionalSlot)
        {
            if (daeSelectedValue == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var dAEInstituteQoutaBySchemeLevel = await _context.DAEInstituteQoutaBySchemeLevel.FindAsync(daeSelectedValue);
                    if(dAEInstituteQoutaBySchemeLevel.InstituteAdditionalSlot != daeAdditionalSlot)
                    {
                        var Obj = new DAEInstituteQoutaBySchemeLevel();
                        Obj = dAEInstituteQoutaBySchemeLevel;
                        Obj.InstituteAdditionalSlot = daeAdditionalSlot;
                        //_context.Update(districtQoutaBySchemeLevel);
                        _context.Entry(dAEInstituteQoutaBySchemeLevel).CurrentValues.SetValues(Obj);
                        await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                    }                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("ViewPolicy", "DistrictQoutaBySchemeLevels", new { id = daePolicyId });
            }
            return View();
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
