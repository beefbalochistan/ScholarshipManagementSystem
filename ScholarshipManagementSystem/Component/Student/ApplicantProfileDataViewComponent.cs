using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Component.Student
{
    public class ApplicantProfileDataViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ApplicantProfileDataViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int userCurrentAccess)
        {
            var applicationDbContext = await _context.Applicant.Include(a => a.SelectionMethod).Include(r => r.District.Division.Provience).Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ApplicantId == id).FirstOrDefaultAsync();
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode("https://beef.org.pk/wp-content/uploads/2021/10/QRCodeBEEFForm.pdf?" + applicationDbContext.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            ViewBag.UserCurrentAccess = userCurrentAccess;
            ViewData["ddMethodList"] = new SelectList(_context.SelectionMethod.Where(a => a.SelectionMethodId > 2), "SelectionMethodId", "Name", applicationDbContext.SelectionMethodId);
            ViewData["ddGenderList"] = new SelectList(_context.Gender, "Name", "Name", applicationDbContext.Gender);
            ViewData["SelectedMethodId"] = new SelectList(_context.SelectionMethod.Where(a => a.SelectionMethodId > 2), "SelectionMethodId", "Name");
            ViewData["ProvienceId"] = new SelectList(_context.Provience.Where(a => a.ProvienceId == 1), "ProvienceId", "Name");
            ViewData["DistrictId"] = new SelectList(_context.District.Where(a => a.Division.ProvienceId == 1), "DistrictId", "Name", applicationDbContext.DistrictId);
            return await Task.FromResult((IViewComponentResult)View("ApplicantProfileData", applicationDbContext));
        }
    }
    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}