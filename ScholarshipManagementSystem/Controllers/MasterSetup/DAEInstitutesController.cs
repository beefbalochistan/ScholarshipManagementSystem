using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class DAEInstitutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DAEInstitutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DAEInstitutes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DAEInstitute.Include(d => d.District);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DAEInstitutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstitute = await _context.DAEInstitute
                .Include(d => d.District)
                .FirstOrDefaultAsync(m => m.DAEInstituteId == id);
            if (dAEInstitute == null)
            {
                return NotFound();
            }

            return View(dAEInstitute);
        }

        // GET: DAEInstitutes/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name");
            return View();
        }

        // POST: DAEInstitutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DAEInstituteId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,Enrollment1stY,Enrollment2ndY,Enrollment3rdY,IsActive,DistrictId,PercentageSlots")] DAEInstitute dAEInstitute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dAEInstitute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", dAEInstitute.DistrictId);
            return View(dAEInstitute);
        }

        // GET: DAEInstitutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstitute = await _context.DAEInstitute.FindAsync(id);
            if (dAEInstitute == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", dAEInstitute.DistrictId);
            return View(dAEInstitute);
        }

        // POST: DAEInstitutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DAEInstituteId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,Enrollment1stY,Enrollment2ndY,Enrollment3rdY,IsActive,DistrictId,PercentageSlots")] DAEInstitute dAEInstitute)
        {
            if (id != dAEInstitute.DAEInstituteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dAEInstitute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DAEInstituteExists(dAEInstitute.DAEInstituteId))
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
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "Name", dAEInstitute.DistrictId);
            return View(dAEInstitute);
        }

        // GET: DAEInstitutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dAEInstitute = await _context.DAEInstitute
                .Include(d => d.District)
                .FirstOrDefaultAsync(m => m.DAEInstituteId == id);
            if (dAEInstitute == null)
            {
                return NotFound();
            }

            return View(dAEInstitute);
        }

        // POST: DAEInstitutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dAEInstitute = await _context.DAEInstitute.FindAsync(id);
            _context.DAEInstitute.Remove(dAEInstitute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DAEInstituteExists(int id)
        {
            return _context.DAEInstitute.Any(e => e.DAEInstituteId == id);
        }
    }
}
