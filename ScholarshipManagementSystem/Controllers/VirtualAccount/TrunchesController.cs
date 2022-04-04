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
        public async Task<IActionResult> Index(bool w, bool x, bool y, bool z, string name)
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a=>a.IsActive == w && a.IsClose == y && a.IsOpen == z);
            if (x)
            {
                applicationDbContext = applicationDbContext.Where(a=>a.IsApproved == x);
            }
            ViewBag.heading = name;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {
            var applicationDbContext = _context.Tranche.Include(t => t.PaymentMethod).Where(a=>a.IsActive == true && a.IsOpen == true && a.IsLock == false);
            return PartialView(await applicationDbContext.ToListAsync());
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

        public async Task<IActionResult> GenerateCSV(int trancheId, string startDate, string endDate)
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
            string filename = "ApplicantCSVFile" + RandomNo(10000, 99999).ToString() + ".csv";
            bool response = WriteCSVFile(rootPath + filename, mylist);
            if (response)
            {
                TrancheDocument trancheDocument = new TrancheDocument();
                trancheDocument.CSVAttachment = Path.Combine("/Documents/Finance/TrancheId" + trancheId.ToString() + "/CSV/" + filename);//Server Path
                trancheDocument.CSVAttachmentOn = DateTime.Today;
                trancheDocument.TrancheId = trancheId;
                _context.Add(trancheDocument);
                _context.SaveChanges();
                var TrancheMaxId = _context.TrancheDocument.Max(a=>a.TrancheDocumentId);
                if (updateCSV_paymentInProcess != "")
                {
                    updateCSV_paymentInProcess = updateCSV_paymentInProcess.TrimEnd(',');
                    await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE [Student].[Applicant] SET [IsPaymentInProcess] = 1, [TrancheDocumentId] = { TrancheMaxId } WHERE [ApplicantId] IN( {updateCSV_paymentInProcess} )");
                }
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
        private bool TrancheExists(int id)
        {           
            return _context.Tranche.Any(e => e.TrancheId == id);
        }
    }
}
