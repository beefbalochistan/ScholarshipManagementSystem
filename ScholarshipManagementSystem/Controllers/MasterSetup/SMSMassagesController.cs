using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class SMSMassagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSMassagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMSMassages
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSMassage.ToListAsync());
        }

        // GET: SMSMassages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSMassage = await _context.SMSMassage
                .FirstOrDefaultAsync(m => m.SMSMassageTypeId == id);
            if (sMSMassage == null)
            {
                return NotFound();
            }

            return View(sMSMassage);
        }

        // GET: SMSMassages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSMassages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SMSMassageTypeId,SMSType,Title,Massage")] SMSMassage sMSMassage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSMassage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSMassage);
        }

        // GET: SMSMassages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSMassage = await _context.SMSMassage.FindAsync(id);
            if (sMSMassage == null)
            {
                return NotFound();
            }
            return View(sMSMassage);
        }

        // POST: SMSMassages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SMSMassageTypeId,SMSType,Title,Massage")] SMSMassage sMSMassage)
        {
            if (id != sMSMassage.SMSMassageTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSMassage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSMassageExists(sMSMassage.SMSMassageTypeId))
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
            return View(sMSMassage);
        }

        // GET: SMSMassages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSMassage = await _context.SMSMassage
                .FirstOrDefaultAsync(m => m.SMSMassageTypeId == id);
            if (sMSMassage == null)
            {
                return NotFound();
            }

            return View(sMSMassage);
        }

        // POST: SMSMassages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sMSMassage = await _context.SMSMassage.FindAsync(id);
            _context.SMSMassage.Remove(sMSMassage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSMassageExists(int id)
        {
            return _context.SMSMassage.Any(e => e.SMSMassageTypeId == id);
        }
    }
}
