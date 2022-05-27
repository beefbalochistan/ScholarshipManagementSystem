using System;
using ScholarshipManagementSystem.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.VirtualAccount;
using Repository.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using CsvHelper;
using ScholarshipManagementSystem.Models;
using System.Globalization;
using System.Collections.Generic;
using Renci.SshNet;
using Microsoft.AspNetCore.Hosting;
using OpenPGP.Services;

namespace ScholarshipManagementSystem.Controllers.VirtualAccount
{
    public class TrancheDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        IWebHostEnvironment _env;
        public TrancheDocumentsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: TrancheDocuments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrancheDocument.Include(t => t.Tranche);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            ViewBag.TrancheId = id;
            /*var applicationDbContext = _context.TrancheDocument.Include(t => t.Tranche).Where(a=>a.TrancheId == id);
            return View(await applicationDbContext.ToListAsync());*/
            return View();
        }
        public IActionResult ReloadTrancheDocument(int id)
        {
            return ViewComponent("TrancheDocument", new { id });
        }
                
        public async Task<JsonResult> SFTPUploadFile(int trancheDocumentId)
        {
            string host = "103.8.14.69";            
            string username = "beef";            
            string password = "FeUbQtB9";
            string port = "22";
            string uploadFile = "";
            var tranchdoc = _context.TrancheDocument.Find(trancheDocumentId);
            if (tranchdoc != null)
            {
                uploadFile = Path.Combine(Directory.GetCurrentDirectory(), tranchdoc.PGPAttachment);
                bool RESULT = SendFileToServer.Send(host, username, password, port, uploadFile);                
                if (RESULT)
                {
                    tranchdoc.IsSendToServer = true;
                    bool IsSend = await _emailSender.SendEmail("saifpanezai@gmail.com", "BEEF Student Scholarship List for Disbersment",
                        $"Following tranche has been send on to your server.\n Kindly process it.");
                    if (IsSend)
                    {
                        tranchdoc.IsEmail = true;
                    }
                    _context.Update(tranchdoc);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = true, message = "Uploaded File Successfully!" });
                }
            }            
            return Json(new { isValid = false, message = "Failed to Upload File!" });
        }


        public async Task<JsonResult> UploadDisbursementFile(int trancheDocumentId, IFormFile excelFile)
        {
            
            var tranchdoc = _context.TrancheDocument.Find(trancheDocumentId);
            List<OmniDisbursement> applicantList;
            if (tranchdoc != null)
            {
                try
                {
                    string filePath = @"D:\LMS\disbursement.csv";
                    string test = "";
                        FileInfo inputFile = new FileInfo(filePath);
                    using (var reader = new StreamReader(inputFile.FullName))
                    {
                        test = reader.ReadLine();                        
                        test = reader.ReadLine();                        
                        test = reader.ReadLine();                        
                        test = reader.ReadLine();                        
                        test = reader.ReadLine();                        
                        using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                        {
                            csvReader.Context.RegisterClassMap<DataRecordMap>();
                            applicantList = csvReader.GetRecords<OmniDisbursement>().ToList();                            
                            //decimal d = decimal.Parse(bookList.ElementAt(1).CustomerCnic, NumberStyles.Float);                            
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                foreach(var applicant in applicantList)
                {

                }
                _context.Update(tranchdoc);
                //await _context.SaveChangesAsync();
                return Json(new { isValid = true, message = "Uploaded File Successfully!" });                
            }
            return Json(new { isValid = false, message = "Failed to Upload File!" });
        }
        // GET: TrancheDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trancheDocument = await _context.TrancheDocument
                .Include(t => t.Tranche)
                .FirstOrDefaultAsync(m => m.TrancheDocumentId == id);
            if (trancheDocument == null)
            {
                return NotFound();
            }

            return View(trancheDocument);
        }

        // GET: TrancheDocuments/Create
        public IActionResult Create()
        {
            ViewData["TrancheId"] = new SelectList(_context.Tranche, "TrancheId", "Name");
            return View();
        }

        // POST: TrancheDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrancheDocumentId,TrancheId,CSVAttachment,CSVAttachmentOn,IsPGPGenerated,PGPAttachment,PGPGeneratedOn,PGPKey,IsEmail,IsSendToServer")] TrancheDocument trancheDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trancheDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrancheId"] = new SelectList(_context.Tranche, "TrancheId", "Name", trancheDocument.TrancheId);
            return View(trancheDocument);
        }

        // GET: TrancheDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trancheDocument = await _context.TrancheDocument.FindAsync(id);
            if (trancheDocument == null)
            {
                return NotFound();
            }
            ViewData["TrancheId"] = new SelectList(_context.Tranche, "TrancheId", "Name", trancheDocument.TrancheId);
            return View(trancheDocument);
        }

        // POST: TrancheDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrancheDocumentId,TrancheId,CSVAttachment,CSVAttachmentOn,IsPGPGenerated,PGPAttachment,PGPGeneratedOn,PGPKey,IsEmail,IsSendToServer")] TrancheDocument trancheDocument)
        {
            if (id != trancheDocument.TrancheDocumentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trancheDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrancheDocumentExists(trancheDocument.TrancheDocumentId))
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
            ViewData["TrancheId"] = new SelectList(_context.Tranche, "TrancheId", "Name", trancheDocument.TrancheId);
            return View(trancheDocument);
        }

        // GET: TrancheDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trancheDocument = await _context.TrancheDocument
                .Include(t => t.Tranche)
                .FirstOrDefaultAsync(m => m.TrancheDocumentId == id);
            if (trancheDocument == null)
            {
                return NotFound();
            }

            return View(trancheDocument);
        }

        // POST: TrancheDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trancheDocument = await _context.TrancheDocument.FindAsync(id);
            _context.TrancheDocument.Remove(trancheDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrancheDocumentExists(int id)
        {
            return _context.TrancheDocument.Any(e => e.TrancheDocumentId == id);
        }
    }
}
