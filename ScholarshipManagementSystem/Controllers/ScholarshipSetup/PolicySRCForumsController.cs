using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View(await applicationDbContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name");
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
                        ViewBag.message = "First Endourse Policy before adding new SRC!";
                        ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
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
        private async Task<bool> GenerateSchemeLevelPolicy(int SRCForumId)
        {
            var preferences = _context.Preference.Find(4);            

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
                if (schemeLevel.SchemeLevelId == 5)// For 9th Pass 10th
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
                else if (schemeLevel.SchemeLevelId == 3)// For 10th Pass 1st Yr
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
                else if (schemeLevel.SchemeLevelId == 4)// For 1st Yr Pass 2nd Yr
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
                else if (schemeLevel.SchemeLevelId == 8)// For 10th Pass DAE 1st Yr
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
                else if (schemeLevel.SchemeLevelId == 9)// DAE 1st Yr Pass 2nd Yr
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
                else if (schemeLevel.SchemeLevelId == 10)// DAE 2nd Yr Pass 3rd Yr
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
                else if (currentScheme.SchemeId == 3 && schemeLevel.Name.Contains("1st")) // Simple Graduation BA/BSc
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
                else if (currentScheme.SchemeId == 4 && schemeLevel.Name.Contains("1st")) // BS/BE 1st Professional
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
                else
                {
                    if(currentScheme.SchemeId == 4) // Bachlor-BS (Other then first year)
                    {
                        Obj.Amount = preferences.SchemeBacholarStipend;
                        POMS = preferences.BSProfThresholdForClass;
                        SQSOMS = preferences.BSProfThresholdForClass/preferences.IOMSBachelorClassQoutaPER*preferences.SQSEVIBachelorClassQoutaPER;
                        SQSEVI = preferences.BSProfThresholdForClass/preferences.IOMSBachelorClassQoutaPER*preferences.SQSOMSBachelorClassQoutaPER;
                        DOMS = (float)preferences.BSProfThresholdForClass/100f*(float)preferences.IOMSBachelorClassQoutaPER;                                            
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();                        
                    }
                    else if(currentScheme.SchemeId == 5) // Master
                    {
                        Obj.Amount = preferences.SchemeMasterStipend;
                        POMS = preferences.MasterThreshold;
                        SQSOMS = preferences.MasterThreshold / preferences.IOMSMasterQoutaPER * preferences.SQSEVIMasterQoutaPER;
                        SQSEVI = preferences.MasterThreshold / preferences.IOMSMasterQoutaPER * preferences.SQSOMSMasterQoutaPER;
                        DOMS = (float)preferences.MasterThreshold / 100f * (float)preferences.IOMSMasterQoutaPER;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();                        
                    }                   
                    else if (currentScheme.SchemeId == 6) // MS/M.Phil
                    {
                        Obj.Amount = preferences.SchemeMSStipend;
                        POMS = preferences.MSThreshold;
                        SQSOMS = preferences.MSThreshold / preferences.IOMSMSQoutaPER * preferences.SQSEVIMSQoutaPER;
                        SQSEVI = preferences.MSThreshold / preferences.IOMSMSQoutaPER * preferences.SQSOMSMSQoutaPER;
                        DOMS = (float)preferences.MSThreshold / 100f * (float)preferences.IOMSMSQoutaPER;
                        Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                        _context.Add(Obj);
                        await _context.SaveChangesAsync();                        
                    }
                }                
            }            
            return true;

        }
        private async Task<bool> GenerateDuplicateSchemeLevelPolicy(int SRCForumId, int FYId)
        {

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
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }        
        // POST: PolicySRCForums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicySRCForumId,Name,Description,Code,IsEndorse,SRCMinutesAttachmentPath,PolicyDocumentAttachmentPath,OtherAttachment,CreatedOn,ScholarshipFiscalYearId")] PolicySRCForum policySRCForum)
        {
            if (id != policySRCForum.PolicySRCForumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
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
