using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using DAL.Models.Domain.ScholarshipSetup;

namespace ScholarshipManagementSystem.Controllers.ScholarshipSetup
{
    [AllowAnonymous]
    public class ScholarshipFiscalYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScholarshipFiscalYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScholarshipFiscalYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScholarshipFiscalYear.ToListAsync());
        }

        // GET: ScholarshipFiscalYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarshipFiscalYear = await _context.ScholarshipFiscalYear
                .FirstOrDefaultAsync(m => m.ScholarshipFiscalYearId == id);
            if (scholarshipFiscalYear == null)
            {
                return NotFound();
            }

            return View(scholarshipFiscalYear);
        }

        // GET: ScholarshipFiscalYears/Create
        public IActionResult Create()
        {
            ViewBag.YearsList = Enumerable.Range((DateTime.Now.Year - 2), 2).Select(g => new SelectListItem { Value = (g.ToString() + " - " + (g + 1).ToString()), Text = (g.ToString() + " - " + (g + 1).ToString()) }).ToList();
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 2, 2).Select(x =>

           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }), "Value", "Text");
            return View();
        }

        // POST: ScholarshipFiscalYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScholarshipFiscalYearId,Name,Code,Description")] ScholarshipFiscalYear scholarshipFiscalYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scholarshipFiscalYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scholarshipFiscalYear);
        }

        // GET: ScholarshipFiscalYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarshipFiscalYear = await _context.ScholarshipFiscalYear.FindAsync(id);
            if (scholarshipFiscalYear == null)
            {
                return NotFound();
            }
            ViewBag.YearsList = new SelectList(Enumerable.Range((DateTime.Now.Year - 2), 2).Select(g => 
            new SelectListItem() 
            { 
                Value = (g.ToString() + " - " + (g + 1).ToString()), 
                Text = (g.ToString() + " - " + (g + 1).ToString()) 
            }), "Value", "Text", scholarshipFiscalYear.Name);
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 2, 2).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }), "Value", "Text", scholarshipFiscalYear.Code);
            return View(scholarshipFiscalYear);
        }

        // POST: ScholarshipFiscalYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScholarshipFiscalYearId,Name,Code,Description")] ScholarshipFiscalYear scholarshipFiscalYear)
        {
            if (id != scholarshipFiscalYear.ScholarshipFiscalYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scholarshipFiscalYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScholarshipFiscalYearExists(scholarshipFiscalYear.ScholarshipFiscalYearId))
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
            ViewBag.YearsList = Enumerable.Range((DateTime.Now.Year - 2), 2).Select(g => new SelectListItem { Value = (g.ToString() + " - " + (g + 1).ToString()), Text = (g.ToString() + " - " + (g + 1).ToString()) }).ToList();
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 2, 2).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }), "Value", "Text");
            return View(scholarshipFiscalYear);
        }

        // GET: ScholarshipFiscalYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarshipFiscalYear = await _context.ScholarshipFiscalYear
                .FirstOrDefaultAsync(m => m.ScholarshipFiscalYearId == id);
            if (scholarshipFiscalYear == null)
            {
                return NotFound();
            }

            return View(scholarshipFiscalYear);
        }

        // POST: ScholarshipFiscalYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scholarshipFiscalYear = await _context.ScholarshipFiscalYear.FindAsync(id);
            _context.ScholarshipFiscalYear.Remove(scholarshipFiscalYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScholarshipFiscalYearExists(int id)
        {
            return _context.ScholarshipFiscalYear.Any(e => e.ScholarshipFiscalYearId == id);
        }
    }
}
