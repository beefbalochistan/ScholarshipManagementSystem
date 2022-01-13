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
using DAL.Models.ViewModels;

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
            var applicationDbContext = _context.SelectionCriteriaGeneral.Include(a=>a.SchemeLevel.Scheme);
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
            var selectNumberList = new List<SelectListItem>();
            for (int i = 9; i>=0; i--)
            {
                selectNumberList.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            selectNumberList.Add(new SelectListItem
            {
                Value = ".",
                Text = "."
            });
            ViewData["NumberList"] = selectNumberList;
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "Value", "Name");
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
                var IsExist = _context.SelectionCriteriaGeneral.Where(a => a.SchemeLevelId == selectionCriteriaGeneral.SchemeLevelId).Count();
                if(IsExist == 0)
                {
                    _context.Add(selectionCriteriaGeneral);
                    await _context.SaveChangesAsync();                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(nameof(selectionCriteriaGeneral.SchemeLevelId), "Selection Criteria Already Defined Against Selected Scheme!");
                    //return BadRequest(ModelState);                    
                }              
            }
            var selectNumberList = new List<SelectListItem>();
            for (int i = 9; i>=0; i--)
            {
                selectNumberList.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            selectNumberList.Add(new SelectListItem
            {
                Value = ".",
                Text = "."
            });
            ViewData["NumberList"] = selectNumberList;
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "Value", "Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "Name");
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
            var selectNumberList = new List<SelectListItem>();
            for (int i = 9; i>=0; i--)
            {
                selectNumberList.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            selectNumberList.Add(new SelectListItem
            {
                Value = ".",
                Text = "."
            });
            ViewData["NumberList"] = selectNumberList;
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Find(selectionCriteriaGeneral.SchemeLevelId).SchemeId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", selectionCriteriaGeneral.SchemeLevelId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "Value", "Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "Name");
            return View(selectionCriteriaGeneral);
        }

        // POST: SelectionCriteriaGenerals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SelectionCriteriaGeneral selectionCriteriaGeneral)
        {
            if (id != selectionCriteriaGeneral.SelectionCriteriaGeneralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = TestExpression(selectionCriteriaGeneral.Expression);
                    var jsonString = JsonConvert.SerializeObject(result.Value);
                    if (jsonString.Contains("true"))
                    {
                        _context.Update(selectionCriteriaGeneral);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(selectionCriteriaGeneral.Expression), "Invalid Expression!");
                    }                  
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
            var selectNumberList = new List<SelectListItem>();
            for (int i = 9; i>=0; i--)
            {
                selectNumberList.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            selectNumberList.Add(new SelectListItem
            {
                Value = ".",
                Text = "."
            });
            ViewData["NumberList"] = selectNumberList;
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Find(selectionCriteriaGeneral.SchemeLevelId).SchemeId);
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", selectionCriteriaGeneral.SchemeLevelId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "Value", "Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "Name");
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
        public async Task<IActionResult> GetSelectionCriteriaGeneral(int SchemeLevelId)
        {
            var applicationDbContext = await _context.SelectionCriteriaGeneral.Where(a => a.SchemeLevelId == SchemeLevelId).ToListAsync();                        
            return PartialView(applicationDbContext);
        }
        public async Task<JsonResult> ApplySelectionCriteria(int PolicySRCForumId, int SchemeLevelId, int SchemeId, int DegreeScholarshipLevelId)
        {
            var record = _context.ResultRepository.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).FirstOrDefault();            
            if (SchemeId >= 4)
            {
                record = _context.ResultRepository.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).FirstOrDefault();
            }
            
            if (record != null)
            {
                if (record.IsDataCleaned)
                {
                    if(record.IsSelctionCriteriaApplied == false)
                    {
                        try
                        {
                            var selectionCriteria = _context.SelectionCriteriaGeneral.Where(a => a.SchemeLevelId == SchemeLevelId).FirstOrDefault();
                            var result = _context.Database.ExecuteSqlRaw("Update ImportResult.ResultContainer set IsOnCriteria=1  WHERE " + selectionCriteria.Expression + " AND ResultRepositoryId=" + record.ResultRepositoryId);
                            result = _context.Database.ExecuteSqlRaw("Update ImportResult.ResultRepository set IsSelctionCriteriaApplied=1  WHERE ResultRepositoryId=" + record.ResultRepositoryId);
                            return Json(new { IsValid = true, message = "Applied Successfully!" });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { IsValid = false, message = "Something Went Wrong!" });
                        }
                    }
                    else
                    {
                        return Json(new { IsValid = false, message = "Selection Criteria Already Applied!" });
                    }                    
                }
                else
                {
                    return Json(new { IsValid = false, message = "Assist Document Before Appling Criteria!" });
                }
            }
            return Json(new {IsValid = false, message = "Result not found!" });
        }
        private bool SelectionCriteriaGeneralExists(int id)
        {
            return _context.SelectionCriteriaGeneral.Any(e => e.SelectionCriteriaGeneralId == id);
        }
        public JsonResult TestExpression(string exp)
        {
            try
            {
                var result = _context.Database.ExecuteSqlRaw("select count(*) from ImportResult.dummy WHERE " + exp);
            }
            catch(Exception ex)
            {
                return Json(new { success = false });
            }            
            return Json(new { success = true });
        }
        public JsonResult GetResultStatus(int PolicySRCForumId, int SchemeLevelId, int SchemeId, int DegreeScholarshipLevelId)
        {
            var record = _context.ResultRepository.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).FirstOrDefault();
            if (SchemeId >= 4)
            {
                record = _context.ResultRepository.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).FirstOrDefault();
            }            
            if (record != null)
            {
                bool IsClean = false;
                bool IsCriteriaApplied = false;
                bool IsMeritListGenerated = false;
                if (record.IsDataCleaned)
                {
                    IsClean = true;
                }
                if (record.IsSelctionCriteriaApplied)
                {
                    IsCriteriaApplied = true;
                }
                if (record.IsMeritListGenerated)
                {
                    IsMeritListGenerated = true;
                }
                return Json(new { success = true, isclean = IsClean, iscriteriaapplied = IsCriteriaApplied, ismeritlistgenerated = IsMeritListGenerated, rrid = record.ResultRepositoryId });
            }
            else
            {
                var tempRecord = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).FirstOrDefault();
                if (SchemeId >= 4)
                {
                    tempRecord = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).FirstOrDefault();
                }
                if(tempRecord == null)
                {
                    return Json(new { success = false, isclean = false, iscriteriaapplied = false, ismeritlistgenerated = false, rrid = 0 });
                }
                return Json(new { success = false, isclean = false, iscriteriaapplied = false, ismeritlistgenerated = false, rrid = tempRecord.ResultRepositoryTempId });
            }            
        }
    }
}
