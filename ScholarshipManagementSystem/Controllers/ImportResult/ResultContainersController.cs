using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.Student;
using DAL.Models.ViewModels;
using DAL.Models.Domain.ScholarshipSetup;
using DAL.Models.Domain.ImportResult;

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
        public IActionResult ReloadEventMeritList(int id, int SLId, int SLPId, int selectedMethod, string selectedStatus, int RRId, int GradingSystem)
        {
            return ViewComponent("MeritList", new { id, SLId, SLPId, selectedMethod , selectedStatus, RRId, GradingSystem });
        }
        public IActionResult ReloadEventMeritListFormCollector(int id, int SLId, int SLPId, int selectedMethod, string selectedStatus, int RRId, int GradingSystem)
        {
            return ViewComponent("MeritListFormCollector", new { id, SLId, SLPId, selectedMethod, selectedStatus, RRId, GradingSystem });
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
            ViewData["DistrictId"] = new SelectList(_context.District.Include(a=>a.Division).Where(a=>a.IsActive == true && a.Division.ProvienceId == 1).OrderBy(a=>a.Name), "DistrictId", "Name");
            var Info = await _context.ResultRepository.Include(a => a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ResultRepositoryId == id).FirstOrDefaultAsync();
            ViewData["GradingSystem"] = Info.SchemeLevelPolicy.SchemeLevel.GradingSystem;
            ViewData["SLId"] = Info.SchemeLevelPolicy.SchemeLevelId;
            if(Info.SchemeLevelPolicy.SchemeLevelId == 1 || Info.SchemeLevelPolicy.SchemeLevelId == 2 || Info.SchemeLevelPolicy.SchemeLevelId == 3 || Info.SchemeLevelPolicy.SchemeLevelId == 7 || Info.SchemeLevelPolicy.SchemeLevelId == 8 || Info.SchemeLevelPolicy.SchemeLevelId == 9)
            {
                ViewData["SLName"] = Info.SchemeLevelPolicy.SchemeLevelId;
            }
            else if(Info.SchemeLevelPolicy.SchemeLevelId == 4 || Info.SchemeLevelPolicy.SchemeLevelId == 5 || Info.SchemeLevelPolicy.SchemeLevelId == 6)
            {
                ViewData["SLName"] = _context.DAEInstitute.Find(Info.DAEInstituteId).Name;
            }
            else
            {
                ViewData["SLName"] = _context.DegreeScholarshipLevel.Find(Info.DegreeScholarshipLevelId).Name;
            }
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
                applicant.ReceivedMarks = result.Marks_;
                applicant.ReceivedCGPA = result.CGPA;
                applicant.RollNumber = result.Roll_NO;
                applicant.SelectionMethodId = 1;// "POMS";
                applicant.RegisterationNumber = result.REG_NO;
                applicant.EntryThrough = "System";
                applicant.TotalMarks = result.TotalMarks_;//KDA
                applicant.TotalGPA = result.TotalGPA;
                applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                applicant.SelectionStatus = "Selected";
                applicant.ApplicantSelectionStatusId = 1;
                applicant.ApplicantCurrentStatusId = 2;
                _context.Add(applicant);
                ResultContainer currentResult = new ResultContainer();
                currentResult = result;
                currentResult.IsSelected = true;
                _context.Update(currentResult);
                counter++;
            }
            await _context.SaveChangesAsync();
            //--------------------------------------------------------------
            var districts = _context.District.Include(a => a.Division).Where(a => a.IsActive == true && a.Division.ProvienceId == 1).ToList();
            var SRCForumId = _context.PolicySRCForum.Where(a => a.ScholarshipFiscalYearId == FYearId && a.IsEndorse == true).Max(a => a.PolicySRCForumId);
            var districtQouta = _context.DistrictQoutaBySchemeLevel.Include(a=>a.SchemeLevelPolicy).Where(a => a.PolicySRCForumId == SRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SLId).ToList();            
            float DOMS = 0;
            foreach (var district in districts)
            {
                DOMS = districtQouta.Where(a => a.DistrictId == district.DistrictId && a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId).Sum(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                var DOMSCandidates = _context.ResultContainer.Where(a=> a.Candidate_District == district.Name && a.DistrictId == district.DistrictId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round(DOMS)).ToList();
                foreach (var result in DOMSCandidates)
                {
                    Applicant applicant = new Applicant();
                    applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0');
                    applicant.Name = result.Name;
                    applicant.DistrictId = result.DistrictId;
                    applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                    applicant.FatherName = result.Father_Name;
                    applicant.ReceivedMarks = result.Marks_;
                    applicant.ReceivedCGPA = result.CGPA;
                    applicant.RollNumber = result.Roll_NO;
                    applicant.SelectionMethodId = 2;// "DOSM";
                    applicant.RegisterationNumber = result.REG_NO;
                    applicant.EntryThrough = "System";
                    applicant.TotalMarks = result.TotalMarks_;//KDA
                    applicant.TotalGPA = result.TotalGPA;
                    applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                    applicant.SelectionStatus = "Selected";
                    applicant.ApplicantSelectionStatusId = 1;                    
                    applicant.ApplicantCurrentStatusId = 2;                    
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
                applicant.ReceivedMarks = result.Marks_;
                applicant.ReceivedCGPA = result.CGPA;
                applicant.RollNumber = result.Roll_NO;
                applicant.SelectionMethodId = 1;// "POMS";
                applicant.RegisterationNumber = result.REG_NO;
                applicant.EntryThrough = "System";
                applicant.TotalMarks = result.TotalMarks_;//KDA
                applicant.TotalGPA = result.TotalGPA;
                applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                applicant.SelectionStatus = "Awaited";
                applicant.ApplicantInboxId = 5;
                applicant.ApplicantSelectionStatusId = 2;
                applicant.ApplicantCurrentStatusId = 2;
                _context.Add(applicant);
                ResultContainer currentResult = new ResultContainer();
                currentResult = result;
                currentResult.IsSelected = true;
                _context.Update(currentResult);
                counter++;
            }
            await _context.SaveChangesAsync();
            //-------------------------DOMS 50%-------------------------------------
            var districts2 = _context.District.Include(a=>a.Division).Where(a=>a.IsActive == true && a.Division.ProvienceId == 1).ToList();
            foreach (var district2 in districts2)
            {                
                DOMS = districtQouta.Where(a => a.DistrictId == district2.DistrictId).Max(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                var DOMSCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == id && a.DistrictId == district2.DistrictId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => x.Marks_).Take((int)Math.Round((DOMS/2))).ToList();
                foreach (var result in DOMSCandidates)
                {
                    Applicant applicant = new Applicant();
                    applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                    applicant.Name = result.Name;
                    applicant.DistrictId = result.DistrictId;
                    applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                    applicant.FatherName = result.Father_Name;
                    applicant.ReceivedMarks = result.Marks_;
                    applicant.ReceivedCGPA = result.CGPA;
                    applicant.RollNumber = result.Roll_NO;
                    applicant.SelectionMethodId = 2;// "DOMS";
                    applicant.RegisterationNumber = result.REG_NO;
                    applicant.EntryThrough = "System";
                    applicant.TotalMarks = result.TotalMarks_;//KDA
                    applicant.TotalGPA = result.TotalGPA;
                    applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                    applicant.SelectionStatus = "Awaited";
                    applicant.ApplicantInboxId = 5;
                    applicant.ApplicantSelectionStatusId = 2;
                    applicant.ApplicantCurrentStatusId = 2;
                    _context.Add(applicant);
                    ResultContainer currentResult = new ResultContainer();
                    currentResult = result;
                    currentResult.IsSelected = true;
                    _context.Update(currentResult);
                    counter++;
                }
            }
            await _context.SaveChangesAsync();
            //-------------------------------------------------------------
            ResultRepository resultRepository = await _context.ResultRepository.FindAsync(id);
            resultRepository.IsMeritListGenerated = true;
            resultRepository.currentCounter = counter;
            _context.Update(resultRepository);
            await _context.SaveChangesAsync();
            //---------------Freez Policy-----------------------------------
            PolicySRCForum policySRCForum = await _context.PolicySRCForum.FindAsync(currentPolicy.PolicySRCForumId);
            policySRCForum.IsFreez = true;            
            _context.Update(policySRCForum);
            await _context.SaveChangesAsync();
            //----------------END Freez-------------------------------------
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> MeritListGenerator(int SLId, int policySRCForumId, int degreeScholarshipLevelId, int DAEInstituteId)
        {
            var currentPolicy = _context.SchemeLevelPolicy.Include(a => a.SchemeLevel.QualificationLevel).Include(a => a.PolicySRCForum.ScholarshipFiscalYear).Where(a => a.PolicySRCForumId == policySRCForumId && a.PolicySRCForum.IsEndorse == true && a.SchemeLevelId == SLId).FirstOrDefault();
            int rrId = _context.ResultRepository.Where(a => a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId && a.DegreeScholarshipLevelId == degreeScholarshipLevelId).Max(a => a.ResultRepositoryId);
            var ISExist = _context.Applicant.Count(a => a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId);
            if (degreeScholarshipLevelId != 0)
            {
                ISExist = _context.Applicant.Count(a => a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId && a.DegreeScholarshipLevelId == degreeScholarshipLevelId);
            }
            else if (DAEInstituteId != 0)
            {
                ISExist = _context.Applicant.Count(a => a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId && a.DAEInstituteId == DAEInstituteId);
            }            
            if(ISExist == 0)
            {
                var currentSchemeLevel = _context.SchemeLevel.Find(SLId);
                int gradingSystem = currentSchemeLevel.GradingSystem;
                if (SLId == 1 || SLId == 2 || SLId == 3)
                {
                    var POMLCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round(currentPolicy.POMS)).ToList();
                    int counter = 1;
                    foreach (var result in POMLCandidates)
                    {
                        Applicant applicant = new Applicant();
                        applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                        applicant.Name = result.Name;
                        applicant.DistrictId = result.DistrictId;
                        applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                        applicant.FatherName = result.Father_Name;
                        applicant.ReceivedMarks = result.Marks_;
                        applicant.ReceivedCGPA = result.CGPA;
                        applicant.RollNumber = result.Roll_NO;
                        applicant.SelectionMethodId = 1;// "POMS";
                        applicant.RegisterationNumber = result.REG_NO;
                        applicant.EntryThrough = "System";
                        applicant.TotalMarks = result.TotalMarks_;//KDA
                        applicant.TotalGPA = result.TotalGPA;
                        applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                        applicant.SelectionStatus = "Selected";
                        applicant.ApplicantSelectionStatusId = 1;
                        applicant.ApplicantCurrentStatusId = 2;
                        _context.Add(applicant);
                        ResultContainer currentResult = new ResultContainer();
                        currentResult = result;
                        currentResult.IsSelected = true;
                        _context.Update(currentResult);
                        counter++;
                    }
                    await _context.SaveChangesAsync();
                    //--------------------------------------------------------------
                    var districts = _context.District.Include(a => a.Division).Where(a => a.IsActive == true && a.Division.ProvienceId == 1).ToList();
                    var SRCForumId = _context.PolicySRCForum.Where(a => a.PolicySRCForumId == policySRCForumId && a.IsEndorse == true).Max(a => a.PolicySRCForumId);
                    var districtQouta = _context.DistrictQoutaBySchemeLevel.Include(a => a.SchemeLevelPolicy).Where(a => a.PolicySRCForumId == SRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SLId).ToList();
                    float DOMS = 0;
                    foreach (var district in districts)
                    {
                        DOMS = districtQouta.Where(a => a.DistrictId == district.DistrictId && a.SchemeLevelPolicyId == currentPolicy.SchemeLevelPolicyId).Sum(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                        var DOMSCandidates = _context.ResultContainer.Where(a => a.Candidate_District == district.Name && a.DistrictId == district.DistrictId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round(DOMS)).ToList();
                        foreach (var result in DOMSCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0');
                            applicant.Name = result.Name;
                            applicant.DistrictId = result.DistrictId;
                            applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.SelectionMethodId = 2;// "DOSM";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.SelectionStatus = "Selected";
                            applicant.ApplicantSelectionStatusId = 1;
                            applicant.ApplicantCurrentStatusId = 2;
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
                    POMLCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round((currentPolicy.POMS/2))).ToList();
                    foreach (var result in POMLCandidates)
                    {
                        Applicant applicant = new Applicant();
                        applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                        applicant.Name = result.Name;
                        applicant.DistrictId = result.DistrictId;
                        applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                        applicant.FatherName = result.Father_Name;
                        applicant.ReceivedMarks = result.Marks_;
                        applicant.ReceivedCGPA = result.CGPA;
                        applicant.RollNumber = result.Roll_NO;
                        applicant.SelectionMethodId = 1;// "POMS";
                        applicant.RegisterationNumber = result.REG_NO;
                        applicant.EntryThrough = "System";
                        applicant.TotalMarks = result.TotalMarks_;//KDA
                        applicant.TotalGPA = result.TotalGPA;
                        applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                        applicant.SelectionStatus = "Awaited";
                        applicant.ApplicantInboxId = 5;
                        applicant.ApplicantSelectionStatusId = 2;
                        applicant.ApplicantCurrentStatusId = 2;
                        _context.Add(applicant);
                        ResultContainer currentResult = new ResultContainer();
                        currentResult = result;
                        currentResult.IsSelected = true;
                        _context.Update(currentResult);
                        counter++;
                    }
                    await _context.SaveChangesAsync();
                    //-------------------------DOMS 50%-------------------------------------
                    var districts2 = _context.District.Include(a => a.Division).Where(a => a.IsActive == true && a.Division.ProvienceId == 1).ToList();
                    foreach (var district2 in districts2)
                    {
                        DOMS = districtQouta.Where(a => a.DistrictId == district2.DistrictId).Max(a => a.DistrictPopulationSlot + a.DistrictMPISlot + a.DistrictAdditionalSlot);
                        var DOMSCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.DistrictId == district2.DistrictId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round((DOMS/2))).ToList();
                        foreach (var result in DOMSCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                            applicant.Name = result.Name;
                            applicant.DistrictId = result.DistrictId;
                            applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.SelectionMethodId = 2;// "DOMS";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.SelectionStatus = "Awaited";
                            applicant.ApplicantInboxId = 5;
                            applicant.ApplicantSelectionStatusId = 2;
                            applicant.ApplicantCurrentStatusId = 2;
                            _context.Add(applicant);
                            ResultContainer currentResult = new ResultContainer();
                            currentResult = result;
                            currentResult.IsSelected = true;
                            _context.Update(currentResult);
                            counter++;
                        }
                    }
                    await _context.SaveChangesAsync();
                    //-------------------------------------------------------------
                    ResultRepository resultRepository = await _context.ResultRepository.FindAsync(rrId);
                    resultRepository.IsMeritListGenerated = true;
                    resultRepository.currentCounter = counter;
                    _context.Update(resultRepository);
                    await _context.SaveChangesAsync();
                }
                else if (SLId == 4 || SLId == 5 || SLId == 6)
                {
                    try
                    {
                        var POMLCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round(currentPolicy.POMS)).ToList();
                        int counter = 1;
                        foreach (var result in POMLCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                            applicant.Name = result.Name;
                            applicant.DistrictId = result.DistrictId;
                            applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.DAEInstituteId = DAEInstituteId;
                            applicant.SelectionMethodId = 1;// "POMS";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.SelectionStatus = "Selected";
                            applicant.ApplicantSelectionStatusId = 1;
                            applicant.ApplicantCurrentStatusId = 2;
                            _context.Add(applicant);
                            ResultContainer currentResult = new ResultContainer();
                            currentResult = result;
                            currentResult.IsSelected = true;
                            _context.Update(currentResult);
                            counter++;
                        }
                        await _context.SaveChangesAsync();
                        //--------------------------------------------------------------                                    
                        var daeQouta = _context.DAEInstituteQoutaBySchemeLevel.Include(a => a.SchemeLevelPolicy).Where(a => a.PolicySRCForumId == policySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SLId && a.DAEInstituteId == DAEInstituteId).FirstOrDefault();
                        var IOMSCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round((daeQouta.SlotAllocate + daeQouta.InstituteAdditionalSlot))).ToList();
                        foreach (var result in IOMSCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                            applicant.Name = result.Name;
                            applicant.DistrictId = 1;
                            applicant.ProvienceId = 1;
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.SelectionMethodId = 2;// "IOMS";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.DAEInstituteId = DAEInstituteId;
                            applicant.SelectionStatus = "Selected";
                            applicant.ApplicantSelectionStatusId = 1;
                            applicant.ApplicantCurrentStatusId = 2;
                            _context.Add(applicant);
                            ResultContainer currentResult = new ResultContainer();
                            currentResult = result;
                            currentResult.IsSelected = true;
                            _context.Update(currentResult);
                            counter++;
                        }
                        await _context.SaveChangesAsync();
                        //-------------------------------------------------------------
                        ResultRepository resultRepository = await _context.ResultRepository.FindAsync(rrId);
                        resultRepository.IsMeritListGenerated = true;
                        resultRepository.currentCounter = counter;
                        _context.Update(resultRepository);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        var temp = 0;
                    }
                }
                else if ((currentSchemeLevel.QualificationLevelId == 5 || currentSchemeLevel.QualificationLevelId == 6 || currentSchemeLevel.QualificationLevelId == 7) && currentSchemeLevel.InstituteId != 2) // Bachlor-BS (Other then first year)
                {
                    try
                    {
                        var POMLCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round(currentPolicy.POMS)).ToList();
                        int counter = 1;
                        foreach (var result in POMLCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                            applicant.Name = result.Name;
                            applicant.DistrictId = result.DistrictId;
                            applicant.ProvienceId = _context.District.Include(a => a.Division.Provience).Where(a => a.DistrictId == applicant.DistrictId).Select(a => a.Division.ProvienceId).FirstOrDefault();
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.SelectionMethodId = 1;// "POMS";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.SelectionStatus = "Selected";
                            applicant.ApplicantSelectionStatusId = 1;
                            applicant.ApplicantCurrentStatusId = 2;
                            _context.Add(applicant);
                            ResultContainer currentResult = new ResultContainer();
                            currentResult = result;
                            currentResult.IsSelected = true;
                            _context.Update(currentResult);
                            counter++;
                        }
                        await _context.SaveChangesAsync();
                        //--------------------------------------------------------------                                    
                        var degreeQouta = _context.DegreeLevelQoutaBySchemeLevel.Include(a => a.SchemeLevelPolicy).Where(a => a.PolicySRCForumId == policySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SLId && a.DegreeScholarshipLevelId == degreeScholarshipLevelId).FirstOrDefault();
                        var IOMSCandidates = _context.ResultContainer.Where(a => a.ResultRepositoryId == rrId && a.IsOnCriteria == true && a.IsSelected == false).OrderByDescending(x => (gradingSystem == 1 ? x.CGPA : x.Marks_)).Take((int)Math.Round((degreeQouta.SlotAllocate + degreeQouta.AdditionalSlotAllocate))).ToList();
                        foreach (var result in IOMSCandidates)
                        {
                            Applicant applicant = new Applicant();
                            applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + counter.ToString().PadLeft(4, '0'); ;
                            applicant.Name = result.Name;
                            applicant.DistrictId = 1;
                            applicant.ProvienceId = 1;
                            applicant.FatherName = result.Father_Name;
                            applicant.ReceivedMarks = result.Marks_;
                            applicant.ReceivedCGPA = result.CGPA;
                            applicant.RollNumber = result.Roll_NO;
                            applicant.SelectionMethodId = 2;// "IOMS";
                            applicant.RegisterationNumber = result.REG_NO;
                            applicant.EntryThrough = "System";
                            applicant.TotalMarks = result.TotalMarks_;//KDA
                            applicant.TotalGPA = result.TotalGPA;
                            applicant.SchemeLevelPolicyId = currentPolicy.SchemeLevelPolicyId;
                            applicant.DegreeScholarshipLevelId = degreeScholarshipLevelId;
                            applicant.SelectionStatus = "Selected";
                            applicant.ApplicantSelectionStatusId = 1;
                            applicant.ApplicantCurrentStatusId = 2;
                            _context.Add(applicant);
                            ResultContainer currentResult = new ResultContainer();
                            currentResult = result;
                            currentResult.IsSelected = true;
                            _context.Update(currentResult);
                            counter++;
                        }
                        await _context.SaveChangesAsync();
                        //-------------------------------------------------------------
                        ResultRepository resultRepository = await _context.ResultRepository.FindAsync(rrId);
                        resultRepository.IsMeritListGenerated = true;
                        resultRepository.currentCounter = counter;
                        _context.Update(resultRepository);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        var temp = 0;
                    }
                }

                //---------------Freez Policy-----------------------------------
                PolicySRCForum policySRCForum = await _context.PolicySRCForum.FindAsync(currentPolicy.PolicySRCForumId);
                policySRCForum.IsFreez = true;
                _context.Update(policySRCForum);
                await _context.SaveChangesAsync();
            }            
            //----------------END Freez-------------------------------------
            //return RedirectToAction(nameof(Details), new { id = rrId });
            return RedirectToAction("Index", "ResultRepositories");
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
            //-----------------------------------------
            var currentRepositoryResult = await _context.ResultRepository.Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a=>a.ResultRepositoryId == id).FirstOrDefaultAsync();
           
            ViewBag.IsDataCleaned = currentRepositoryResult.IsDataCleaned;
            ViewBag.IsSelctionCriteriaApplied = currentRepositoryResult.IsSelctionCriteriaApplied;
            ViewBag.IsMeritListGenerated = currentRepositoryResult.IsMeritListGenerated;
            ViewBag.IsSelctionCriteriaDefined = _context.SelectionCriteria.Count(a=>a.ResultRepositoryId == id);
            ViewBag.FYearId = currentRepositoryResult.ScholarshipFiscalYearId;
            ViewBag.SLId = currentRepositoryResult.SchemeLevelPolicy.SchemeLevelId;
            ViewBag.GradingSystem = currentRepositoryResult.SchemeLevelPolicy.SchemeLevel.GradingSystem;
            ViewBag.SLPId = currentRepositoryResult.SchemeLevelPolicyId;
            ViewBag.SLName = currentRepositoryResult.SchemeLevelPolicy.SchemeLevel.Name;            
            var list1 = new List<SelectListItem>
            {
               new SelectListItem{ Text="All", Value = "All", Selected = true },
               new SelectListItem{ Text="Selected", Value = "Selected" },
               new SelectListItem{ Text="Awaited", Value = "Awaited" },
            };
            ViewData["ddStatusList"] = list1;
            var selectionMethodList = _context.SelectionMethod.ToList();
            selectionMethodList.Insert(0, new SelectionMethod { SelectionMethodId = 0, Name = "All" });
            ViewData["ddMethodList"] = new SelectList(selectionMethodList, "SelectionMethodId", "Name");                  
            var districtList = _context.District.Where(a => a.IsActive == true).ToList();
            districtList.Insert(0, new District { DistrictId = 0, Name = "All" }); 
            ViewData["DistrictId"] = new SelectList(districtList, "DistrictId", "Name");
            var currentPolicy = _context.SchemeLevelPolicy.Find(currentRepositoryResult.SchemeLevelPolicyId);
            viewUploadedResult.schemeLevelPolicy = currentPolicy;
            return View(viewUploadedResult);
        }

        public async Task<IActionResult> ResultDetailFormCollector(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            //-----------------------------------------                       
            //-----------------------------------------
            var currentRepositoryResult = await _context.ResultRepository.Include(a => a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ResultRepositoryId == id).FirstOrDefaultAsync();

            ViewBag.IsDataCleaned = currentRepositoryResult.IsDataCleaned;
            ViewBag.IsSelctionCriteriaApplied = currentRepositoryResult.IsSelctionCriteriaApplied;
            ViewBag.IsMeritListGenerated = currentRepositoryResult.IsMeritListGenerated;
            ViewBag.IsSelctionCriteriaDefined = _context.SelectionCriteria.Count(a => a.ResultRepositoryId == id);
            ViewBag.FYearId = currentRepositoryResult.ScholarshipFiscalYearId;
            ViewBag.SLId = currentRepositoryResult.SchemeLevelPolicy.SchemeLevelId;
            ViewBag.GradingSystem = currentRepositoryResult.SchemeLevelPolicy.SchemeLevel.GradingSystem;
            ViewBag.SLPId = currentRepositoryResult.SchemeLevelPolicyId;
            ViewBag.SLName = currentRepositoryResult.SchemeLevelPolicy.SchemeLevel.Name;
            var list1 = new List<SelectListItem>
            {
               new SelectListItem{ Text="All", Value = "All", Selected = true },
               new SelectListItem{ Text="Selected", Value = "Selected" },
               new SelectListItem{ Text="Awaited", Value = "Awaited" },
            };
            ViewData["ddStatusList"] = list1;
            var selectionMethodList = _context.SelectionMethod.ToList();
            selectionMethodList.Insert(0, new SelectionMethod { SelectionMethodId = 0, Name = "All" });
            ViewData["ddMethodList"] = new SelectList(selectionMethodList, "SelectionMethodId", "Name");
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
