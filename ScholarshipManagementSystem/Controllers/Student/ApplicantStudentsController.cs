using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.Student;
using Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using DAL.Models;
using System.Security.Claims;

namespace ScholarshipManagementSystem.Controllers.Student
{
    public class ApplicantStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostEnvironment hostingEnv;

        public ApplicantStudentsController(ApplicationDbContext context, IHostEnvironment env, UserManager<ApplicationUser> userManager)
        {            
            _userManager = userManager;        
            this.hostingEnv = env;
            _context = context;
        }

        // GET: ApplicantStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicantStudent.Include(a => a.Applicant);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> _Index(int id)
        {            
            var applicationDbContext = _context.ApplicantStudent.Include(a=>a.ApplicationUserFrom.BEEFSection).Include(a=>a.ApplicationUserTo.BEEFSection).Where(a=>a.ApplicantId == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        public ActionResult DisplayPDF(int id)
        {
            byte[] byteArray = _context.ApplicantStudent.Find(8).AttachFileData;
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
        // GET: ApplicantStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent
                .Include(a => a.Applicant)                
                .FirstOrDefaultAsync(m => m.ApplicantStudentId == id);
            if (applicantStudent == null)
            {
                return NotFound();
            }

            return View(applicantStudent);
        }
                
        // GET: ApplicantStudents/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Name");
            return View();
        }

        // POST: ApplicantStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeverityLevelId,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicantStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);            
            return View(applicantStudent);
        }

