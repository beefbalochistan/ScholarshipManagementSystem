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
using DAL.Models.Domain.Student;
using CsvHelper;
using ScholarshipManagementSystem.Models;
using System.Text;
using System.Globalization;
using DAL.Models.Domain.ScholarshipSetup;
using DAL.Models.Domain.MasterSetup;
using OpenPGP.Services;

namespace ScholarshipManagementSystem.Controllers.VirtualAccount
{
    public class TranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tranchees
        public async Task<IActionResult> Index(bool x, bool y, string name)
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a=> a.IsApproved == y);            
            ViewBag.heading = name;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> CompletedTranche(string name)
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a => a.IsClose == true);
            ViewBag.heading = name;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ActiveTrancheDisbursement()
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a => a.IsApproved == true && a.IsClose == false);            
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a=>a.IsActive == true && a.IsOpen == true && a.IsLock == false);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        public async Task<JsonResult> OnOffTranche(int trancheId, bool IsChecked)
        {
            var tranche = _context.Tranche.Find(trancheId);
            tranche.IsActive = IsChecked;
            _context.Update(tranche);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, message = "Applicant Resumed Successfully!" });
        }
        [HttpPost]
        public async Task<JsonResult> RequestForApprovalTranche(int trancheId, bool IsChecked, string ChequeNo)
        {
            var tranche = _context.Tranche.Find(trancheId);
            tranche.IsLock = true;            
            if (IsChecked)
            {
                tranche.IsActive = false;
            }
            tranche.ChequeNo = ChequeNo;
            _context.Update(tranche);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, message = "Applicant Resumed Successfully!" });
        }
        [HttpPost]
        public async Task<JsonResult> TrancheApprovedRequest(int TrancheId, decimal ApprovedAmount, IFormFile Attachment)
        {
            var currentTrache = _context.Tranche.Find(TrancheId);
            //----------------Upload Attachment----------------------
            if (Attachment != null)
            {
                if (Attachment.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(Attachment.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension                                             
                    /*using (var target = new MemoryStream())
                    {
                        Attachment.CopyTo(target);
                        obj.AttachFileData = target.ToArray();
                    }*/
                    var rootPath = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Tranche\\TId" + TrancheId.ToString() + "\\");
                    fileName = fileName.Replace("&", "n"); fileName = fileName.Replace(" ", ""); fileName = fileName.Replace("#", "H"); fileName = fileName.Replace("(", ""); fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Document" + randomNumber.ToString() + fileName;
                    currentTrache.ApprovedAttachment = Path.Combine("/Documents/Tranche/TId" + TrancheId + "/" + fileName);//Server Path
                    string sPath = Path.Combine(rootPath);
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                    using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                    {
                        await Attachment.CopyToAsync(stream);
                    }
                }
            }
            //-------------------------------------------------------
            currentTrache.ApprovedOn = DateTime.Today;
            currentTrache.ApprovedAmount = ApprovedAmount;
            currentTrache.IsApproved = true;
            currentTrache.IsActive = false;
            currentTrache.IsLock = true;
            //currentTrache.IsClose = true;
            currentTrache.IsOpen = false;
            _context.Update(currentTrache);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, message = "Tranche Approved Successfully!" });
        }
        // GET: Tranchees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tranche = await _context.Tranche
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(m => m.TrancheId == id);
            if (Tranche == null)
            {
                return NotFound();
            }

            return View(Tranche);
        }

        // GET: Tranchees/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");            
            Tranche obj = new Tranche();
            obj.IsActive = true;
            obj.IsApproved = false;
            obj.IsClose = false;
            obj.IsLock = false;
            obj.IsOpen = true;
            obj.ApplicantCount = 0;
            ViewBag.CurrentFiscalYearCode = _context.PolicySRCForum.Include(a => a.ScholarshipFiscalYear).OrderByDescending(a=>a.ScholarshipFiscalYearId).Select(a=>a.ScholarshipFiscalYear.Code).FirstOrDefault();
            obj.CurrentCommittedAmount = 0;
            return View(obj);
        }

        // POST: Tranchees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrancheId,Name,PaymentMethodId,SchemeLevelId,IsOpen,IsClose,IsLock,IsApproved,CreatedOn,ApprovedOn,ApprovedAttachment,CurrentCommittedAmount,ApprovedAmount,IsActive,ApplicantCount")] Tranche Tranche)
        {
            if (ModelState.IsValid)
            {
                Tranche.CreatedOn = DateTime.Today.Date;
                _context.Add(Tranche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {w = true, x = false, y = false, z = true, name = "Open Tranche" });
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "name", Tranche.PaymentMethodId);            
            return View(Tranche);
        }

        // GET: Tranchees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tranche = await _context.Tranche.FindAsync(id);
            if (Tranche == null)
            {
                return NotFound();
            }
            Tranche.ApprovedOn = DateTime.Today.Date;
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", Tranche.PaymentMethodId);            
            return View(Tranche);
        }

        // POST: Tranchees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tranche Tranche, IFormFile attachment)
        {
            if (id != Tranche.TrancheId)
            {
                return NotFound();
            }
            if (Tranche.IsClose)
            {
                if (!Tranche.IsApproved)
                {
                    ModelState.AddModelError(nameof(Tranche.IsClose), "You should approve tranche before closing it!");
                    ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", Tranche.PaymentMethodId);
                    return View(Tranche);
                }
                else
                {
                    Tranche.IsActive = false;
                }                
            }
            if (Tranche.IsApproved)
            {
                if (attachment == null)
                {
                    ModelState.AddModelError(nameof(Tranche.ApprovedAttachment), "You should attach evidence before approving the tranche!");
                    return View(Tranche);
                }                
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (attachment != null && attachment.Length > 0)
                    {                        
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Tranche\\TId" + Tranche.TrancheId.ToString() + "\\");
                        string fileName = Path.GetFileName(attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);                        
                        Tranche.ApprovedAttachment = Path.Combine("/Documents/Tranche/TId" + Tranche.TrancheId.ToString(), fileName);//Server Path
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
                    _context.Update(Tranche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrancheExists(Tranche.TrancheId))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", Tranche.PaymentMethodId);            
            return View(Tranche);
        }

        // GET: Tranchees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tranche = await _context.Tranche
                .Include(t => t.PaymentMethod)
                .FirstOrDefaultAsync(m => m.TrancheId == id);
            if (Tranche == null)
            {
                return NotFound();
            }

            return View(Tranche);
        }

        // POST: Tranchees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Tranche = await _context.Tranche.FindAsync(id);
            _context.Tranche.Remove(Tranche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<JsonResult> GeneratePGP(int trancheDocumentId)
        {
            //string outFileServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Finance\\csvfile324.csv.pgp");
            string publicKeyFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Finance\\22D3A388A3A8EAEDB539C3890FCB241B3A6D0898.asc");
            string inFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Finance\\TrancheId2\\CSV\\ApplicantCSVFile56711.csv");

            var tranchdoc = _context.TrancheDocument.Find(trancheDocumentId);
            if (tranchdoc != null)
            {
                string outFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + tranchdoc.CSVAttachment + ".pgp");
                inFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + tranchdoc.CSVAttachment);
                bool RESULT = PGPService.GeneratePGPFile(outFilePath, inFilePath, publicKeyFilePath);
                if (RESULT)
                {
                    tranchdoc.IsPGPGenerated = true;
                    tranchdoc.PGPGeneratedOn = DateTime.Today;
                    tranchdoc.PGPAttachment = tranchdoc.CSVAttachment + ".pgp";
                    tranchdoc.PGPKey = publicKeyFilePath;

                    _context.Update(tranchdoc);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = true, message = "PGP Generated Successfully!" });
                }
            }
            return Json(new { isValid = false, message = "Failed to Generate PGP File!" });
        }
        public async Task<IActionResult> GenerateCSV(int trancheId, string startDate, string endDate, int paymentMethodModeId)
        {
            var applicants = await _context.Applicant.Include(a=>a.SchemeLevelPolicy).Include(a=>a.District).Where(a => a.TrancheId == trancheId && a.IsDisbursed == false).Select(a => new Applicant { ApplicantId = a.ApplicantId, ApplicantReferenceNo = a.ApplicantReferenceNo, Name = a.Name, BFormCNIC = a.BFormCNIC, StudentMobile = a.StudentMobile, District = new District { Name = a.District.Name}, SchemeLevelPolicy = new SchemeLevelPolicy { Amount = a.SchemeLevelPolicy.Amount } }).ToArrayAsync();
            List<CSVModel> mylist = new List<CSVModel>();
            int counter = 1;
            string updateCSV_paymentInProcess = "";
            foreach(Applicant applicant in applicants)
            {
                CSVModel student = new CSVModel();
                student.Sno = counter++;
                student.ApplicantReferenceNo = applicant.ApplicantReferenceNo;
                student.Name = applicant.Name;
                student.CNIC = applicant.BFormCNIC;
                student.StudentMobile = applicant.StudentMobile;
                student.District = applicant.District.Name;
                student.Amount = applicant.SchemeLevelPolicy.Amount;
                student.StartDate = startDate.ToString();
                student.EndDate = endDate;
                mylist.Add(student);
                updateCSV_paymentInProcess += applicant.ApplicantId + ",";
            }
            
            //Here We are calling function to write file  
            //WriteCSVFile(@"D:\Documents\NewStudentFile.csv", mylist);
            var rootPath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Finance\\TrancheId" + trancheId.ToString() + "\\CSV\\");            
            if (!System.IO.Directory.Exists(rootPath))
            {
                System.IO.Directory.CreateDirectory(rootPath);
            }
            string filename = _context.Tranche.Find(trancheId).Name /*+ "-" + RandomNo(10000, 99999).ToString()*/ + ".csv";
            bool response = WriteCSVFile(rootPath + filename, mylist);
            if (response)
            {
                TrancheDocument trancheDocument = new TrancheDocument();
                trancheDocument.CSVAttachment = Path.Combine("/Documents/Finance/TrancheId" + trancheId.ToString() + "/CSV/" + filename);//Server Path
                trancheDocument.CSVAttachmentOn = DateTime.Today;
                trancheDocument.TrancheId = trancheId;
                trancheDocument.PaymentMethodModeId = paymentMethodModeId;
                string count = (_context.TrancheDocument.Count(a => a.TrancheId == trancheId) + 1).ToString();                
                trancheDocument.TrancheDocumentName = _context.Tranche.Find(trancheId).Name + "-" + count.PadLeft(3, '0');
                _context.Add(trancheDocument);
                //------------------------------
                var currentTranche = _context.Tranche.Find(trancheId);
                currentTranche.IsDisbursementInProcess = true;
                _context.Update(currentTranche);
                //------------------------------
                _context.SaveChanges();
                var TrancheMaxId = _context.TrancheDocument.Max(a=>a.TrancheDocumentId);
                if (updateCSV_paymentInProcess != "")
                {
                    updateCSV_paymentInProcess = updateCSV_paymentInProcess.TrimEnd(',');
                    await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE [Student].[Applicant] SET [IsPaymentInProcess] = 1, [TrancheDocumentId] = { TrancheMaxId } WHERE [ApplicantId] IN( {updateCSV_paymentInProcess} )");
                }
                var MaxTranchDocumentId = _context.TrancheDocument.Max(a=>a.TrancheDocumentId);
                await GeneratePGP(MaxTranchDocumentId);
            }            
            return RedirectToAction("_Index","TrancheDocuments", new { id = trancheId});
        }
        private readonly Random _random = new Random();
        public int RandomNo(int min, int max)
        {
            return _random.Next(min, max);
        }
        public bool WriteCSVFile(string path, List<CSVModel> student)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
                using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    cw.WriteHeader<CSVModel>();
                    cw.NextRecord();
                    foreach (CSVModel stu in student)
                    {
                        cw.WriteRecord<CSVModel>(stu);
                        cw.NextRecord();
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        public async Task<JsonResult> GetTrancheName(int PaymentMethodId, string CurrentFYCode)
        {
            string count = (_context.Tranche.Count(a => a.PaymentMethodId == PaymentMethodId) + 1).ToString();
            string name = "T" + CurrentFYCode + _context.PaymentMethod.Find(PaymentMethodId).Code + count.PadLeft(3, '0');
            return Json(new { isValid = true, name = name });
        }
        private bool TrancheExists(int id)
        {           
            return _context.Tranche.Any(e => e.TrancheId == id);
        }
    }
}
