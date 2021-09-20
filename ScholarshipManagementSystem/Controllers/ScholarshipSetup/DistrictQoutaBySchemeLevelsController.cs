
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.ScholarshipSetup;
using ScholarshipManagementSystem.Models.ViewModels;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    [AllowAnonymous]
    public class DistrictQoutaBySchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictQoutaBySchemeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DistrictQoutaBySchemeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DistrictQoutaBySchemeLevel.Include(d => d.District).Include(d => d.SRCForum);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ViewPolicy(int id)
        {            
            
            List<PolicyView> PolicyList = new List<PolicyView>();
            string sql = "EXEC[scholar].[PolicyView] @PolicySRCForumId";
            List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@PolicySRCForumId", Value = id }
                    };
            var sp_policyView = _context.PolicyView.FromSqlRaw<PolicyView>(sql, parms.ToArray()).ToList();
            foreach (var schemeLevel in sp_policyView)
            {
                PolicyView Obj = new PolicyView();
                Obj.Amount = schemeLevel.Amount;
                Obj.DAEAdditionalSlot = schemeLevel.DAEAdditionalSlot;
                Obj.DegreeAdditionalSlot = schemeLevel.DegreeAdditionalSlot;
                Obj.DistrictAdditionalSlot = schemeLevel.DistrictAdditionalSlot;
                Obj.DOMS = schemeLevel.DOMS;
                Obj.POMS = schemeLevel.POMS;
                Obj.SQSOMS = schemeLevel.SQSOMS;
                Obj.SQSEVIs = schemeLevel.SQSEVIs;
                Obj.InstituteId = schemeLevel.InstituteId;
                Obj.OrderBy = schemeLevel.OrderBy;
                Obj.PolicyForum = schemeLevel.PolicyForum;
                Obj.PolicySRCForumId = schemeLevel.PolicySRCForumId;
                Obj.PolicyYear = schemeLevel.PolicyYear;
                Obj.QualificationLevel = schemeLevel.QualificationLevel;
                Obj.QualificationLevelId = schemeLevel.QualificationLevelId;
                Obj.Scheme = schemeLevel.Scheme;
                Obj.SchemeId = schemeLevel.SchemeId;
                Obj.SchemeLevel = schemeLevel.SchemeLevel;
                Obj.SchemeLevelCode = schemeLevel.SchemeLevelCode;
                Obj.SchemeLevelId = schemeLevel.SchemeLevelId;
                Obj.SchemeLevelPolicyId = schemeLevel.SchemeLevelPolicyId;
                Obj.ScholarshipSlot = schemeLevel.ScholarshipSlot;
                Obj.Year = schemeLevel.Year;                              
                if (schemeLevel.QualificationLevelId == 1 || schemeLevel.QualificationLevelId == 2 || schemeLevel.QualificationLevelId == 4 || schemeLevel.SchemeLevelId == 9)
                {                                    
                    var applicationDbContext = _context.DistrictQoutaBySchemeLevel.Include(a => a.District).Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a => a.PolicySRCForumId == schemeLevel.PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == schemeLevel.SchemeLevelId).ToList();
                    List<PolicyDetailView> PolicyDetailList = new List<PolicyDetailView>();
                    foreach (var district in applicationDbContext)
                    {
                        PolicyDetailView PDVObj = new PolicyDetailView();
                        PDVObj.CurrentYearPopulation = district.CurrentYearPopulation;
                        PDVObj.DistrictAdditionalSlot = district.DistrictAdditionalSlot;
                        PDVObj.DistrictId = district.DistrictId;
                        PDVObj.District = district.District;
                        PDVObj.DistrictMPISlot = district.DistrictMPISlot;
                        PDVObj.DistrictPopulationSlot = district.DistrictPopulationSlot;
                        PDVObj.DistrictQoutaBySchemeLevelId = district.DistrictQoutaBySchemeLevelId;
                        PDVObj.MPI = district.MPI;
                        PDVObj.MPIDifferenceFromStatndard = district.MPIDifferenceFromStatndard;
                        PDVObj.PolicySRSForumId = district.PolicySRCForumId;
                        PDVObj.SchemeLevelPolicyId = district.SchemeLevelPolicyId;
                        PDVObj.StipendAmount = district.StipendAmount;
                        PDVObj.Threshold = district.Threshold;
                        PolicyDetailList.Add(PDVObj);
                    }                    
                    Obj.DistrictPolicyDetailViewList = PolicyDetailList.ToList();
                    PolicyList.Add(Obj);
                }
                else if(schemeLevel.QualificationLevelId == 3)
                {                   
                    var applicationDbContext = _context.DAEInstituteQoutaBySchemeLevel.Include(a => a.DAEInstitute).Where(a => a.PolicySRCForumId == schemeLevel.PolicySRCForumId).ToList();
                    List<DAEPolicyDetailView> PolicyDetailList = new List<DAEPolicyDetailView>();
                    foreach (var daeInstitute in applicationDbContext)
                    {
                        DAEPolicyDetailView DPDObj = new DAEPolicyDetailView();
                        DPDObj.ClassEnrollment = daeInstitute.ClassEnrollment;
                        DPDObj.DAEInstituteId = daeInstitute.DAEInstituteId;
                        DPDObj.DAEInstituteName = daeInstitute.DAEInstitute.Name;
                        DPDObj.DAEInstituteQoutaBySchemeLevelId = daeInstitute.DAEInstituteQoutaBySchemeLevelId;
                        DPDObj.InstituteAdditionalSlot = daeInstitute.InstituteAdditionalSlot;
                        DPDObj.SchemeLevelPolicyId = daeInstitute.SchemeLevelPolicyId;
                        DPDObj.SlotAllocate = daeInstitute.SlotAllocate;
                        DPDObj.StipendAmount = daeInstitute.StipendAmount;
                        DPDObj.InstituteAdditionalSlot = daeInstitute.InstituteAdditionalSlot;
                        DPDObj.Threshold = daeInstitute.Threshold;
                        DPDObj.Year = daeInstitute.Year;
                        DPDObj.PolicySRCForumId = daeInstitute.PolicySRCForumId;                        
                        PolicyDetailList.Add(DPDObj);
                    }                    
                    Obj.DAEPolicyDetailViewList = PolicyDetailList.ToList();
                    PolicyList.Add(Obj);
                }                
                
            }

            //------------------------------------Degree------------------------------
            List<PolicyView> PolicyList2 = new List<PolicyView>();
            string sql2 = "EXEC[scholar].[DegreePolicyView] @PolicySRCForumId";
            List<SqlParameter> parms2 = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@PolicySRCForumId", Value = id }
            };
            var degreeResult = _context.PolicyView.FromSqlRaw<PolicyView>(sql2, parms2.ToArray()).ToList();
            //------------------------------------------------------------------------
            foreach (var schemeLevel in degreeResult)
            {
                PolicyView PVObj = new PolicyView();
                PVObj.Amount = schemeLevel.Amount;
                PVObj.DAEAdditionalSlot = schemeLevel.DAEAdditionalSlot;                
                PVObj.DegreeAdditionalSlot = schemeLevel.DegreeAdditionalSlot;
                PVObj.DistrictAdditionalSlot = schemeLevel.DistrictAdditionalSlot;
                PVObj.DOMS = schemeLevel.DOMS;
                PVObj.POMS = schemeLevel.POMS;
                PVObj.SQSOMS = schemeLevel.SQSOMS;
                PVObj.SQSEVIs = schemeLevel.SQSEVIs;
                PVObj.InstituteId = schemeLevel.InstituteId;
                PVObj.OrderBy = schemeLevel.OrderBy;
                PVObj.PolicyForum = schemeLevel.PolicyForum;
                PVObj.PolicySRCForumId = schemeLevel.PolicySRCForumId;
                PVObj.PolicyYear = schemeLevel.PolicyYear;
                PVObj.QualificationLevel = schemeLevel.QualificationLevel;
                PVObj.QualificationLevelId = schemeLevel.QualificationLevelId;
                PVObj.Scheme = schemeLevel.Scheme;
                PVObj.SchemeId = schemeLevel.SchemeId;
                PVObj.SchemeLevel = schemeLevel.SchemeLevel;
                PVObj.SchemeLevelCode = schemeLevel.SchemeLevelCode;
                PVObj.SchemeLevelId = schemeLevel.SchemeLevelId;
                PVObj.SchemeLevelPolicyId = schemeLevel.SchemeLevelPolicyId;
                PVObj.ScholarshipSlot = schemeLevel.ScholarshipSlot;
                PVObj.Year = schemeLevel.Year;
                if (schemeLevel.SchemeId >= 4)//Hard KDA
                {                    
                    string query = "EXEC[scholar].[DegreeSecondLevel] @SchemeId, @CurrentYear, @PolicySRCForumId";
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SchemeId", Value = schemeLevel.SchemeId }, 
                        new SqlParameter { ParameterName = "@CurrentYear", Value = schemeLevel.Year },
                        new SqlParameter { ParameterName = "@PolicySRCForumId", Value = id }
                    };
                    var sp_result_degreeSecondLevel = _context.DegreeSecondLevel.FromSqlRaw<DegreeSecondLevel>(query, parameters.ToArray()).ToList();
                    //---------------------------Third Leyer-----------------------------------------
                    string sp_degree_third_level = "EXEC[scholar].[DegreeThirdLevel] @SchemeId, @PolicySRCForumId";
                    List<SqlParameter> sp_degree_third_level_parameters = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SchemeId", Value = schemeLevel.SchemeId },                        
                        new SqlParameter { ParameterName = "@PolicySRCForumId", Value = id }
                    };
                    var sp_result_degreeThirdLevel = _context.DegreeThirdLevel.FromSqlRaw<DegreeThirdLevel>(sp_degree_third_level, sp_degree_third_level_parameters.ToArray()).ToList();
                    //-------------------------------------------------------------------------------
                    List<DegreeSecondLevel> DegreeSecondLevelList = new List<DegreeSecondLevel>();
                    foreach (var degreeLevel in sp_result_degreeSecondLevel)
                    {
                        DegreeSecondLevel DSLObj = new DegreeSecondLevel();
                        DSLObj.DegreeLevel = degreeLevel.DegreeLevel;
                        DSLObj.Scheme = degreeLevel.Scheme;
                        DSLObj.SchemeId = degreeLevel.SchemeId;
                        DSLObj.InstituteId = degreeLevel.InstituteId;
                        DSLObj.CurrentYear = degreeLevel.CurrentYear;
                        DSLObj.SchemeLevel = degreeLevel.SchemeLevel;
                        DSLObj.SchemeLevelId = degreeLevel.SchemeLevelId;

                        //-----------------Third Leyer Implementation-----------------
                        List<DegreeThirdLevel> DegreeThirdLevelList = new List<DegreeThirdLevel>();
                        foreach (var degreeLeveldetail in sp_result_degreeThirdLevel.Where(a=>a.SchemeId == degreeLevel.SchemeId && a.CurrentYear == degreeLevel.CurrentYear && a.InstituteId == degreeLevel.InstituteId))
                        {
                            DegreeThirdLevel DTLObj = new DegreeThirdLevel();
                            DTLObj.AdditionalSlotAllocate = degreeLeveldetail.AdditionalSlotAllocate;
                            DTLObj.ClassEnrollment = degreeLeveldetail.ClassEnrollment;
                            DTLObj.CurrentYear = degreeLeveldetail.CurrentYear;
                            DTLObj.DegreeScholarshipLevelId = degreeLeveldetail.DegreeScholarshipLevelId;
                            DTLObj.DegreeScholarshipLevel = degreeLeveldetail.DegreeScholarshipLevel;
                            DTLObj.InstituteId = degreeLeveldetail.InstituteId;
                            DTLObj.SchemeId = degreeLeveldetail.SchemeId;
                            DTLObj.SlotAllocate = degreeLeveldetail.SlotAllocate;
                            DTLObj.StipendAmount = degreeLeveldetail.StipendAmount;
                            DTLObj.Threshold = degreeLeveldetail.Threshold;
                            DegreeThirdLevelList.Add(DTLObj);
                        }
                        //------------------------------------------------------------
                        DSLObj.DegreeThirdLevelList = DegreeThirdLevelList;
                        DegreeSecondLevelList.Add(DSLObj);
                    }
                    PVObj.DegreeSecondLevelList = DegreeSecondLevelList.ToList();
                    PolicyList.Add(PVObj);
                }
            }                                 
            return View(PolicyList.OrderBy(a => a.SchemeId).ToList());
        }
        // GET: DistrictQoutaBySchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel
                .Include(d => d.District)
                .Include(d => d.SRCForum)
                .FirstOrDefaultAsync(m => m.DistrictQoutaBySchemeLevelId == id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code");
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code");
            return View();
        }

        // POST: DistrictQoutaBySchemeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictQoutaBySchemeLevelId,DistrictId,Threshold,CurrentYearPopulation,SchemeLevelPolicyId,DistrictPopulationSlot,DistrictMPISlot,PolicySRCForumId,MPI")] DistrictQoutaBySchemeLevel districtQoutaBySchemeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(districtQoutaBySchemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel.FindAsync(id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // POST: DistrictQoutaBySchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DistrictQoutaBySchemeLevelId,DistrictId,Threshold,CurrentYearPopulation,SchemeLevelPolicyId,DistrictPopulationSlot,DistrictMPISlot,PolicySRCForumId,MPI")] DistrictQoutaBySchemeLevel districtQoutaBySchemeLevel)
        {
            if (id != districtQoutaBySchemeLevel.DistrictQoutaBySchemeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(districtQoutaBySchemeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictQoutaBySchemeLevelExists(districtQoutaBySchemeLevel.DistrictQoutaBySchemeLevelId))
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
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Code", districtQoutaBySchemeLevel.DistrictId);
            ViewData["PolicySRCForumId"] = new SelectList(_context.PolicySRCForum, "PolicySRCForumId", "Code", districtQoutaBySchemeLevel.PolicySRCForumId);
            return View(districtQoutaBySchemeLevel);
        }

        // GET: DistrictQoutaBySchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel
                .Include(d => d.District)
                .Include(d => d.PolicySRCForumId)
                .FirstOrDefaultAsync(m => m.DistrictQoutaBySchemeLevelId == id);
            if (districtQoutaBySchemeLevel == null)
            {
                return NotFound();
            }

            return View(districtQoutaBySchemeLevel);
        }

        // POST: DistrictQoutaBySchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var districtQoutaBySchemeLevel = await _context.DistrictQoutaBySchemeLevel.FindAsync(id);
            _context.DistrictQoutaBySchemeLevel.Remove(districtQoutaBySchemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictQoutaBySchemeLevelExists(int id)
        {
            return _context.DistrictQoutaBySchemeLevel.Any(e => e.DistrictQoutaBySchemeLevelId == id);
        }
    }
}
