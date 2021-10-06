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
            var applicationDbContext = _context.SelectionCriteria.Include(s => s.ExcelColumnName).Include(s => s.Operator).Include(s => s.ResultRepository.SchemeLevel).Where(a=>a.ResultRepositoryId == id);
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
            Type type = typeof(ColumnLabel);
            PropertyInfo[] properties = type.GetProperties();
            var record = _context.ColumnLabel.Find(_context.ResultContainer.Where(a => a.ResultRepositoryId == id).Max(a => a.ColumnLabelId));
            var excelColumns = _context.ExcelColumnName.ToList();
            List<ExcelColumnName> activeExcelColumn = new List<ExcelColumnName>();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(record).ToString() != "")
                {
                    foreach (var column in excelColumns)
                    {
                        if (property.GetValue(record).ToString() == column.Name) // check obj has value for that particular property
                        {
                            ExcelColumnName excelColumnName = new ExcelColumnName();
                            excelColumnName.Name = column.Name;
                            excelColumnName.ExcelColumnNameId = column.ExcelColumnNameId;
                            activeExcelColumn.Add(excelColumnName);
                        }
                    }
                }
            }                        
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository.Include(a => a.SchemeLevel).Where(a => a.ResultRepositoryId == id), "SchemeLevelId", "SchemeLevel.Name");
            ViewData["ExcelColumnNameId"] = new SelectList(activeExcelColumn, "ExcelColumnNameId", "Name");
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "ExcelColumnNameId", "ExcelColumnNameId", selectionCriteria.ExcelColumnNameId);
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
