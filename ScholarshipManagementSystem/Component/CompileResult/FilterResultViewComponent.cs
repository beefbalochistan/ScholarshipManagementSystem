using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Component.CompileResult
{
    public class FilterResultViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FilterResultViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int RRId)
        {
            ViewUploadedResult viewUploadedResult = new ViewUploadedResult();
            var applicationDbContext = await _context.ResultContainer
                                                                    .Where(a => a.ResultRepositoryId == RRId && a.DistrictId == id)
                                                                    .Select(a=> new ResultContainer { Roll_NO = a.Roll_NO, REG_NO = a.REG_NO, Name = a.Name, Father_Name = a.Father_Name, Candidate_District = a.Candidate_District, Marks_ = a.Marks_, Pass_Fail = a.Pass_Fail, ResultContainerId = a.ResultContainerId, Institute = a.Institute, Group = a.Group, Institute_District = a.Institute_District, Remarks = a.Remarks, CNIC = a.CNIC, CGPA = a.CGPA })
                                                                    .ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == RRId).FirstOrDefaultAsync();
            viewUploadedResult.columnLabel = obj;
            return await Task.FromResult((IViewComponentResult)View("FilterResult", viewUploadedResult));
        }
    }
}