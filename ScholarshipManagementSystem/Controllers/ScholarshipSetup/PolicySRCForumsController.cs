using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    public class PolicySRCForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicySRCForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PolicySRCForums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PolicySRCForum.Include(p => p.ScholarshipFiscalYear);
            ViewBag.IsEndourse = _context.PolicySRCForum.Count(a => a.IsEndorse == false);
            return View(await applicationDbContext.ToListAsync());
        }        
        [HttpPost]
        public JsonResult AjaxPostCall()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [scholar].[DistrictQoutaBySchemeLevel]");
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [dbo].[DAEInstituteQoutaBySchemeLevel]");
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [scholar].[DegreeLevelQoutaBySchemeLevel]");
            _context.Database.ExecuteSqlRaw("delete [scholar].[SchemeLevelPolicy]");
            _context.Database.ExecuteSqlRaw("delete [scholar].[PolicySRCForum]");
            return Json("1");
        }
        public async Task<IActionResult> PolicyFiscalYear()
        {
            var applicationDbContext = _context.PolicySRCForum.Include(p => p.ScholarshipFiscalYear);

            /*var grouped = from b in _context.Products
                          group b.Id by b.Category.Name into g
                          select new
                          {
                              Key = g.Key,
                              Products = g.Count()
                          };*/
            //var result = grouped.ToList();
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: PolicySRCForums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum
                .Include(p => p.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.PolicySRCForumId == id);
            if (policySRCForum == null)
            {
                return NotFound();
            }

            return View(policySRCForum);
        }

        // GET: PolicySRCForums/Create
        public IActionResult Create(int id)
        {
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear.Where(a=>a.ScholarshipFiscalYearId == id), "ScholarshipFiscalYearId", "Name");
            PolicySRCForum obj = new PolicySRCForum();
            obj.CreatedOn = DateTime.Now;
            obj.IsEndorse = false;
            return View(obj);
        }

        // POST: PolicySRCForums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicySRCForumId,Name,Description,Code,IsEndorse,SRCMinutesAttachmentPath,PolicyDocumentAttachmentPath,OtherAttachment,CreatedOn,ScholarshipFiscalYearId")] PolicySRCForum policySRCForum)
        {
            if (ModelState.IsValid)
            {
                var IsSRCExistWithSameFiscalYear = _context.PolicySRCForum.Count(a => a.ScholarshipFiscalYearId == policySRCForum.ScholarshipFiscalYearId);
                if(IsSRCExistWithSameFiscalYear > 0)
                {
                    var IsPreviousSRCEndoursed = _context.PolicySRCForum.Count(a => a.ScholarshipFiscalYearId == policySRCForum.ScholarshipFiscalYearId && a.IsEndorse == false);
                    if(IsPreviousSRCEndoursed > 0)
                    {
                        ViewBag.message = "First endourse previous policy before adding new SRC of same fiscal year!";
                        ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear.Where(a => a.ScholarshipFiscalYearId == policySRCForum.ScholarshipFiscalYearId), "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
                        return View(policySRCForum);
                    }
                    else
                    {
                        //Doublicate
                        _context.Add(policySRCForum);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.PolicySRCForum.Max(a => a.PolicySRCForumId);
                        await GenerateDuplicateSchemeLevelPolicy(MaxId, policySRCForum.ScholarshipFiscalYearId);
                    }
                }
                else
                {
                    //New
                    _context.Add(policySRCForum);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.PolicySRCForum.Max(a => a.PolicySRCForumId);
                    await GenerateSchemeLevelPolicy(MaxId);                    
                }                
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }
        private async Task<int> DistrictSlotAllocation(float DOMSSlot, int SRCId, float DistrictThreshold, int SchemeLevelPolicyId, int Stipend, float districtSlotPopulationPer, float districtSlotMPIPer)
        {
            var totalPopulation = _context.DistrictDetail.Include(a=>a.District).Where(a=>a.District.IsActive == true).Sum(a=>a.Population);
            float OnePercentPopulation = (float)totalPopulation / 100f;
            float OnePercentSlot = (float)DOMSSlot / 100f;

            var districtDetails = _context.DistrictDetail.ToList();
            //MPI Difference and IsActive and pass two parameters from preferences
            var totalMPIDifference = districtDetails.Sum(a=>a.MPIDifferenceFromStatndard);
            foreach (var district in districtDetails)
            {
                DistrictQoutaBySchemeLevel Obj = new DistrictQoutaBySchemeLevel();
              //changes of next two lines
                float DistrictPoplationSlot = district.Population / OnePercentPopulation * ((float)OnePercentSlot/100f * districtSlotPopulationPer);
                float DistrictMPISlot = district.MPIDifferenceFromStatndard / totalMPIDifference * ((float)DOMSSlot/100f *districtSlotMPIPer);
                float DistrictDOMSSlots = DistrictPoplationSlot + DistrictMPISlot;
                float AdditionalSlot = 0;
                if(DistrictDOMSSlots < DistrictThreshold)
                {
                    AdditionalSlot = DistrictThreshold - DistrictDOMSSlots;
                }
                Obj.DistrictId = district.DistrictId;
                Obj.CurrentYearPopulation = district.Population;
                Obj.MPI = district.MPIScore;
                Obj.MPIDifferenceFromStatndard = district.MPIDifferenceFromStatndard;
                Obj.Threshold = DistrictThreshold;
                Obj.DistrictPopulationSlot = DistrictPoplationSlot;
                Obj.DistrictMPISlot = DistrictMPISlot;
                Obj.Threshold = DistrictThreshold;
                Obj.PolicySRCForumId = SRCId;
                Obj.SchemeLevelPolicyId = SchemeLevelPolicyId;
                Obj.StipendAmount = Stipend;
                Obj.DistrictAdditionalSlot = AdditionalSlot;
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }

        private async Task<int> ByDegreeLevelSlotAllocation(int SRCForumId, int SchemeLevelPolicyId, int schemeLevelId, int Stipend)
        {            
            var schemeLevelDepartments = _context.DegreeScholarshipLevel.Where(a=>a.SchemeLevelId == schemeLevelId && a.IsActive == true).ToList();
            foreach (var degreeLevel in schemeLevelDepartments)
            {
                DegreeLevelQoutaBySchemeLevel Obj = new DegreeLevelQoutaBySchemeLevel();
                
                Obj.ClassEnrollment = degreeLevel.Enrollment;
                Obj.DegreeScholarshipLevelId = degreeLevel.DegreeScholarshipLevelId;
                Obj.AdditionalSlotAllocate = 0;
                Obj.PolicySRCForumId = SRCForumId;
                Obj.SchemeLevelPolicyId = SchemeLevelPolicyId;
                Obj.SlotAllocate = degreeLevel.Slot;
                Obj.StipendAmount = Stipend;
                Obj.Threshold = 0;                
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<int> ByEqualThresholdSlotAllocation(int SRCForumId, int SchemeLevelPolicyId, int schemeLevelId, int Stipend, float Threshold)
        {
            var schemeLevelDepartments = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevelId && a.IsActive == true).ToList();
            foreach (var degreeLevel in schemeLevelDepartments)
            {
                DegreeLevelQoutaBySchemeLevel Obj = new DegreeLevelQoutaBySchemeLevel();

                Obj.ClassEnrollment = degreeLevel.Enrollment;
                Obj.DegreeScholarshipLevelId = degreeLevel.DegreeScholarshipLevelId;
                Obj.AdditionalSlotAllocate = 0;
                Obj.PolicySRCForumId = SRCForumId;
                Obj.SchemeLevelPolicyId = SchemeLevelPolicyId;
                Obj.SlotAllocate = Threshold;
                Obj.StipendAmount = Stipend;
                Obj.Threshold = Threshold;
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<int> ByTotalSlot_SlotAllocation(float DOMS, int SRCForumId, int SchemeLevelPolicyId, int schemeLevelId, int Stipend, float Threshold, int qualificationLevelId, int year)
        {
            var DepartmentCount = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel.Degree).Count(a => a.DegreeLevel.Degree.QualificationLevelId == qualificationLevelId && a.DegreeLevel.Year == year && a.IsActive == true);
            var schemeLevelDepartments = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevelId && a.IsActive == true).ToList();
            var preDegreeLevelSlot = DOMS / DepartmentCount;
            foreach (var degreeLevel in schemeLevelDepartments)
            {
                DegreeLevelQoutaBySchemeLevel Obj = new DegreeLevelQoutaBySchemeLevel();
                Obj.ClassEnrollment = degreeLevel.Enrollment;
                Obj.DegreeScholarshipLevelId = degreeLevel.DegreeScholarshipLevelId;
                Obj.AdditionalSlotAllocate = 0;
                Obj.PolicySRCForumId = SRCForumId;
                Obj.SchemeLevelPolicyId = SchemeLevelPolicyId;
                Obj.SlotAllocate = preDegreeLevelSlot;
                Obj.StipendAmount = Stipend;
                Obj.Threshold = 0;
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<int> DAEInstituteSlotAllocation(float DOMSSlot, int SRCId,int SchemeLvevePolicylId, float DAEThreshold, int Stipend, int TotalEnrollment, string year)
        {
            var DAEInstitutes = await _context.DAEInstitute.Where(a => a.IsActive == true).ToListAsync();
            var OnePercentEnrollment = (float)TotalEnrollment / 100f;
            var OnePercentSlot = (float)DOMSSlot / 100f;
            foreach (var institute in DAEInstitutes)
            {
                DAEInstituteQoutaBySchemeLevel Obj = new DAEInstituteQoutaBySchemeLevel();
                Obj.PolicySRCForumId = SRCId;
                Obj.Year = year;
                Obj.SchemeLevelPolicyId = SchemeLvevePolicylId;
                Obj.StipendAmount = Stipend;
                Obj.DAEInstituteId = institute.DAEInstituteId;
                Obj.Threshold = (float)DAEThreshold;
                var classEnrollment = (year == "1st" ? institute.Enrollment1stY : (year == "2nd" ? institute.Enrollment2ndY : (year == "3rd" ? institute.Enrollment3rdY : 0)));
                Obj.ClassEnrollment = classEnrollment;
                Obj.SlotAllocate = classEnrollment / OnePercentEnrollment * OnePercentSlot;
                if(Obj.SlotAllocate < DAEThreshold)
                {
                    Obj.InstituteAdditionalSlot = DAEThreshold - Obj.SlotAllocate;
                }
                else
                {
                    Obj.InstituteAdditionalSlot = 0;
                }
                await _context.AddAsync(Obj);
            }
            await _context.SaveChangesAsync();
                return 1;
        }
        private async Task<int> ByEnrollmentSlotAllocation(float DOMS, int SRCForumId, int SchemeLevelPolicyId, int schemeLevelId, int Stipend, float Threshold)
        {
            var schemeLevelDepartments = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevelId && a.IsActive == true).ToList();
            var totalEnrollment = schemeLevelDepartments.Sum(a=>a.Enrollment);
            var OnePercentEnrollment = totalEnrollment / 100f;
            var OnePercentDOMSQouta = DOMS / 100f;
            foreach (var degreeLevel in schemeLevelDepartments)
            {
                DegreeLevelQoutaBySchemeLevel Obj = new DegreeLevelQoutaBySchemeLevel();
                Obj.ClassEnrollment = degreeLevel.Enrollment;
                Obj.DegreeScholarshipLevelId = degreeLevel.DegreeScholarshipLevelId;
                Obj.AdditionalSlotAllocate = 0;
                Obj.PolicySRCForumId = SRCForumId;
                Obj.SchemeLevelPolicyId = SchemeLevelPolicyId;
                Obj.SlotAllocate = degreeLevel.Enrollment / OnePercentEnrollment * OnePercentDOMSQouta;
                Obj.StipendAmount = Stipend;
                Obj.Threshold = 0;
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<bool> GenerateSchemeLevelPolicy(int SRCForumId)
        {
            var preferences = _context.Preference.Find(4);      //Hard Code KDA      

            var SchemeLevels = _context.SchemeLevel.Where(a => a.IsActive == true).OrderBy(a=>a.SchemeId).Select(x => new { x.SchemeLevelId, x.Name }).ToList();            
            foreach(var schemeLevel in SchemeLevels)
            {
                SchemeLevelPolicy Obj = new SchemeLevelPolicy();                
                var currentScheme = _context.SchemeLevel.Find(schemeLevel.SchemeLevelId);                
                //Obj.Amount = currentScheme.SchemeId == 1 ? preferences.SchemeMatricStipend : (currentScheme.SchemeId == 2 ? preferences.SchemeIntermediateStipend : (currentScheme.SchemeId == 3 ? preferences.SchemeBacholarStipend : (currentScheme.SchemeId == 4 ? preferences.SchemeMasterStipend : (currentScheme.SchemeId == 5 ? preferences.SchemeMSStipend : 0))));
                float POMS = 0;
                float SQSOMS = 0;
                float SQSEVI = 0;
                float DOMS = 0;
                if (schemeLevel.SchemeLevelId == 1)// For 9th Pass 10th
                {
                    Obj.Amount = preferences.SchemeMatricStipend;
                    POMS = preferences.SlotMetric / 100f * preferences.POMSMatricQoutaPER;
                    SQSOMS = preferences.SlotMetric / 100f * preferences.SQSOMSMatricQoutaPER;
                    SQSEVI = preferences.SlotMetric / 100f * preferences.SQSEVIMatricQoutaPER;
                    DOMS = preferences.SlotMetric / 100f * preferences.DOMSMatricQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotMetric / 100 * preferences.DOMSMatricQoutaPER;                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a=>a.SchemeLevelPolicyId);
                    await DistrictSlotAllocation(DOMS, SRCForumId, preferences.IntermediateThreshold, MaxId,  preferences.SchemeIntermediateStipend, preferences.DistrictSlotPopulationPer, preferences.DistrictSlotMPIPer);                    
                }
                else if (schemeLevel.SchemeLevelId == 2)// For 10th Pass 1st Yr
                {
                    Obj.Amount = preferences.SchemeIntermediateStipend;
                    POMS = preferences.SlotFAFSc1Y / 100f * preferences.POMSIntermediateQoutaPER;
                    SQSOMS = preferences.SlotFAFSc1Y / 100f * preferences.SQSOMSIntermediateQouta;
                    SQSEVI = preferences.SlotFAFSc1Y / 100f * preferences.SQSIntermediateEVIQouta;
                    DOMS = preferences.SlotFAFSc1Y / 100f * preferences.DOMSIntermediateQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotFAFSc1Y / 100 * preferences.DOMSIntermediateQoutaPER;                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);                    
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);                    
                    await DistrictSlotAllocation(DOMS, SRCForumId, preferences.IntermediateThreshold, MaxId,  preferences.SchemeIntermediateStipend, preferences.DistrictSlotPopulationPer, preferences.DistrictSlotMPIPer);
                }
                else if (schemeLevel.SchemeLevelId == 3)// For 1st Yr Pass 2nd Yr
                {
                    Obj.Amount = preferences.SchemeIntermediateStipend;
                    POMS = preferences.SlotFAFSc2Y / 100f * preferences.POMSIntermediateQoutaPER;
                    SQSOMS = preferences.SlotFAFSc2Y / 100f * preferences.SQSOMSIntermediateQouta;
                    SQSEVI = preferences.SlotFAFSc2Y / 100f * preferences.SQSIntermediateEVIQouta;
                    DOMS = preferences.SlotFAFSc2Y / 100f * preferences.DOMSIntermediateQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotFAFSc2Y / 100 * preferences.DOMSIntermediateQoutaPER;                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DistrictSlotAllocation(DOMS, SRCForumId, preferences.IntermediateThreshold, MaxId, preferences.SchemeIntermediateStipend, preferences.DistrictSlotPopulationPer, preferences.DistrictSlotMPIPer);
                }
                else if (schemeLevel.SchemeLevelId == 4)// For 10th Pass DAE 1st Yr
                {
                    Obj.Amount = preferences.SchemeDAEStipend;
                    POMS = preferences.SlotDAE1Y / 100f * preferences.IOMSDAEQoutaPER;
                    SQSOMS = preferences.SlotDAE1Y / 100f * preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = preferences.SlotDAE1Y / 100f * preferences.SQSEVIDAEQoutaPER;
                    DOMS = preferences.SlotDAE1Y / 100f * preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotDAE1Y / 100 * preferences.DOMSDAEQoutaPER;
                    var TotalDAE1stY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment1stY);                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId, MaxId, preferences.DAEThreshold, preferences.SchemeDAEStipend, TotalDAE1stY, "1st");
                }
                else if (schemeLevel.SchemeLevelId == 5)// DAE 1st Yr Pass 2nd Yr
                {
                    Obj.Amount = preferences.SchemeDAEStipend;
                    POMS = preferences.SlotDAE2Y / 100f * preferences.IOMSDAEQoutaPER;
                    SQSOMS = preferences.SlotDAE2Y / 100f * preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = preferences.SlotDAE2Y / 100f * preferences.SQSEVIDAEQoutaPER;
                    DOMS = preferences.SlotDAE2Y / 100f * preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotDAE1Y / 100 * preferences.DOMSDAEQoutaPER;
                    var TotalDAE2ndY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment2ndY);                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId, MaxId, preferences.DAEThreshold, preferences.SchemeDAEStipend, TotalDAE2ndY, "2nd");
                }
                else if (schemeLevel.SchemeLevelId == 6)// DAE 2nd Yr Pass 3rd Yr
                {
                    Obj.Amount = preferences.SchemeDAEStipend;
                    POMS = preferences.SlotDAE3Y / 100f * preferences.IOMSDAEQoutaPER;
                    SQSOMS = preferences.SlotDAE3Y / 100f * preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = preferences.SlotDAE3Y / 100f * preferences.SQSEVIDAEQoutaPER;
                    DOMS = preferences.SlotDAE3Y / 100f * preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = preferences.SlotDAE1Y / 100 * preferences.DOMSDAEQoutaPER;
                    var TotalDAE3rdY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment3rdY);                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId, MaxId, preferences.DAEThreshold, preferences.SchemeDAEStipend, TotalDAE3rdY, "3rd");
                }
                else if (currentScheme.SchemeId == 3 && currentScheme.InstituteId == 2) // Simple Graduation BA/BSc And Result from Board
                {
                    Obj.Amount = preferences.SchemeGraduationStipend;
                    POMS = preferences.SlotBacholar1Y / 100f * preferences.IOMSGraduationQoutaPER;
                    SQSOMS = preferences.SlotBacholar1Y / 100f * preferences.SQSOMSGraduationQoutaPER;
                    SQSEVI = preferences.SlotBacholar1Y / 100f * preferences.SQSEVIGraduationQoutaPER;
                    DOMS = preferences.SlotBacholar1Y / 100f * preferences.DOMSGraduationQoutaPER;                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DistrictSlotAllocation(DOMS, SRCForumId, preferences.GraduationThreshold, MaxId,  preferences.SchemeGraduationStipend, preferences.DistrictSlotPopulationPer, preferences.DistrictSlotMPIPer);
                }
                else if (currentScheme.SchemeId == 4 && currentScheme.InstituteId == 2) // BS/BE 1st Professional And Result from Board
                {
                    Obj.Amount = preferences.SchemeBacholarStipend;
                    POMS = preferences.SlotBacholar1Y / 100f * preferences.IOMSBachelor1stYQoutaPER;
                    SQSOMS = preferences.SlotBacholar1Y / 100f * preferences.SQSOMSBachelor1stYQoutaPER;
                    SQSEVI = preferences.SlotBacholar1Y / 100f * preferences.SQSEVIBachelor1stYQoutaPER;
                    DOMS = preferences.SlotBacholar1Y / 100f * preferences.DOMSBachelor1stYQoutaPER;                    
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                    _context.Add(Obj);
                    await _context.SaveChangesAsync();
                    var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                    await DistrictSlotAllocation(DOMS, SRCForumId, preferences.BSProfDistrictThresholdFor1stY, MaxId, preferences.SchemeBacholarStipend, preferences.DistrictSlotPopulationPer, preferences.DistrictSlotMPIPer);
                }
                else if(currentScheme.QualificationLevelId == 5 && currentScheme.InstituteId != 2) // Bachlor-BS (Other then first year)
                {
                    Obj.Amount = preferences.SchemeBacholarStipend;                        
                    //In this method we get total slot from degree level slots which is allocatted earlier on the basis of exprience
                    if(preferences.SlotGraduationPROFCalculationMethod == "By Degree Level Slot")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true).Sum(a => a.Slot); ;
                        POMS = DOMS/preferences.DOMSBachelorClassQoutaPER * preferences.IOMSBachelorClassQoutaPER;                            
                        SQSOMS = DOMS / preferences.DOMSBachelorClassQoutaPER * preferences.SQSOMSBachelorClassQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSBachelorClassQoutaPER * preferences.SQSEVIBachelorClassQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByDegreeLevelSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend);
                    }  
                    else if (preferences.SlotGraduationPROFCalculationMethod == "By Equal Threshold")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Count(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true) * preferences.BSProfThresholdForClass;
                        POMS = DOMS / preferences.DOMSBachelorClassQoutaPER * preferences.IOMSBachelorClassQoutaPER;
                        SQSOMS = DOMS / preferences.DOMSBachelorClassQoutaPER * preferences.SQSOMSBachelorClassQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSBachelorClassQoutaPER * preferences.SQSEVIBachelorClassQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEqualThresholdSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend, preferences.BSProfThresholdForClass);
                    }
                    else if (preferences.SlotGraduationPROFCalculationMethod == "By Total Slot")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a=>a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a=>a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = degreeLevelYear == 2 ? preferences.GraduationPROF2ndYSlot : (degreeLevelYear == 3 ? preferences.GraduationPROF3rdYSlot : (degreeLevelYear == 4 ? preferences.GraduationPROF4thYSlot : (degreeLevelYear == 5 ? preferences.GraduationPROF5thYSlot : 0)));
                        DOMS = totalslot / 100f * preferences.DOMSBachelorClassQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSBachelorClassQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSBachelorClassQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIBachelorClassQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByTotalSlot_SlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend, preferences.BSProfThresholdForClass, currentScheme.QualificationLevelId, degreeLevelYear);
                    }
                    else if (preferences.SlotGraduationPROFCalculationMethod == "By Enrollment")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a => a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = degreeLevelYear == 2 ? preferences.GraduationPROF2ndYSlot : (degreeLevelYear == 3 ? preferences.GraduationPROF3rdYSlot : (degreeLevelYear == 4 ? preferences.GraduationPROF4thYSlot : (degreeLevelYear == 5 ? preferences.GraduationPROF5thYSlot : 0)));
                        DOMS = totalslot / 100f * preferences.DOMSBachelorClassQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSBachelorClassQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSBachelorClassQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIBachelorClassQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEnrollmentSlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend, preferences.BSProfThresholdForClass);
                    }
                }
                else if (currentScheme.QualificationLevelId == 6) // Master
                {
                    Obj.Amount = preferences.SchemeMasterStipend;
                    //In this method we get total slot from degree level slots which is allocatted earlier on the basis of exprience
                    if (preferences.SlotMasterCalculationMethod == "By Degree Level Slot")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true).Sum(a => a.Slot); ;
                        POMS = DOMS / preferences.DOMSMasterQoutaPER * preferences.IOMSMasterQoutaPER;
                        SQSOMS = DOMS / preferences.DOMSMasterQoutaPER * preferences.SQSOMSBachelorClassQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSMasterQoutaPER * preferences.SQSEVIBachelorClassQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByDegreeLevelSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMasterStipend);
                    }
                    else if (preferences.SlotMasterCalculationMethod == "By Equal Threshold")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Count(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true) * preferences.BSProfThresholdForClass;
                        POMS = DOMS / preferences.DOMSMasterQoutaPER * preferences.IOMSMasterQoutaPER;
                        SQSOMS = DOMS / preferences.DOMSMasterQoutaPER * preferences.SQSOMSMasterQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSMasterQoutaPER * preferences.SQSEVIMasterQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEqualThresholdSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMasterStipend, preferences.MasterThreshold);
                    }
                    else if (preferences.SlotMasterCalculationMethod == "By Total Slot")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a => a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = degreeLevelYear == 1 ? preferences.Master1stYSlot : (degreeLevelYear == 2 ? preferences.Master2ndYSlot : 0);
                        DOMS = totalslot / 100f * preferences.DOMSMasterQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSMasterQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSMasterQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIMasterQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByTotalSlot_SlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend, preferences.BSProfThresholdForClass, currentScheme.QualificationLevelId, degreeLevelYear);
                    }
                    else if (preferences.SlotMasterCalculationMethod == "By Enrollment")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a => a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = degreeLevelYear == 1 ? preferences.Master1stYSlot : (degreeLevelYear == 2 ? preferences.Master2ndYSlot : 0);
                        DOMS = totalslot / 100f * preferences.DOMSMasterQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSMasterQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSMasterQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIMasterQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEnrollmentSlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMasterStipend, preferences.MasterThreshold);
                    }
                }
                else if (currentScheme.QualificationLevelId == 7) // MS/M.Phil
                {
                    Obj.Amount = preferences.SchemeMSStipend;
                    //In this method we get total slot from degree level slots which is allocatted earlier on the basis of exprience
                    if (preferences.SlotMSCalculationMethod == "By Degree Level Slot")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true).Sum(a => a.Slot); ;
                        POMS = DOMS / preferences.DOMSMSQoutaPER * preferences.IOMSMSQoutaPER;
                        SQSOMS = DOMS / preferences.DOMSMSQoutaPER * preferences.SQSOMSMSQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSMSQoutaPER * preferences.SQSEVIMSQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByDegreeLevelSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMSStipend);
                    }
                    else if (preferences.SlotMSCalculationMethod == "By Equal Threshold")
                    {
                        DOMS = _context.DegreeScholarshipLevel.Count(a => a.SchemeLevelId == schemeLevel.SchemeLevelId && a.IsActive == true) * preferences.MSThreshold;
                        POMS = DOMS / preferences.DOMSMSQoutaPER * preferences.IOMSMSQoutaPER;
                        SQSOMS = DOMS / preferences.DOMSMSQoutaPER * preferences.SQSOMSMSQoutaPER;
                        SQSEVI = DOMS / preferences.DOMSMSQoutaPER * preferences.SQSEVIMSQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEqualThresholdSlotAllocation(SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMSStipend, preferences.MSThreshold);
                    }
                    else if (preferences.SlotMSCalculationMethod == "By Total Slot")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a => a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = preferences.MSSlot;
                        DOMS = totalslot / 100f * preferences.DOMSMSQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSMSQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSMSQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIMSQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByTotalSlot_SlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeBacholarStipend, preferences.BSProfThresholdForClass, currentScheme.QualificationLevelId, degreeLevelYear);
                    }
                    else if (preferences.SlotMSCalculationMethod == "By Enrollment")
                    {
                        int degreeLevelYear = _context.DegreeScholarshipLevel.Include(a => a.DegreeLevel).Where(a => a.SchemeLevelId == schemeLevel.SchemeLevelId).Select(a => a.DegreeLevel.Year).FirstOrDefault();
                        var totalslot = preferences.MSSlot;
                        DOMS = totalslot / 100f * preferences.DOMSMSQoutaPER;
                        POMS = totalslot / 100f * preferences.IOMSMSQoutaPER;
                        SQSOMS = totalslot / 100f * preferences.SQSOMSMSQoutaPER;
                        SQSEVI = totalslot / 100f * preferences.SQSEVIMSQoutaPER;
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();
                        var MaxId = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);
                        await ByEnrollmentSlotAllocation(DOMS, SRCForumId, MaxId, schemeLevel.SchemeLevelId, preferences.SchemeMSStipend, preferences.MSThreshold);
                    }
                }
            }            
            return true;

        }
        private async Task<bool> GenerateDuplicateSchemeLevelPolicy(int SRCForumId, int FYId)
        {
            var SRCForumObj = await _context.PolicySRCForum.FindAsync(SRCForumId);
            _context.Add(SRCForumObj);
            _context.SaveChanges();
            var MaxSRCForumId = _context.PolicySRCForum.Max(a=>a.PolicySRCForumId);

            var currentSchemeLevelPolicy = _context.SchemeLevelPolicy.Where(a => a.PolicySRCForumId == SRCForumId).ToList();
            foreach(var policy in currentSchemeLevelPolicy)
            {
                SRCForumObj.PolicySRCForumId = MaxSRCForumId;
                _context.Add(SRCForumObj);
                _context.SaveChanges();
                var MaxSchemeLevelPolicy = _context.SchemeLevelPolicy.Max(a => a.SchemeLevelPolicyId);

                var districtQoutaExist = _context.DistrictQoutaBySchemeLevel.Count(a=>a.SchemeLevelPolicyId == policy.SchemeLevelPolicyId);
                var dAEQoutaExist = _context.DAEInstituteQoutaBySchemeLevel.Count(a=>a.SchemeLevelPolicyId == policy.SchemeLevelPolicyId);
                var degreeQoutaExist = _context.DegreeLevelQoutaBySchemeLevel.Count(a=>a.SchemeLevelPolicyId == policy.SchemeLevelPolicyId);
                if(districtQoutaExist > 0)
                {
                    var districtQoutaBySchemeLevel = _context.DistrictQoutaBySchemeLevel.Where(a => a.PolicySRCForumId == SRCForumId && a.SchemeLevelPolicyId == policy.SchemeLevelPolicyId).ToList();
                    foreach (var district in districtQoutaBySchemeLevel)
                    {/*
                        SRCForumObj.PolicySRCForumId = MaxSRCForumId;
                        SRCForumObj.PolicySRCForumId = MaxSRCForumId;
                        _context.Add(SRCForumObj);
                        _context.SaveChanges();*/
                    }
                }
                else if(degreeQoutaExist > 0)
                {

                }
                else if(dAEQoutaExist > 0)
                {

                }
            }
            
            
            return true;
        }
        // GET: PolicySRCForums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum.FindAsync(id);
            if (policySRCForum == null)
            {
                return NotFound();
            }
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear.Where(a => a.ScholarshipFiscalYearId == policySRCForum.ScholarshipFiscalYearId), "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
            policySRCForum.IsEndorse = true;
            return View(policySRCForum);
        }        
        // POST: PolicySRCForums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PolicySRCForum policySRCForum, IFormFile minutesFile, IFormFile policydocFile, IFormFile otherFile)
        {
            if (id != policySRCForum.PolicySRCForumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (minutesFile != null && minutesFile.Length > 0)
                    {
                        var fiscalYear = _context.ScholarshipFiscalYear.Find(policySRCForum.ScholarshipFiscalYearId).Name;
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Policy\\" + fiscalYear + "\\SRCMinutes" + policySRCForum.PolicySRCForumId + "\\");
                        string fileName = Path.GetFileName(minutesFile.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "SRCMinutes" + randomNumber.ToString() + fileName;
                        policySRCForum.SRCMinutesAttachmentPath = Path.Combine("/Documents/Policy/",fiscalYear,"SRCMinutes" + policySRCForum.PolicySRCForumId.ToString(), fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await minutesFile.CopyToAsync(stream);
                        }
                        //-----------------------------------                                                                 
                    }
                    if (policydocFile != null && policydocFile.Length > 0)
                    {
                        var fiscalYear = _context.ScholarshipFiscalYear.Find(policySRCForum.ScholarshipFiscalYearId).Name;
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Policy\\" + fiscalYear + "\\Policy" + policySRCForum.PolicySRCForumId + "\\");
                        string fileName = Path.GetFileName(policydocFile.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Policy" + randomNumber.ToString() + fileName;
                        policySRCForum.PolicyDocumentAttachmentPath = Path.Combine("/Documents/Policy/", fiscalYear, "Policy" + policySRCForum.PolicySRCForumId.ToString(), fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await policydocFile.CopyToAsync(stream);
                        }
                        //-----------------------------------                                                                 
                    }
                    if (otherFile != null && otherFile.Length > 0)
                    {
                        var fiscalYear = _context.ScholarshipFiscalYear.Find(policySRCForum.ScholarshipFiscalYearId).Name;
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Policy\\" + fiscalYear + "\\PolicyOther" + policySRCForum.PolicySRCForumId + "\\");
                        string fileName = Path.GetFileName(otherFile.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "PolicyDoc" + randomNumber.ToString() + fileName;
                        policySRCForum.OtherAttachment = Path.Combine("/Documents/Policy/", fiscalYear, "PolicyOther" + policySRCForum.PolicySRCForumId.ToString(), fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await otherFile.CopyToAsync(stream);
                        }
                        //-----------------------------------                                                                 
                    }
                    _context.Update(policySRCForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicySRCForumExists(policySRCForum.PolicySRCForumId))
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
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear.Where(a => a.ScholarshipFiscalYearId == policySRCForum.ScholarshipFiscalYearId), "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }

        // GET: PolicySRCForums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policySRCForum = await _context.PolicySRCForum
                .Include(p => p.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.PolicySRCForumId == id);
            if (policySRCForum == null)
            {
                return NotFound();
            }

            return View(policySRCForum);
        }

        // POST: PolicySRCForums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policySRCForum = await _context.PolicySRCForum.FindAsync(id);
            _context.PolicySRCForum.Remove(policySRCForum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicySRCForumExists(int id)
        {
            return _context.PolicySRCForum.Any(e => e.PolicySRCForumId == id);
        }
    }
}
