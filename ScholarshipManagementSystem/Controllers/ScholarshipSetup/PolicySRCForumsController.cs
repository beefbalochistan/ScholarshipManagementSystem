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
                    }
                }
                else
                {
                    //New
                    _context.Add(policySRCForum);
                    await _context.SaveChangesAsync();
                    await GenerateSchemeLevelPolicy(_context.PolicySRCForum.Max(a => a.PolicySRCForumId));
                }                
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name", policySRCForum.ScholarshipFiscalYearId);
            return View(policySRCForum);
        }
        private async Task<int> DistrictSlotAllocation(int DOMSSlot, int SRCId, int DistrictThreshold, int SchemeLevelId, int Stipend)
        {
            var totalPopulation = _context.DistrictDetail.Sum(a=>a.Population);
            float OnePercentPopulation = totalPopulation / 100;
            float OnePercentSlot = DOMSSlot / 100;

            var districtDetails = _context.DistrictDetail.ToList();            
            foreach (var district in districtDetails)
            {
                DistrictQoutaBySchemeLevel Obj = new DistrictQoutaBySchemeLevel();
                float DistrictPoplationSlot = district.Population / OnePercentPopulation * OnePercentSlot;
                float DistrictMPISlot = district.MPIScore;
                float DistrictDOMSSlots = DistrictPoplationSlot + DistrictMPISlot;
                float AdditionalSlot = 0;
                if(DistrictDOMSSlots < DistrictThreshold)
                {
                    AdditionalSlot = DistrictThreshold - DistrictDOMSSlots;
                }
                Obj.DistrictId = district.DistrictId;
                Obj.CurrentYearPopulation = district.Population;
                Obj.MPI = district.MPIScore;
                Obj.Threshold = DistrictThreshold;
                Obj.DistrictPopulationSlot = DistrictPoplationSlot;
                Obj.DistrictMPISlot = DistrictMPISlot;
                Obj.Threshold = DistrictThreshold;
                Obj.PolicySRCForumId = SRCId;
                Obj.SchemeLevelId = SchemeLevelId;
                Obj.StipendAmount = Stipend;
                Obj.DistrictAdditionalSlot = AdditionalSlot;
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<int> DAEInstituteSlotAllocation(int IOMSSlot, int SRCId,int SchemeLvevelId, int DAEThreshold, int Stipend, int TotalEnrollment)
        {
            var DAEInstitutes = _context.DAEInstitute.Where(a => a.IsActive == true).ToList();
            var OnePercentEnrollment = TotalEnrollment / 100;
            var OnePercentSlot = IOMSSlot / 100;
            foreach (var institute in DAEInstitutes)
            {
                DAEInstituteQoutaBySchemeLevel Obj = new DAEInstituteQoutaBySchemeLevel();
                Obj.PolicySRCForumId = SRCId;
                Obj.SchemeLevelId = SchemeLvevelId;
                Obj.StipendAmount = Stipend;
                Obj.DAEInstituteId = institute.DAEInstituteId;
                Obj.Threshold = DAEThreshold;
                Obj.ClassEnrollment = institute.Enrollment1stY;
                Obj.SlotAllocate = institute.Enrollment1stY / OnePercentEnrollment * OnePercentSlot;
                if(Obj.SlotAllocate < DAEThreshold)
                {
                    Obj.InstituteAdditionalSlot = DAEThreshold - Obj.SlotAllocate;
                }
                else
                {
                    Obj.InstituteAdditionalSlot = 0;
                }
            }
                return 1;
        }
        private async Task<bool> GenerateSchemeLevelPolicy(int SRCForumId)
        {
            var Preferences = _context.Preference.Find(1);            

            var SchemeLevels = _context.SchemeLevel.Where(a => a.IsActive == true).Select(a => a.SchemeLevelId).ToList();            
            foreach(var schemeLevelId in SchemeLevels)
            {
                SchemeLevelPolicy Obj = new SchemeLevelPolicy();
                var currentScheme = _context.SchemeLevel.Find(schemeLevelId);                
                Obj.Amount = currentScheme.SchemeId == 1 ? Preferences.SchemeMatricStipend : (currentScheme.SchemeId == 2 ? Preferences.SchemeIntermediateStipend : (currentScheme.SchemeId == 3 ? Preferences.SchemeBacholarStipend : (currentScheme.SchemeId == 4 ? Preferences.SchemeMasterStipend : (currentScheme.SchemeId == 5 ? Preferences.SchemeMSStipend : 0))));
                var POMS = 0;
                var SQSOMS = 0;
                var SQSEVI = 0;
                var DOMS = 0;
                if (schemeLevelId == 5)// For 9th Pass 10th
                {
                    POMS = Preferences.SlotMetric / 100 * Preferences.POMSMatricQoutaPER;
                    SQSOMS = Preferences.SlotMetric / 100 * Preferences.SQSOMSMatricQoutaPER;
                    SQSEVI = Preferences.SlotMetric / 100 * Preferences.SQSEVIMatricQoutaPER;
                    DOMS = Preferences.SlotMetric / 100 * Preferences.DOMSMatricQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotMetric / 100 * Preferences.DOMSMatricQoutaPER;
                    await DistrictSlotAllocation(DOMS, SRCForumId, Preferences.MatricThreshold, schemeLevelId, Preferences.SchemeMatricStipend);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (schemeLevelId == 3)// For 10th Pass 1st Yr
                {
                    POMS = Preferences.SlotFAFSc1Y / 100 * Preferences.POMSIntermediateQoutaPER;
                    SQSOMS = Preferences.SlotFAFSc1Y / 100 * Preferences.SQSOMSIntermediateQouta;
                    SQSEVI = Preferences.SlotFAFSc1Y / 100 * Preferences.SQSIntermediateEVIQouta;
                    DOMS = Preferences.SlotFAFSc1Y / 100 * Preferences.DOMSIntermediateQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotFAFSc1Y / 100 * Preferences.DOMSIntermediateQoutaPER;
                    await DistrictSlotAllocation(DOMS, SRCForumId, schemeLevelId, Preferences.IntermediateThreshold, Preferences.SchemeIntermediateStipend);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (schemeLevelId == 4)// For 1st Yr Pass 2nd Yr
                {
                    POMS = Preferences.SlotFAFSc2Y / 100 * Preferences.POMSIntermediateQoutaPER;
                    SQSOMS = Preferences.SlotFAFSc2Y / 100 * Preferences.SQSOMSIntermediateQouta;
                    SQSEVI = Preferences.SlotFAFSc2Y / 100 * Preferences.SQSIntermediateEVIQouta;
                    DOMS = Preferences.SlotFAFSc2Y / 100 * Preferences.DOMSIntermediateQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotFAFSc2Y / 100 * Preferences.DOMSIntermediateQoutaPER;
                    await DistrictSlotAllocation(DOMS, SRCForumId, schemeLevelId, Preferences.IntermediateThreshold, Preferences.SchemeIntermediateStipend);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (schemeLevelId == 8)// For 10th Pass DAE 1st Yr
                {                    
                    POMS = Preferences.SlotDAE1Y / 100 * Preferences.IOMSDAEQoutaPER;
                    SQSOMS = Preferences.SlotDAE1Y / 100 * Preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = Preferences.SlotDAE1Y / 100 * Preferences.SQSEVIDAEQoutaPER;
                    DOMS = Preferences.SlotDAE1Y / 100 * Preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotDAE1Y / 100 * Preferences.DOMSDAEQoutaPER;
                    var TotalDAE1stY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment1stY);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId,schemeLevelId, Preferences.DAEThreshold, Preferences.SchemeDAEStipend, TotalDAE1stY);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (schemeLevelId == 9)// DAE 1st Yr Pass 2nd Yr
                {
                    POMS = Preferences.SlotDAE2Y / 100 * Preferences.IOMSDAEQoutaPER;
                    SQSOMS = Preferences.SlotDAE2Y / 100 * Preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = Preferences.SlotDAE2Y / 100 * Preferences.SQSEVIDAEQoutaPER;
                    DOMS = Preferences.SlotDAE2Y / 100 * Preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotDAE1Y / 100 * Preferences.DOMSDAEQoutaPER;
                    var TotalDAE2ndY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment2ndY);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId, schemeLevelId, Preferences.DAEThreshold, Preferences.SchemeDAEStipend, TotalDAE2ndY);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (schemeLevelId == 10)// DAE 2nd Yr Pass 3rd Yr
                {
                    POMS = Preferences.SlotDAE3Y / 100 * Preferences.IOMSDAEQoutaPER;
                    SQSOMS = Preferences.SlotDAE3Y / 100 * Preferences.SQSOMSDAEQoutaPER;
                    SQSEVI = Preferences.SlotDAE3Y / 100 * Preferences.SQSEVIDAEQoutaPER;
                    DOMS = Preferences.SlotDAE3Y / 100 * Preferences.DOMSDAEQoutaPER;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    //var DDOMSInitialValue = Preferences.SlotDAE1Y / 100 * Preferences.DOMSDAEQoutaPER;
                    var TotalDAE3rdY = _context.DAEInstitute.Where(a => a.IsActive == true).Sum(a => a.Enrollment3rdY);
                    await DAEInstituteSlotAllocation(DOMS, SRCForumId, schemeLevelId, Preferences.DAEThreshold, Preferences.SchemeDAEStipend, TotalDAE3rdY);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else
                {
                    if(currentScheme.SchemeId == 6) // Simple Graduation
                    {
                        POMS = Preferences.SlotBacholar1Y / 100 * Preferences.POMSGraduationQoutaPER;
                        SQSOMS = Preferences.SlotBacholar1Y / 100 * Preferences.SQSOMSGraduationQoutaPER;
                        SQSEVI = Preferences.SlotBacholar1Y / 100 * Preferences.SQSEVIGraduationQoutaPER;
                        DOMS = Preferences.SlotBacholar1Y / 100 * Preferences.DOMSGraduationQoutaPER;                        
                        await DistrictSlotAllocation(DOMS, SRCForumId, schemeLevelId, Preferences.GraduationThreshold, Preferences.SchemeGraduationStipend);
                        Obj.POMS = POMS;
                        Obj.SQSOMS = SQSOMS;
                        Obj.SQSEVIs = SQSEVI;
                        Obj.DOMS = DOMS;
                        Obj.SchemeLevelId = schemeLevelId;
                        Obj.PolicySRCForumId = SRCForumId;
                        Obj.CreatedOn = DateTime.Now;
                    }
                    else if(currentScheme.SchemeId == 3) // Bachlor-BS
                    {

                    }
                    else if (currentScheme.SchemeId == 4) // Master
                    {

                    }
                    else if (currentScheme.SchemeId == 5) // MS/M.Phil
                    {

                    }
                }
                _context.Add(Obj);
            }
            await _context.SaveChangesAsync();
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
