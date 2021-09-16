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
    public class ResultUploadTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultUploadTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultUploadTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResultUploadType.ToListAsync());
        }

        // GET: ResultUploadTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ResultUploadType = await _context.ResultUploadType
                .FirstOrDefaultAsync(m => m.ResultUploadTypeId == id);
            if (ResultUploadType == null)
            {
                return NotFound();
            }

            return View(ResultUploadType);
        }

        // GET: ResultUploadTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResultUploadTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultUploadTypeId,Name,Code,Description")] ResultUploadType ResultUploadType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ResultUploadType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ResultUploadType);
        }

        // GET: ResultUploadTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ResultUploadType = await _context.ResultUploadType.FindAsync(id);
            if (ResultUploadType == null)
            {
                return NotFound();
            }
            return View(ResultUploadType);
        }

        // POST: ResultUploadTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultUploadTypeId,Name,Code,Description")] ResultUploadType ResultUploadType)
        {
            if (id != ResultUploadType.ResultUploadTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ResultUploadType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultUploadTypeExists(ResultUploadType.ResultUploadTypeId))
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
            return View(ResultUploadType);
        }

        // GET: ResultUploadTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ResultUploadType = await _context.ResultUploadType
                .FirstOrDefaultAsync(m => m.ResultUploadTypeId == id);
            if (ResultUploadType == null)
            {
                return NotFound();
            }

            return View(ResultUploadType);
        }

        // POST: ResultUploadTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ResultUploadType = await _context.ResultUploadType.FindAsync(id);
            _context.ResultUploadType.Remove(ResultUploadType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultUploadTypeExists(int id)
        {
            return _context.ResultUploadType.Any(e => e.ResultUploadTypeId == id);
        }
    }
}
