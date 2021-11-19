using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.Student;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var applicationDbContext = await _context.Applicant.Include(a => a.SelectionMethod).Include(r => r.District.Division.Provience).Include(a=>a.SchemeLevelPolicy.SchemeLevel).Where(a => a.ApplicantId == id).Select(a => new Applicant { ApplicantId = a.ApplicantId, ApplicantReferenceNo = a.ApplicantReferenceNo, RollNumber = a.RollNumber, Name = a.Name, FatherName = a.FatherName, SelectionMethod = new SelectionMethod { Name = a.SelectionMethod.Name }, SelectionStatus = a.SelectionStatus, District = new District {Name = a.District.Name }, Provience = new Provience { Name = a.Provience.Name }, RegisterationNumber = a.RegisterationNumber, DateOfBirth = a.DateOfBirth, Gender = a.Gender, BFormCNIC = a.BFormCNIC, Email = a.Email, StudentMobile = a.StudentMobile, FatherMobile = a.FatherMobile, FatherCareTakerCNIC = a.FatherCareTakerCNIC, RelationWithCareTaker = a.RelationWithCareTaker, Religion = a.Religion, HomeAddress = a.HomeAddress, TehsilName = a.TehsilName, OldInstitudeNameAddress = a.OldInstitudeNameAddress, Year = a.Year, TelephoneWithCode = a.TelephoneWithCode, CurrentInsituteName = a.CurrentInsituteName, CurrentInsituteHOD = a.CurrentInsituteHOD, CurrentInsitutePhone = a.CurrentInsitutePhone, CurrentInsituteFax = a.CurrentInsituteFax, CurrentInsituteFocalPerson = a.CurrentInsituteFocalPerson, CurrentInsituteFocalDesignation = a.CurrentInsituteFocalDesignation, CurrentInsituteFocalEmail = a.CurrentInsituteFocalEmail, CurrentInsituteFocalMobile = a.CurrentInsituteFocalMobile, CurrentInsituteAddress = a.CurrentInsituteAddress, TotalMarks = a.TotalMarks, ReceivedMarks = a.ReceivedMarks, TotalGPA = a.TotalGPA, ReceivedCGPA = a.ReceivedCGPA }).FirstOrDefaultAsync();
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode("https://beef.org.pk/wp-content/uploads/2021/10/QRCodeBEEFForm.pdf?" + applicationDbContext.ApplicantReferenceNo, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
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