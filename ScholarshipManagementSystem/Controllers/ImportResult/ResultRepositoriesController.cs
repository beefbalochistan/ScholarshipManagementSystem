using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.ViewModels;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class ResultRepositoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultRepositoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultRepositories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ResultRepository.Include(r => r.SchemeLevelPolicy.SchemeLevel).Include(r => r.ScholarshipFiscalYear);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Test(ViewUploadedResult viewUploadedResult)
        {                        
            return View(viewUploadedResult);
        }
        // GET: ResultRepositories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository
                .Include(r => r.SchemeLevelPolicy.SchemeLevel)
                .Include(r => r.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.ResultRepositoryId == id);
            if (resultRepository == null)
            {
                return NotFound();
            }

            return View(resultRepository);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResultRepository resultRepository, IFormFile batchUsers, List<string> selectedColumn)
        {
            if (ModelState.IsValid)
            {
                if (_context.ResultRepository.Include(a=>a.SchemeLevelPolicy).Count(a => a.ScholarshipFiscalYearId == resultRepository.ScholarshipFiscalYearId && a.SchemeLevelPolicy.SchemeLevelId == resultRepository.SchemeLevelPolicyId) == 0)
                {
                    if (batchUsers?.Length > 0)
                    {
                        _context.Add(resultRepository);
                        await _context.SaveChangesAsync();
                        int MaxResultRespositoryId = _context.ResultRepository.Max(a => a.ResultRepositoryId);
                        // convert to a stream
                        var stream = batchUsers.OpenReadStream();

                        //List<ResultContainer> resultContainer = new List<ResultContainer>();

                        try
                        {
                            using (var package = new ExcelPackage(stream))
                            {
                                var worksheet = package.Workbook.Worksheets.First();
                                var rowCount = worksheet.Dimension.Rows;
                                var colCount = worksheet.Dimension.Columns;
                                List<int> columnList = new List<int>();
                                for (var val = 0; val < selectedColumn.Count; val++)
                                {
                                    for (var col = 1; col <= colCount; col++)
                                    {
                                        if (worksheet.Cells[1, col].Value.ToString().ToLower() == selectedColumn.ElementAt(val).ToLower())
                                        {
                                            columnList.Add(col);
                                            break;
                                        }
                                    }
                                }
                                //-------------------------------------------------------
                                List<string> columnNameList = new List<string>();
                                int counter = 0;
                                for (var val = 1; val <= 13; val++)//KDA Hard
                                {
                                    if (counter < columnList.Count && val == columnList.ElementAt(counter))
                                    {
                                        columnNameList.Add(selectedColumn.ElementAt(counter).ToString());
                                        counter++;
                                    }
                                    else
                                    {
                                        columnNameList.Add("");
                                    }
                                }
                                var columnLabel = new ColumnLabel()
                                {
                                    C1 = columnNameList.ElementAt(0),
                                    C2 = columnNameList.ElementAt(1),
                                    C3 = columnNameList.ElementAt(2),
                                    C4 = columnNameList.ElementAt(3),
                                    C5 = columnNameList.ElementAt(4),
                                    C6 = columnNameList.ElementAt(5),
                                    C7 = columnNameList.ElementAt(6),
                                    C8 = columnNameList.ElementAt(7),
                                    C9 = columnNameList.ElementAt(8),
                                    C10 = columnNameList.ElementAt(9),
                                    C11 = columnNameList.ElementAt(10),
                                    C12 = columnNameList.ElementAt(11),
                                    C13 = columnNameList.ElementAt(12),                                    
                                    IsActive = true,
                                    ResultRepositoryId = MaxResultRespositoryId
                                };
                                _context.Add(columnLabel);
                                await _context.SaveChangesAsync();                                
                                if (columnList.Count == selectedColumn.Count)
                                {
                                    //-------------------------------------------------------                                    
                                    var districts = _context.District.Select(a=> new District {DistrictId = a.DistrictId, Name = a.Name.ToLower() }).ToList();                                                                  
                                    for (var row = 2; row < rowCount; row++)
                                    {
                                        counter = 0;
                                        try
                                        {
                                            List<string> specificExcelRow = new List<string>();
                                            for (var val = 1; val <= 13; val++)//KDA Hard
                                            {
                                                if (counter < columnList.Count && val == columnList.ElementAt(counter))
                                                {
                                                    specificExcelRow.Add(worksheet.Cells[row, columnList.ElementAt(counter)].Value?.ToString());
                                                    counter++;
                                                }
                                                else
                                                {
                                                    specificExcelRow.Add("");
                                                }
                                            }

                                            var result = new ResultContainer()
                                            {      
                                                //ColumnLabelId = 43,
                                                ResultRepositoryId = MaxResultRespositoryId,
                                                Roll_NO = specificExcelRow.ElementAt(0),
                                                REG_NO = specificExcelRow.ElementAt(1),
                                                Name = specificExcelRow.ElementAt(2),
                                                Father_Name = specificExcelRow.ElementAt(3),
                                                Institute = specificExcelRow.ElementAt(4),
                                                Group = specificExcelRow.ElementAt(5),
                                                Candidate_District = specificExcelRow.ElementAt(6),
                                                Institute_District = specificExcelRow.ElementAt(7),                                                                                                
                                                Marks_ = specificExcelRow.ElementAt(8),
                                                Pass_Fail = specificExcelRow.ElementAt(9),
                                                Remarks = specificExcelRow.ElementAt(10),                                                
                                                CNIC = specificExcelRow.ElementAt(11),                                                
                                                CGPA = specificExcelRow.ElementAt(12),                                                
                                                DistrictId = 1
                                            };                                                                             
                                            string districtName = result.Candidate_District.ToLower();
                                            if (districts.Count(a => a.Name == districtName) != 0)
                                            {
                                                try
                                                {
                                                    result.DistrictId = districts.Where(a => a.Name == districtName).Max(a => a.DistrictId);
                                                }        
                                                catch(Exception ex)
                                                {                                                    
                                                    ViewBag.Message = ex.Message.ToString();
                                                }
                                            }
                                            await _context.AddAsync(result);
                                        }
                                        catch (Exception ex)
                                        {
                                            ViewBag.Message = ex.Message.ToString();
                                        }
                                    }                                    
                                    await _context.SaveChangesAsync();                                   
                                }
                                else
                                {

                                }
                            }
                            //return RedirectToAction("Index", "ResultContainers", new { id = MaxResultRespositoryId });
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }                    
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "Name", "Name");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            ViewBag.Message = "Record already exist of " + _context.ScholarshipFiscalYear.Find(resultRepository.ScholarshipFiscalYearId).Name + " Fiscal Year of Scheme " + _context.SchemeLevel.Find(resultRepository.SchemeLevelPolicy.SchemeLevelId).Name;
            resultRepository.CreatedOn = DateTime.Now.Date;
            return View(resultRepository);            
        }
        // GET: ResultRepositories/Create
        public IActionResult Create()
        {
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a=>a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName, "Name", "Name");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code");
            ResultRepository obj = new ResultRepository();
            obj.CreatedOn = DateTime.Now.Date;
            return View(obj);
        }

        // POST: ResultRepositories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultRepositoryId,resultFilePath,ScholarshipFiscalYearId,SchemeLevelId,CreatedOn,IsSelctionCriteriaApplied,IsDataCleaned")] ResultRepository resultRepository)
        {
            if (ModelState.IsValid)
            {
                if (_context.ResultRepository.Count(a=>a.ScholarshipFiscalYearId == resultRepository.ScholarshipFiscalYearId && a.SchemeLevelId == resultRepository.SchemeLevelId) == 0)
                {
                    _context.Add(resultRepository);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Record already exist of "+ _context.ScholarshipFiscalYear.Find(resultRepository.ScholarshipFiscalYearId).Name +" Fiscal Year of Scheme " + _context.SchemeLevel.Find(resultRepository.SchemeLevelId).Name;
                }
                
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }*/

        // GET: ResultRepositories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository.FindAsync(id);
            if (resultRepository == null)
            {
                return NotFound();
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }

        // POST: ResultRepositories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultRepositoryId,resultFilePath,ScholarshipFiscalYearId,SchemeLevelId,currentCounter,CreatedOn,IsSelctionCriteriaApplied,IsDataCleaned")] ResultRepository resultRepository)
        {
            if (id != resultRepository.ResultRepositoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultRepository);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultRepositoryExists(resultRepository.ResultRepositoryId))
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
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }

        // GET: ResultRepositories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository
                .Include(r => r.SchemeLevelPolicy.SchemeLevel)
                .Include(r => r.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.ResultRepositoryId == id);
            if (resultRepository == null)
            {
                return NotFound();
            }

            return View(resultRepository);
        }

        // POST: ResultRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultRepository = await _context.ResultRepository.FindAsync(id);
            _context.ResultRepository.Remove(resultRepository);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultRepositoryExists(int id)
        {
            return _context.ResultRepository.Any(e => e.ResultRepositoryId == id);
        }
    }
}
