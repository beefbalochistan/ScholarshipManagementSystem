using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;
using Newtonsoft.Json;
using DAL.Models.Domain.ImportResult;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class SelectionCriteriaGeneralsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectionCriteriaGeneralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SelectionCriteriaGenerals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SelectionCriteriaGeneral.Include(s => s.ExcelColumnName).Include(s => s.Operator).Include(a=>a.SchemeLevel.Scheme);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SelectionCriteriaGenerals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteriaGeneral = await _context.SelectionCriteriaGeneral
                .Include(s => s.ExcelColumnName)
                .Include(s => s.Operator)
                .FirstOrDefaultAsync(m => m.SelectionCriteriaGeneralId == id);
            if (selectionCriteriaGeneral == null)
            {
                return NotFound();
            }

            return View(selectionCriteriaGeneral);
        }
        public async Task<JsonResult> GetSchemeLevels(int schemeId)
        {
            List<SchemeLevel> schemeLevels = await _context.SchemeLevel.Where(a => a.SchemeId == schemeId).Select(a => new SchemeLevel { SchemeLevelId = a.SchemeLevelId, Name = a.Name }).ToListAsync();
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
        // GET: SelectionCriteriaGenerals/Create
        public IActionResult Create()
        {            
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "Name");
            return View();
        }

        // POST: SelectionCriteriaGenerals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SelectionCriteriaGeneral selectionCriteriaGeneral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selectionCriteriaGeneral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteriaGeneral.ExcelColumnNameId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteriaGeneral.OperatorId);
            return View(selectionCriteriaGeneral);
        }

        // GET: SelectionCriteriaGenerals/Edit/5
        public async Task<IActionResult> Edit(int? id, string message)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteriaGeneral = await _context.SelectionCriteriaGeneral.FindAsync(id);
            if (selectionCriteriaGeneral == null)
            {
                return NotFound();
            }
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Where(a=>a.SchemeLevelId == selectionCriteriaGeneral.SchemeLevelId).Select(a=>a.SchemeId).FirstOrDefault());          
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteriaGeneral.ExcelColumnNameId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteriaGeneral.OperatorId);
            return View(selectionCriteriaGeneral);
        }

        // POST: SelectionCriteriaGenerals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectionCriteriaGeneralId,OperatorId,ExcelColumnNameId,Condition")] SelectionCriteriaGeneral selectionCriteriaGeneral)
        {
            if (id != selectionCriteriaGeneral.SelectionCriteriaGeneralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectionCriteriaGeneral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectionCriteriaGeneralExists(selectionCriteriaGeneral.SelectionCriteriaGeneralId))
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
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Where(a => a.SchemeLevelId == selectionCriteriaGeneral.SchemeLevelId).Select(a => a.SchemeId).FirstOrDefault());
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteriaGeneral.ExcelColumnNameId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteriaGeneral.OperatorId);
            return View(selectionCriteriaGeneral);
        }

        // GET: SelectionCriteriaGenerals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteriaGeneral = await _context.SelectionCriteriaGeneral
                .Include(s => s.ExcelColumnName)
                .Include(s => s.Operator)
                .FirstOrDefaultAsync(m => m.SelectionCriteriaGeneralId == id);
            if (selectionCriteriaGeneral == null)
            {
                return NotFound();
            }

            return View(selectionCriteriaGeneral);
        }

        // POST: SelectionCriteriaGenerals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selectionCriteriaGeneral = await _context.SelectionCriteriaGeneral.FindAsync(id);
            _context.SelectionCriteriaGeneral.Remove(selectionCriteriaGeneral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetSelectionCriteriaGeneral(int rrId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            var applicationDbContext = _context.SelectionCriteriaGeneral.Include(r => r.Operator).Include(r => r.ExcelColumnName).Where(a => a.SchemeLevelId == SchemeLevelId).ToList();
            if (DegreeScholarshipLevelId != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).ToList();
            }
            ColumnLabel columnLabels = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == rrId).FirstOrDefaultAsync();
            List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.ToList();
            List<SelectionCriteriaGeneral> selectionCriteriaGenerals = new List<SelectionCriteriaGeneral>();
            string record = JsonConvert.SerializeObject(columnLabels);
            foreach (var criteria in applicationDbContext)
            {
                if (record.Contains(criteria.ExcelColumnName.Name))
                {
                    SelectionCriteriaGeneral obj = new SelectionCriteriaGeneral();
                    obj = criteria;
                    selectionCriteriaGenerals.Add(obj);
                }
            }

            return PartialView(selectionCriteriaGenerals);
        }
        private bool SelectionCriteriaGeneralExists(int id)
        {
            return _context.SelectionCriteriaGeneral.Any(e => e.SelectionCriteriaGeneralId == id);
        }
    }
}
