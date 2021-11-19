using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.MasterSetup;
using Repository.Data;
using Microsoft.AspNetCore.Identity;
using DAL.Models;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class UserAccessToForwardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAccessToForwardsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserAccessToForwards
        public async Task<IActionResult> Index(string Id)
        {
            var applicationDbContext = _context.userAccessToForward.Include(u => u.ApplicantCurrentStatus).Where(a=>a.UserId == Id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2(string Id)
        {
            var applicationDbContext = _context.ApplicantCurrentStatus;
            ViewBag.Id = Id;
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: UserAccessToForwards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToForward = await _context.userAccessToForward
                .Include(u => u.ApplicantCurrentStatus)
                .FirstOrDefaultAsync(m => m.UserAccessToForwardId == id);
            if (userAccessToForward == null)
            {
                return NotFound();
            }

            return View(userAccessToForward);
        }
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }

        // GET: UserAccessToForwards/Create
        public IActionResult AssignRights(string Id)
        {
            UserAccessToForward obj = new UserAccessToForward();
            obj.UserId = Id;
            ViewData["ApplicantCurrentStatusId"] = new SelectList(_context.ApplicantCurrentStatus, "ApplicantCurrentStatusId", "ProcessState");
            return View(obj);
        }

        // POST: UserAccessToForwards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRights([Bind("UserAccessToForwardId,UserId,ApplicantCurrentStatusId")] UserAccessToForward userAccessToForward)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccessToForward);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { Id = userAccessToForward.UserId});
            }
            ViewData["ApplicantCurrentStatusId"] = new SelectList(_context.ApplicantCurrentStatus, "ApplicantCurrentStatusId", "ProcessState", userAccessToForward.ApplicantCurrentStatusId);
            return View(userAccessToForward);
        }

        // GET: UserAccessToForwards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToForward = await _context.userAccessToForward.FindAsync(id);
            if (userAccessToForward == null)
            {
                return NotFound();
            }
            ViewData["ApplicantCurrentStatusId"] = new SelectList(_context.ApplicantCurrentStatus, "ApplicantCurrentStatusId", "ApplicantCurrentStatusId", userAccessToForward.ApplicantCurrentStatusId);
            return View(userAccessToForward);
        }

        // POST: UserAccessToForwards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAccessToForwardId,UserId,ApplicantCurrentStatusId")] UserAccessToForward userAccessToForward)
        {
            if (id != userAccessToForward.UserAccessToForwardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccessToForward);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccessToForwardExists(userAccessToForward.UserAccessToForwardId))
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
            ViewData["ApplicantCurrentStatusId"] = new SelectList(_context.ApplicantCurrentStatus, "ApplicantCurrentStatusId", "ApplicantCurrentStatusId", userAccessToForward.ApplicantCurrentStatusId);
            return View(userAccessToForward);
        }

        // GET: UserAccessToForwards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToForward = await _context.userAccessToForward
                .Include(u => u.ApplicantCurrentStatus)
                .FirstOrDefaultAsync(m => m.UserAccessToForwardId == id);
            if (userAccessToForward == null)
            {
                return NotFound();
            }

            return View(userAccessToForward);
        }

        // POST: UserAccessToForwards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccessToForward = await _context.userAccessToForward.FindAsync(id);
            _context.userAccessToForward.Remove(userAccessToForward);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccessToForwardExists(int id)
        {
            return _context.userAccessToForward.Any(e => e.UserAccessToForwardId == id);
        }
    }
}
