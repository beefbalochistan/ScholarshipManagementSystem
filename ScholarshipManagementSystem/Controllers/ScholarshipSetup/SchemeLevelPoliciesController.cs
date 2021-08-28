using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    [AllowAnonymous]
    public class SchemeLevelPoliciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchemeLevelPoliciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchemeLevelPolicies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchemeLevelPolicy.Include(s => s.PolicySRCForum).Include(s => s.SchemeLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchemeLevelPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPolicy = await _context.SchemeLevelPolicy
                .Include(s => s.PolicySRCForum)
                .Include(s => s.SchemeLevel)
                .FirstOrDefaultAsync(m => m.SchemeLevelPolicyId == id);
            if (schemeLevelPolicy == null)
            {
                return NotFound();
            }

            return View(schemeLevelPolicy);
        }

        // GET: SchemeLevelPolicies/Create
        public IActionResult Create()
        {
            ViewData["PolicySRCForumId"] = new SelectList(_context.Set<PolicySRCForum>(), "PolicySRCForumId", "Name");
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            return View();
        }

        // POST: SchemeLevelPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchemeLevelPolicyId,SchemeLevelId,PolicySRCForumId,Amount,ScholarshipQouta,POMS,DOMS,SQSOMS,SQSEVIs,CreatedOn")] SchemeLevelPolicy schemeLevelPolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schemeLevelPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolicySRCForumId"] = new SelectList(_context.Set<PolicySRCForum>(), "PolicySRCForumId", "Name", schemeLevelPolicy.PolicySRCForumId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPolicy.SchemeLevelId);
            return View(schemeLevelPolicy);
        }

        // GET: SchemeLevelPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPolicy = await _context.SchemeLevelPolicy.FindAsync(id);
            if (schemeLevelPolicy == null)
            {
                return NotFound();
            }
            ViewData["PolicySRCForumId"] = new SelectList(_context.Set<PolicySRCForum>(), "PolicySRCForumId", "Name", schemeLevelPolicy.PolicySRCForumId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPolicy.SchemeLevelId);
            return View(schemeLevelPolicy);
        }

        // POST: SchemeLevelPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchemeLevelPolicyId,SchemeLevelId,PolicySRCForumId,Amount,ScholarshipQouta,POMS,DOMS,SQSOMS,SQSEVIs,CreatedOn")] SchemeLevelPolicy schemeLevelPolicy)
        {
            if (id != schemeLevelPolicy.SchemeLevelPolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schemeLevelPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchemeLevelPolicyExists(schemeLevelPolicy.SchemeLevelPolicyId))
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
            ViewData["PolicySRCForumId"] = new SelectList(_context.Set<PolicySRCForum>(), "PolicySRCForumId", "Name", schemeLevelPolicy.PolicySRCForumId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", schemeLevelPolicy.SchemeLevelId);
            return View(schemeLevelPolicy);
        }

        // GET: SchemeLevelPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevelPolicy = await _context.SchemeLevelPolicy
                .Include(s => s.PolicySRCForum)
                .Include(s => s.SchemeLevel)
                .FirstOrDefaultAsync(m => m.SchemeLevelPolicyId == id);
            if (schemeLevelPolicy == null)
            {
                return NotFound();
            }

            return View(schemeLevelPolicy);
        }

        // POST: SchemeLevelPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schemeLevelPolicy = await _context.SchemeLevelPolicy.FindAsync(id);
            _context.SchemeLevelPolicy.Remove(schemeLevelPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchemeLevelPolicyExists(int id)
        {
            return _context.SchemeLevelPolicy.Any(e => e.SchemeLevelPolicyId == id);
        }
    }
}
