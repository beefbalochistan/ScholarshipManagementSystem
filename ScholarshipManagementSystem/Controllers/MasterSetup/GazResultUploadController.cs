using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagementSystem.Models.Domain.MasterSetup;
using ScholarshipManagementSystem.Models.ViewModels;
using System.Security.Claims;
using ScholarshipManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{

    public class GazResultUploadController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        private readonly ApplicationDbContext _context;
        [Obsolete]
        public GazResultUploadController(ApplicationDbContext context, IHostingEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            ResultUploadTypeViewModel model = new();
            model.ResultUploadType = PopulateResultUploadType();
            return View(model);
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Index(ResultUploadTypeViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                //Create a Folder. 
                ResultUpload Rmodel = new ResultUpload();
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(model.ExcelFile.FileName);
                string Extension = Path.GetExtension(model.ExcelFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ExcelFile.CopyTo(stream);
                }
                using (var dataStream = new MemoryStream())
                {
                    model.ExcelFile.CopyTo(dataStream);
                    Rmodel.VFile = dataStream.ToArray();
                }
                Rmodel.Name = fileName;
                Rmodel.ResultUploadTypeId = Convert.ToInt32(model.ResultUploadTypeId);
                _context.Add(Rmodel);
                _context.SaveChanges();
                string conString = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03 
                        conString = this.Configuration.GetConnectionString("Excel03ConString");
                        break; 
                    case ".xlsx": //Excel 07 
                        conString = this.Configuration.GetConnectionString("Excel07ConString");
                        break;
                } 
                DataTable dt = new DataTable();
                //Read the connection string for the Excel file. 
                conString = string.Format(conString, filePath, "Yes");
                using (OleDbConnection connExcel = new(conString))
                {
                    using (OleDbCommand cmdExcel = new())
                    {
                        using (OleDbDataAdapter odaExcel = new())
                        {
                            cmdExcel.Connection = connExcel; 
                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close(); 
                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                //Insert the Data read from the Excel file to Database Table.
                conString = this.Configuration.GetConnectionString("DefaultConnection");
                bool ResultStatus = false;
              if (model.ResultUploadTypeId == "1")
                {
                    ResultStatus= BluckResultHSSC(conString, dt);
                }
                else if (model.ResultUploadTypeId == "2")
                {
                    ResultStatus= BluckResultSSC(conString, dt);
                }
              if (ResultStatus)
                {
                    ViewBag.Message = "Data Saved ... ";
                    ViewBag.MessageType = "Success";
                }
                else
                {
                    ViewBag.Message = "Error while Saveing ... "; 
                }
            }
            model.ResultUploadType = PopulateResultUploadType();
            return View(model);
        }
        #region "BluckResult" 
        private bool BluckResultHSSC(string conString, DataTable dt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "[master].[GazResultHssc]";
                        //// Map the Excel columns
                        sqlBulkCopy.ColumnMappings.Add("Roll_NO", "Roll_NO");
                        sqlBulkCopy.ColumnMappings.Add("REG_NO", "REG_NO");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Father_Name", "Father_Name");
                        sqlBulkCopy.ColumnMappings.Add("Institute", "Institute");
                        sqlBulkCopy.ColumnMappings.Add("Institute_District", "Institute_District");
                        sqlBulkCopy.ColumnMappings.Add("Group", "Group");
                        sqlBulkCopy.ColumnMappings.Add("Candidate_District", "Candidate_District");
                        sqlBulkCopy.ColumnMappings.Add("Marks_", "Marks_");
                        sqlBulkCopy.ColumnMappings.Add("Type", "Type");
                        sqlBulkCopy.ColumnMappings.Add("Pass_Fail", "Pass_Fail");
                        sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true ;
        }
        private bool BluckResultSSC(string conString, DataTable dt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "[master].[GazResultSsc]";
                        //// Map the Excel columns
                        sqlBulkCopy.ColumnMappings.Add("Roll_NO", "Roll_NO");
                        sqlBulkCopy.ColumnMappings.Add("REG_NO", "REG_NO");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Father_Name", "Father_Name");
                        sqlBulkCopy.ColumnMappings.Add("Institute", "Institute");
                        sqlBulkCopy.ColumnMappings.Add("Group", "Group");
                        sqlBulkCopy.ColumnMappings.Add("candidate_district", "candidate_district");
                        sqlBulkCopy.ColumnMappings.Add("institute_district", "institute_district");
                        sqlBulkCopy.ColumnMappings.Add("Marks_", "Marks_"); 
                        sqlBulkCopy.ColumnMappings.Add("Pass_Fail", "Pass_Fail");
                        sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
        private SelectList PopulateResultUploadType()
        {
            var roleList = _context.ResultUploadType.Select(r => new { r.ResultUploadTypeId, r.Name }); 
            return new SelectList(roleList, "ResultUploadTypeId", "Name");
        }

    }
}
