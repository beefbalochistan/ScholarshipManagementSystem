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
    public class UserAccessToSchemeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAccessToSchemeLevelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserAccessToSchemeLevels
        public async Task<IActionResult> Index(string Id)
        {
            var applicationDbContext = _context.UserAccessToSchemeLevel.Include(a=>a.SchemeLevel).Where(a => a.UserId == Id);
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

        // GET: UserAccessToSchemeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToSchemeLevel = await _context.UserAccessToSchemeLevel
                .Include(u => u.SchemeLevel)
                .FirstOrDefaultAsync(m => m.UserAccessToSchemeLevelId == id);
            if (userAccessToSchemeLevel == null)
            {
                return NotFound();
            }

            return View(userAccessToSchemeLevel);
        }

        // GET: UserAccessToSchemeLevels/Create
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }

        // GET: UserAccessToForwards/Create
        public IActionResult AssignRights(string Id)
        {
            UserAccessToSchemeLevel obj = new UserAccessToSchemeLevel();
            obj.UserId = Id;
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name");
            return View(obj);
        }

        // POST: UserAccessToForwards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRights([Bind("UserAccessToSchemeLevelId,UserId,SchemeLevelId")] UserAccessToSchemeLevel userAccessToSchemeLevel)
        {
            if (ModelState.IsValid)
            {
                var IsExist = _context.UserAccessToSchemeLevel.Count(a => a.UserId == userAccessToSchemeLevel.UserId && a.SchemeLevelId == userAccessToSchemeLevel.SchemeLevelId);
                if (IsExist > 0)
                {
                    ModelState.AddModelError(nameof(userAccessToSchemeLevel.SchemeLevelId), "Access already Assigned!");
                    return BadRequest(ModelState);
                }
                _context.Add(userAccessToSchemeLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { Id = userAccessToSchemeLevel.UserId });
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", userAccessToSchemeLevel.SchemeLevelId);
            return View(userAccessToSchemeLevel);
        }


        // GET: UserAccessToSchemeLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToSchemeLevel = await _context.UserAccessToSchemeLevel.FindAsync(id);
            if (userAccessToSchemeLevel == null)
            {
                return NotFound();
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", userAccessToSchemeLevel.SchemeLevelId);
            return View(userAccessToSchemeLevel);
        }

        // POST: UserAccessToSchemeLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAccessToSchemeLevelId,UserId,SchemeLevelId")] UserAccessToSchemeLevel userAccessToSchemeLevel)
        {
            if (id != userAccessToSchemeLevel.UserAccessToSchemeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccessToSchemeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccessToSchemeLevelExists(userAccessToSchemeLevel.UserAccessToSchemeLevelId))
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
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", userAccessToSchemeLevel.SchemeLevelId);
            return View(userAccessToSchemeLevel);
        }

        // GET: UserAccessToSchemeLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccessToSchemeLevel = await _context.UserAccessToSchemeLevel
                .Include(u => u.SchemeLevel)
                .FirstOrDefaultAsync(m => m.UserAccessToSchemeLevelId == id);
            if (userAccessToSchemeLevel == null)
            {
                return NotFound();
            }

            return View(userAccessToSchemeLevel);
        }

        // POST: UserAccessToSchemeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccessToSchemeLevel = await _context.UserAccessToSchemeLevel.FindAsync(id);
            _context.UserAccessToSchemeLevel.Remove(userAccessToSchemeLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccessToSchemeLevelExists(int id)
        {
            return _context.UserAccessToSchemeLevel.Any(e => e.UserAccessToSchemeLevelId == id);
        }
    }
}
