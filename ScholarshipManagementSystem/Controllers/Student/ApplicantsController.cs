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
using Microsoft.AspNetCore.Identity;
using DAL.Models;
using DAL.Models.ViewModels.ApplicantInProcess;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {            
            var applicationDbContext = _context.Applicant.Include(a => a.DegreeScholarshipLevel).Include(a => a.District).Include(a => a.Provience)/*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> GetApplicantListInTranche(int id)
        {
            var applicationDbContext = _context.Applicant.Include(a => a.SchemeLevelPolicy.SchemeLevel).Where(a => a.TrancheId == id).Select(a=> new Applicant { ApplicantReferenceNo = a.ApplicantReferenceNo, RollNumber = a.RollNumber, Name = a.Name, SchemeLevelPolicy = new SchemeLevelPolicy { SchemeLevelPolicyId = a.SchemeLevelPolicyId, SchemeLevel = new SchemeLevel { Name = a.SchemeLevelPolicy.SchemeLevel.Name } }});/*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> GetApplicantListInTrancheForDisbursement(int id)
        {
            ViewBag.TrancheId = id;
            var applicationDbContext = _context.Applicant.Include(a => a.SchemeLevelPolicy.SchemeLevel).Where(a => a.TrancheId == id && a.IsDisbursed == false && a.IsPaymentInProcess == false).Select(a => new Applicant { ApplicantReferenceNo = a.ApplicantReferenceNo, RollNumber = a.RollNumber, Name = a.Name, SchemeLevelPolicy = new SchemeLevelPolicy { SchemeLevelPolicyId = a.SchemeLevelPolicyId, SchemeLevel = new SchemeLevel { Name = a.SchemeLevelPolicy.SchemeLevel.Name } } });/*.Include(a => a.SchemeLevelPolicy.SchemeLevel)*/;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> GetWaitingResultList(int MaxFYId, int applicantCurrentStatusId, int SchemeLevelId)
        {
            var applicationDbContext = await _context.SPApplicantWaiting.FromSqlRaw("exec [Student].[ApplicantWaiting] {0}, {1}, {2}", applicantCurrentStatusId, MaxFYId, SchemeLevelId).ToListAsync();           
            return PartialView(applicationDbContext);
        }
        public async Task<IActionResult> GetRejectedResultList(int MaxFYId, int applicantCurrentStatusId, int SchemeLevelId)
        {
            var applicationDbContext = await _context.SPApplicantRejected.FromSqlRaw("exec [Student].[ApplicantRejected] {0}, {1}, {2}", applicantCurrentStatusId, MaxFYId, SchemeLevelId).ToListAsync();
            return PartialView(applicationDbContext);
        }
        public async Task<IActionResult> GetResultList(int MaxFYId, int applicantCurrentStatusId, int SchemeLevelId)
        {                                
            var applicationDbContext = await _context.SPApplicantInProcess.FromSqlRaw("exec [Student].[ApplicantInProcess] {0}, {1}, {2}", applicantCurrentStatusId, MaxFYId, SchemeLevelId).ToListAsync();
            /*ParentApplicantInProcess mymodel = new ParentApplicantInProcess();
            mymodel.SPApplicantInProcessList = applicationDbContext;
            mymodel.SPApplicantInProcessSummaryList = await _context.SPApplicantInProcessSummary.FromSqlRaw("exec [Student].[ApplicantInProcessSummary] {0}, {1}", applicantCurrentStatusId, MaxFYId).ToListAsync(); ;*/
            //-----------------------------------
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);          
            ViewBag.UserCurrentAccess = currentUser.ApplicantCurrentStatusId;
            ViewData["SeverityLevelId"] = _context.SeverityLevel;
            var SectionCommentList = _context.SectionComment.Where(a => a.BEEFSectionId == currentUser.ApplicantCurrentStatusId)
                .Select(s => new
                {
                    SectionCommentId = s.SeverityLevelId,
                    Description = string.Format("{0}", s.Comment)
                })
                .ToList();
            ViewData["SectionCommentId"] = new SelectList(SectionCommentList, "SectionCommentId", "Description");
            ViewData["UserAccessToForwardId"] = new SelectList(_context.userAccessToForward.Include(a => a.ApplicantCurrentStatus).Where(a => a.UserId == currentUser.Id), "UserAccessToForwardId", "ApplicantCurrentStatus.ProcessState");
            //-----------------------------------
            return PartialView(applicationDbContext);
        }
        public async Task<IActionResult> ApplicantInProcess()
        {
            int MaxFYId = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Max(a => a.ScholarshipFiscalYearId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);            
            int applicantCurrentStatusId = currentUser.ApplicantCurrentStatusId;            
            ViewBag.MaxFYId = MaxFYId;
            ViewBag.applicantCurrentStatusId = applicantCurrentStatusId;
            ViewBag.SchemeLevelId = _context.UserAccessToSchemeLevel.Where(a=>a.UserId == currentUser.Id).Select(a=>a.SchemeLevelId).FirstOrDefault();

            var applicationDbContext = await _context.SPApplicantInProcessSummary.FromSqlRaw("exec [Student].[ApplicantInProcessSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            ViewBag.TotalCases = applicationDbContext.Sum(a=>a.Applicant);            
            return View(applicationDbContext);
        }
        public async Task<IActionResult> ApplicantRejected()
        {
            int MaxFYId = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Max(a => a.ScholarshipFiscalYearId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            int applicantCurrentStatusId = currentUser.ApplicantCurrentStatusId;
            ViewBag.MaxFYId = MaxFYId;
            ViewBag.applicantCurrentStatusId = applicantCurrentStatusId;
            ViewBag.SchemeLevelId = _context.UserAccessToSchemeLevel.Where(a => a.UserId == currentUser.Id).Select(a => a.SchemeLevelId).FirstOrDefault();

            var applicationDbContext = await _context.SPApplicantRejectedSummary.FromSqlRaw("exec [Student].[ApplicantRejectedSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            ViewBag.TotalCases = applicationDbContext.Sum(a => a.Applicant);
            return View(applicationDbContext);
        }
        public async Task<IActionResult> ApplicantWaiting()
        {
            int MaxFYId = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Max(a => a.ScholarshipFiscalYearId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            int applicantCurrentStatusId = currentUser.ApplicantCurrentStatusId;
            ViewBag.MaxFYId = MaxFYId;
            ViewBag.applicantCurrentStatusId = applicantCurrentStatusId;
            ViewBag.SchemeLevelId = _context.UserAccessToSchemeLevel.Where(a => a.UserId == currentUser.Id).Select(a => a.SchemeLevelId).FirstOrDefault();

            var applicationDbContext = await _context.SPApplicantWaitingSummary.FromSqlRaw("exec [Student].[ApplicantWaitingSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
            ViewBag.TotalCases = applicationDbContext.Sum(a => a.Applicant);
            return View(applicationDbContext);
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
        public async Task<IActionResult> ApplicantTracking(string RefId)
        {            
            if(RefId != null)
            {
                ViewBag.RefId = RefId;
            }
            var currentUserId = await _userManager.GetUserAsync(HttpContext.User);
            //var CurrentUser = await _userManager.Users.Where(a => a.Id == currentUserId.Id).FirstOrDefaultAsync();
            ViewBag.UserCurrentAccess = currentUserId.ApplicantCurrentStatusId;
            ViewData["SeverityLevelId"] = _context.SeverityLevel;
            var SectionCommentList = _context.SectionComment.Include(a=>a.SeverityLevel).Where(a=>a.BEEFSectionId == currentUserId.BEEFSectionId)
                .Select(s => new
                {
                    SectionCommentId = s.SeverityLevelId,
                    Description = string.Format("{0} - Severity {1}", s.Comment, s.SeverityLevel.Meaning)
                })
                .ToList();            
            ViewData["SectionCommentId"] = new SelectList(SectionCommentList, "SectionCommentId", "Description");
            //ViewData["SectionCommentId"] = new SelectList(_context.SectionComment, "SectionCommentId", "Comment");
            ViewData["UserAccessToForwardId"] = new SelectList(_context.userAccessToForward.Include(a=>a.ApplicantCurrentStatus).Where(a=>a.UserId == currentUserId.Id), "UserAccessToForwardId", "ApplicantCurrentStatus.ProcessState");
            ViewBag.IsInRoleReject = false;
            var IsInRoleReject = await _userManager.IsInRoleAsync(currentUserId, "Reject");
            if(IsInRoleReject)
            {
                ViewBag.IsInRoleReject = true;
            }
            ViewBag.IsInRoleResume = false;
            var IsInRoleResume = await _userManager.IsInRoleAsync(currentUserId, "Resume");
            if (IsInRoleResume)
            {
                var IsRejected = _context.Applicant.Count(a => a.ApplicantReferenceNo == RefId && a.ApplicantSelectionStatusId == 4);
                if(IsRejected > 0)
                {
                    ViewBag.IsInRoleResume = true;
                }                
            }
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
                ApplicantCurrentStatusId = a.ApplicantCurrentStatusId,                
                ApplicantFinanceCurrentStatusId = a.ApplicantFinanceCurrentStatusId,                
                FormSubmittedOnDate = a.FormSubmittedOnDate,                
                ApplicantSelectionStatusId = a.ApplicantSelectionStatusId,                
                BFormCNIC = a.Picture == null ? "" : string.Format("data:image/png;base64,{0}", Convert.ToBase64String(a.Picture)),                
                ApplicantId = a.ApplicantId
            }).FirstOrDefault();
            if(applicantInfo != null)
            {
                QRCodeGenerator QrGenerator = new QRCodeGenerator();
                QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(applicantInfo.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
                QRCode QrCode = new QRCode(QrCodeInfo);
                Bitmap QrBitmap = QrCode.GetGraphic(60);
                byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                applicantInfo.Year = QrUri;
            }            
            return Json(applicantInfo);
        }
        public IActionResult ReloadApplicantProfileData(int id, int userCurrentAccess)
        {
            return ViewComponent("ApplicantProfileData", new { id, userCurrentAccess});
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
        public async Task<JsonResult> ResumeApplicant(int applicantId)
        {
            var applicantInfo = _context.Applicant.Find(applicantId);
            if (applicantInfo != null)
            {
                applicantInfo.ApplicantSelectionStatusId = 1;
                applicantInfo.ApplicantCurrentStatusId = 4;
                applicantInfo.SelectionStatus = "Selected";
                _context.Update(applicantInfo);
                await _context.SaveChangesAsync();
                //-----------------------------------------------------------
            }
            else
            {
                return Json(new { isValid = false, message = "Failed to Resume!" });
            }
            return Json(new { isValid = true, message = "Applicant Resumed Successfully!" });
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
                //mobileNo = "03327822567";
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
            model.IsFormEntered = false;            
            model.ApplicantCurrentStatusId = 1;            
            model.EntryThrough = "Manual";
            model.SelectionStatus = "Pending";
            model.ApplicantSelectionStatusId = 3;
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
            applicant.ApplicantSelectionStatusId = 3;
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
                    applicant.ApplicantCurrentStatusId = 4;//KDA Hard
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
                    applicant.ApplicantCurrentStatusId = 4;//KDA Hard
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicantFormEditFromProfile(Applicant applicant)
        {          
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(applicant);
                    var oldResult = await _context.Applicant.FindAsync(applicant.ApplicantId);
                    _context.Entry(oldResult).CurrentValues.SetValues(applicant);
                    await _context.SaveChangesAsync(User?.FindFirst(ClaimTypes.NameIdentifier).Value);                    
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
            }
            return RedirectToAction(nameof(ApplicantTracking), new { RefId = applicant.ApplicantReferenceNo });
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
