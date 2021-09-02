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
        private async Task<int> DistrictSlotAllocation(int DOMSSlot, int SRCId, int DistrictThreshold)
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
                _context.Add(Obj);                
            }
            await _context.SaveChangesAsync();
            return 1;
        }
        private async Task<bool> GenerateSchemeLevelPolicy(int SRCForumId)
        {
            var Preferences = _context.Preference.Find(1);
            var PreferencesSlot = _context.PreferencesSlot.Find(1);

            var SchemeLevels = _context.SchemeLevel.Where(a => a.IsActive == true).Select(a => a.SchemeLevelId).ToList();            
            foreach(var schemeLevelId in SchemeLevels)
            {
                SchemeLevelPolicy Obj = new SchemeLevelPolicy();
                var currentScheme = _context.SchemeLevel.Find(schemeLevelId);                
                Obj.Amount = currentScheme.SchemeId == 1 ? Preferences.SchemeMatrictStipend : (currentScheme.SchemeId == 2 ? Preferences.SchemeIntermediateStipend : (currentScheme.SchemeId == 3 ? Preferences.SchemeBacholarStipend : (currentScheme.SchemeId == 4 ? Preferences.SchemeMasterStipend : (currentScheme.SchemeId == 5 ? Preferences.SchemeMSStipend : 0))));
                var POMS = 0;
                var SQSOMS = 0;
                var SQSEVI = 0;
                var DOMS = 0;
                if (currentScheme.SchemeLevelId == 5)// For 9th Pass 10th
                {
                    POMS = PreferencesSlot.SlotMetric / 100 * Preferences.POMSIBoardQouta;
                    SQSOMS = PreferencesSlot.SlotMetric / 100 * Preferences.SQSOMSQouta;
                    SQSEVI = PreferencesSlot.SlotMetric / 100 * Preferences.SQSEVIQouta;
                    DOMS = PreferencesSlot.SlotMetric / 100 * Preferences.DOMSBoardQouta;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    var DDOMSInitialValue = PreferencesSlot.SlotMetric / 100 * Preferences.DOMSBoardQouta;
                    await DistrictSlotAllocation(DDOMSInitialValue, SRCForumId, Preferences.DistrictThreshold);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (currentScheme.SchemeLevelId == 3)// For 10th Pass 1st Yr
                {
                    POMS = PreferencesSlot.SlotFAFSc1Y / 100 * Preferences.POMSIBoardQouta;
                    SQSOMS = PreferencesSlot.SlotFAFSc1Y / 100 * Preferences.SQSOMSQouta;
                    SQSEVI = PreferencesSlot.SlotFAFSc1Y / 100 * Preferences.SQSEVIQouta;
                    DOMS = PreferencesSlot.SlotFAFSc1Y / 100 * Preferences.DOMSBoardQouta;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    var DDOMSInitialValue = PreferencesSlot.SlotFAFSc1Y / 100 * Preferences.DOMSBoardQouta;
                    await DistrictSlotAllocation(DDOMSInitialValue, SRCForumId, Preferences.DistrictThreshold);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (currentScheme.SchemeLevelId == 4)// For 1st Yr Pass 2nd Yr
                {
                    POMS = PreferencesSlot.SlotFAFSc2Y / 100 * Preferences.POMSIBoardQouta;
                    SQSOMS = PreferencesSlot.SlotFAFSc2Y / 100 * Preferences.SQSOMSQouta;
                    SQSEVI = PreferencesSlot.SlotFAFSc2Y / 100 * Preferences.SQSEVIQouta;
                    DOMS = PreferencesSlot.SlotFAFSc2Y / 100 * Preferences.DOMSBoardQouta;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    var DDOMSInitialValue = PreferencesSlot.SlotFAFSc2Y / 100 * Preferences.DOMSBoardQouta;
                    await DistrictSlotAllocation(DDOMSInitialValue, SRCForumId, Preferences.DistrictThreshold);
                    Obj.POMS = POMS;
                    Obj.SQSOMS = SQSOMS;
                    Obj.SQSEVIs = SQSEVI;
                    Obj.DOMS = DOMS;
                    Obj.SchemeLevelId = schemeLevelId;
                    Obj.PolicySRCForumId = SRCForumId;
                    Obj.CreatedOn = DateTime.Now;
                }
                else if (currentScheme.SchemeLevelId == 8)// For 10th Pass DAE 1st Yr
                {
                    POMS = PreferencesSlot.SlotDAE1Y / 100 * Preferences.POMSIBoardQouta;
                    SQSOMS = PreferencesSlot.SlotDAE1Y / 100 * Preferences.SQSOMSQouta;
                    SQSEVI = PreferencesSlot.SlotDAE1Y / 100 * Preferences.SQSEVIQouta;
                    DOMS = PreferencesSlot.SlotDAE1Y / 100 * Preferences.DOMSBoardQouta;
                    //Value of DDOMS before assigning the additional slot of to meet threshold of the district
                    var DDOMSInitialValue = PreferencesSlot.SlotDAE1Y / 100 * Preferences.DOMSBoardQouta;
                    await DistrictSlotAllocation(DDOMSInitialValue, SRCForumId, Preferences.DistrictThreshold);
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
