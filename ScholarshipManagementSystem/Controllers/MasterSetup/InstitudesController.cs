using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    [AllowAnonymous]
    public class InstitutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstitutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Institutes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Institute.Include(i => i.InstituteType).Include(i => i.Provience);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Institutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Institute = await _context.Institute
                .Include(i => i.InstituteType)
                .FirstOrDefaultAsync(m => m.InstituteId == id);
            if (Institute == null)
            {
                return NotFound();
            }

            return View(Institute);
        }

        // GET: Institutes/Create
        public IActionResult Create()
        {
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteType, "InstituteTypeId", "Name");
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name");
            return View();
        }
     
        // POST: Institutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteId,InstituteTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institute Institute, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null && Attachment.Length > 0)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Institutes\\Logo\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Logo" + randomNumber.ToString() + fileName;
                    Institute.LogoPath = Path.Combine("/Institutes/Logo/", fileName);//Server Path
                    string sPath = Path.Combine(rootPath);
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                    //-----------------------------------
                    using var image = Image.Load(Attachment.OpenReadStream());
                    image.Mutate(x => x.Resize(60, 60));
                    image.Save(FullPathWithFileName);
                }
                _context.Add(Institute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteType, "InstituteTypeId", "Name", Institute.InstituteTypeId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", Institute.ProvienceId);
            return View(Institute);
        }

        // GET: Institutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Institute = await _context.Institute.FindAsync(id);
            if (Institute == null)
            {
                return NotFound();
            }
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteType, "InstituteTypeId", "Name", Institute.InstituteTypeId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", Institute.ProvienceId);
            return View(Institute);
        }

        // POST: Institutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteId,InstituteTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institute Institute, IFormFile Attachment)
        {
            if (id != Institute.InstituteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Attachment != null && Attachment.Length > 0)
                    {
                        var rootPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\Institutes\\Logo\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Logo" + randomNumber.ToString() + fileName;
                        Institute.LogoPath = Path.Combine("/Institutes/Logo/", fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        //-----------------------------------
                        using var image = Image.Load(Attachment.OpenReadStream());
                        image.Mutate(x => x.Resize(60, 60));
                        image.Save(FullPathWithFileName);
                    }
                    _context.Update(Institute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(Institute.InstituteId))
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
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteType, "InstituteTypeId", "Name", Institute.InstituteTypeId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", Institute.ProvienceId);
            return View(Institute);
        }

        // GET: Institutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Institute = await _context.Institute
                .Include(i => i.InstituteType)
                .FirstOrDefaultAsync(m => m.InstituteId == id);
            if (Institute == null)
            {
                return NotFound();
            }

            return View(Institute);
        }

        // POST: Institutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Institute = await _context.Institute.FindAsync(id);
            _context.Institute.Remove(Institute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(int id)
        {
            return _context.Institute.Any(e => e.InstituteId == id);
        }
    }
}
