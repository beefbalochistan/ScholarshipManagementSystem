using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Component.Tranche
{
    public class TrancheDocumentViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public TrancheDocumentViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.TrancheId = id;
            var applicationDbContext = await _context.TrancheDocument.Where(a => a.TrancheId == id).ToListAsync();            
            return await Task.FromResult((IViewComponentResult)View("TrancheDocument", applicationDbContext));
        }
    }
}
