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
    public class DistrictDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DistrictDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DistrictDetail.Include(d => d.District);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DistrictDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtDetail = await _context.DistrictDetail
                .Include(d => d.District)
                .FirstOrDefaultAsync(m => m.DistrictDetailId == id);
            if (districtDetail == null)
            {
                return NotFound();
            }

            return View(districtDetail);
        }

        // GET: DistrictDetails/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name");
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 11).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }).OrderByDescending(a=>a.Value), "Value", "Text");
            return View();
        }

        // POST: DistrictDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictDetailId,DistrictId,MPIScore,Population,MaleRatio,FemaleRatio,CensesYear,GrowthRate")] DistrictDetail districtDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(districtDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", districtDetail.DistrictId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name");
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 11).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }).OrderByDescending(a=>a.Value), "Value", "Text");
            return View(districtDetail);
        }

        // GET: DistrictDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtDetail = await _context.DistrictDetail.FindAsync(id);
            if (districtDetail == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", districtDetail.DistrictId);            
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 11).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }).OrderByDescending(a=>a.Value), "Value", "Text",districtDetail.CensesYear);
            return View(districtDetail);
        }

        // POST: DistrictDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DistrictDetailId,DistrictId,MPIScore,Population,MaleRatio,FemaleRatio,CensesYear,GrowthRate")] DistrictDetail districtDetail)
        {
            if (id != districtDetail.DistrictDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(districtDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictDetailExists(districtDetail.DistrictDetailId))
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
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", districtDetail.DistrictId);
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 10, 11).Select(x =>
           new SelectListItem()
           {
               Text = x.ToString(),
               Value = x.ToString()
           }).OrderByDescending(a=>a.Value), "Value", "Text", districtDetail.CensesYear);
            return View(districtDetail);
        }

        // GET: DistrictDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districtDetail = await _context.DistrictDetail
                .Include(d => d.District)
                .FirstOrDefaultAsync(m => m.DistrictDetailId == id);
            if (districtDetail == null)
            {
                return NotFound();
            }

            return View(districtDetail);
        }

        // POST: DistrictDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var districtDetail = await _context.DistrictDetail.FindAsync(id);
            _context.DistrictDetail.Remove(districtDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictDetailExists(int id)
        {
            return _context.DistrictDetail.Any(e => e.DistrictDetailId == id);
        }
    }
}
