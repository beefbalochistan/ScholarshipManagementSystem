using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class SchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchemeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchemeLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchemeLevel.Include(s => s.QualificationLevel).Include(s => s.Scheme).Include(s => s.Institute).OrderBy(a=>a.OrderBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevel = await _context.SchemeLevel
                .Include(s => s.QualificationLevel)
                .Include(s => s.Scheme)
                .Include(s => s.Institute)
                .FirstOrDefaultAsync(m => m.SchemeLevelId == id);
            if (schemeLevel == null)
            {
                return NotFound();
            }

            return View(schemeLevel);
        }

        // GET: SchemeLevels/Create
        public IActionResult Create()
        {
            ViewData["QualificationLevelId"] = new SelectList(_context.QualificationLevel, "QualificationLevelId", "Name");
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "Name");
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");
            var gradingSystemList = new List<SelectListItem>
            {
               new SelectListItem{ Text="GPA Based", Value = "1", Selected = true },
               new SelectListItem{ Text="Marks Based", Value = "2" },
            };
            ViewData["ddGradingSystemList"] = gradingSystemList;
            return View();
        }

        // POST: SchemeLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchemeLevel schemeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QualificationLevelId"] = new SelectList(_context.QualificationLevel, "QualificationLevelId", "Name", schemeLevel.QualificationLevelId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "Name");
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", schemeLevel.SchemeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", schemeLevel.PaymentMethodId);
            var gradingSystemList = new List<SelectListItem>
            {
               new SelectListItem{ Text="GPA Based", Value = "1", Selected = true },
               new SelectListItem{ Text="Marks Based", Value = "2" },
            };
            ViewData["ddGradingSystemList"] = gradingSystemList;
            return View(schemeLevel);
        }

        // GET: SchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevel = await _context.SchemeLevel.FindAsync(id);
            if (schemeLevel == null)
            {
                return NotFound();
            }
            ViewData["QualificationLevelId"] = new SelectList(_context.QualificationLevel, "QualificationLevelId", "Name", schemeLevel.QualificationLevelId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "Name");
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", schemeLevel.SchemeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");
            var gradingSystemList = new List<SelectListItem>
            {
               new SelectListItem{ Text="GPA Based", Value = "1", Selected = true },
               new SelectListItem{ Text="Marks Based", Value = "2" },
            };
            ViewData["ddGradingSystemList"] = gradingSystemList;
            return View(schemeLevel);
        }

        // POST: SchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SchemeLevel schemeLevel)
        {
            if (id != schemeLevel.SchemeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schemeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchemeLevelExists(schemeLevel.SchemeLevelId))
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
            ViewData["QualificationLevelId"] = new SelectList(_context.QualificationLevel, "QualificationLevelId", "Name", schemeLevel.QualificationLevelId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "InstituteId", "Name", schemeLevel.InstituteId);
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name", schemeLevel.SchemeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", schemeLevel.PaymentMethodId);
            var gradingSystemList = new List<SelectListItem>
            {
               new SelectListItem{ Text="GPA Based", Value = "1", Selected = true },
               new SelectListItem{ Text="Marks Based", Value = "2" },
            };
            ViewData["ddGradingSystemList"] = gradingSystemList;
            return View(schemeLevel);
        }

        // GET: SchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schemeLevel = await _context.SchemeLevel
                .Include(s => s.QualificationLevel)
                .Include(s => s.Scheme)
                .FirstOrDefaultAsync(m => m.SchemeLevelId == id);
            if (schemeLevel == null)
            {
                return NotFound();
            }

            return View(schemeLevel);
        }

        // POST: SchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schemeLevel = await _context.SchemeLevel.FindAsync(id);
            _context.SchemeLevel.Remove(schemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchemeLevelExists(int id)
        {
            return _context.SchemeLevel.Any(e => e.SchemeLevelId == id);
        }
    }
}
