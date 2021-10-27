using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.Student;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applicant.Include(a => a.DegreeScholarshipLevel).Include(a => a.District).Include(a => a.Provience)/*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .Include(a => a.DegreeScholarshipLevel)
                .Include(a => a.District)
                .Include(a => a.Provience)
                .Include(a => a.SelectionMethod)
                /*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }
        public async Task<JsonResult> GetDistricts(int provienceId)
        {
            List<District> districts = await _context.District.Include(a=>a.Division).Where(a=>a.Division.ProvienceId == provienceId && a.IsActive == true).ToListAsync();
            var districtList = districts.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.DistrictId.ToString(),
            });
            var test = Json(districtList);
            return test;
        }
        // GET: Applicants/Create
        public IActionResult Create(int SLPId)
        {
            var genderList = new List<SelectListItem>
            {               
               new SelectListItem{ Text="Male", Value = "Male", Selected = true },
               new SelectListItem{ Text="Female", Value = "Female" },
            };
            ViewData["ddGenderList"] = genderList;
            
            ViewData["ddMethodList"] = new SelectList(_context.SelectionMethod.Where(a=>a.SelectionMethodId > 2), "SelectionMethodId", "Name");
            var provienceList = _context.Provience.ToList();            
            ViewData["DistrictId"] = new SelectList(_context.District.Where(a=>a.DistrictId == 0), "DistrictId", "Name");
            provienceList.Insert(0, new Provience { ProvienceId = 0, Name = "Select" });
            ViewData["ProvienceId"] = new SelectList(provienceList, "ProvienceId", "Name");
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");            
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name");

            Applicant applicant = new Applicant();
            applicant.SelectionStatus = "Pending";
            applicant.EntryThrough = "Manual";
            applicant.SchemeLevelPolicyId = SLPId;
            applicant.DateOfBirth = DateTime.Now.Date;
            applicant.Year = DateTime.Now.Year.ToString();            
            return View(applicant);
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Applicant applicant, IFormFile scannedDocument, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                var schemeInfo = _context.SchemeLevelPolicy.Include(a=>a.PolicySRCForum).Where(a=>a.SchemeLevelPolicyId == applicant.SchemeLevelPolicyId).FirstOrDefault();
                var currentPolicy = _context.SchemeLevelPolicy.Include(a => a.SchemeLevel.QualificationLevel).Include(a => a.PolicySRCForum.ScholarshipFiscalYear).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == schemeInfo.PolicySRCForum.ScholarshipFiscalYearId && a.PolicySRCForum.IsEndorse == true && a.SchemeLevelId == schemeInfo.SchemeLevelId).FirstOrDefault();
                var resultRepository = _context.ResultRepository.Where(a=>a.SchemeLevelPolicyId == applicant.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == currentPolicy.PolicySRCForum.ScholarshipFiscalYearId).FirstOrDefault();
                applicant.ApplicantReferenceNo = currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + (++resultRepository.currentCounter).ToString().PadLeft(4, '0');
                try
                {
                    if (scannedDocument != null && scannedDocument.Length > 0)
                    {                        
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "\\SchemeLevel" + currentPolicy.SchemeLevel.Code + "\\");
                        string fileName = Path.GetFileName(scannedDocument.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Document" + randomNumber.ToString() + fileName;
                        applicant.ScanDocument = Path.Combine("/Documents/Applicant/" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "/SchemeLevel" + currentPolicy.SchemeLevel.Code + "/" + fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await scannedDocument.CopyToAsync(stream);
                        }
                        //-----------------------------------                                                                 
                    }

                    if (picture != null && picture.Length > 0)
                    {
                        using (var dataStream = new MemoryStream())
                        {
                            await picture.CopyToAsync(dataStream);
                            applicant.Picture = dataStream.ToArray();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (true)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _context.Add(applicant);                                
                await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);
                _context.Update(resultRepository);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "ResultContainers", new { id = resultRepository.ResultRepositoryId});
            }
            var genderList = new List<SelectListItem>
            {
               new SelectListItem{ Text="Male", Value = "Male", Selected = true },
               new SelectListItem{ Text="Female", Value = "Female" },
            };
            ViewData["ddGenderList"] = genderList;
            ViewData["ddMethodList"] = new SelectList(_context.SelectionMethod, "SelectionMethodId", "Name");
            var provienceList = _context.Provience.ToList();
            ViewData["DistrictId"] = new SelectList(_context.District.Where(a => a.DistrictId == 0), "DistrictId", "Name");
            provienceList.Insert(0, new Provience { ProvienceId = 0, Name = "Select" });
            ViewData["ProvienceId"] = new SelectList(provienceList, "ProvienceId", "Name");
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name");
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id, int RRId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(applicant.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            ViewData["ddMethodList"] = new SelectList(_context.SelectionMethod, "SelectionMethodId", "Name", applicant.SelectionMethodId);
            var genderList = new List<SelectListItem>
            {
               new SelectListItem{ Text="Male", Value = "Male", Selected = true },
               new SelectListItem{ Text="Female", Value = "Female" },
            };
            ViewData["ddGenderList"] = genderList;
            var provienceList = _context.Provience.ToList();                        
            provienceList.Insert(0, new Provience { ProvienceId = 0, Name = "Select" });
            ViewData["ProvienceId"] = new SelectList(provienceList, "ProvienceId", "Name", applicant.ProvienceId);
            ViewData["DistrictId"] = new SelectList(_context.District.Include(a=>a.Division).Where(a => a.Division.ProvienceId == applicant.ProvienceId), "DistrictId", "Name", applicant.DistrictId);
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name", applicant.SchemeLevelPolicyId);
            if(applicant.Picture != null)
            {
                string imreBase64Data = Convert.ToBase64String(applicant.Picture);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view  
                ViewBag.ImageData = imgDataURL;
            }
            ViewBag.RRId = RRId;
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Applicant applicant, IFormFile scannedDocument, IFormFile scannedOtherDocument, IFormFile picture)
        {
            if (id != applicant.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var schemeInfo = _context.SchemeLevelPolicy.Include(a => a.PolicySRCForum).Where(a => a.SchemeLevelPolicyId == applicant.SchemeLevelPolicyId).FirstOrDefault();
                    var currentPolicy = _context.SchemeLevelPolicy.Include(a => a.SchemeLevel.QualificationLevel).Include(a => a.PolicySRCForum.ScholarshipFiscalYear).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == schemeInfo.PolicySRCForum.ScholarshipFiscalYearId && a.PolicySRCForum.IsEndorse == true && a.SchemeLevelId == schemeInfo.SchemeLevelId).FirstOrDefault();
                    ViewBag.RRId = _context.ResultRepository.Where(a => a.SchemeLevelPolicyId == applicant.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == currentPolicy.PolicySRCForum.ScholarshipFiscalYearId).Select(a=>a.ResultRepositoryId).FirstOrDefault();                     
                    if (scannedDocument != null && scannedDocument.Length > 0)
                        {
                            var rootPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "\\SchemeLevel" + currentPolicy.SchemeLevel.Code + "\\");
                            string fileName = Path.GetFileName(scannedDocument.FileName);
                            fileName = fileName.Replace("&", "n");
                            fileName = fileName.Replace(" ", "");
                            fileName = fileName.Replace("#", "H");
                            fileName = fileName.Replace("(", "");
                            fileName = fileName.Replace(")", "");
                            Random random = new Random();
                            int randomNumber = random.Next(1, 1000);
                            fileName = "Document" + randomNumber.ToString() + fileName;
                            applicant.ScanDocument = Path.Combine("/Documents/Applicant/" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "/SchemeLevel" + currentPolicy.SchemeLevel.Code + "/" + fileName);//Server Path
                            string sPath = Path.Combine(rootPath);
                            if (!System.IO.Directory.Exists(sPath))
                            {
                                System.IO.Directory.CreateDirectory(sPath);
                            }
                            string FullPathWithFileName = Path.Combine(sPath, fileName);
                            using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                            {
                                await scannedDocument.CopyToAsync(stream);
                            }
                            //-----------------------------------                                                                 
                        }

                    if (scannedOtherDocument != null && scannedOtherDocument.Length > 0)
                    {
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "\\SchemeLevel" + currentPolicy.SchemeLevel.Code + "\\");
                        string fileName = Path.GetFileName(scannedOtherDocument.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Document" + randomNumber.ToString() + fileName;
                        applicant.ScanOtherDocument = Path.Combine("/Documents/Applicant/" + currentPolicy.PolicySRCForum.ScholarshipFiscalYear.Code + "/SchemeLevel" + currentPolicy.SchemeLevel.Code + "/" + fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await scannedOtherDocument.CopyToAsync(stream);
                        }
                        //-----------------------------------                                                                 
                    }
                    if (picture != null && picture.Length > 0)
                        {
                            using (var dataStream = new MemoryStream())
                            {
                                await picture.CopyToAsync(dataStream);
                                applicant.Picture = dataStream.ToArray();
                            }
                        }                                     
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Message = "Data has been saved successfully!";                
            }
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(applicant.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            ViewData["ddMethodList"] = new SelectList(_context.SelectionMethod, "SelectionMethodId", "Name", applicant.SelectionMethodId);
            var genderList = new List<SelectListItem>
            {
               new SelectListItem{ Text="Male", Value = "Male", Selected = true },
               new SelectListItem{ Text="Female", Value = "Female" },
            };
            ViewData["ddGenderList"] = genderList;
            var provienceList = _context.Provience.ToList();
            provienceList.Insert(0, new Provience { ProvienceId = 0, Name = "Select" });
            ViewData["ProvienceId"] = new SelectList(provienceList, "ProvienceId", "Name", applicant.ProvienceId);
            ViewData["DistrictId"] = new SelectList(_context.District.Include(a => a.Division).Where(a => a.Division.ProvienceId == applicant.ProvienceId), "DistrictId", "Name", applicant.DistrictId);
            ViewData["DegreeScholarshipLevelId"] = new SelectList(_context.DegreeScholarshipLevel, "DegreeScholarshipLevelId", "DegreeScholarshipLevelId");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name", applicant.SchemeLevelPolicyId);
            if(applicant.Picture != null)
            {
                string imreBase64Data = Convert.ToBase64String(applicant.Picture);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view  
                ViewBag.ImageData = imgDataURL;
            }            
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .Include(a => a.DegreeScholarshipLevel)
                .Include(a => a.District)
                .Include(a => a.Provience)
                /*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            _context.Applicant.Remove(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.ApplicantId == id);
        }
    }
    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
