using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.Student;
using ScholarshipManagementSystem.Models.ViewModels;

namespace ScholarshipManagementSystem.Controllers.ImportResult
{
    public class ResultContainersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultContainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultContainers
        public async Task<IActionResult> Index(int id)
        {            
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            var applicationDbContext = await _context.ResultContainer.Include(r => r.ColumnLabel).Include(r => r.ResultRepository).Where(a => a.ResultRepositoryId == id).ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.FindAsync(applicationDbContext.Max(a=>a.ColumnLabelId));
            viewUploadedResult.columnLabel = obj;
            //-----------------------------------------
            List<int> statistics = new List<int>();
            Type type = typeof(ResultContainer);
            PropertyInfo[] properties = type.GetProperties();
            int counter = 0;
            int columnCount = 0;
            bool IsDataCleaned = true;
            var desiredTable = applicationDbContext.GroupBy(o => o.C1,
                                        o => 1, // don't need the whole object
                                        (key, g) => new { key, count = g.Sum() }).Where(a=>a.count > 1);
            var desiredDictionary = applicationDbContext.ToDictionary(x => x.ResultContainerId, x => x.C3);
            foreach (PropertyInfo property in properties)
            {
                if (columnCount > 0 && columnCount < 16)
                {
                    counter = 0;
                    foreach (var record in applicationDbContext)
                    {
                        if (property.GetValue(record) == null) // check obj has value for that particular property
                        {
                            counter++;
                            IsDataCleaned = false;
                        }
                    }
                    statistics.Add(counter);
                }
                columnCount++;
            }
            viewUploadedResult.compileResult = statistics;
            //-----------------------------------------
            ResultRepository currentRepositoryResult = await _context.ResultRepository.FindAsync(id);
            if(currentRepositoryResult.IsDataCleaned != IsDataCleaned)
            {
                currentRepositoryResult.IsDataCleaned = IsDataCleaned;
                _context.Update(currentRepositoryResult);
                await _context.SaveChangesAsync();
            }           
            //-----------------------------------------
            return View(viewUploadedResult);
        }
        private string GetPropertyName(dynamic obj, string column)
        {
            Type type = obj.GetType();            
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj).ToString() == column) // check obj has value for that particular property
                {
                    return property.Name;
                }
            }
            return "";
        }
        public async Task<IActionResult> MeritGenerator(int id, int SLId, int FYearId)
        {
            var currentPolicy = _context.SchemeLevelPolicy.Include(a => a.PolicySRCForum).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == FYearId && a.PolicySRCForum.IsEndorse == true && a.SchemeLevelId == SLId).FirstOrDefault();
            //var POML = _context.ResultContainer.Where(a => a.ResultRepositoryId == id && a.IsOnCriteria == true).Take((int)currentPolicy.POMS);
            //---------------Get Column ---------------------------------
            ColumnLabel columnLabel = _context.ColumnLabel.Find(_context.ResultContainer.Where(a => a.ResultRepositoryId == id).Max(a => a.ColumnLabelId));            
            string markColumnName = GetPropertyName(columnLabel, "Marks_");                                          
            //-----------------------------------------------------------
            //var POMLCandidates = _context.ResultContainer.OrderBy(x =>((string)x.GetType().GetProperty(markColumnName).GetValue(x, null)));
            var POMLCandidates = _context.ResultContainer.Where(a=>a.ResultRepositoryId == id && a.IsOnCriteria == true).OrderByDescending(x => EF.Property<object>(x, markColumnName)).Take((int)currentPolicy.POMS).ToList();

            foreach(var result in POMLCandidates)
            {
                Applicant applicant = new Applicant();
                applicant.Name = result.GetType().GetProperty(GetPropertyName(columnLabel, "Name")).GetValue(result, null).ToString();
                applicant.DistrictId = _context.District.Where(a=>a.Name == result.GetType().GetProperty(GetPropertyName(columnLabel, "Name")).GetValue(result, null).ToString()).Select(a=>a.DivisionId).FirstOrDefault();
                applicant.FatherName = result.GetType().GetProperty(GetPropertyName(columnLabel, "Father_Name")).GetValue(result, null).ToString();
                applicant.ReceivedMarks = (int)result.GetType().GetProperty(GetPropertyName(columnLabel, "Marks_")).GetValue(result, null);
                applicant.RollNumber = result.GetType().GetProperty(GetPropertyName(columnLabel, "Roll_NO")).GetValue(result, null).ToString();
                applicant.SelectedMethod = "POMS";
                applicant.TotalMarks = 1100;//KDA
                applicant.SelectionStatus = "Pending";
                applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code;
            }
           
            return RedirectToAction(nameof(Details), new { id });
        }
        public async Task<IActionResult> ApplyCriteria(int id)
        {
            int currentColumn = 0;
            ResultRepository currentRepository = _context.ResultRepository.Find(id);
            var currentCriteria = await _context.SelectionCriteria.Include(a=>a.Operator).Where(a => a.ResultRepositoryId == id).ToListAsync();
            foreach(var criteria in currentCriteria)
            {
                var columnName = _context.ExcelColumnName.Find(criteria.ExcelColumnNameId).Name;
                var columnLabel = _context.ColumnLabel.Find(_context.ResultContainer.Where(a=>a.ResultRepositoryId == id).Select(a=>a.ColumnLabelId).FirstOrDefault());
                if(columnLabel.C1 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C1 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid="+ id +")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 1;
                }
                else if(columnLabel.C2 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C2 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 2;
                }
                else if (columnLabel.C3 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C3 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 3;
                }
                else if (columnLabel.C4 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C4 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 4;
                }
                else if (columnLabel.C5 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C5 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 5;
                }
                else if (columnLabel.C6 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C6 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 6;
                }
                else if (columnLabel.C7 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C7 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 7;
                }
                else if (columnLabel.C8 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C8 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 8;
                }
                else if (columnLabel.C9 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C9 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 9;
                }
                else if (columnLabel.C10 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C10 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 10;
                }
                else if (columnLabel.C11 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C11 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 11;
                }
                else if (columnLabel.C12 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C12 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 12;
                }
                else if (columnLabel.C13 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C13 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 13;
                }
                else if (columnLabel.C14 == columnName)
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C14 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 14;
                }
                else
                {
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE C15 " + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid=" + id + ")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();
                    currentColumn = 15;
                }                
            }
            return RedirectToAction(nameof(Details), new { id});
        }
            // GET: ResultContainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;            
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            var applicationDbContext = await _context.ResultContainer.Include(r => r.ColumnLabel).Include(r => r.ResultRepository).Where(a => a.ResultRepositoryId == id).ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.FindAsync(_context.ResultContainer.Max(a => a.ColumnLabelId));
            viewUploadedResult.columnLabel = obj;
            //-----------------------------------------
            List<int> statistics = new List<int>();
            Type type = typeof(ResultContainer);
            PropertyInfo[] properties = type.GetProperties();
            int counter = 0;
            int columnCount = 0;
            bool IsDataCleaned = true;
            var desiredTable = applicationDbContext.GroupBy(o => o.C1,
                                        o => 1, // don't need the whole object
                                        (key, g) => new { key, count = g.Sum() }).Where(a => a.count > 1);
            var desiredDictionary = applicationDbContext.ToDictionary(x => x.ResultContainerId, x => x.C3);
            foreach (PropertyInfo property in properties)
            {
                if (columnCount > 0 && columnCount < 16)
                {
                    counter = 0;
                    foreach (var record in applicationDbContext)
                    {
                        if (property.GetValue(record) == null) // check obj has value for that particular property
                        {
                            counter++;
                            IsDataCleaned = false;
                        }
                    }
                    statistics.Add(counter);
                }
                columnCount++;
            }
            viewUploadedResult.compileResult = statistics;
            //-----------------------------------------
            ResultRepository currentRepositoryResult = await _context.ResultRepository.FindAsync(id);
            /*if (currentRepositoryResult.IsDataCleaned != IsDataCleaned)
            {
                currentRepositoryResult.IsDataCleaned = IsDataCleaned;
                _context.Update(currentRepositoryResult);
                await _context.SaveChangesAsync();
            }*/
            ViewBag.IsDataCleaned = currentRepositoryResult.IsDataCleaned;
            ViewBag.IsSelctionCriteriaApplied = currentRepositoryResult.IsSelctionCriteriaApplied;
            ViewBag.FYearId = currentRepositoryResult.ScholarshipFiscalYearId;
            ViewBag.SLId = currentRepositoryResult.SchemeLevelId;
            return View(viewUploadedResult);
        }

        // GET: ResultContainers/Create
        public IActionResult Create()
        {
            ViewData["ColumnLabelId"] = new SelectList(_context.ColumnLabel, "ColumnLabelId", "ColumnLabelId");
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId");
            return View();
        }

        // POST: ResultContainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultContainerId,ResultRepositoryId,ColumnLabelId,C1,C2,C3,C4,C5,C6,C7,C8,C9,C10,C11,C12,C13,C14,C15,IsOnCriteria,IsSelected")] ResultContainer resultContainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultContainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnLabelId"] = new SelectList(_context.ColumnLabel, "ColumnLabelId", "ColumnLabelId", resultContainer.ColumnLabelId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", resultContainer.ResultRepositoryId);
            return View(resultContainer);
        }

        // GET: ResultContainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultContainer = await _context.ResultContainer.Include(a=>a.ColumnLabel).Where(a=>a.ResultContainerId == id).FirstOrDefaultAsync();
            ViewBag.PID = resultContainer.ResultRepositoryId;
            if (resultContainer == null)
            {
                return NotFound();
            }
            ViewData["ColumnLabelId"] = new SelectList(_context.ColumnLabel, "ColumnLabelId", "ColumnLabelId", resultContainer.ColumnLabelId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", resultContainer.ResultRepositoryId);
            return View(resultContainer);
        }

        // POST: ResultContainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultContainerId,ResultRepositoryId,ColumnLabelId,C1,C2,C3,C4,C5,C6,C7,C8,C9,C10,C11,C12,C13,C14,C15,IsOnCriteria,IsSelected")] ResultContainer resultContainer)
        {
            if (id != resultContainer.ResultContainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(resultContainer);
                    await _context.SaveChangesAsync();*/
                    var oldResult = await _context.ResultContainer.FindAsync(id);
                    _context.Entry(oldResult).CurrentValues.SetValues(resultContainer);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultContainerExists(resultContainer.ResultContainerId))
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
            ViewData["ColumnLabelId"] = new SelectList(_context.ColumnLabel, "ColumnLabelId", "ColumnLabelId", resultContainer.ColumnLabelId);
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", resultContainer.ResultRepositoryId);
            return View(resultContainer);
        }

        // GET: ResultContainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultContainer = await _context.ResultContainer
                .Include(r => r.ColumnLabel)
                .Include(r => r.ResultRepository)
                .FirstOrDefaultAsync(m => m.ResultContainerId == id);
            if (resultContainer == null)
            {
                return NotFound();
            }

            return View(resultContainer);
        }

        // POST: ResultContainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultContainer = await _context.ResultContainer.FindAsync(id);
            _context.ResultContainer.Remove(resultContainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultContainerExists(int id)
        {
            return _context.ResultContainer.Any(e => e.ResultContainerId == id);
        }
    }
}
