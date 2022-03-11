using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.VirtualAccount;
using Repository.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ScholarshipManagementSystem.Controllers.VirtualAccount
{
    public class TrunchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrunchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trunches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trunch.Include(t => t.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            var applicationDbContext = _context.Trunch.Include(t => t.PaymentMethod).Where(a=>a.IsActive == true && a.IsOpen == true && a.IsLock == false);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: Trunches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trunch = await _context.Trunch
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(m => m.TrunchId == id);
            if (trunch == null)
            {
                return NotFound();
            }

            return View(trunch);
        }

        // GET: Trunches/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");            
            Trunch obj = new Trunch();
            obj.IsActive = true;
            obj.IsApproved = false;
            obj.IsClose = false;
            obj.IsLock = false;
            obj.IsOpen = true;
            obj.ApplicantCount = 0;
            obj.CurrentCommittedAmount = 0;
            return View(obj);
        }

        // POST: Trunches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrunchId,Name,PaymentMethodId,SchemeLevelId,IsOpen,IsClose,IsLock,IsApproved,CreatedOn,ApprovedOn,ApprovedAttachment,CurrentCommittedAmount,ApprovedAmount,IsActive,ApplicantCount")] Trunch trunch)
        {
            if (ModelState.IsValid)
            {
                trunch.CreatedOn = DateTime.Today.Date;
                _context.Add(trunch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "name", trunch.PaymentMethodId);            
            return View(trunch);
        }

        // GET: Trunches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trunch = await _context.Trunch.FindAsync(id);
            if (trunch == null)
            {
                return NotFound();
            }
            trunch.ApprovedOn = DateTime.Today.Date;
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", trunch.PaymentMethodId);            
            return View(trunch);
        }

        // POST: Trunches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trunch trunch, IFormFile attachment)
        {
            if (id != trunch.TrunchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (attachment != null && attachment.Length > 0)
                    {                        
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Trunch\\TId" + trunch.TrunchId + "\\");
                        string fileName = Path.GetFileName(attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "SRCMinutes" + randomNumber.ToString() + fileName;
                        trunch.ApprovedAttachment = Path.Combine("/Documents/Trunch/TId", fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await attachment.CopyToAsync(stream);
                        }                                                                                     
                    }
                    //-----------------------------------    
                    _context.Update(trunch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrunchExists(trunch.TrunchId))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", trunch.PaymentMethodId);            
            return View(trunch);
        }

        // GET: Trunches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trunch = await _context.Trunch
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(m => m.TrunchId == id);
            if (trunch == null)
            {
                return NotFound();
            }

            return View(trunch);
        }

        // POST: Trunches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trunch = await _context.Trunch.FindAsync(id);
            _context.Trunch.Remove(trunch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrunchExists(int id)
        {
            return _context.Trunch.Any(e => e.TrunchId == id);
        }
    }
}
