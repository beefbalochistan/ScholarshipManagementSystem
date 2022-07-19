using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class UserAccessToPaymentMethodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAccessToPaymentMethodsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserAccessToPaymentMethods
        public async Task<IActionResult> Index(string Id)
        {
            var applicationDbContext = _context.UserAccessToPaymentMethod.Include(a => a.PaymentMethod).Where(a => a.UserId == Id);
            var user = await _userManager.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            ViewBag.Id = Id;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2(string Id)
        {
            var applicationDbContext = _context.ApplicantCurrentStatus;
            ViewBag.Id = Id;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserAccessToPaymentMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToPaymentMethod = await _context.UserAccessToPaymentMethod
                .Include(u => u.PaymentMethod)
                .FirstOrDefaultAsync(m => m.UserAccessToPaymentMethodId == id);
            if (userAccessToPaymentMethod == null)
            {
                return NotFound();
            }

            return View(userAccessToPaymentMethod);
        }

        // GET: UserAccessToPaymentMethods/Create
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id && a.ApplicantCurrentStatusId == 25).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }

        // GET: UserAccessToForwards/Create
        public IActionResult AssignRights(string Id)
        {
            UserAccessToPaymentMethod obj = new UserAccessToPaymentMethod();
            obj.UserId = Id;
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name");
            return View(obj);
        }

        // POST: UserAccessToForwards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRights([Bind("UserAccessToPaymentMethodId,UserId,PaymentMethodId")] UserAccessToPaymentMethod userAccessToPaymentMethod)
        {
            if (ModelState.IsValid)
            {
                var IsExist = _context.UserAccessToPaymentMethod.Count(a => a.UserId == userAccessToPaymentMethod.UserId && a.PaymentMethodId == userAccessToPaymentMethod.PaymentMethodId);
                if (IsExist > 0)
                {
                    ModelState.AddModelError(nameof(userAccessToPaymentMethod.PaymentMethodId), "Access already Assigned!");
                    return BadRequest(ModelState);
                }
                _context.Add(userAccessToPaymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { Id = userAccessToPaymentMethod.UserId });
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "Name", userAccessToPaymentMethod.PaymentMethodId);
            return View(userAccessToPaymentMethod);
        }

        // GET: UserAccessToPaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToPaymentMethod = await _context.UserAccessToPaymentMethod.FindAsync(id);
            if (userAccessToPaymentMethod == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userAccessToPaymentMethod.UserId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", userAccessToPaymentMethod.PaymentMethodId);
            return View(userAccessToPaymentMethod);
        }

        // POST: UserAccessToPaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAccessToPaymentMethodId,UserId,PaymentMethodId")] UserAccessToPaymentMethod userAccessToPaymentMethod)
        {
            if (id != userAccessToPaymentMethod.UserAccessToPaymentMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccessToPaymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccessToPaymentMethodExists(userAccessToPaymentMethod.UserAccessToPaymentMethodId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userAccessToPaymentMethod.UserId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", userAccessToPaymentMethod.PaymentMethodId);
            return View(userAccessToPaymentMethod);
        }

        // GET: UserAccessToPaymentMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToPaymentMethod = await _context.UserAccessToPaymentMethod
                .Include(u => u.ApplicationUser)
                .Include(u => u.PaymentMethod)
                .FirstOrDefaultAsync(m => m.UserAccessToPaymentMethodId == id);
            if (userAccessToPaymentMethod == null)
            {
                return NotFound();
            }

            return View(userAccessToPaymentMethod);
        }

        // POST: UserAccessToPaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccessToPaymentMethod = await _context.UserAccessToPaymentMethod.FindAsync(id);
            _context.UserAccessToPaymentMethod.Remove(userAccessToPaymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccessToPaymentMethodExists(int id)
        {
            return _context.UserAccessToPaymentMethod.Any(e => e.UserAccessToPaymentMethodId == id);
        }
    }
}
