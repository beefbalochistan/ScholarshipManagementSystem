using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class InstitudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Institudes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Institude.Include(i => i.InstitudeType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Institudes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institude = await _context.Institude
                .Include(i => i.InstitudeType)
                .FirstOrDefaultAsync(m => m.InstitudeId == id);
            if (institude == null)
            {
                return NotFound();
            }

            return View(institude);
        }

        // GET: Institudes/Create
        public IActionResult Create()
        {
            ViewData["InstitudeTypeId"] = new SelectList(_context.InstitudeType, "InstitudeTypeId", "Name");
            return View();
        }

        // POST: Institudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstitudeId,InstitudeTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institude institude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstitudeTypeId"] = new SelectList(_context.InstitudeType, "InstitudeTypeId", "Name", institude.InstitudeTypeId);
            return View(institude);
        }

        // GET: Institudes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institude = await _context.Institude.FindAsync(id);
            if (institude == null)
            {
                return NotFound();
            }
            ViewData["InstitudeTypeId"] = new SelectList(_context.InstitudeType, "InstitudeTypeId", "Name", institude.InstitudeTypeId);
            return View(institude);
        }

        // POST: Institudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstitudeId,InstitudeTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institude institude)
        {
            if (id != institude.InstitudeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitudeExists(institude.InstitudeId))
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
            ViewData["InstitudeTypeId"] = new SelectList(_context.InstitudeType, "InstitudeTypeId", "Name", institude.InstitudeTypeId);
            return View(institude);
        }

        // GET: Institudes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institude = await _context.Institude
                .Include(i => i.InstitudeType)
                .FirstOrDefaultAsync(m => m.InstitudeId == id);
            if (institude == null)
            {
                return NotFound();
            }

            return View(institude);
        }

        // POST: Institudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institude = await _context.Institude.FindAsync(id);
            _context.Institude.Remove(institude);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitudeExists(int id)
        {
            return _context.Institude.Any(e => e.InstitudeId == id);
        }
    }
}
