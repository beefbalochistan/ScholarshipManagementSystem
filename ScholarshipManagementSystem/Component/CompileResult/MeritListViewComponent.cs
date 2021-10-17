using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Component.CompileResult
{
    public class MeritListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public MeritListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int SLPId, string selectedMethod, string selectedStatus)
        {            
            var applicationDbContext = await _context.Applicant.Include(r => r.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicyId == SLPId).ToListAsync();
            if (id != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DistrictId == id).ToList();
            }     
            if(selectedMethod != "All")
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectedMethod == selectedMethod).ToList();
            }
            if (selectedStatus != "All")
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectionStatus == selectedStatus).ToList();
            }
            return await Task.FromResult((IViewComponentResult)View("MeritList", applicationDbContext));
        }
    }
}