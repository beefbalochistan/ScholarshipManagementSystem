using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.ImportResult;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ImportResult
{
    public class DocumentAssistIndicatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentAssistIndicatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DocumentAssistIndicators
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DocumentAssistIndicator.Include(d => d.DocumentAssist).Include(d => d.ExcelColumnName).Include(d => d.ResultRepository);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DocumentAssistIndicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .Include(d => d.ResultRepository)
                .FirstOrDefaultAsync(m => m.DocumentAssistIndicatorId == id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }

            return View(documentAssistIndicator);
        }
        public async Task<JsonResult> GetSchemeLevels(int policySRCForumId, int schemeId)
        {
            List<SchemeLevel> schemeLevels = await _context.SchemeLevelPolicy.Include(a => a.SchemeLevel).Where(a => a.PolicySRCForumId == policySRCForumId && a.SchemeLevel.SchemeId == schemeId).Select(a => new SchemeLevel { SchemeLevelId = a.SchemeLevelId, Name = a.SchemeLevel.Name }).ToListAsync();
            var schemeLevelList = schemeLevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.SchemeLevelId.ToString(),
            });
            return Json(schemeLevelList);
        }
        public async Task<JsonResult> GetDegreeLevels(int schemeLevelId)
        {
            List<DegreeScholarshipLevel> degreeLevels = await _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevelId).ToListAsync();
            var degreeLevelList = degreeLevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.DegreeLevelId.ToString(),
            });
            return Json(degreeLevelList);
        }
        // GET: DocumentAssistIndicators/Create
        public IActionResult Create()
        {
            var schemeList = _context.Scheme.ToList();
            schemeList.Insert(0, new Scheme { SchemeId = 0, Name = "Select" });
            ViewData["SchemeId"] = new SelectList(schemeList, "SchemeId", "Name");
            var qry = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Join(_context.ScholarshipFiscalYear, s => s.ScholarshipFiscalYearId, i => i.ScholarshipFiscalYearId, (s, i) => new { s, i })
             .GroupBy(g => new { g.s.ScholarshipFiscalYearId })
             .Select(g => new PolicySRCForum
             {
                 PolicySRCForumId = g.Max(p => p.s.PolicySRCForumId),
                 Code = g.Max(p => p.i.Code)
             });
            ViewData["PolicySRCForumId"] = new SelectList(qry, "PolicySRCForumId", "Code");
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "ExcelColumnNameId");
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId");
            return View();
        }

        // POST: DocumentAssistIndicators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentAssistIndicatorId,ExcelColumnNameId,DocumentAssistId,ResultRepositoryId")] DocumentAssistIndicator documentAssistIndicator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentAssistIndicator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // GET: DocumentAssistIndicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator.FindAsync(id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // POST: DocumentAssistIndicators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentAssistIndicatorId,ExcelColumnNameId,DocumentAssistId,ResultRepositoryId")] DocumentAssistIndicator documentAssistIndicator)
        {
            if (id != documentAssistIndicator.DocumentAssistIndicatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentAssistIndicator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentAssistIndicatorExists(documentAssistIndicator.DocumentAssistIndicatorId))
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
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistIndicator.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "ExcelColumnNameId", documentAssistIndicator.ExcelColumnNameId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", documentAssistIndicator.ResultRepositoryId);
            return View(documentAssistIndicator);
        }

        // GET: DocumentAssistIndicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistIndicator = await _context.DocumentAssistIndicator
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .Include(d => d.ResultRepository)
                .FirstOrDefaultAsync(m => m.DocumentAssistIndicatorId == id);
            if (documentAssistIndicator == null)
            {
                return NotFound();
            }

            return View(documentAssistIndicator);
        }

        // POST: DocumentAssistIndicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentAssistIndicator = await _context.DocumentAssistIndicator.FindAsync(id);
            _context.DocumentAssistIndicator.Remove(documentAssistIndicator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentAssistIndicatorExists(int id)
        {
            return _context.DocumentAssistIndicator.Any(e => e.DocumentAssistIndicatorId == id);
        }
    }
}
