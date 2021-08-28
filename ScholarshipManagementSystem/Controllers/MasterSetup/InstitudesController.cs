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
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
            var applicationDbContext = _context.Institude.Include(i => i.InstitudeType).Include(i => i.Provience);
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
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name");
            return View();
        }
     
        // POST: Institudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstitudeId,InstitudeTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institude institude, IFormFile Attachment)
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
                    institude.LogoPath = Path.Combine("/Institutes/Logo/", fileName);//Server Path
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
                _context.Add(institude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstitudeTypeId"] = new SelectList(_context.InstitudeType, "InstitudeTypeId", "Name", institude.InstitudeTypeId);
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", institude.ProvienceId);
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
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", institude.ProvienceId);
            return View(institude);
        }

        // POST: Institudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstitudeId,InstitudeTypeId,Name,NameAbbreviation,Website,Email,PhoneNo,FaxNo,ProvienceId,Address,FocalPersonName,FocalPersonEmail,FocalPersonPhoneNo,LogoPath")] Institude institude, IFormFile Attachment)
        {
            if (id != institude.InstitudeId)
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
                        institude.LogoPath = Path.Combine("/Institutes/Logo/", fileName);//Server Path
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
            ViewData["ProvienceId"] = new SelectList(_context.Provience, "ProvienceId", "Name", institude.ProvienceId);
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
