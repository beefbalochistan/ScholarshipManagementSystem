
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
    public class DistrictQoutaBySchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictQoutaBySchemeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DistrictQoutaBySchemeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DistrictQoutaBySchemeLevel.Include(d => d.District).Include(d => d.SRCForum);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DistrictQoutaBySchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel
                .Include(d => d.District)
                .Include(d => d.SRCForum)
                .FirstOrDefaultAsync(m => m.DistrictQoutaBySchemeLevelId == id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code");
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code");
            return View();
        }

        // POST: DistrictQoutaBySchemeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictQoutaBySchemeLevelId,DistrictId,Threshold,CurrentYearPopulation,DistrictPopulationSlot,DistrictMPISlot,PolicySRCForumId,MPI")] DistrictQoutaBySchemeLevel districtQoutaBySchemeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(districtQoutaBySchemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel.FindAsync(id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // POST: DistrictQoutaBySchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DistrictQoutaBySchemeLevelId,DistrictId,Threshold,CurrentYearPopulation,DistrictPopulationSlot,DistrictMPISlot,PolicySRCForumId,MPI")] DistrictQoutaBySchemeLevel districtQoutaBySchemeLevel)
        {
            if (id != districtQoutaBySchemeLevel.DistrictQoutaBySchemeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(districtQoutaBySchemeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictQoutaBySchemeLevelExists(districtQoutaBySchemeLevel.DistrictQoutaBySchemeLevelId))
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
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel
                .Include(d => d.District)
                .Include(d => d.PolicySRCForumId)
                .FirstOrDefaultAsync(m => m.DistrictQoutaBySchemeLevelId == id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(districtQoutaBySchemeLevel);
        }

        // POST: DistrictQoutaBySchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel.FindAsync(id);
            _context.DistrictQoutaBySchemeLevel.Remove(districtQoutaBySchemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictQoutaBySchemeLevelExists(int id)
        {
            return _context.DistrictQoutaBySchemeLevel.Any(e => e.DistrictQoutaBySchemeLevelId == id);
        }
    }
}
