using DAL.Models.Domain.ImportResult;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.Domain.ScholarshipSetup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Controllers.ImportResult
{
    public class ResultAnalyzerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultAnalyzerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ResultAnalyserPanel()
        {
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            var qry = _context.PolicySRCForum.Where(a => a.IsEndorse == true).Join(_context.ScholarshipFiscalYear, s => s.ScholarshipFiscalYearId, i => i.ScholarshipFiscalYearId, (s, i) => new { s, i })
             .GroupBy(g => new { g.s.ScholarshipFiscalYearId })
             .Select(g => new PolicySRCForum
             {
                 PolicySRCForumId = g.Max(p => p.s.PolicySRCForumId),
                 Code = g.Max(p => p.i.Code)
             });
            ViewData["PolicySRCForumId"] = new SelectList(qry, "PolicySRCForumId", "Code");
            //ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeId", "Name");            
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "Name", "Name", true);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Name");
            ViewData["DocumentAssistId"] = new SelectList(_context.DocumentAssist, "DocumentAssistId", "ConditionalOperator");
            ResultRepositoryTemp obj = new ResultRepositoryTemp();
            obj.CreatedOn = DateTime.Now.Date;
            return View(obj);
        }
        public JsonResult AttachFilePost(IFormFile excelFile)
        {

            if (excelFile != null)
            {
                List<string> columnList = new List<string>();
                var stream = excelFile.OpenReadStream();
                try
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var colCount = worksheet.Dimension.Columns;
                        for (var col = 1; col <= colCount; col++)
                        {
                            columnList.Add(worksheet.Cells[1, col].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = "Something went wrong!" });
                }
                var selectListItems = columnList.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == "item 1" });
                return Json(new { isValid = true, message = "Result Attached successfully!", allColumn = selectListItems });
            }
            else
            {
                return Json(new { isValid = false, message = "Please attach result file!" });
            }
        }
        public async Task<JsonResult> UploadFilePost(IFormFile excelFile, /*bool isReImportResult, */string selectedColumn, int PolicySRCForumId, int SchemeId, int SchemeLevelId, int DegreeScholarshipLevelId)
        {
            try
            {
                if (excelFile != null)
                {
                    var IsResultAlreadyExist = 0;
                    if (SchemeId < 4)
                    {
                        if (selectedColumn.Contains("Roll_NO") && selectedColumn.Contains("Name") && selectedColumn.Contains("Father_Name") && selectedColumn.Contains("Candidate_District")  && selectedColumn.Contains("Marks_"))
                        {
                            IsResultAlreadyExist = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Count(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId);
                        }
                        else
                        {
                            return Json(new { isValid = false, reupload = false, message = "Please select mandatory columns!" });
                        }
                    }
                    else
                    {
                        if (selectedColumn.Contains("Roll_NO") && selectedColumn.Contains("Name") && selectedColumn.Contains("Father_Name")  && (selectedColumn.Contains("Marks_") || selectedColumn.Contains("CGPA")))
                        {
                            IsResultAlreadyExist = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Count(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId);
                        }
                        else
                        {
                            return Json(new { isValid = false, reupload = false, message = "Please select mandatory columns!" });
                        }
                    }
                    if(/*isReImportResult == true && */IsResultAlreadyExist != 0)
                    {
                        var ResultRepositoryTempId = 0;
                        if (SchemeId < 4)
                        {
                            ResultRepositoryTempId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).Max(a=>a.ResultRepositoryTempId);                           
                        }
                        else
                        {
                            ResultRepositoryTempId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).Max(a => a.ResultRepositoryTempId);
                        }
                        _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultContainerTemp] where ResultRepositoryTempId = " + ResultRepositoryTempId);
                        _context.Database.ExecuteSqlRaw("delete [ImportResult].[ResultRepositoryTemp] where ResultRepositoryTempId = " + ResultRepositoryTempId);
                        _context.Database.ExecuteSqlRaw("delete [ImportResult].[ColumnLabelTemp] where ResultRepositoryTempId = " + ResultRepositoryTempId);
                        //_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('ImportResult.ResultContainerTemp', RESEED, 1);");                        
                        IsResultAlreadyExist = 0;
                    }                    
                    if (IsResultAlreadyExist == 0)
                    {
                        int MaxResultRespositoryId = 0;
                        var stream = excelFile.OpenReadStream();
                        try
                        {
                            using (var package = new ExcelPackage(stream))
                            {
                                var worksheet = package.Workbook.Worksheets.First();
                                var rowCount = worksheet.Dimension.Rows;
                                var colCount = worksheet.Dimension.Columns;
                                List<int> excelColumnList = new List<int>();
                                List<int> DbColumnList = new List<int>();
                                List<string> selectedColumnList = selectedColumn.Split(',').ToList();
                                bool isFound = false;
                                for (var val = 0; val < selectedColumnList.Count; val++)
                                {
                                    isFound = false;
                                    for (var col = 1; col <= colCount; col++)
                                    {
                                        if (worksheet.Cells[1, col].Value.ToString().ToLower() == selectedColumnList.ElementAt(val).ToLower())
                                        {
                                            excelColumnList.Add(col);
                                            DbColumnList.Add(_context.ExcelColumnName.Where(a=>a.IsActive == true).Where(a=>a.Name.ToLower() == selectedColumnList.ElementAt(val).ToLower()).Select(a=>a.ExcelColumnNameId).FirstOrDefault());
                                            isFound = true;
                                            break;
                                        }
                                    }
                                    if (isFound == false)
                                    {
                                        return Json(new { isValid = false, message = "Column '"+ selectedColumnList.ElementAt(val) +"' missing in excel file!" });
                                    }
                                }
                                //-------------------------------------------------------
                                List<string> columnNameList = new List<string>();
                                int counter = 0;
                                for (var val = 1; val <= 16; val++)//KDA Hard
                                {
                                    if (counter < DbColumnList.Count && val == DbColumnList.ElementAt(counter))
                                    {
                                        columnNameList.Add(selectedColumnList.ElementAt(counter).ToString());
                                        counter++;
                                    }
                                    else
                                    {
                                        columnNameList.Add("");
                                    }
                                }
                                if (excelColumnList.Count == selectedColumnList.Count)
                                {
                                    ResultRepositoryTemp resultRepository = new ResultRepositoryTemp();
                                    resultRepository.CreatedOn = DateTime.Today;
                                    resultRepository.DegreeScholarshipLevelId = DegreeScholarshipLevelId;
                                    resultRepository.ScholarshipFiscalYearId = _context.PolicySRCForum.Find(PolicySRCForumId).ScholarshipFiscalYearId;
                                    resultRepository.SchemeLevelPolicyId = _context.SchemeLevelPolicy.Where(a => a.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelId == SchemeLevelId).Select(a => a.SchemeLevelPolicyId).FirstOrDefault();
                                    _context.Add(resultRepository);
                                    _context.SaveChanges();
                                    MaxResultRespositoryId = _context.ResultRepositoryTemp.Max(a => a.ResultRepositoryTempId);
                                    var columnLabel = new ColumnLabelTemp()
                                    {
                                        C1 = columnNameList.ElementAt(0),
                                        C2 = columnNameList.ElementAt(1),
                                        C3 = columnNameList.ElementAt(2),
                                        C4 = columnNameList.ElementAt(3),
                                        C5 = columnNameList.ElementAt(4),
                                        C6 = columnNameList.ElementAt(5),
                                        C7 = columnNameList.ElementAt(6),
                                        C8 = columnNameList.ElementAt(7),
                                        C9 = columnNameList.ElementAt(8),
                                        C10 = columnNameList.ElementAt(9),
                                        C11 = columnNameList.ElementAt(10),
                                        C12 = columnNameList.ElementAt(11),
                                        C13 = columnNameList.ElementAt(12),
                                        C14 = columnNameList.ElementAt(13),
                                        IsActive = true,
                                        ResultRepositoryTempId = MaxResultRespositoryId
                                    };
                                    _context.Add(columnLabel);
                                    await _context.SaveChangesAsync();
                                    //-------------------------------------------------------
                                    var currentSchemeLevel = _context.SchemeLevel.Find(SchemeLevelId);
                                    var districts = _context.District.Select(a => new District { DistrictId = a.DistrictId, Name = a.Name.ToLower() }).ToList();
                                    for (var row = 2; row < rowCount; row++)
                                    {
                                        counter = 0;
                                        try
                                        {
                                            List<string> specificExcelRow = new List<string>();
                                            for (var val = 1; val <= 16; val++)//KDA Hard
                                            {
                                                if (counter < DbColumnList.Count && val == DbColumnList.ElementAt(counter))
                                                {
                                                    specificExcelRow.Add(worksheet.Cells[row, excelColumnList.ElementAt(counter)].Value?.ToString());
                                                    counter++;
                                                }
                                                else
                                                {
                                                    specificExcelRow.Add("");
                                                }
                                            }
                                            int a;
                                            decimal b;
                                            bool resA = int.TryParse(specificExcelRow.ElementAt(8), out a);
                                            bool resB = decimal.TryParse(specificExcelRow.ElementAt(12), out b);
                                            var result = new ResultContainerTemp()
                                            {
                                                //ColumnLabelId = 43,
                                                ResultRepositoryTempId = MaxResultRespositoryId,
                                                Roll_NO = specificExcelRow.ElementAt(0),
                                                REG_NO = specificExcelRow.ElementAt(1),
                                                Name = specificExcelRow.ElementAt(2),
                                                Father_Name = specificExcelRow.ElementAt(3),
                                                Institute = specificExcelRow.ElementAt(4),
                                                Group = specificExcelRow.ElementAt(5),
                                                Candidate_District = specificExcelRow.ElementAt(6),
                                                Institute_District = specificExcelRow.ElementAt(7),
                                                Marks_ = resA == true ? a : 0,
                                                Pass_Fail = specificExcelRow.ElementAt(9),
                                                Remarks = specificExcelRow.ElementAt(10),
                                                CNIC = specificExcelRow.ElementAt(11),
                                                CGPA = resB == true ? b : 0,
                                                Department = specificExcelRow.ElementAt(13),
                                                TotalGPA = currentSchemeLevel.TotalMarks_GPA,
                                                TotalMarks_ = currentSchemeLevel.TotalMarks_GPA,
                                                DistrictId = 1
                                            };
                                            string districtName = result.Candidate_District.ToLower();
                                            if (districts.Count(a => a.Name == districtName) != 0)
                                            {
                                                try
                                                {
                                                    result.DistrictId = districts.Where(a => a.Name == districtName).Max(a => a.DistrictId);
                                                }
                                                catch (Exception ex)
                                                {
                                                    ViewBag.Message = ex.Message.ToString();
                                                }
                                            }
                                            await _context.AddAsync(result);
                                        }
                                        catch (Exception ex)
                                        {
                                            ViewBag.Message = ex.Message.ToString();
                                        }
                                    }
                                    //----------------------Upload Excel File-----------------------------
                                    ResultRepositoryTemp currentObj = _context.ResultRepositoryTemp.Find(MaxResultRespositoryId);
                                    var fileName = Path.GetFileName(excelFile.FileName);
                                    var rootPath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Result\\RRID" + MaxResultRespositoryId.ToString() + "\\");
                                    fileName = fileName.Replace("&", "n"); fileName = fileName.Replace(" ", ""); fileName = fileName.Replace("#", "H"); fileName = fileName.Replace("(", ""); fileName = fileName.Replace(")", "");
                                    Random random = new Random();
                                    int randomNumber = random.Next(1, 1000);
                                    fileName = "Document" + randomNumber.ToString() + fileName;
                                    currentObj.resultFilePath = Path.Combine("/Documents/Result/RRID" + MaxResultRespositoryId.ToString() + "/" + fileName);//Server Path
                                    string sPath = Path.Combine(rootPath);
                                    if (!System.IO.Directory.Exists(sPath))
                                    {
                                        System.IO.Directory.CreateDirectory(sPath);
                                    }
                                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                                    using (var stream2 = new FileStream(FullPathWithFileName, FileMode.Create))
                                    {
                                        await excelFile.CopyToAsync(stream2);
                                    }
                                    _context.Update(currentObj);
                                    //----------------------End Upload Excel File-------------------------
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    return Json(new { isValid = false, reupload = false, message = "Selected Column not matched with uploaded selected file!", resultRepositoryId = MaxResultRespositoryId });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json(new { isValid = false, reupload = false, message = "Something went wrong!", resultRepositoryId = MaxResultRespositoryId });
                        }
                        return Json(new { isValid = true, reupload = false, message = "Result Imported successfully!", resultRepositoryId = MaxResultRespositoryId });
                    }
                    else
                    {
                        var resultRepositoryId = 0;
                        if (SchemeId < 4)
                        {
                            resultRepositoryId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).Max(a => a.ResultRepositoryTempId);
                        }
                        else
                        {
                            resultRepositoryId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId).Max(a => a.ResultRepositoryTempId);
                        }
                        return Json(new { isValid = false, reupload = true, resultRepositoryId = resultRepositoryId, message = "Result already imported!" });
                    }
                }
                else
                {
                    return Json(new { isValid = false, reupload = false, message = "Please attach result file!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, reupload = false, message = ex.Message });
            }
        }


        public async Task<JsonResult> GetDegreeLevels(int schemeLevelId)
        {
            List<DegreeScholarshipLevel> degreeLevels = await _context.DegreeScholarshipLevel.Where(a => a.SchemeLevelId == schemeLevelId).ToListAsync();
            var degreeLevelList = degreeLevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.DegreeScholarshipLevelId.ToString(),
            });
            return Json(degreeLevelList);
        }
        public async Task<JsonResult> GetDAEInstitutes()
        {
            List<DAEInstitute> dAEInstitutes = await _context.DAEInstitute.ToListAsync();
            var dAEInstituteList = dAEInstitutes.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.DAEInstituteId.ToString(),
            });
            return Json(dAEInstituteList);
        }
        public async Task<JsonResult> GetSchemeLevels(int policySRCForumId, int schemeId)
        {
            List<SchemeLevel> schemeLevels = await _context.SchemeLevelPolicy.Include(a => a.SchemeLevel).Where(a => a.PolicySRCForumId == policySRCForumId && a.SchemeLevel.SchemeId == schemeId).Select(a => new SchemeLevel { SchemeLevelId = a.SchemeLevelId, Name = a.SchemeLevel.Name }).ToListAsync();
            var schemeLevelList = schemeLevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.SchemeLevelId.ToString(),
            });
            return Json(schemeLevelList);
        }
        public async Task<JsonResult> GetSelectedColumns(int rrId)
        {
            ColumnLabel columnLabels = await _context.ColumnLabel.Where(a => a.ResultRepositoryId == rrId).FirstOrDefaultAsync();
            List<ExcelColumnName> excelColumnNames = _context.ExcelColumnName.Where(a=>a.IsActive == true).ToList();
            List<ExcelColumnName> selectedExcelColumnNames = new List<ExcelColumnName>();
            string record = JsonConvert.SerializeObject(columnLabels);
            foreach (var columnName in excelColumnNames)
            {
                if (record.Contains(columnName.Name))
                {
                    selectedExcelColumnNames.Add(columnName);
                }
            }
            var selectedColumnLabelList = selectedExcelColumnNames.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.ExcelColumnNameId.ToString(),
            });
            return Json(selectedColumnLabelList);
        }
    }
}
