using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScholarshipManagementSystem.Constants;
using ScholarshipManagementSystem.Helpers;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Controllers
{
[Authorize(Roles = "SuperAdmin")]
public class PermissionController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public PermissionController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<ActionResult> Index(string roleId)
    {
        var model = new PermissionViewModel();
        var allPermissions = new List<RoleClaimsViewModel>();
        /*allPermissions.GetPermissions(typeof(Permissions.Country), roleId);*/
        allPermissions.GetPermissions(typeof(Permissions.Division), roleId);
        allPermissions.GetPermissions(typeof(Permissions.District), roleId);
        allPermissions.GetPermissions(typeof(Permissions.DistrictDetail), roleId);
        allPermissions.GetPermissions(typeof(Permissions.ScholarshipType), roleId);
        allPermissions.GetPermissions(typeof(Permissions.ScholarshipFiscalYear), roleId);
        allPermissions.GetPermissions(typeof(Permissions.Degree), roleId);
        allPermissions.GetPermissions(typeof(Permissions.Discipline), roleId);
        allPermissions.GetPermissions(typeof(Permissions.InstituteType), roleId);
        allPermissions.GetPermissions(typeof(Permissions.Institute), roleId);
        allPermissions.GetPermissions(typeof(Permissions.InstituteDepartment), roleId);
        allPermissions.GetPermissions(typeof(Permissions.QualificationLevel), roleId);
        allPermissions.GetPermissions(typeof(Permissions.Scheme), roleId);
        allPermissions.GetPermissions(typeof(Permissions.SchemeLevel), roleId);
        allPermissions.GetPermissions(typeof(Permissions.PolicySRCForum), roleId);
        allPermissions.GetPermissions(typeof(Permissions.DistrictQoutaBySchemeLevel), roleId);
        var role = await _roleManager.FindByIdAsync(roleId);
        model.RoleId = roleId;
        var claims = await _roleManager.GetClaimsAsync(role);
        var allClaimValues = allPermissions.Select(a => a.Value).ToList();
        var roleClaimValues = claims.Select(a => a.Value).ToList();
        var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
        foreach (var permission in allPermissions)
        {
            if (authorizedClaims.Any(a => a == permission.Value))
            {
                permission.Selected = true;
            }
        }
        model.RoleClaims = allPermissions;
        return View(model);
    }

    public async Task<IActionResult> Update(PermissionViewModel model)
    {
        var role = await _roleManager.FindByIdAsync(model.RoleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }
        var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddPermissionClaim(role, claim.Value);
        }
        return RedirectToAction("Index", new { roleId = model.RoleId });
    }
}
}
