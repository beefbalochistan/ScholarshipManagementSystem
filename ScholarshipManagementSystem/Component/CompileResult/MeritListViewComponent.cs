using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagementSystem.Data;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.Domain.Student;
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

        public async Task<IViewComponentResult> InvokeAsync(int id, int SLPId, int selectedMethod, string selectedStatus, int RRId)
        {            
            var applicationDbContext = await _context.Applicant.Include(a=>a.SelectionMethod).Include(r => r.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicyId == SLPId).Select(a=> new Applicant { ApplicantId = a.ApplicantId, ApplicantReferenceNo = a.ApplicantReferenceNo, RollNumber = a.RollNumber, Name = a.Name, FatherName = a.FatherName, SelectionMethod = a.SelectionMethod, SelectionStatus = a.SelectionStatus, DistrictId = a.DistrictId }).ToListAsync();
            if (id != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.DistrictId == id).ToList();
            }     
            if(selectedMethod != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectionMethodId == selectedMethod).ToList();
            }
            if (selectedStatus != "All")
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectionStatus == selectedStatus).ToList();
            }
            ViewBag.RRId = RRId;
            return await Task.FromResult((IViewComponentResult)View("MeritList", applicationDbContext));
        }
    }
}