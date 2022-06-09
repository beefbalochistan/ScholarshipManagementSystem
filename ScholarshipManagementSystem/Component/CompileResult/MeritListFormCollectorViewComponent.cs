using DAL.Models.Domain.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Component.CompileResult
{
    public class MeritListFormCollectorViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public MeritListFormCollectorViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int SLPId, int selectedMethod, string selectedStatus, int RRId, int GradingSystem)
        {
            var applicationDbContext = await _context.Applicant.Include(a => a.SelectionMethod).Include(a => a.District).Include(r => r.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicyId == SLPId).Select(a => new Applicant { ApplicantId = a.ApplicantId, RegisterationNumber = a.RegisterationNumber, ApplicantReferenceNo = a.ApplicantReferenceNo, RollNumber = a.RollNumber, Name = a.Name, FatherName = a.FatherName, SelectionMethod = a.SelectionMethod, SelectionStatus = a.SelectionStatus, ApplicantSelectionStatusId = a.ApplicantSelectionStatusId, DistrictId = a.DistrictId, ReceivedCGPA = a.ReceivedCGPA, TotalMarks = a.TotalMarks, ReceivedMarks = a.ReceivedMarks, District = a.District }).OrderByDescending(a => a.ReceivedMarks).ToListAsync();
            int SLId = _context.SchemeLevelPolicy.Find(SLPId).SchemeLevelId;
            ViewBag.SLId = SLId;
            if (SLId == 1 || SLId == 2 || SLId == 3 || SLId == 7 || SLId == 8 || SLId == 9)
                if (id != 0)
                {
                    applicationDbContext = applicationDbContext.Where(a => a.DistrictId == id).ToList();
                }
            if (selectedMethod != 0)
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectionMethod.SelectionMethodId == selectedMethod).ToList();
            }
            if (selectedStatus != "All")
            {
                applicationDbContext = applicationDbContext.Where(a => a.SelectionStatus == selectedStatus).ToList();
            }
            ViewBag.RRId = RRId;
            ViewBag.GradingSystem = GradingSystem;
            return await Task.FromResult((IViewComponentResult)View("MeritListFormCollector", applicationDbContext));
        }
    }
}