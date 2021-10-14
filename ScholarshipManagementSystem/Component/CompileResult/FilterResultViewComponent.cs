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
            var applicationDbContext = await _context.ResultContainer.Include(r => r.ResultRepository).Where(a => a.ResultRepositoryId == RRId && a.DistrictId == id).ToListAsync();
            viewUploadedResult.resultContainerList = applicationDbContext;
            ColumnLabel obj = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == RRId).FirstOrDefaultAsync();
            viewUploadedResult.columnLabel = obj;
            return await Task.FromResult((IViewComponentResult)View("FilterResult", viewUploadedResult));
        }
    }
}