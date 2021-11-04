using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using DAL.Models.Domain.MasterSetup;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class PreferencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreferencesController(ApplicationDbContext context)
        {
            _context = context;
        }       

        // GET: Preferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preference.FindAsync(id);
            var list = new List<SelectListItem>
                {
                    new SelectListItem{ Text="By Total Slot", Value = "By Total Slot" },
                    new SelectListItem{ Text="By Enrollment", Value = "By Enrollment" },
                    new SelectListItem{ Text="By Degree Level Slot", Value = "By Degree Level Slot" },                    
                    new SelectListItem{ Text="By Equal Threshold", Value = "By Equal Threshold" },                    
                };
            ViewData["ddList"] = list;
            if (preference == null)
            {
                return NotFound();
            }
            return View(preference);
        }

        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Preference preference)
        {
            if (id != preference.PreferenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferenceExists(preference.PreferenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), 4);
            }
            return View(preference);
        }      
        private bool PreferenceExists(int id)
        {
            return _context.Preference.Any(e => e.PreferenceId == id);
        }
    }
}
