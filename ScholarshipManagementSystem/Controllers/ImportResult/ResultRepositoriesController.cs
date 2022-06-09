using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Repository.Data;
using DAL.Models.Domain.MasterSetup;
using DAL.Models.ViewModels;
using DAL.Models.Domain.ScholarshipSetup;
using Newtonsoft.Json;
using DAL.Models.Domain.ImportResult;
using System.IO;

namespace ScholarshipManagementSystem.Controllers.MasterSetup
{
    public class ResultRepositoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultRepositoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultRepositories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ResultRepository.Include(r => r.SchemeLevelPolicy.SchemeLevel.DegreeScholarshipLevels).Include(a=>a.DAEInstitute).Include(r => r.ScholarshipFiscalYear);
            ViewBag.Schemes = _context.Scheme.ToList();
            ViewBag.SchemeLevels = _context.SchemeLevel.ToList();
            ViewBag.DegreeScholarshipLevel = _context.DegreeScholarshipLevel.ToList();
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexFormCollector()
        {
            var applicationDbContext = _context.ResultRepository.Include(r => r.SchemeLevelPolicy.SchemeLevel.DegreeScholarshipLevels).Include(a => a.DAEInstitute).Include(r => r.ScholarshipFiscalYear).Where(a=>a.IsMeritListGenerated == true);
            ViewBag.Schemes = _context.Scheme.ToList();
            ViewBag.SchemeLevels = _context.SchemeLevel.ToList();
            ViewBag.DegreeScholarshipLevel = _context.DegreeScholarshipLevel.ToList();
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            var applicationDbContext = _context.Scheme;
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Test(ViewUploadedResult viewUploadedResult)
        {                        
            return View(viewUploadedResult);
        }
        // GET: ResultRepositories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository
                .Include(r => r.SchemeLevelPolicy.SchemeLevel)
                .Include(r => r.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.ResultRepositoryId == id);
            if (resultRepository == null)
            {
                return NotFound();
            }

            return View(resultRepository);
        }
        private string GetPropertyName(dynamic obj, string column)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj).ToString() == column) // check obj has value for that particular property
                {
                    return property.Name;
                }
            }
            return "";
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResultRepository resultRepository, IFormFile attachment, List<string> selectedColumn)
        {
            if (ModelState.IsValid)
            {
                if (_context.ResultRepository.Include(a=>a.SchemeLevelPolicy).Count(a => a.ScholarshipFiscalYearId == resultRepository.ScholarshipFiscalYearId && a.SchemeLevelPolicy.SchemeLevelId == resultRepository.SchemeLevelPolicyId) == 0)
                {
                    if (attachment?.Length > 0)
                    {
                        _context.Add(resultRepository);
                        await _context.SaveChangesAsync();
                        int MaxResultRespositoryId = _context.ResultRepository.Max(a => a.ResultRepositoryId);
                        // convert to a stream
                        var stream = attachment.OpenReadStream();

                        //List<ResultContainer> resultContainer = new List<ResultContainer>();

                        try
                        {
                            using (var package = new ExcelPackage(stream))
                            {
                                var worksheet = package.Workbook.Worksheets.First();
                                var rowCount = worksheet.Dimension.Rows;
                                var colCount = worksheet.Dimension.Columns;
                                List<int> columnList = new List<int>();
                                for (var val = 0; val < selectedColumn.Count; val++)
                                {
                                    for (var col = 1; col <= colCount; col++)
                                    {
                                        if (worksheet.Cells[1, col].Value.ToString().ToLower() == selectedColumn.ElementAt(val).ToLower())
                                        {
                                            columnList.Add(col);
                                            break;
                                        }
                                    }
                                }
                                //-------------------------------------------------------
                                List<string> columnNameList = new List<string>();
                                int counter = 0;
                                for (var val = 1; val <= 13; val++)//KDA Hard
                                {
                                    if (counter < columnList.Count && val == columnList.ElementAt(counter))
                                    {
                                        columnNameList.Add(selectedColumn.ElementAt(counter).ToString());
                                        counter++;
                                    }
                                    else
                                    {
                                        columnNameList.Add("");
                                    }
                                }
                                var columnLabel = new ColumnLabel()
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
                                    IsActive = true,
                                    ResultRepositoryId = MaxResultRespositoryId
                                };
                                _context.Add(columnLabel);
                                await _context.SaveChangesAsync();                                
                                if (columnList.Count == selectedColumn.Count)
                                {
                                    //-------------------------------------------------------                                    
                                    var districts = _context.District.Select(a=> new District {DistrictId = a.DistrictId, Name = a.Name.ToLower() }).ToList();                                                                  
                                    for (var row = 2; row < rowCount; row++)
                                    {
                                        counter = 0;
                                        try
                                        {
                                            List<string> specificExcelRow = new List<string>();
                                            for (var val = 1; val <= 13; val++)//KDA Hard
                                            {
                                                if (counter < columnList.Count && val == columnList.ElementAt(counter))
                                                {
                                                    specificExcelRow.Add(worksheet.Cells[row, columnList.ElementAt(counter)].Value?.ToString());
                                                    counter++;
                                                }
                                                else
                                                {
                                                    specificExcelRow.Add("");
                                                }
                                            }

                                            var result = new ResultContainer()
                                            {      
                                                //ColumnLabelId = 43,
                                                ResultRepositoryId = MaxResultRespositoryId,
                                                Roll_NO = specificExcelRow.ElementAt(0),
                                                REG_NO = specificExcelRow.ElementAt(1),
                                                Name = specificExcelRow.ElementAt(2),
                                                Father_Name = specificExcelRow.ElementAt(3),
                                                Institute = specificExcelRow.ElementAt(4),
                                                Group = specificExcelRow.ElementAt(5),
                                                Candidate_District = specificExcelRow.ElementAt(6),
                                                Institute_District = specificExcelRow.ElementAt(7),                                                                                                
                                                Marks_ = int.Parse(specificExcelRow.ElementAt(8)),
                                                Pass_Fail = specificExcelRow.ElementAt(9),
                                                Remarks = specificExcelRow.ElementAt(10),                                                
                                                CNIC = specificExcelRow.ElementAt(11),                                                
                                                CGPA = decimal.Parse(specificExcelRow.ElementAt(12)),                                                
                                                DistrictId = 1
                                            };                                                                             
                                            string districtName = result.Candidate_District.ToLower();
                                            if (districts.Count(a => a.Name == districtName) != 0)
                                            {
                                                try
                                                {
                                                    result.DistrictId = districts.Where(a => a.Name == districtName).Max(a => a.DistrictId);
                                                }        
                                                catch(Exception ex)
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
                                    ResultRepository currentObj = _context.ResultRepository.Find(MaxResultRespositoryId);
                                    var fileName = Path.GetFileName(attachment.FileName);
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
                                        await attachment.CopyToAsync(stream2);
                                    }
                                    _context.Update(currentObj);
                                    //----------------------End Upload Excel File-------------------------
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {

                                }
                            }
                            //return RedirectToAction("Index", "ResultContainers", new { id = MaxResultRespositoryId });
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }                    
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "Name", "Name");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            ViewBag.Message = "Record already exist of " + _context.ScholarshipFiscalYear.Find(resultRepository.ScholarshipFiscalYearId).Name + " Fiscal Year of Scheme " + _context.SchemeLevel.Find(resultRepository.SchemeLevelPolicy.SchemeLevelId).Name;
            resultRepository.CreatedOn = DateTime.Now.Date;
            return View(resultRepository);            
        }
        public async Task<JsonResult> GetDegreeLevels(int schemeLevelId)
        {            
            List<DegreeScholarshipLevel> degreeLevels = await _context.DegreeScholarshipLevel.Where(a=>a.SchemeLevelId == schemeLevelId).ToListAsync();
            var degreeLevelList = degreeLevels.Select(m => new SelectListItem()
            {
                Text = m.Name.ToString(),
                Value = m.DegreeLevelId.ToString(),
            });
            return Json(degreeLevelList);
        }
        public async Task<JsonResult> GetSchemeLevels(int policySRCForumId, int schemeId)
        {            
            List<SchemeLevel> schemeLevels = await _context.SchemeLevelPolicy.Include(a=>a.SchemeLevel).Where(a => a.PolicySRCForumId == policySRCForumId && a.SchemeLevel.SchemeId == schemeId).Select(a=> new SchemeLevel { SchemeLevelId = a.SchemeLevelId, Name = a.SchemeLevel.Name }).ToListAsync();
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
        public IActionResult ResultImporter()
        {
            ViewData["SchemeId"] = new SelectList(_context.Scheme, "SchemeId", "Name");
            var qry = _context.PolicySRCForum.Where(a=>a.IsEndorse == true).Join(_context.ScholarshipFiscalYear, s => s.ScholarshipFiscalYearId, i => i.ScholarshipFiscalYearId, (s, i) => new { s, i })
             .GroupBy(g => new { g.s.ScholarshipFiscalYearId })
             .Select(g => new PolicySRCForum
             {
                 PolicySRCForumId = g.Max(p => p.s.PolicySRCForumId),                 
                 Code = g.Max(p => p.i.Code)
             });
            ViewData["PolicySRCForumId"] = new SelectList(qry, "PolicySRCForumId", "Code");                                   
            ViewData["ExcelColumnNameId"] = _context.ExcelColumnName.Where(a => a.IsActive == true)                                    
                                    .Select(x => new SelectListItem() { Text = x.Name, Value = x.Name, Selected= true });            
            ViewData["SchemeLevelMandatoryColumnId"] = _context.SchemeLevelMandatoryColumn
                                    .Include(a => a.ExcelColumnName)
                                    .Select(x => new SelectListItem() { Text = x.ExcelColumnName.Name, Value = x.ExcelColumnName.Name, Selected= true });
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
        public async Task<JsonResult> UploadFilePost(IFormFile excelFile, IFormFile pdfFile/*, bool isReImportResult*/, string selectedColumn, int PolicySRCForumId, int SchemeId, int SchemeLevelId, int DegreeScholarshipLevelId, int DAEInstituteId)
        {
            try
            {
                if (excelFile != null)
                {                    
                    var schemeLevelMandatoryColumn = _context.SchemeLevelMandatoryColumn.Include(a=>a.ExcelColumnName).Where(a => a.SchemeLevelId == SchemeLevelId).ToList();
                    foreach(var column in schemeLevelMandatoryColumn)
                    {
                        if (!selectedColumn.Contains(column.ExcelColumnName.Name))
                        {                            
                            return Json(new { isValid = false, reupload = false, message = "Please select mandatory columns!" });
                        }
                    }
                    var IsResultAlreadyExist = 0;
                    if (SchemeId < 4)
                    {                        
                            if(DAEInstituteId == 0)
                            {
                                IsResultAlreadyExist = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Count(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId);
                            }
                            else
                            {
                                IsResultAlreadyExist = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Count(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DAEInstituteId == DAEInstituteId);
                            }                                                   
                    }
                    else
                    {                        
                        IsResultAlreadyExist = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Count(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DegreeScholarshipLevelId == DegreeScholarshipLevelId);                       
                    }
                    if (IsResultAlreadyExist != 0)
                    {
                        var ResultRepositoryTempId = 0;
                        if (SchemeId < 4)
                        {
                            if(DAEInstituteId == 0)
                            {
                                ResultRepositoryTempId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).Max(a => a.ResultRepositoryTempId);
                            }
                            else
                            {
                                ResultRepositoryTempId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DAEInstituteId == DAEInstituteId).Max(a => a.ResultRepositoryTempId);
                            }                       
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
                                            DbColumnList.Add(_context.ExcelColumnName.Where(a=>a.IsActive == true).Where(a => a.Name.ToLower() == selectedColumnList.ElementAt(val).ToLower()).Select(a => a.ExcelColumnNameId).FirstOrDefault());
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
                                    resultRepository.DAEInstituteId = DAEInstituteId;
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
                                        bool IsRowEmpty = true;
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
                                                if(!string.IsNullOrEmpty(specificExcelRow.Last()))
                                                {
                                                    IsRowEmpty = false;
                                                }
                                            }
                                            if(IsRowEmpty)
                                            {
                                                continue;
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
                                    //----------------------Upload Pdf File-----------------------------
                                    currentObj = _context.ResultRepositoryTemp.Find(MaxResultRespositoryId);
                                    fileName = Path.GetFileName(pdfFile.FileName);
                                    rootPath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot\\Documents\\ResultScanned\\RRID" + MaxResultRespositoryId.ToString() + "\\");
                                    fileName = fileName.Replace("&", "n"); fileName = fileName.Replace(" ", ""); fileName = fileName.Replace("#", "H"); fileName = fileName.Replace("(", ""); fileName = fileName.Replace(")", "");
                                    random = new Random();
                                    randomNumber = random.Next(1, 1000);
                                    fileName = "Document" + randomNumber.ToString() + fileName;
                                    currentObj.resultScannedFilePath = Path.Combine("/Documents/ResultScanned/RRID" + MaxResultRespositoryId.ToString() + "/" + fileName);//Server Path
                                    sPath = Path.Combine(rootPath);
                                    if (!System.IO.Directory.Exists(sPath))
                                    {
                                        System.IO.Directory.CreateDirectory(sPath);
                                    }
                                    FullPathWithFileName = Path.Combine(sPath, fileName);
                                    using (var stream2 = new FileStream(FullPathWithFileName, FileMode.Create))
                                    {
                                        await pdfFile.CopyToAsync(stream2);
                                    }
                                    _context.Update(currentObj);
                                    //----------------------End Upload Pdf File-------------------------
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
                            if(DAEInstituteId == 0)
                            {
                                resultRepositoryId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId).Max(a => a.ResultRepositoryTempId);
                            }
                            else
                            {
                                resultRepositoryId = _context.ResultRepositoryTemp.Include(a => a.SchemeLevelPolicy).Where(a => a.SchemeLevelPolicy.PolicySRCForumId == PolicySRCForumId && a.SchemeLevelPolicy.SchemeLevelId == SchemeLevelId && a.DAEInstituteId == DAEInstituteId).Max(a => a.ResultRepositoryTempId);
                            }                         
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
        // GET: ResultRepositories/Create
        public IActionResult Create()
        {
            ViewData["SchemeLevelPolicyId"] = new SelectList(_context.SchemeLevelPolicy.Include(a=>a.SchemeLevel), "SchemeLevelPolicyId", "SchemeLevel.Name");
            ViewData["ExcelColumnNameId"] = new SelectList(_context.ExcelColumnName.Where(a=>a.IsActive == true), "Name", "Name");
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code");
            ResultRepository obj = new ResultRepository();
            obj.CreatedOn = DateTime.Now.Date;
            return View(obj);
        }

        // POST: ResultRepositories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultRepositoryId,resultFilePath,ScholarshipFiscalYearId,SchemeLevelId,CreatedOn,IsSelctionCriteriaApplied,IsDataCleaned")] ResultRepository resultRepository)
        {
            if (ModelState.IsValid)
            {
                if (_context.ResultRepository.Count(a=>a.ScholarshipFiscalYearId == resultRepository.ScholarshipFiscalYearId && a.SchemeLevelId == resultRepository.SchemeLevelId) == 0)
                {
                    _context.Add(resultRepository);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Record already exist of "+ _context.ScholarshipFiscalYear.Find(resultRepository.ScholarshipFiscalYearId).Name +" Fiscal Year of Scheme " + _context.SchemeLevel.Find(resultRepository.SchemeLevelId).Name;
                }
                
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }*/

        // GET: ResultRepositories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository.FindAsync(id);
            if (resultRepository == null)
            {
                return NotFound();
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }

        // POST: ResultRepositories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultRepositoryId,resultFilePath,ScholarshipFiscalYearId,SchemeLevelId,currentCounter,CreatedOn,IsSelctionCriteriaApplied,IsDataCleaned")] ResultRepository resultRepository)
        {
            if (id != resultRepository.ResultRepositoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultRepository);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultRepositoryExists(resultRepository.ResultRepositoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchemeLevelId"] = new SelectList(_context.SchemeLevel, "SchemeLevelId", "Name", resultRepository.SchemeLevelPolicy.SchemeLevelId);
            ViewData["ScholarshipFiscalYearId"] = new SelectList(_context.ScholarshipFiscalYear, "ScholarshipFiscalYearId", "Code", resultRepository.ScholarshipFiscalYearId);
            return View(resultRepository);
        }

        // GET: ResultRepositories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultRepository = await _context.ResultRepository
                .Include(r => r.SchemeLevelPolicy.SchemeLevel)
                .Include(r => r.ScholarshipFiscalYear)
                .FirstOrDefaultAsync(m => m.ResultRepositoryId == id);
            if (resultRepository == null)
            {
                return NotFound();
            }

            return View(resultRepository);
        }

        // POST: ResultRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultRepository = await _context.ResultRepository.FindAsync(id);
            _context.ResultRepository.Remove(resultRepository);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultRepositoryExists(int id)
        {
            return _context.ResultRepository.Any(e => e.ResultRepositoryId == id);
        }

        public async Task<IActionResult> AddDocumentAssistIndicator(int rrId, int DocumentAssistId, int ExcelColumnNameId, int IsAdd)
        {
            if(IsAdd == 1)
            {
                var IsExist = _context.DocumentAssistIndicator.Count(a => a.ExcelColumnNameId == ExcelColumnNameId && a.DocumentAssistId == DocumentAssistId);
                if (IsExist == 0)
                {
                    DocumentAssistIndicator obj = new DocumentAssistIndicator();
                    obj.DocumentAssistId = DocumentAssistId;
                    obj.ResultRepositoryId = rrId;
                    obj.ExcelColumnNameId = ExcelColumnNameId;
                    _context.Add(obj);
                    _context.SaveChanges();
                }                
            }
            var applicationDbContext = _context.DocumentAssistIndicator.Include(r => r.DocumentAssist).Include(r => r.ExcelColumnName);
            return PartialView(await applicationDbContext.ToListAsync());            
        }
        public async Task<IActionResult> AddSelectionCriteria(int rrId, int operatorId, int ExcelColumnNameId, string value, int IsAdd)
        {
            if (IsAdd == 1)
            {
                var IsExist = _context.SelectionCriteria.Count(a => a.ExcelColumnNameId == ExcelColumnNameId && a.OperatorId == operatorId);
                if (IsExist == 0)
                {
                    SelectionCriteria obj = new SelectionCriteria();
                    obj.OperatorId = operatorId;
                    obj.ResultRepositoryId = rrId;
                    obj.ExcelColumnNameId = ExcelColumnNameId;
                    obj.Condition = value;
                    _context.Add(obj);
                    _context.SaveChanges();
                }
            }
            var applicationDbContext = _context.SelectionCriteria.Include(r => r.Operator).Include(r => r.ExcelColumnName);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        public async Task<int> CallSP(string SP,string column, int rrId, int DocumentAssistId, string fromTable)
        {
            var applicationDbContext = _context.SPAssistDocumentViewer.FromSqlRaw<SPAssistDocumentViewer>(SP, column, rrId, fromTable);
            int result = 0;
            try
            {
                if (DocumentAssistId == 1)
                {
                    List<SPAssistDocumentViewer> temp = new List<SPAssistDocumentViewer>();
                    temp = applicationDbContext.ToList();                    
                    result = temp.Count();
                }
                else if (DocumentAssistId == 2)
                {
                    if (applicationDbContext != null)
                    {
                        List<SPAssistDocumentViewer> temp = new List<SPAssistDocumentViewer>();
                        temp = applicationDbContext.ToList();
                        result = temp.ElementAt(0).TotalFind;
                    }
                }
            }  catch(Exception ex)
            {

            }
            return result;
        }
        public async Task<IActionResult> AssistDocument(int rrId)
        {
            var records = _context.DocumentAssistIndicator.Include(a=>a.ExcelColumnName).Include(a=>a.DocumentAssist).Where(a => a.ResultRepositoryId == rrId).ToList();
            ViewBag.TotalRecord = _context.ResultContainerTemp.Count(a => a.ResultRepositoryTempId == rrId);
            List<SPAssistDocumentViewer> sPAssistDocumentViewers = new List<SPAssistDocumentViewer>();            
            foreach (var record in records)
            {
                if (record.DocumentAssistId == 1)
                {
                    DocumentAssistIndicator obj = new DocumentAssistIndicator();
                    obj = _context.DocumentAssistIndicator.Find(record.DocumentAssistIndicatorId);
                    obj.TotalFind = await CallSP("exec [ImportResult].[SPCheckDouplicateReport] {0}, {1}, {2}", record.ExcelColumnName.Name, rrId, record.DocumentAssistId, "ResultContainerTemp");
                    _context.Update(obj);
                }
                else if (record.DocumentAssistId == 2)
                {
                    DocumentAssistIndicator obj = new DocumentAssistIndicator();
                    obj = _context.DocumentAssistIndicator.Find(record.DocumentAssistIndicatorId);
                    obj.TotalFind = await CallSP("exec [ImportResult].[SPCheckNullValuesReport] {0}, {1}, {2}", record.ExcelColumnName.Name, rrId, record.DocumentAssistId, "ResultContainerTemp");
                    _context.Update(obj);
                }
            }
            return PartialView(records);
        }
    }
}
