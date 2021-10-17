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
        public IActionResult ReloadEvents(int id, int RRId)
        {
            return ViewComponent("FilterResult", new { id, RRId });
        }
        public IActionResult ReloadEventMeritList(int id, int SLPId, string selectedMethod, string selectedStatus)
        {
            return ViewComponent("MeritList", new { id, SLPId, selectedMethod , selectedStatus });
        }
        // GET: ResultContainers
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.RRId = id;
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            var applicationDbContext = await _context.ResultContainer.Include(r => r.ResultRepository).Where(a => a.ResultRepositoryId == id && a.DistrictId == 1).ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.Where(a=>a.ResultRepositoryId == id).FirstOrDefaultAsync();
            viewUploadedResult.columnLabel = obj;
            //-----------------------------------------
            ViewData["DistrictId"] = new SelectList(_context.District.Where(a=>a.IsActive == true), "DistrictId", "Name");
            return View(viewUploadedResult);
        }
        public async Task<IActionResult> CompileResult(int id)
        {
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            var applicationDbContext = await _context.ResultContainer.Include(r => r.ResultRepository).Where(a => a.ResultRepositoryId == id).ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == id).FirstOrDefaultAsync();
            viewUploadedResult.columnLabel = obj;
            //-----------------------------------------
            List<int> statistics = new List<int>();
            Type type = typeof(ResultContainer);
            PropertyInfo[] properties = type.GetProperties();
            int counter = 0;
            int columnCount = 0;
            bool IsDataCleaned = true;

            foreach (PropertyInfo property in properties)
            {
                if (columnCount > 0 && columnCount < 13)//KDA Hard
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
            if (currentRepositoryResult.IsDataCleaned != IsDataCleaned)
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
            var currentPolicy = _context.SchemeLevelPolicy.Include(a=>a.SchemeLevel.QualificationLevel).Include(a => a.PolicySRCForum.ScholarshipFiscalYear).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == FYearId && a.PolicySRCForum.IsEndorse == true && a.SchemeLevelId == SLId).FirstOrDefault();
            //var POML = _context.ResultContainer.Where(a => a.ResultRepositoryId == id && a.IsOnCriteria == true).Take((int)currentPolicy.POMS);
            //---------------Get Column ---------------------------------                                                         
            //-----------------------------------------------------------
            //var POMLCandidates = _context.ResultContainer.OrderBjy(x =>((string)x.GetType().GetProperty(markColumnName).GetValue(x, null)));
            var POMLCandidates = _context.ResultContainer.Where(a=>a.ResultRepositoryId == id && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round(currentPolicy.POMS)).ToList();
            int counter = 1;
            foreach(var result in POMLCandidates)
            {
                Applicant applicant = new Applicant();
                applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                applicant.Name = result.Name;
                applicant.DistrictId = result.DistrictId;
                applicant.ProvienceId = _context.District.Include(a=>a.Division.Provience).Where(a=>a.DistrictId == applicant.DistrictId).Select(a=>a.Division.ProvienceId).FirstOrDefault();
                applicant.FatherName = result.Father_Name;
                applicant.ReceivedMarks = int.Parse(result.Marks_);
                applicant.RollNumber = result.Roll_NO;
                applicant.SelectedMethod = "POMS";
                applicant.RegisterationNumber = result.REG_NO;
                applicant.EntryThrough = "System";
                applicant.TotalMarks = 1100;//KDA
                applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                applicant.SelectionStatus = "Selected";
                _context.Add(applicant);
                ResultContainer currentResult = new ResultContainer();
                currentResult = result;
                currentResult.IsSelected = true;
                _context.Update(currentResult);
                counter++;
            }
            await _context.SaveChangesAsync();
            //--------------------------------------------------------------
            var districts = _context.District.Where(a=>a.IsActive == true).ToList();
            var SRCForumId = _context.PolicySRCForum.Where(a => a.ScholarshipFiscalYearId == FYearId && a.IsEndorse == true).Max(a => a.PolicySRCForumId);
            var districtQouta = _context.DistrictQoutaBySchemeLevel.Include(a=>a.SchemeLevelPolicy).Where(a => a.PolicySRCForumId == SRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SLId).ToList();            
            float DOMS = 0;
            foreach (var district in districts)
            {
                DOMS = districtQouta.Where(a => a.DistrictId == district.DistrictId && a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId).Sum(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                var DOMSCandidates = _context.ResultContainer.Where(a=> a.Candidate_District == district.Name && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round(DOMS)).ToList();
                foreach (var result in DOMSCandidates)
                {
                    Applicant applicant = new Applicant();
                    applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                    applicant.Name = result.Name;
                    applicant.DistrictId = result.DistrictId;
                    applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                    applicant.FatherName = result.Father_Name;
                    applicant.ReceivedMarks = int.Parse(result.Marks_);
                    applicant.RollNumber = result.Roll_NO;
                    applicant.SelectedMethod = "DOSM";
                    applicant.RegisterationNumber = result.REG_NO;
                    applicant.EntryThrough = "System";
                    applicant.TotalMarks = 1100;//KDA
                    applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                    applicant.SelectionStatus = "Selected";
                    _context.Add(applicant);
                    ResultContainer currentResult = new ResultContainer();
                    currentResult = result;
                    currentResult.IsSelected = true;
                    _context.Update(currentResult);
                    counter++;
                }                
            }
            await _context.SaveChangesAsync();
            //----------------------------POMS 50%----------------------------------
            POMLCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == id && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round((currentPolicy.POMS/2))).ToList();            
            foreach (var result in POMLCandidates)
            {
                Applicant applicant = new Applicant();
                applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                applicant.Name = result.Name;
                applicant.DistrictId = result.DistrictId;
                applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                applicant.FatherName = result.Father_Name;
                applicant.ReceivedMarks = int.Parse(result.Marks_);
                applicant.RollNumber = result.Roll_NO;
                applicant.SelectedMethod = "POMS";
                applicant.RegisterationNumber = result.REG_NO;
                applicant.EntryThrough = "System";
                applicant.TotalMarks = 1100;//KDA
                applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                applicant.SelectionStatus = "Awaited";
                _context.Add(applicant);
                ResultContainer currentResult = new ResultContainer();
                currentResult = result;
                currentResult.IsSelected = true;
                _context.Update(currentResult);
                counter++;
            }
            await _context.SaveChangesAsync();
            //-------------------------DOMS 50%-------------------------------------                                                
            foreach (var district in districts)
            {
                DOMS = districtQouta.Where(a => a.DistrictId == district.DistrictId).Max(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                var DOMSCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == id && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round((DOMS/2))).ToList();
                foreach (var result in DOMSCandidates)
                {
                    Applicant applicant = new Applicant();
                    applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                    applicant.Name = result.Name;
                    applicant.DistrictId = result.DistrictId;
                    applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                    applicant.FatherName = result.Father_Name;
                    applicant.ReceivedMarks = int.Parse(result.Marks_);
                    applicant.RollNumber = result.Roll_NO;
                    applicant.SelectedMethod = "DOMS";
                    applicant.RegisterationNumber = result.REG_NO;
                    applicant.EntryThrough = "System";
                    applicant.TotalMarks = 1100;//KDA
                    applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                    applicant.SelectionStatus = "Awaited";
                    _context.Add(applicant);
                    ResultContainer currentResult = new ResultContainer();
                    currentResult = result;
                    currentResult.IsSelected = true;
                    _context.Update(currentResult);
                    counter++;
                }
            }
            await _context.SaveChangesAsync();

            ResultRepository resultRepository = await _context.ResultRepository.FindAsync(id);
            resultRepository.IsMeritListGenerated = true;
            _context.Update(resultRepository);
            await _context.SaveChangesAsync();
            //--------------------------------------------------------------
            return RedirectToAction(nameof(Details), new { id });
        }
        public async Task<IActionResult> ApplyCriteria(int id)
        {            
            ResultRepository currentRepository = _context.ResultRepository.Find(id);
            var currentCriteria = await _context.SelectionCriteria.Include(a=>a.Operator).Where(a => a.ResultRepositoryId == id).ToListAsync();
            foreach(var criteria in currentCriteria)
            {
                var columnName = _context.ExcelColumnName.Find(criteria.ExcelColumnNameId).Name;
                
                    _context.Database.ExecuteSqlRaw("Update Importresult.ResultContainer set IsOnCriteria=1 where ResultContainerId in (Select ResultcontainerId from ImportResult.ResultContainer WHERE " + columnName + criteria.Operator.Name + " " + criteria.Condition + " and resultrepositoryid="+ id +")");
                    currentRepository.IsSelctionCriteriaApplied = true;
                    _context.Update(currentRepository);
                    await _context.SaveChangesAsync();                                          
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
            //-----------------------------------------
           
            bool IsDataCleaned = true;
            //-----------------------------------------
            var currentRepositoryResult = await _context.ResultRepository.Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a=>a.ResultRepositoryId == id).FirstOrDefaultAsync();
            if (currentRepositoryResult.IsDataCleaned != IsDataCleaned)
            {
                currentRepositoryResult.IsDataCleaned = IsDataCleaned;
                _context.Update(currentRepositoryResult);
                await _context.SaveChangesAsync();
            }
            ViewBag.IsDataCleaned = currentRepositoryResult.IsDataCleaned;
            ViewBag.IsSelctionCriteriaApplied = currentRepositoryResult.IsSelctionCriteriaApplied;
            ViewBag.IsMeritListGenerated = currentRepositoryResult.IsMeritListGenerated;
            ViewBag.IsSelctionCriteriaDefined = _context.SelectionCriteria.Count(a=>a.ResultRepositoryId == id);
            ViewBag.FYearId = currentRepositoryResult.ScholarshipFiscalYearId;
            ViewBag.SLId = currentRepositoryResult.SchemeLevelPolicy.SchemeLevelId;
            ViewBag.SLPId = currentRepositoryResult.SchemeLevelPolicyId;
            ViewBag.SLName = currentRepositoryResult.SchemeLevelPolicy.SchemeLevel.Name;
            if(currentRepositoryResult.IsSelctionCriteriaApplied == true)
            {

            }
            var list1 = new List<SelectListItem>
            {
               new SelectListItem{ Text="All", Value = "All", Selected = true },
               new SelectListItem{ Text="Selected", Value = "Selected" },
               new SelectListItem{ Text="Awaited", Value = "Awaited" },
            };
            ViewData["ddStatusList"] = list1;
            var list2 = new List<SelectListItem>
            {
               new SelectListItem{ Text="All", Value = "All", Selected = true },
               new SelectListItem{ Text="POMS", Value = "POMS" },
               new SelectListItem{ Text="DOMS", Value = "DOMS" },
               new SelectListItem{ Text="SQSOMS", Value = "SQSOMS" },
               new SelectListItem{ Text="SQSEVI", Value = "SQSEVI" },
            };
            ViewData["ddMethodList"] = list2;           
            var districtList = _context.District.Where(a => a.IsActive == true).ToList();
            districtList.Insert(0, new District { DistrictId = 0, Name = "All" }); 
            ViewData["DistrictId"] = new SelectList(districtList, "DistrictId", "Name");
            var currentPolicy = _context.SchemeLevelPolicy.Find(currentRepositoryResult.SchemeLevelPolicyId);
            viewUploadedResult.schemeLevelPolicy = currentPolicy;
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
        public async Task<IActionResult> Create(ResultContainer resultContainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultContainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
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

            var resultContainer = await _context.ResultContainer.Where(a=>a.ResultContainerId == id).FirstOrDefaultAsync();
            ViewBag.PID = resultContainer.ResultRepositoryId;
            if (resultContainer == null)
            {
                return NotFound();
            }            
            ViewData["ResultRepositoryId"] = new SelectList(_context.ResultRepository, "ResultRepositoryId", "ResultRepositoryId", resultContainer.ResultRepositoryId);
            return View(resultContainer);
        }

        // POST: ResultContainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResultContainer resultContainer)
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
                return RedirectToAction(nameof(Index), new { resultContainer.ResultRepositoryId});
            }            
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
