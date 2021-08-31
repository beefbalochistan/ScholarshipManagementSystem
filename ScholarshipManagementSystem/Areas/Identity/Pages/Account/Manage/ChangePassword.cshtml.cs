using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ScholarshipManagementSystem.Common;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models;
using ScholarshipManagementSystem.Models.Domain.AutoSMSApi;

namespace ScholarshipManagementSystem.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly ApplicationDbContext _context;

        public ChangePasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            ViewData["Username"] = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            //--------------------SMS Alert------------------------------
            SMSAPIService ConfigObj = new SMSAPIService();
            ConfigObj = _context.SMSAPIService.Find(1);
            SMSAPI SMSObj = new SMSAPI(ConfigObj.Username, ConfigObj.Password, ConfigObj.Mask, ConfigObj.SendSMSURL);
            var mobileNo = AlignPhoneNo(user.PhoneNumber);
            var Text = "Your BEEF account password has been successfully updated!";
            var response = SMSObj.SendSingleSMS(Text, mobileNo, "English");
            SMSAPIServiceAuditTrail SMSRecord = new SMSAPIServiceAuditTrail();
            SMSRecord.DestinationNumber = mobileNo;
            SMSRecord.Language = "English";
            SMSRecord.ResponseMessage = response;
            SMSRecord.ResponseType = "Text";
            SMSRecord.SendOn = DateTime.Now;
            SMSRecord.Text = Text;
            SMSRecord.TextLength = Text.Length;
            SMSRecord.MessageFor = Enums.MessageFor.Employee.ToString();
            SMSRecord.UserId = user.Id;
            _context.Add(SMSRecord);
            await _context.SaveChangesAsync();
            //-----------------------------------------------------------
            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }

        private string AlignPhoneNo(string number)
        {
            if(number.FirstOrDefault() == '0')
            {
                return "92" + (number.Remove(0, 1));
            }
            return number;
        }
    }
}
