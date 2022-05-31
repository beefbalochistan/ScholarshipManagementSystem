using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DAL.Models;
using System.Net.Mail;
using Repository.Data;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Models;
using ScholarshipManagementSystem.Common;

namespace ScholarshipManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly reCaptchaService _reCaptchaService;       
        public LoginModel(SignInManager<ApplicationUser> signInManager, reCaptchaService reCaptchaService,
            ILogger<LoginModel> logger,            
        UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _reCaptchaService = reCaptchaService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email / Username")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
            [Required]
            public string token { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();            
            ReturnUrl = returnUrl;
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            //google reCaptcha confirmation
            var reCaptcharesult = _reCaptchaService.tokenVerify(Input.token);
            if (!reCaptcharesult.Result.success && reCaptcharesult.Result.score <= 0.5)
            {
                ModelState.AddModelError(string.Empty, "You are not a human.");
                return Page();

            }
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        userName = user.UserName;
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser currentUser;
                    bool emailStatus = false;
                    if (IsValidEmail(Input.Email))
                    {
                        emailStatus = await _userManager.IsEmailConfirmedAsync(await _userManager.FindByEmailAsync(Input.Email));
                        currentUser = await _userManager.FindByEmailAsync(Input.Email);
                    }
                    else
                    {                        
                        emailStatus = await _userManager.IsEmailConfirmedAsync(await _userManager.FindByNameAsync(Input.Email));
                        currentUser = await _userManager.FindByNameAsync(Input.Email);
                    }

                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(Input.Email), "Email is unconfirmed, please confirm it first");
                        return Page();
                    }
                    int MaxFYId = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Max(a => a.ScholarshipFiscalYearId);                    
                    int applicantCurrentStatusId = currentUser.ApplicantCurrentStatusId;
                    /*var InProcessSummary = await _context.SPApplicantInProcessSummary.FromSqlRaw("exec [Student].[ApplicantInProcessSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
                    var RejectedSummary = await _context.SPApplicantRejectedSummary.FromSqlRaw("exec [Student].[ApplicantRejectedSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
                    var WaitingSummary = await _context.SPApplicantWaitingSummary.FromSqlRaw("exec [Student].[ApplicantWaitingSummarySchemeLevelWise] {0}, {1},  {2}", applicantCurrentStatusId, MaxFYId, currentUser.Id).ToListAsync();
                    MyStaticClass.SetInProcessFile(InProcessSummary.Sum(a => a.Applicant));
                    MyStaticClass.SetRejectedFile(RejectedSummary.Sum(a => a.Applicant));
                    MyStaticClass.SetWaitingFile(WaitingSummary.Sum(a => a.Applicant));*/
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
