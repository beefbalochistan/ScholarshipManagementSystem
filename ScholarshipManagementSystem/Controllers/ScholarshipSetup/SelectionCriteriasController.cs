using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    public class SelectionCriteriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectionCriteriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SelectionCriterias
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Id = id;
            var applicationDbContext = _context.SelectionCriteria.Include(s => s.ExcelColumnName).Include(s => s.Operator).Include(s => s.ResultRepository.SchemeLevelPolicy.SchemeLevel).Where(a=>a.ResultRepositoryId == id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SelectionCriterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteria = await _context.SelectionCriteria
                .Include(s => s.ExcelColumnName)
                .Include(s => s.Operator)
                .Include(s => s.ResultRepository)
                .FirstOrDefaultAsync(m => m.SelectionCriteriaId == id);
            if (selectionCriteria == null)
            {
                return NotFound();
            }

            return View(selectionCriteria);
        }

        // GET: SelectionCriterias/Create
        public IActionResult Create(int id)
        {            
            var excelColumns = _context.ExcelColumnName.ToList();
            var record = _context.ColumnLabel.Where(a => a.ResultRepositoryId == id).FirstOrDefault();            
            List<SelectListItem> Listitems = new List<SelectListItem>(); 
            if(record.C1 != "")
                Listitems.Add(new SelectListItem { Text = record.C1, Value = excelColumns.ElementAt(0).ExcelColumnNameId.ToString() });
            if (record.C2 != "")
                Listitems.Add(new SelectListItem { Text = record.C2, Value = excelColumns.ElementAt(1).ExcelColumnNameId.ToString() });
            if (record.C3 != "")
                Listitems.Add(new SelectListItem { Text = record.C3, Value = excelColumns.ElementAt(2).ExcelColumnNameId.ToString() });
            if (record.C4 != "")
                Listitems.Add(new SelectListItem { Text = record.C4, Value = excelColumns.ElementAt(3).ExcelColumnNameId.ToString() });
            if (record.C5 != "")
                Listitems.Add(new SelectListItem { Text = record.C5, Value = excelColumns.ElementAt(4).ExcelColumnNameId.ToString() });
            if (record.C6 != "")
                Listitems.Add(new SelectListItem { Text = record.C6, Value = excelColumns.ElementAt(5).ExcelColumnNameId.ToString() });
            if (record.C7 != "")
                Listitems.Add(new SelectListItem { Text = record.C7, Value = excelColumns.ElementAt(6).ExcelColumnNameId.ToString() });
            if (record.C8 != "")
                Listitems.Add(new SelectListItem { Text = record.C8, Value = excelColumns.ElementAt(7).ExcelColumnNameId.ToString() });
            if (record.C9 != "")
                Listitems.Add(new SelectListItem { Text = record.C9, Value = excelColumns.ElementAt(8).ExcelColumnNameId.ToString() });
            if (record.C10 != "")
                Listitems.Add(new SelectListItem { Text = record.C10, Value = excelColumns.ElementAt(9).ExcelColumnNameId.ToString() });
            if (record.C11 != "")
                Listitems.Add(new SelectListItem { Text = record.C11, Value = excelColumns.ElementAt(10).ExcelColumnNameId.ToString() });
            if (record.C12 != "")
                Listitems.Add(new SelectListItem { Text = record.C12, Value = excelColumns.ElementAt(11).ExcelColumnNameId.ToString() });
            if (record.C13 != "")
                Listitems.Add(new SelectListItem { Text = record.C13, Value = excelColumns.ElementAt(12).ExcelColumnNameId.ToString() });
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository.Include(a => a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ResultRepositoryId == id), "ResultRepositoryId", "SchemeLevelPolicy.SchemeLevel.Name");
            ViewData["ExcelColumnNameId"] = new SelectList(Listitems, "Value", "Text");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name");
            SelectionCriteria obj = new SelectionCriteria();
            obj.ResultRepositoryId = id;
            return View(obj);
        }

        // POST: SelectionCriterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectionCriteriaId,ResultRepositoryId,OperatorId,ExcelColumnNameId,Condition")] SelectionCriteria selectionCriteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selectionCriteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = selectionCriteria.ResultRepositoryId});
            }
            var excelColumns = _context.ExcelColumnName.ToList();
            var record = _context.ColumnLabel.Where(a => a.ResultRepositoryId == selectionCriteria.ResultRepositoryId).FirstOrDefault();
            List<SelectListItem> Listitems = new List<SelectListItem>();
            if (record.C1 != "")
                Listitems.Add(new SelectListItem { Text = record.C1, Value = excelColumns.ElementAt(0).ExcelColumnNameId.ToString() });
            if (record.C2 != "")
                Listitems.Add(new SelectListItem { Text = record.C2, Value = excelColumns.ElementAt(1).ExcelColumnNameId.ToString() });
            if (record.C3 != "")
                Listitems.Add(new SelectListItem { Text = record.C3, Value = excelColumns.ElementAt(2).ExcelColumnNameId.ToString() });
            if (record.C4 != "")
                Listitems.Add(new SelectListItem { Text = record.C4, Value = excelColumns.ElementAt(3).ExcelColumnNameId.ToString() });
            if (record.C5 != "")
                Listitems.Add(new SelectListItem { Text = record.C5, Value = excelColumns.ElementAt(4).ExcelColumnNameId.ToString() });
            if (record.C6 != "")
                Listitems.Add(new SelectListItem { Text = record.C6, Value = excelColumns.ElementAt(5).ExcelColumnNameId.ToString() });
            if (record.C7 != "")
                Listitems.Add(new SelectListItem { Text = record.C7, Value = excelColumns.ElementAt(6).ExcelColumnNameId.ToString() });
            if (record.C8 != "")
                Listitems.Add(new SelectListItem { Text = record.C8, Value = excelColumns.ElementAt(7).ExcelColumnNameId.ToString() });
            if (record.C9 != "")
                Listitems.Add(new SelectListItem { Text = record.C9, Value = excelColumns.ElementAt(8).ExcelColumnNameId.ToString() });
            if (record.C10 != "")
                Listitems.Add(new SelectListItem { Text = record.C10, Value = excelColumns.ElementAt(9).ExcelColumnNameId.ToString() });
            if (record.C11 != "")
                Listitems.Add(new SelectListItem { Text = record.C11, Value = excelColumns.ElementAt(10).ExcelColumnNameId.ToString() });
            if (record.C12 != "")
                Listitems.Add(new SelectListItem { Text = record.C12, Value = excelColumns.ElementAt(11).ExcelColumnNameId.ToString() });
            if (record.C13 != "")
                Listitems.Add(new SelectListItem { Text = record.C13, Value = excelColumns.ElementAt(12).ExcelColumnNameId.ToString() });            
            ViewData["ExcelColumnNameId"] = new SelectList(Listitems, "Value", "Text", selectionCriteria.ExcelColumnNameId);            
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteria.OperatorId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", selectionCriteria.ResultRepositoryId);
            return View(selectionCriteria);
        }

        // GET: SelectionCriterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteria = await _context.SelectionCriteria.FindAsync(id);
            if (selectionCriteria == null)
            {
                return NotFound();
            }
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteria.ExcelColumnNameId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteria.OperatorId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", selectionCriteria.ResultRepositoryId);
            return View(selectionCriteria);
        }

        // POST: SelectionCriterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectionCriteriaId,ResultRepositoryId,OperatorId,ExcelColumnNameId,Condition")] SelectionCriteria selectionCriteria)
        {
            if (id != selectionCriteria.SelectionCriteriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectionCriteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectionCriteriaExists(selectionCriteria.SelectionCriteriaId))
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
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteria.ExcelColumnNameId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name", selectionCriteria.OperatorId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", selectionCriteria.ResultRepositoryId);
            return View(selectionCriteria);
        }

        // GET: SelectionCriterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectionCriteria = await _context.SelectionCriteria
                .Include(s => s.ExcelColumnName)
                .Include(s => s.Operator)
                .Include(s => s.ResultRepository)
                .FirstOrDefaultAsync(m => m.SelectionCriteriaId == id);
            if (selectionCriteria == null)
            {
                return NotFound();
            }

            return View(selectionCriteria);
        }

        // POST: SelectionCriterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selectionCriteria = await _context.SelectionCriteria.FindAsync(id);
            _context.SelectionCriteria.Remove(selectionCriteria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectionCriteriaExists(int id)
        {
            return _context.SelectionCriteria.Any(e => e.SelectionCriteriaId == id);
        }
    }
}
