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
using ScholarshipManagementSystem.Common;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.Student;
using SMSService.Models.Domain.AutoSMSApi;
using DAL.Models.Domain.ScholarshipSetup;

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
        public IActionResult CollectForm(string id)
        {
            ViewData["SelectedMethodId"] = new SelectList(_context.SelectionMethod.Where(a => a.SelectionMethodId > 2), "SelectionMethodId", "Name");           
            ViewData["DistrictId"] = new SelectList(_context.District.Where(a => a.Division.ProvienceId == 1), "DistrictId", "Name");
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel).Where(a=>a.PolicySRCForum.ScholarshipFiscalYearId == 0), "SchemeLevelPolicyId", "SchemeLevel.Name");                        
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Name");                        
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a => a.SchemeLevel).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == 0).Select(a => new SchemeLevel { SchemeLevelId = a.SchemeLevelId, Name = a.SchemeLevel.Name }).ToList(), "SchemeLevelId", "Name");
            ViewBag.refId = id;
            return View();
        }
        public IActionResult ApplicantTracking()
        {           
            return View();
        }
        public IActionResult ApplicantFormEntry(string message)
        {
            ViewBag.message = message;
            return View();
        }
        public IActionResult ApplicantFileAttachment()
        {            
            return View();
        }
        public IActionResult ApplicantSendSMS()
        {            
            return View();
        }
        [HttpPost]
        public JsonResult AjaxApplicantInformation(string refno)
        {
            var applicantInfo = _context.Applicant.Include(a=>a.SelectionMethod).Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ApplicantReferenceNo == refno).Select(a=> new Applicant { 
                Name = a.Name,
                FatherName = a.FatherName,
                RollNumber = a.RollNumber,
                ApplicantReferenceNo = a.ApplicantReferenceNo,
                HomeAddress = a.SchemeLevelPolicy.SchemeLevel.Name,
                SelectionMethodId = a.SelectionMethodId,
                Religion = a.SelectionMethod.Name,
                IsFormSubmitted = a.IsFormSubmitted,
                IsFormEntered = a.IsFormEntered,
                Attach_Picture = a.Attach_Picture,                
                Attach_Affidavit = a.Attach_Affidavit,
                Attach_CNIC_BForm = a.Attach_CNIC_BForm,
                Attach_DMC_Transcript = a.Attach_DMC_Transcript,
                Attach_Father_Death_Certificate = a.Attach_Father_Death_Certificate,
                Attach_Father_Mother_Guardian_CNIC = a.Attach_Father_Mother_Guardian_CNIC,
                Attach_Minority_Certificate = a.Attach_Minority_Certificate,
                Attach_Payslip = a.Attach_Payslip,                
                StudentMobile = a.StudentMobile,                
                FormSubmittedOnDate = a.FormSubmittedOnDate,                
                BFormCNIC = a.Picture == null ? "" : string.Format("data:image/png;base64,{0}", Convert.ToBase64String(a.Picture)),
                ApplicantId = a.ApplicantId
            }).FirstOrDefault();
            return Json(applicantInfo);
        }
        public IActionResult ReloadApplicantProfileData(int id)
        {
            return ViewComponent("ApplicantProfileData", new { id});
        }
        public async Task<JsonResult> AjaxApplicantSubmit(int applicantId, bool fourPicture, bool dmc, bool cnic, bool guardiancnic, bool paySlip, bool deathCertificate, bool affidavit, bool minorityCertificate, string mobileNo)
        {
            var applicantInfo = await _context.Applicant.FindAsync(applicantId);
            if(applicantInfo != null)
            {
                applicantInfo.Attach_Picture = fourPicture;
                applicantInfo.Attach_DMC_Transcript = dmc;
                applicantInfo.Attach_CNIC_BForm = cnic;
                applicantInfo.Attach_Father_Mother_Guardian_CNIC = guardiancnic;
                if(applicantInfo.SelectionMethodId > 2)
                {
                    applicantInfo.Attach_Payslip = paySlip;
                    applicantInfo.Attach_Affidavit = affidavit;
                    applicantInfo.Attach_Minority_Certificate = minorityCertificate;
                    applicantInfo.Attach_Father_Death_Certificate = deathCertificate;
                }
                applicantInfo.IsFormSubmitted = true;
                applicantInfo.FormSubmittedOnDate = DateTime.Now;
                applicantInfo.StudentMobile2 = mobileNo;
                _context.Update(applicantInfo);
                await _context.SaveChangesAsync();
                //--------------------SMS Alert------------------------------
                SMSAPIService ConfigObj = new SMSAPIService();
                ConfigObj = _context.SMSAPIService.Find(1);
                SMSAPI SMSObj = new SMSAPI(ConfigObj.Username, ConfigObj.Password, ConfigObj.Mask, ConfigObj.SendSMSURL);                
                var Text = "Dear "+ applicantInfo.Name +",\nWe have successfully received your application. Your reference ID is "+ applicantInfo.ApplicantReferenceNo +" and Receipt ID is "+ applicantInfo.ApplicantId +". Keep visit our website www.beef.org.pk for updates.Thanks\nBEEF.";
                string contactNo = "92" + (mobileNo.Remove(0, 1));
                var response = SMSObj.SendSingleSMS(Text, contactNo, "English");
                SMSAPIServiceAuditTrail SMSRecord = new SMSAPIServiceAuditTrail();
                SMSRecord.DestinationNumber = mobileNo;
                SMSRecord.Language = "English";
                SMSRecord.ResponseMessage = response;
                SMSRecord.ResponseType = "Text";
                SMSRecord.SendOn = DateTime.Now;
                SMSRecord.Text = Text;
                SMSRecord.TextLength = Text.Length;
                SMSRecord.MessageFor = DAL.Enums.MessageFor.Employee.ToString();
                SMSRecord.UserId = User.Identity.Name;
                SMSRecord.SendBy = "System";
                SMSRecord.ReferenceId = applicantInfo.ApplicantReferenceNo;
                SMSRecord.ApplicantId = applicantInfo.ApplicantId;
                _context.Add(SMSRecord);
                await _context.SaveChangesAsync();
                //-----------------------------------------------------------
            }
            else
            {
                return Json(new { isValid = false });
            }
            return Json(new { isValid = true});
        }       
        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile files, int applicantId, string title)
        {            
            if (files.Length > 0)
            {
                ApplicantAttachment obj = new ApplicantAttachment();
                obj.ApplicantId = applicantId;
                obj.Title = title;
                obj.UploadedOn = DateTime.Now;
                var rootPath = Path.Combine(
                          Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\Attachments\\ID" + applicantId + "\\");
                string fileName = Path.GetFileName(files.FileName);
                fileName = fileName.Replace("&", "n");fileName = fileName.Replace(" ", "");fileName = fileName.Replace("#", "H");fileName = fileName.Replace("(", "");fileName = fileName.Replace(")", "");
                Random random = new Random();
                int randomNumber = random.Next(1, 1000);
                fileName = "Document" + randomNumber.ToString() + fileName;
                obj.AttachmentPath = Path.Combine("/Documents/Applicant/Attachments/ID" + applicantId + "/" + fileName);//Server Path
                string sPath = Path.Combine(rootPath);
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                string FullPathWithFileName = Path.Combine(sPath, fileName);
                using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }            
            return Json(new { isValid = true });
        }
        public async Task<JsonResult> SendSMS(string mobileNo, string name, string message, string applicantReferenceNo, int applicantId)
        {            
            if (mobileNo != null)
            {
                mobileNo = "03327822567";
                //--------------------SMS Alert------------------------------
                SMSAPIService ConfigObj = new SMSAPIService();
                ConfigObj = _context.SMSAPIService.Find(1);
                SMSAPI SMSObj = new SMSAPI(ConfigObj.Username, ConfigObj.Password, ConfigObj.Mask, ConfigObj.SendSMSURL);
                var Text = "Dear " + name + ",\n" + message + "\nBEEF.";
                string contactNo = "92" + (mobileNo.Remove(0, 1));
                var response = SMSObj.SendSingleSMS(Text, contactNo, "English");
                SMSAPIServiceAuditTrail SMSRecord = new SMSAPIServiceAuditTrail();
                SMSRecord.DestinationNumber = mobileNo;
                SMSRecord.Language = "English";
                SMSRecord.ResponseMessage = response;
                SMSRecord.ResponseType = "Text";
                SMSRecord.SendOn = DateTime.Now;
                SMSRecord.Text = Text;
                SMSRecord.TextLength = Text.Length;
                SMSRecord.MessageFor = DAL.Enums.MessageFor.Employee.ToString();
                SMSRecord.UserId = User.Identity.Name;
                SMSRecord.SendBy = "System";
                SMSRecord.ReferenceId = applicantReferenceNo;
                SMSRecord.ApplicantId = applicantId;
                _context.Add(SMSRecord);
                await _context.SaveChangesAsync();
                //-----------------------------------------------------------
            }
            else
            {
                return Json(new { isValid = false });
            }
            return Json(new { isValid = true });
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
        public async Task<JsonResult> GetSchemeLevels(int FYId)
        {
            List<SchemeLevel> schemelevels = await _context.SchemeLevelPolicy.Include(a=>a.PolicySRCForum).Include(a => a.SchemeLevel).Where(a => a.PolicySRCForum.ScholarshipFiscalYearId == FYId).Select(a=> new SchemeLevel {SchemeLevelId = a.SchemeLevelPolicyId, Name = a.SchemeLevel.Name }).ToListAsync();
            var schemelevelList = schemelevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.SchemeLevelId.ToString()                
            });// KDA Hard SchemeLevelId = SchemeLevelPolicyId
            return Json(schemelevelList);            
        }       
        [ValidateAntiForgeryToken]
        [HttpPost]        
        public async Task<IActionResult> CollectForm(Applicant model, int ScholarshipFiscalYearId, int SchemeLevelPolicyId)
        {
            model.IsFormSubmitted = false;            
            model.EntryThrough = "Manual";
            model.SelectionStatus = "Pending";
            model.ProvienceId = 1;            
            var currentPolicy = await _context.SchemeLevelPolicy.Include(a=>a.SchemeLevel.QualificationLevel).Where(a=>a.SchemeLevelPolicyId == SchemeLevelPolicyId).FirstOrDefaultAsync();
            var resultRepository = await _context.ResultRepository.Where(a => a.SchemeLevelPolicyId == model.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == ScholarshipFiscalYearId).FirstOrDefaultAsync();
            model.ApplicantReferenceNo = _context.ScholarshipFiscalYear.FindAsync(ScholarshipFiscalYearId).Result.Code + currentPolicy.SchemeLevel.QualificationLevel.Code + currentPolicy.SchemeLevel.Code + (++resultRepository.currentCounter).ToString().PadLeft(4, '0');
            _context.Applicant.Add(model);
            _context.Update(resultRepository);
            await _context.SaveChangesAsync();                               
            return RedirectToAction(nameof(CollectForm), new { id = model.ApplicantReferenceNo });
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
                ApplicantAttachment applicantAttachment = new ApplicantAttachment();
                applicantAttachment.ApplicantId = applicant.ApplicantId;
                applicantAttachment.AttachmentPath = applicant.ScanDocument;
                applicantAttachment.Title = "Scanned Documents";
                applicantAttachment.UploadedOn = DateTime.Now.Date;
                _context.Add(applicantAttachment);
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
        public async Task<IActionResult> Edit(int? id, int? RRId)
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
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode("https://beef.org.pk/wp-content/uploads/2021/10/QRCodeBEEFForm.pdf?" + applicant.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
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
            applicant.DateOfBirth = DateTime.Now.Date;
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
                        //-----------------------------------   First time
                        ApplicantAttachment applicantAttachment = new ApplicantAttachment();
                        applicantAttachment.ApplicantId = applicant.ApplicantId;
                        applicantAttachment.AttachmentPath = applicant.ScanDocument;
                        applicantAttachment.Title = "Scanned Documents";
                        applicantAttachment.UploadedOn = DateTime.Now.Date;
                        _context.Add(applicantAttachment);
                        await _context.SaveChangesAsync();
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

        public async Task<IActionResult> ApplicantFormEdit(int? id)
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
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode("https://beef.org.pk/wp-content/uploads/2021/10/QRCodeBEEFForm.pdf?" + applicant.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
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
            if (applicant.Picture != null)
            {
                string imreBase64Data = Convert.ToBase64String(applicant.Picture);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view  
                ViewBag.ImageData = imgDataURL;
            }            
            applicant.DateOfBirth = DateTime.Now.Date;
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicantFormEdit(int id, Applicant applicant, IFormFile scannedDocument, IFormFile scannedOtherDocument, IFormFile picture)
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
                    ViewBag.RRId = _context.ResultRepository.Where(a => a.SchemeLevelPolicyId == applicant.SchemeLevelPolicyId && a.ScholarshipFiscalYearId == currentPolicy.PolicySRCForum.ScholarshipFiscalYearId).Select(a => a.ResultRepositoryId).FirstOrDefault();
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
                        //-----------------------------------   First time
                        ApplicantAttachment applicantAttachment = new ApplicantAttachment();
                        applicantAttachment.ApplicantId = applicant.ApplicantId;
                        applicantAttachment.AttachmentPath = applicant.ScanDocument;
                        applicantAttachment.Title = "Scanned Documents";
                        applicantAttachment.UploadedOn = DateTime.Now.Date;
                        _context.Add(applicantAttachment);
                        await _context.SaveChangesAsync();
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
                    applicant.IsFormEntered = true;
                    applicant.ApplicantCurrentStatusId = 2;
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
                return RedirectToAction(nameof(ApplicantFormEntry), new { message = "Data has been saved successfully!" });
            }            
            return RedirectToAction(nameof(ApplicantFormEntry), new { message = "Invalid Input!" });
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