        // GET: ApplicantStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent.FindAsync(id);
            if (applicantStudent == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);            
            return View(applicantStudent);
        }

        // POST: ApplicantStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantStudentId,ApplicantId,SelectionStatus,ApplicantReferenceId,SeverityLevelId,Comments,Attachment,EmployeeId,UserName")] ApplicantStudent applicantStudent)
        {
            if (id != applicantStudent.ApplicantStudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantStudentExists(applicantStudent.ApplicantStudentId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", applicantStudent.ApplicantId);            
            return View(applicantStudent);
        }

        // GET: ApplicantStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStudent = await _context.ApplicantStudent
                .Include(a => a.Applicant)                
                .FirstOrDefaultAsync(m => m.ApplicantStudentId == id);
            if (applicantStudent == null)
            {
                return NotFound();
            }

            return View(applicantStudent);
        }

        // POST: ApplicantStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicantStudent = await _context.ApplicantStudent.FindAsync(id);
            _context.ApplicantStudent.Remove(applicantStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantStudentExists(int id)
        {
            return _context.ApplicantStudent.Any(e => e.ApplicantStudentId == id);
        }

        [HttpPost]
        public async Task<string> SubmitComment(int applicantId, string applicantRefNo, string comment,int ForwardApplicantCurrentStatusId, string UserId, int userCurrentAccess, IFormFile Attachment)
        {
            var applicantInfo = await _context.Applicant.FindAsync(applicantId);           
            if (!(applicantId == 0 || applicantRefNo == "" || comment == "" || UserId == ""))
            {                
                ApplicantStudent obj = new ApplicantStudent();
                obj.ApplicantId = applicantId;
                obj.ApplicantReferenceId = applicantRefNo;
                obj.Comments = comment;
                obj.CreatedOn = DateTime.Now;                
                obj.FromUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;                
                obj.UserName = User.Identity.Name;                              
                var forwardTo = ForwardApplicantCurrentStatusId;
                obj.ToUserId = UserId;
                var ForwardUserInfo = _context.Users.Find(UserId);
                obj.ForwardToUserName = ForwardUserInfo.FirstName + " " + ForwardUserInfo.LastName;
                applicantInfo.ApplicantInboxId = 1;
                if (applicantInfo.ApplicantCurrentStatusId > forwardTo)
                {
                    applicantInfo.ApplicantInboxId = 2;
                }                                
                applicantInfo.ApplicantCurrentStatusId = forwardTo;               
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
                        using (var target = new MemoryStream())
                        {
                            Attachment.CopyTo(target);
                            obj.AttachFileData = target.ToArray();
                        }
                        var rootPath = Path.Combine(
                          Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\Comments\\ID" + applicantId + "\\");                        
                        fileName = fileName.Replace("&", "n"); fileName = fileName.Replace(" ", ""); fileName = fileName.Replace("#", "H"); fileName = fileName.Replace("(", ""); fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Document" + randomNumber.ToString() + fileName;
                        obj.Attachment = Path.Combine("/Documents/Applicant/Comments/ID" + applicantId + "/" + fileName);//Server Path
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
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                        obj.AttachFileName = newFileName;
                        obj.AttachFileType = fileExtension;
                    }
                }
                //-------------------------------------------------------
                _context.Add(obj);                
                if (userCurrentAccess == 15)//KDA Hard
                {
                    if(applicantInfo.TrancheId != null)
                    {
                        var applicantPolicyInfo = _context.SchemeLevelPolicy.Include(a => a.SchemeLevel).Where(a => a.SchemeLevelPolicyId == applicantInfo.SchemeLevelPolicyId).FirstOrDefault();
                        var applicantPaymentMethodId = applicantPolicyInfo.SchemeLevel.PaymentMethodId;
                        if (applicantPaymentMethodId == 0)
                        {
                            return "Failed, Tranche not available so far!";
                        }
                        var trancheId = _context.Tranche.Where(a => a.IsActive == true && a.IsOpen == true && a.PaymentMethodId == applicantPaymentMethodId).Select(a => a.TrancheId).FirstOrDefault();
                        applicantInfo.TrancheId = trancheId;
                        var currentTranche = _context.Tranche.Find(trancheId);
                        currentTranche.ApplicantCount++;
                        currentTranche.CurrentCommittedAmount += applicantPolicyInfo.Amount;
                        _context.Update(currentTranche);
                    }                    
                }
                _context.Update(applicantInfo);
                await _context.SaveChangesAsync();
                return "Uploaded Successfully!";
            }
            return "Failed to Upload!";
        }


        /*[HttpPost]
        public async Task<string> SubmitCommentInBulk(string applicantIdList, string comment, int severityLevelId, int userAccessToForwardId, int userCurrentAccess, IFormFile Attachment)
        {
            

            List<int> candidates = applicantIdList.Split(',').Select(x => int.Parse(x)).ToList();
            foreach (var applicantId in candidates)
            {
                var applicantInfo = _context.Applicant.Find(applicantId);
                if (applicantInfo.ApplicantReferenceNo != "" && comment != "" && userAccessToForwardId != 0)
                {
                    ApplicantStudent obj = new ApplicantStudent();
                    obj.ApplicantId = applicantId;
                    obj.ApplicantReferenceId = applicantInfo.ApplicantReferenceNo;
                    obj.Comments = comment;
                    obj.CreatedOn = DateTime.Now;                    
                    obj.ApplicantCurrentStatusId = userCurrentAccess;
                    obj.UserName = User.Identity.Name;                    
                    obj.ForwardToUserName = _userManager.Users.FirstOrDefault(a => a.ApplicantCurrentStatusId == _context.userAccessToForward.Find(obj.UserAccessToForwardId).ApplicantCurrentStatusId).FirstName;
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
                            using (var target = new MemoryStream())
                            {
                                Attachment.CopyTo(target);
                                obj.AttachFileData = target.ToArray();
                            }
                            var rootPath = Path.Combine(
                              Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Applicant\\Comments\\ID" + applicantId + "\\");
                            fileName = fileName.Replace("&", "n"); fileName = fileName.Replace(" ", ""); fileName = fileName.Replace("#", "H"); fileName = fileName.Replace("(", ""); fileName = fileName.Replace(")", "");
                            Random random = new Random();
                            int randomNumber = random.Next(1, 1000);
                            fileName = "Document" + randomNumber.ToString() + fileName;
                            obj.Attachment = Path.Combine("/Documents/Applicant/Comments/ID" + applicantId + "/" + fileName);//Server Path
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
                            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                            obj.AttachFileName = newFileName;
                            obj.AttachFileType = fileExtension;
                        }
                    }
                    //-------------------------------------------------------
                    _context.Add(obj);
                    applicantInfo.ApplicantCurrentStatusId = _context.userAccessToForward.Find(userAccessToForwardId).ApplicantCurrentStatusId;
                    _context.Update(applicantInfo);
                }
                else
                {
                    return "Failed to Upload!";
                }             
            }
            await _context.SaveChangesAsync();
            return "Uploaded Successfully!";            
        }*/
    }
}
