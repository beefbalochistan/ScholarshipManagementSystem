using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;
using DAL.Models.Domain.ScholarshipSetup;
using Newtonsoft.Json;
using DAL.Models.ViewModels;
using DAL.Models.Domain.ImportResult;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class DocumentAssistGeneralsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentAssistGeneralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DocumentAssistGenerals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DocumentAssistGeneral.Include(d => d.DocumentAssist).Include(d => d.ExcelColumnName).Include(a=>a.SchemeLevel.Scheme);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DocumentAssistGenerals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistGeneral = await _context.DocumentAssistGeneral
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .FirstOrDefaultAsync(m => m.DocumentAssistGeneralId == id);
            if (documentAssistGeneral == null)
            {
                return NotFound();
            }

            return View(documentAssistGeneral);
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
        // GET: DocumentAssistGenerals/Create
        public IActionResult Create()
        {                                          
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");            
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "ConditionalOperator");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "Name");            
            return View();
        }

        // POST: DocumentAssistGenerals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentAssistGeneral documentAssistGeneral, int PolicySRCForumId, int SchemeId)
        {
            if (ModelState.IsValid)
            {
                var IsExist = _context.DocumentAssistGeneral.Count(a=>a.DocumentAssistId == documentAssistGeneral.DocumentAssistId && a.ExcelColumnNameId == documentAssistGeneral.ExcelColumnNameId);
                _context.Add(documentAssistGeneral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                        
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", SchemeId);            
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "Name", documentAssistGeneral.ExcelColumnNameId);
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "DocumentAssistId", documentAssistGeneral.DocumentAssistId);            
            return View(documentAssistGeneral);
        }

        // GET: DocumentAssistGenerals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistGeneral = await _context.DocumentAssistGeneral.FindAsync(id);
            if (documentAssistGeneral == null)
            {
                return NotFound();
            }
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Where(a => a.SchemeLevelId == documentAssistGeneral.SchemeLevelId).Select(a => a.SchemeId).FirstOrDefault());
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "ConditionalOperator", documentAssistGeneral.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "Name", documentAssistGeneral.ExcelColumnNameId);
            return View(documentAssistGeneral);
        }

        // POST: DocumentAssistGenerals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentAssistGeneral documentAssistGeneral)
        {
            if (id != documentAssistGeneral.DocumentAssistGeneralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentAssistGeneral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentAssistGeneralExists(documentAssistGeneral.DocumentAssistGeneralId))
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
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", _context.SchemeLevel.Where(a => a.SchemeLevelId == documentAssistGeneral.SchemeLevelId).Select(a => a.SchemeId).FirstOrDefault());
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "ConditionalOperator", documentAssistGeneral.DocumentAssistId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "ExcelColumnNameId", "Name", documentAssistGeneral.ExcelColumnNameId);
            return View(documentAssistGeneral);
        }

        // GET: DocumentAssistGenerals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentAssistGeneral = await _context.DocumentAssistGeneral
                .Include(d => d.DocumentAssist)
                .Include(d => d.ExcelColumnName)
                .FirstOrDefaultAsync(m => m.DocumentAssistGeneralId == id);
            if (documentAssistGeneral == null)
            {
                return NotFound();
            }

            return View(documentAssistGeneral);
        }

        // POST: DocumentAssistGenerals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentAssistGeneral = await _context.DocumentAssistGeneral.FindAsync(id);
            _context.DocumentAssistGeneral.Remove(documentAssistGeneral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetDocumentAssistGeneral(int rrId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            var applicationDbContext = _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName)/*.Where(a=>a.SchemeLevelId == SchemeLevelId)*/.ToList();
            /*if(DegreeScholarshipLevelId != 0)
            {
                applicationDbContext = applicationDbContext.Where(a=>a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).ToList();
            }*/
            ColumnLabel columnLabels = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == rrId).FirstOrDefaultAsync();
            List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.Where(a=>a.IsActive == true).ToList();
            List<DocumentAssistGeneral> documentAssistGenerals = new List<DocumentAssistGeneral>();
            string record = JsonConvert.SerializeObject(columnLabels);
            foreach (var indicator in applicationDbContext)
            {
                if (record.Contains(indicator.ExcelColumnName.Name))
                {
                    documentAssistGenerals.Add(indicator);
                }                           
            }
            
            return PartialView(documentAssistGenerals);
        }

        public async Task<IActionResult> GetDocumentAssistGeneralTemp(int rrId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            var applicationDbContext = _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName)/*.Where(a => a.SchemeLevelId == SchemeLevelId)*/.ToList();
            /*if (DegreeScholarshipLevelId != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).ToList();
            }*/
            ColumnLabelTemp columnLabels = await _context.ColumnLabelTemp.Where(a => a.ResultRepositoryTempId == rrId).FirstOrDefaultAsync();
            List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.Where(a=>a.IsActive == true).ToList();
            List<DocumentAssistGeneral> documentAssistGenerals = new List<DocumentAssistGeneral>();
            string record = JsonConvert.SerializeObject(columnLabels);
            foreach (var indicator in applicationDbContext)
            {
                if (record.Contains(indicator.ExcelColumnName.Name))
                {
                    documentAssistGenerals.Add(indicator);
                }
            }

            return PartialView(documentAssistGenerals.OrderBy(a=>a.ExcelColumnNameId));
        }

        public async Task<IActionResult> AssistDocument(int rrId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            /*var applicationDbContext = await _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName).Where(a => a.SchemeLevelId == SchemeLevelId).ToListAsync();
            if (DegreeScholarshipLevelId != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).ToList();
            }*/
            var resultRepositoryTemp = _context.ResultRepositoryTemp.Find(rrId);
            var IsDocAssist = 0;
            if(DegreeScholarshipLevelId != 0)
            {
                IsDocAssist = _context.ResultRepository.Count(a => a.DegreeScholarshipLevelId == DegreeScholarshipLevelId && a.ScholarshipFiscalYearId == resultRepositoryTemp.ScholarshipFiscalYearId && a.IsDataCleaned == true);
            }
            else
            {
                IsDocAssist = _context.ResultRepository.Count(a => a.SchemeLevelPolicyId == resultRepositoryTemp.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == resultRepositoryTemp.ScholarshipFiscalYearId && a.IsDataCleaned == true);
            }
            List<DocumentAssistGeneral> documentAssistGenerals = new List<DocumentAssistGeneral>();
            if (IsDocAssist == 0)
            {
                var applicationDbContext = await _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName).ToListAsync();
                ColumnLabelTemp columnLabels = await _context.ColumnLabelTemp.Where(a => a.ResultRepositoryTempId == rrId).FirstOrDefaultAsync();
                List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.Where(a=>a.IsActive == true).ToList();                
                string record = JsonConvert.SerializeObject(columnLabels);
                foreach (var indicator in applicationDbContext)
                {
                    if (record.Contains(indicator.ExcelColumnName.Name))
                    {
                        DocumentAssistGeneral obj = new DocumentAssistGeneral();
                        obj = indicator;
                        documentAssistGenerals.Add(obj);
                    }
                }
                ViewBag.RRId = rrId;
                ViewBag.DegreeScholarshipLevelId = DegreeScholarshipLevelId;
                ViewBag.TotalRecord = _context.ResultContainerTemp.Count(a => a.ResultRepositoryTempId == rrId);
                List<SPAssistDocumentViewer> sPAssistDocumentViewers = new List<SPAssistDocumentViewer>();
                int counter = 0;
                foreach (var documentAssist in documentAssistGenerals)
                {
                    if (documentAssist.DocumentAssistId == 1)
                    {
                        documentAssistGenerals.ElementAt(counter).TotalFind = await CallSP("exec [ImportResult].[SPCheckNullValues] {0}, {1}, {2}, {3}", documentAssist.ExcelColumnName.Name, rrId, documentAssist.DocumentAssistId, "ResultContainerTemp", "ResultRepositoryTemp");
                    }
                    else if (documentAssist.DocumentAssistId == 2)
                    {
                        documentAssistGenerals.ElementAt(counter).TotalFind = await CallSP("exec [ImportResult].[SPCheckDouplicate] {0}, {1}, {2}, {3}", documentAssist.ExcelColumnName.Name, rrId, documentAssist.DocumentAssistId, "ResultContainerTemp", "ResultRepositoryTemp");
                    }
                    counter++;
                }
                ViewBag.TotalIssues = documentAssistGenerals.Sum(a => a.TotalFind);
                if (documentAssistGenerals.Sum(a => a.TotalFind) == 0)
                {
                    resultRepositoryTemp = _context.ResultRepositoryTemp.Find(rrId);
                    ResultRepository resultRepository = new ResultRepository();
                    resultRepository.CreatedOn = DateTime.Today;
                    resultRepository.currentCounter = 0;
                    resultRepository.DegreeScholarshipLevelId = resultRepositoryTemp.DegreeScholarshipLevelId;
                    resultRepository.IsDataCleaned = true;
                    resultRepository.IsMeritListGenerated = false;
                    resultRepository.IsSelctionCriteriaApplied = false;
                    resultRepository.resultFilePath = resultRepositoryTemp.resultFilePath;
                    resultRepository.resultScannedFilePath = resultRepositoryTemp.resultScannedFilePath;
                    resultRepository.SchemeLevelPolicyId = resultRepositoryTemp.SchemeLevelPolicyId;
                    resultRepository.ScholarshipFiscalYearId = resultRepositoryTemp.ScholarshipFiscalYearId;
                    _context.Add(resultRepository);
                    _context.SaveChanges();
                    int RRMaxId = _context.ResultRepository.Max(a => a.ResultRepositoryId);
                    //-----------------------------Add Column Entry---------------------------------
                    ColumnLabel column = new ColumnLabel();
                    column.C1 = columnLabels.C1;
                    column.C2 = columnLabels.C2;
                    column.C3 = columnLabels.C3;
                    column.C4 = columnLabels.C4;
                    column.C5 = columnLabels.C5;
                    column.C6 = columnLabels.C6;
                    column.C7 = columnLabels.C7;
                    column.C8 = columnLabels.C8;
                    column.C9 = columnLabels.C9;
                    column.C10 = columnLabels.C10;
                    column.C11 = columnLabels.C11;
                    column.C12 = columnLabels.C12;
                    column.C13 = columnLabels.C13;
                    column.C14 = columnLabels.C14;
                    column.C15 = columnLabels.C15;
                    column.C16 = columnLabels.C16;
                    column.IsActive = columnLabels.IsActive;
                    column.ResultRepositoryId = RRMaxId;
                    _context.Add(column);
                    _context.SaveChanges();
                    //------------------------------------------------------------------------------
                    var resultContainerTemp = _context.ResultContainerTemp.Where(a => a.ResultRepositoryTempId == rrId).ToList();
                    foreach (var result in resultContainerTemp)
                    {
                        ResultContainer obj = new ResultContainer();
                        obj.Candidate_District = result.Candidate_District;
                        obj.CGPA = result.CGPA;
                        obj.CNIC = result.CNIC;
                        obj.Department = result.Department;
                        obj.DistrictId = result.DistrictId;
                        obj.Father_Name = result.Father_Name;
                        obj.Group = result.Group;
                        obj.Institute = result.Institute;
                        obj.Institute_District = result.Institute_District;
                        obj.IsOnCriteria = false;
                        obj.IsSelected = false;
                        obj.Marks_ = result.Marks_;
                        obj.Name = result.Name;
                        obj.Pass_Fail = result.Pass_Fail;
                        obj.REG_NO = result.REG_NO;
                        obj.Remarks = result.Remarks;
                        obj.Roll_NO = result.Roll_NO;
                        obj.ResultRepositoryId = RRMaxId;
                        _context.Add(obj);
                    }
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultContainerTemp] where ResultRepositoryTempId = " + rrId);
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultRepositoryTemp] where ResultRepositoryTempId = " + rrId);
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ColumnLabelTemp] where ResultRepositoryTempId = " + rrId);
                    ViewBag.IsAssist = false;
                }
                else
                {
                    ViewBag.IsAssist = false;
                }                     
            }
            else
            {
                ViewBag.IsAssist = true;
            }
            return PartialView(documentAssistGenerals);
        }
        public async Task<JsonResult> ProcessResultRequestWithIssues(int rrId, int degreeScholarshipLevelId)
        {
            int RRMaxId = 0;
            try
            {
                var resultRepositoryTemp = _context.ResultRepositoryTemp.Find(rrId);
                var IsAlreadyExist = 0;
                if(degreeScholarshipLevelId == 0)
                {
                    IsAlreadyExist = _context.ResultRepository.Count(a => a.SchemeLevelPolicyId == resultRepositoryTemp.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == resultRepositoryTemp.ScholarshipFiscalYearId);
                }
                else
                {
                    IsAlreadyExist = _context.ResultRepository.Count(a => a.DegreeScholarshipLevelId == resultRepositoryTemp.DegreeScholarshipLevelId && a.ScholarshipFiscalYearId == resultRepositoryTemp.ScholarshipFiscalYearId);
                }                
                if (IsAlreadyExist == 0)
                {
                    ResultRepository resultRepository = new ResultRepository();
                    resultRepository.CreatedOn = DateTime.Today;
                    resultRepository.currentCounter = 0;
                    resultRepository.DegreeScholarshipLevelId = resultRepositoryTemp.DegreeScholarshipLevelId;
                    resultRepository.IsDataCleaned = true;
                    resultRepository.IsMeritListGenerated = false;
                    resultRepository.IsSelctionCriteriaApplied = false;
                    resultRepository.resultFilePath = resultRepositoryTemp.resultFilePath;
                    resultRepository.resultScannedFilePath = resultRepositoryTemp.resultScannedFilePath;
                    resultRepository.SchemeLevelPolicyId = resultRepositoryTemp.SchemeLevelPolicyId;
                    resultRepository.ScholarshipFiscalYearId = resultRepositoryTemp.ScholarshipFiscalYearId;
                    _context.Add(resultRepository);
                    _context.SaveChanges();
                    RRMaxId = _context.ResultRepository.Max(a => a.ResultRepositoryId);
                    //-----------------------------Add Column Entry---------------------------------
                    ColumnLabelTemp columnLabels = await _context.ColumnLabelTemp.Where(a => a.ResultRepositoryTempId == rrId).FirstOrDefaultAsync();
                    ColumnLabel column = new ColumnLabel();
                    column.C1 = columnLabels.C1;
                    column.C2 = columnLabels.C2;
                    column.C3 = columnLabels.C3;
                    column.C4 = columnLabels.C4;
                    column.C5 = columnLabels.C5;
                    column.C6 = columnLabels.C6;
                    column.C7 = columnLabels.C7;
                    column.C8 = columnLabels.C8;
                    column.C9 = columnLabels.C9;
                    column.C10 = columnLabels.C10;
                    column.C11 = columnLabels.C11;
                    column.C12 = columnLabels.C12;
                    column.C13 = columnLabels.C13;
                    column.C14 = columnLabels.C14;
                    column.C15 = columnLabels.C15;
                    column.C16 = columnLabels.C16;
                    column.IsActive = columnLabels.IsActive;
                    column.ResultRepositoryId = RRMaxId;
                    _context.Add(column);
                    _context.SaveChanges();
                    //------------------------------------------------------------------------------
                    var resultContainerTemp = _context.ResultContainerTemp.Where(a => a.ResultRepositoryTempId == rrId).ToList();
                    foreach (var result in resultContainerTemp)
                    {
                        ResultContainer obj = new ResultContainer();
                        obj.Candidate_District = result.Candidate_District;
                        obj.CGPA = result.CGPA;
                        obj.CNIC = result.CNIC;
                        obj.Department = result.Department;
                        obj.DistrictId = result.DistrictId;
                        obj.Father_Name = result.Father_Name;
                        obj.Group = result.Group;
                        obj.Institute = result.Institute;
                        obj.Institute_District = result.Institute_District;
                        obj.IsOnCriteria = false;
                        obj.IsSelected = false;
                        obj.Marks_ = result.Marks_;
                        obj.Name = result.Name;
                        obj.Pass_Fail = result.Pass_Fail;
                        obj.REG_NO = result.REG_NO;
                        obj.Remarks = result.Remarks;
                        obj.Roll_NO = result.Roll_NO;
                        obj.ResultRepositoryId = RRMaxId;
                        _context.Add(obj);
                    }
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultContainerTemp] where ResultRepositoryTempId = " + rrId);
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultRepositoryTemp] where ResultRepositoryTempId = " + rrId);
                    _context.Database.ExecuteSqlRaw("delete [ImportResult].[ColumnLabelTemp] where ResultRepositoryTempId = " + rrId);
                }
                else
                {
                    return Json(new { isValid = true, message = "Document Already Assist Successfully!"});
                }        
            }
            catch(Exception ex)
            {
                return Json(new { isValid = false, message = "Something went wrong!" });
            }            
            return Json(new {isValid = true, message = "Assessment Completed Successfully!"});
        }
        public async Task<IActionResult> AssistDocumentTemp(int rrId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            /*var applicationDbContext = await _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName).Where(a => a.SchemeLevelId == SchemeLevelId).ToListAsync();
            if (DegreeScholarshipLevelId != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).ToList();
            }*/
            var applicationDbContext = await _context.DocumentAssistGeneral.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName).ToListAsync();
            ColumnLabelTemp columnLabels = await _context.ColumnLabelTemp.Where(a => a.ResultRepositoryTempId == rrId).FirstOrDefaultAsync();
            List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.Where(a=>a.IsActive == true).ToList();
            List<DocumentAssistGeneral> documentAssistGenerals = new List<DocumentAssistGeneral>();
            string record = JsonConvert.SerializeObject(columnLabels);
            foreach (var indicator in applicationDbContext)
            {
                if (record.Contains(indicator.ExcelColumnName.Name))
                {
                    DocumentAssistGeneral obj = new DocumentAssistGeneral();
                    obj = indicator;
                    documentAssistGenerals.Add(obj);
                }
            }
            ViewBag.RRId = rrId;
            ViewBag.TotalRecord = _context.ResultContainerTemp.Count(a => a.ResultRepositoryTempId == rrId);
            List<SPAssistDocumentViewer> sPAssistDocumentViewers = new List<SPAssistDocumentViewer>();
            int counter = 0;
            foreach (var documentAssist in documentAssistGenerals)
            {
                if (documentAssist.DocumentAssistId == 1)
                {
                    documentAssistGenerals.ElementAt(counter).TotalFind = await CallSP("exec [ImportResult].[SPCheckNullValues] {0}, {1}, {2}, {3}", documentAssist.ExcelColumnName.Name, rrId, documentAssist.DocumentAssistId, "ResultContainerTemp", "ResultRepositoryTemp");
                }
                else if (documentAssist.DocumentAssistId == 2)
                {
                    documentAssistGenerals.ElementAt(counter).TotalFind = await CallSP("exec [ImportResult].[SPCheckDouplicate] {0}, {1}, {2}, {3}", documentAssist.ExcelColumnName.Name, rrId, documentAssist.DocumentAssistId, "ResultContainerTemp", "ResultRepositoryTemp");
                }
                counter++;
            }
            return PartialView(documentAssistGenerals);
        }
        public async Task<int> CallSP(string SP, string column, int rrId, int DocumentAssistId, string fromTable, string fromparent)
        {            
            int result = 0;
            try
            {
                if (DocumentAssistId == 1)
                {
                    var applicationDbContext = _context.SPAssistDocumentViewer.FromSqlRaw<SPAssistDocumentViewer>(SP, column, rrId, fromTable, fromparent).ToList<SPAssistDocumentViewer>();
                    List<SPAssistDocumentViewer> temp = new List<SPAssistDocumentViewer>();
                    temp = applicationDbContext.ToList();
                    result =  temp.ElementAt(0).TotalFind;                    
                }
                else if (DocumentAssistId == 2)
                {
                    var applicationDbContext = _context.SPAssistDocumentViewer.FromSqlRaw<SPAssistDocumentViewer>(SP, column, rrId, fromTable, fromparent).ToList<SPAssistDocumentViewer>();
                    if (applicationDbContext != null)
                    {
                        List<SPAssistDocumentViewer> temp = new List<SPAssistDocumentViewer>();
                        temp = applicationDbContext.ToList();
                        result = temp.Count();                        
                    }                    
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public async Task<IActionResult> ViewScannedRecords(string column, int rrId, int DocumentAssistId)
        {

            try
            {
                if (DocumentAssistId == 1)
                {
                    var applicationDbContext = await _context.SPDocumentViewerReport.FromSqlRaw("exec [ImportResult].[SPCheckNullValuesReport] {0}, {1}, {2}, {3}", column, rrId, "ResultContainerTemp", "ResultRepositoryTemp").ToListAsync();
                    return PartialView(applicationDbContext);
                }
                else if (DocumentAssistId == 2)
                {
                    var applicationDbContext = await _context.SPDocumentViewerReport.FromSqlRaw("exec [ImportResult].[SPCheckDouplicateReport] {0}, {1}, {2}, {3}", column, rrId, "ResultContainerTemp", "ResultRepositoryTemp").ToListAsync();
                    return PartialView(applicationDbContext);
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView();
        }
        public async Task<IActionResult> ViewScannedRecordsTemp(string column, int rrId, int DocumentAssistId)
        {
                        
            try
            {
                if (DocumentAssistId == 1)
                {
                    var applicationDbContext = await _context.SPDocumentViewerReport.FromSqlRaw("exec [ImportResult].[SPCheckNullValuesReport] {0}, {1}, {2}, {3}", column, rrId, "ResultContainerTemp", "ResultRepositoryTemp").ToListAsync();
                    return PartialView(applicationDbContext);
                }
                else if (DocumentAssistId == 2)
                {
                    var applicationDbContext = await _context.SPDocumentViewerReport.FromSqlRaw("exec [ImportResult].[SPCheckDouplicateReport] {0}, {1}, {2}, {3}", column, rrId, "ResultContainerTemp", "ResultRepositoryTemp").ToListAsync();
                    return PartialView(applicationDbContext);
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView();
        }
        private bool DocumentAssistGeneralExists(int id)
        {
            return _context.DocumentAssistGeneral.Any(e => e.DocumentAssistGeneralId == id);
        }
    }
}
