using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ExcelDataReader;
using System.Web.Mvc;
using System.Net.Http;
using CostAllocationApp.Dtos;
using CostAllocationApp.BLL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;
using CostAllocationApp.DAL;
using System.Data;
using CostAllocationApp.Controllers.Api;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CostAllocationApp.Controllers
{
    public class ForecastsController : Controller
    {

        EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
        UploadExcelBLL _uploadExcelBll = new UploadExcelBLL();
        UploadExcel _uploadExcel;
        char[] trimElements = { '\r', '\n', ' ' };
        SectionBLL sectionBLL = new SectionBLL();
        Utility _utility = new Utility();
        EmployeeBLL employeeBLL = new EmployeeBLL();
        Employee employee = new Employee();
        UserBLL userBLL = null;
        public ForecastsController()
        {
            userBLL = new UserBLL();
        }
        // GET: Forecasts
        public ActionResult CreateForecast()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            string requestType = Request.QueryString["forecastType"];

            if (TempData["seccess"] != null)
            {
                ViewBag.Success = TempData["seccess"];
            }
            else
            {
                ViewBag.Success = null;

            }
            ForecastViewModal forecastViewModal = new ForecastViewModal
            {
                _sections = sectionBLL.GetAllSections()
            };
            ViewBag.ErrorCount = 0;
            ViewBag.ImportViewOrForecastView = requestType;

            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/CreateForecast".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            return View(forecastViewModal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase uploaded_file, int upload_year)
        {
            ForecastViewModal forecastViewModal = new ForecastViewModal
            {
                _sections = sectionBLL.GetAllSections()
            };
            TempData["seccess"] = null;
            Dictionary<int, int> check = new Dictionary<int, int>();
            if (ModelState.IsValid)
            {

                if ((uploaded_file != null && uploaded_file.ContentLength > 0) && upload_year > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = uploaded_file.InputStream;

                    IExcelDataReader reader = null;


                    //if (uploaded_file.FileName.EndsWith(".xls"))
                    //{
                    //    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    //}
                    if (uploaded_file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        ViewBag.ErrorCount = 1;
                        return View("CreateForecast");
                    }
                    //int fieldcount = reader.FieldCount;
                    //int rowcount = reader.RowCount;
                    //DataTable dt = new DataTable();
                    DataRow row;
                    DataTable dt_ = new DataTable();
                    try
                    {
                        int tempAssignmentId = 0;
                        string tempRow = "";
                        int tempYear = upload_year;
                        dt_ = reader.AsDataSet().Tables[0];
                        int rowcount = dt_.Rows.Count;

                        for (int i = 2; i < rowcount; i++)
                        {
                            _uploadExcel = new UploadExcel();

                            if (i == 127)
                            {

                            }

                            //section 
                            if (!string.IsNullOrEmpty(dt_.Rows[i][0].ToString()))
                            {
                                _uploadExcel.SectionId = _uploadExcelBll.GetSectionIdByName(dt_.Rows[i][0].ToString().Trim(trimElements));
                                _uploadExcel.SectionId = _uploadExcelBll.GetSectionIdByName(dt_.Rows[i][0].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.SectionId = 0;
                            }

                            //department 
                            if (!string.IsNullOrEmpty(dt_.Rows[i][1].ToString()))
                            {
                                _uploadExcel.DepartmentId = _uploadExcelBll.GetDepartmentIdByName(dt_.Rows[i][1].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.DepartmentId = 0;
                            }

                            //incharge
                            if (!string.IsNullOrEmpty(dt_.Rows[i][2].ToString()))
                            {
                                _uploadExcel.InchargeId = _uploadExcelBll.GetInchargeIdByName(dt_.Rows[i][2].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.InchargeId = 0;
                            }
                            //role
                            if (!string.IsNullOrEmpty(dt_.Rows[i][3].ToString()))
                            {
                                _uploadExcel.RoleId = _uploadExcelBll.GetRoleIdByName(dt_.Rows[i][3].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.RoleId = 0;
                            }

                            //explanation
                            if (!string.IsNullOrEmpty(dt_.Rows[i][4].ToString()))
                            {
                                _uploadExcel.ExplanationId = _uploadExcelBll.GetExplanationIdByName(dt_.Rows[i][4].ToString().Trim(trimElements));
                            }

                            //name
                            if (string.IsNullOrEmpty(dt_.Rows[i][5].ToString().Trim(trimElements)))
                            {
                                continue;
                            }
                            else
                            {
                                employee.IsActive = true;
                                employee.CreatedBy = "";
                                employee.CreatedDate = DateTime.Now;
                                employee.FullName = dt_.Rows[i][5].ToString().Trim(trimElements);
                                int result = employeeBLL.CheckForEmployeeName(employee.FullName);
                                if (result > 0)
                                {
                                    _uploadExcel.EmployeeId = result;
                                }
                                else
                                {
                                    result = employeeBLL.CreateEmployee(employee);
                                }

                                _uploadExcel.EmployeeId = result;
                                _uploadExcel.EmployeeName = employee.FullName;
                            }

                            //compnay
                            if (!string.IsNullOrEmpty(dt_.Rows[i][6].ToString()))
                            {
                                _uploadExcel.CompanyId = _uploadExcelBll.GetCompanyIdByName(dt_.Rows[i][6].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.CompanyId = 0;
                            }

                            //grade and unit price
                            bool isGradeEmpty = false;
                            bool isUnitPriceEmpty = false;
                            if (string.IsNullOrEmpty(dt_.Rows[i][7].ToString()))
                            {
                                isGradeEmpty = true;
                            }
                            if (string.IsNullOrEmpty(dt_.Rows[i][8].ToString()))
                            {
                                isUnitPriceEmpty = true;
                            }
                            else if (Convert.ToInt32(dt_.Rows[i][8]) < 0)
                            {
                                isUnitPriceEmpty = true;
                            }

                            if (!isGradeEmpty && !isUnitPriceEmpty)
                            {
                                _uploadExcel.GradeId = _uploadExcelBll.GetGradeIdByGradeName(dt_.Rows[i][7].ToString().Trim(trimElements));
                                _uploadExcel.UnitPrice = Convert.ToInt32(dt_.Rows[i][8].ToString().Trim(trimElements));
                            }
                            else if (!isGradeEmpty)
                            {
                                _uploadExcel.GradeId = _uploadExcelBll.GetGradeIdByGradeName(dt_.Rows[i][7].ToString().Trim(trimElements));
                                _uploadExcel.UnitPrice = _uploadExcelBll.GetUnitPriceByGradeName(dt_.Rows[i][7].ToString().Trim(trimElements));
                            }
                            else if (!isUnitPriceEmpty)
                            {
                                _uploadExcel.GradeId = 0;
                                _uploadExcel.UnitPrice = Convert.ToInt32(dt_.Rows[i][8].ToString().Trim(trimElements));
                            }
                            //remarks
                            if (!string.IsNullOrEmpty(dt_.Rows[i][21].ToString()))
                            {
                                _uploadExcel.Remarks = dt_.Rows[i][21].ToString().Trim(trimElements);
                            }
                            else
                            {
                                _uploadExcel.Remarks = "";
                            }
                            
                            var assignmentViewModels = employeeAssignmentBLL.GetEmployeesByName(employee.FullName);

                            if (assignmentViewModels.Count > 0)
                            {
                                CreateAssignmentForExcelUpload(_uploadExcel, i, upload_year, assignmentViewModels.Count);
                                tempAssignmentId = employeeAssignmentBLL.GetLastId();
                            }
                            else
                            {
                                CreateAssignmentForExcelUpload(_uploadExcel, i, upload_year);
                                tempAssignmentId = employeeAssignmentBLL.GetLastId();
                            }

                            decimal octInput = 0;
                            decimal.TryParse(dt_.Rows[i][9].ToString(), out octInput);

                            decimal novInput = 0;
                            decimal.TryParse(dt_.Rows[i][10].ToString(), out novInput);

                            decimal decInput = 0;
                            decimal.TryParse(dt_.Rows[i][11].ToString(), out decInput);

                            decimal janInput = 0;
                            decimal.TryParse(dt_.Rows[i][12].ToString(), out janInput);

                            decimal febInput = 0;
                            decimal.TryParse(dt_.Rows[i][13].ToString(), out febInput);

                            decimal marInput = 0;
                            decimal.TryParse(dt_.Rows[i][14].ToString(), out marInput);

                            decimal aprInput = 0;
                            decimal.TryParse(dt_.Rows[i][15].ToString(), out aprInput);

                            decimal mayInput = 0;
                            decimal.TryParse(dt_.Rows[i][16].ToString(), out mayInput);

                            decimal junInput = 0;
                            decimal.TryParse(dt_.Rows[i][17].ToString(), out junInput);

                            decimal julInput = 0;
                            decimal.TryParse(dt_.Rows[i][18].ToString(), out julInput);

                            decimal augInput = 0;
                            decimal.TryParse(dt_.Rows[i][19].ToString(), out augInput);

                            decimal septInput = 0;
                            decimal.TryParse(dt_.Rows[i][20].ToString(), out septInput);

                            tempRow = $@"10_{octInput}_{octInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},11_{novInput}_{novInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},12_{decInput}_{decInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},1_{janInput}_{janInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},2_{febInput}_{febInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},3_{marInput}_{marInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},4_{aprInput}_{aprInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},5_{mayInput}_{mayInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},6_{junInput}_{junInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},7_{julInput}_{julInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},8_{augInput}_{augInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},9_{septInput}_{septInput * Convert.ToDecimal(_uploadExcel.UnitPrice)}";

                            SendToApi(tempAssignmentId, tempRow, tempYear);
                        }


                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("File", ex);
                        ViewBag.ErrorCount = 1;
                        return View("CreateForecast", forecastViewModal);
                    }

                    //DataSet result = new DataSet();
                    //result.Tables.Add(dt);
                    reader.Close();
                    reader.Dispose();
                    //DataTable tmp = result.Tables[0];
                    //Session["tmpdata"] = tmp;  //store datatable into session
                    TempData["seccess"] = "Data imported successfully";
                    return RedirectToAction("CreateForecast", new { forecastType = "imprt" });
                }
                else
                {
                    ViewBag.ErrorCount = 1;
                    ModelState.AddModelError("File", "invalid File or Year");
                }
            }
            //return View("CreateForecast", forecastViewModal);
            return View("CreateForecast",new { forecastType = "imprt" });            
        }

        public void SendToApi(int assignmentId, string row, int year)
        {

            SendToForecaseApiDto sendToForecaseApiDto = new SendToForecaseApiDto();
            sendToForecaseApiDto.Data = row;
            sendToForecaseApiDto.Year = year;
            sendToForecaseApiDto.AssignmentId = assignmentId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("" + _utility.Address + "/api/Forecasts?data=" + row + "&year=" + year + "&assignmentId=" + assignmentId);

                //HTTP POST
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //return RedirectToAction("Index");
                }
            }

        }

        //public int CreateAssignmentForExcelUpload(DataTable dt_, int i, int subCodeCount = 0)
        public int CreateAssignmentForExcelUpload(UploadExcel dt_, int i,int upload_year=0, int subCodeCount = 0)
        {
            EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            employeeAssignmentDTO = new EmployeeAssignmentDTO();
            //employeeAssignment.EmployeeName = dt_.EmployeeName;
            employeeAssignment.EmployeeId = dt_.EmployeeId.ToString();
            //employeeAssignment.SectionId = Convert.ToInt32(dt_.SectionId.ToString().Trim(trimElements));
            employeeAssignment.SectionId = String.IsNullOrEmpty(dt_.SectionId.ToString()) ? null : dt_.SectionId;
            //employeeAssignment.InchargeId = Convert.ToInt32(dt_.InchargeId.ToString().Trim(trimElements));
            employeeAssignment.InchargeId = String.IsNullOrEmpty(dt_.InchargeId.ToString()) ? null : dt_.InchargeId;
            //employeeAssignment.DepartmentId = Convert.ToInt32(dt_.DepartmentId.ToString().Trim(trimElements));
            employeeAssignment.DepartmentId = String.IsNullOrEmpty(dt_.DepartmentId.ToString()) ? null : dt_.DepartmentId;
            //employeeAssignment.RoleId = Convert.ToInt32(dt_.RoleId.ToString().Trim(trimElements));
            employeeAssignment.RoleId = String.IsNullOrEmpty(dt_.RoleId.ToString()) ? null : dt_.RoleId;
            //employeeAssignment.CompanyId = Convert.ToInt32(dt_.CompanyId.ToString().Trim(trimElements));
            employeeAssignment.CompanyId = String.IsNullOrEmpty(dt_.CompanyId.ToString()) ? null : dt_.CompanyId;
            employeeAssignment.ExplanationId = String.IsNullOrEmpty(dt_.ExplanationId.ToString()) ? null : dt_.ExplanationId.ToString().Trim(trimElements);
            employeeAssignment.UnitPrice = Convert.ToInt32(dt_.UnitPrice.ToString().Trim(trimElements));
            //employeeAssignment.GradeId = Convert.ToInt32(dt_.GradeId.ToString().Trim(trimElements));
            employeeAssignment.GradeId = String.IsNullOrEmpty(dt_.GradeId.ToString()) ? null : dt_.GradeId;
            employeeAssignment.SubCode = subCodeCount + 1;
            employeeAssignment.BCYR = false;
            employeeAssignment.BCYRCell = "";

            employeeAssignment.CreatedBy = "";
            employeeAssignment.CreatedDate = DateTime.Now;
            employeeAssignment.IsActive = "1";
            employeeAssignment.Remarks = dt_.Remarks;
            employeeAssignment.Year = upload_year.ToString();
            employeeAssignment.EmployeeName = dt_.EmployeeName;

            int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
            if (result == 0)
            {
                throw new Exception();
            }
            return result;
        }

        public ActionResult GetHistories()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/GetHistories".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            return View();
        }

        public ActionResult ActualCosts()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/ActualCosts".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            string requestType = Request.QueryString["forecastType"];

            if (TempData["seccess"] != null)
            {
                ViewBag.Success = TempData["seccess"];
            }
            else
            {
                ViewBag.Success = null;

            }
            ForecastViewModal forecastViewModal = new ForecastViewModal
            {
                _sections = sectionBLL.GetAllSections()
            };
            ViewBag.ErrorCount = 0;
            ViewBag.ImportViewOrForecastView = requestType;
            return View(forecastViewModal);
        }

        public ActionResult ActualCostConfirm()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/ActualCosts".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }

            return View();


        }

        public ActionResult CalculateActualCost()
        {
            return View();
        }

        public ActionResult GetTotal()
        {
            return View();
        }

        public ActionResult Apportionment()
        {
            return View();
        }

        public ActionResult GetSukeyWithQA()
        {
            return View();
        }
        // GET: Approve Forecasts
        public ActionResult ApproveForecast()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            if (TempData["seccess"] != null)
            {
                ViewBag.Success = TempData["seccess"];
            }
            else
            {
                ViewBag.Success = null;

            }
            ForecastViewModal forecastViewModal = new ForecastViewModal
            {
                _sections = sectionBLL.GetAllSections()
            };
            ViewBag.ErrorCount = 0;            
            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/CreateForecast".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            return View(forecastViewModal);
        }
        public ActionResult ApproveHistories()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }
            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Forecasts/GetHistories".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            return View();
        }

        [HttpPost]
        //[Route("Forecasts/DownloadHistoryData/")]
        public ActionResult DownloadHistoryData(int hid_approve_timestamp_id=0)
        {
            ForecastBLL forecastBLL = new ForecastBLL();
            List<object> forecastHistoryList = new List<object>();
            List<Forecast> historyList = forecastBLL.GetApprovalHistoriesByTimeStampId(hid_approve_timestamp_id);
            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();          
            string timeStampName = forecastBLL.GetApproveHistoryTimeStampName(hid_approve_timestamp_id);


            if (!string.IsNullOrEmpty(timeStampName)) { 
                if (distinctAssignmentId.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        //*****************Download: Original: Start***********************//
                        var sheet = package.Workbook.Worksheets.Add("Download(original)");
                        //sheet.Cells["A1"].Value = "利用者";
                        //sheet.Cells["A1"].Style.Font.Bold = true;
                        //sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheet.Cells["A1"].Value = "区分(Section)	";
                        //sheet.Cells["A1"].Style.Font.Bold = true;
                        //sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["A1:A1"].AutoFitColumns();
                        sheet.Cells["A1"].Value = "従業員名(Emp)";
                        sheet.Cells["A1"].Style.Font.Bold = true; ;
                        sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["A1"].AutoFitColumns();

                        sheet.Cells["B1"].Value = "部署(Dept)";
                        sheet.Cells["B1"].Style.Font.Bold = true;
                        sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["B1"].AutoFitColumns();

                        //sheet.Cells["C1"].Value = "担当作業(In chg)	";
                        //sheet.Cells["C1"].Style.Font.Bold = true;
                        //sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["C1"].AutoFitColumns();

                        //sheet.Cells["D1"].Value = "役割(Role)";
                        //sheet.Cells["D1"].Style.Font.Bold = true;
                        //sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["D1"].AutoFitColumns();

                        //sheet.Cells["E1"].Value = "説明(expl)";
                        //sheet.Cells["E1"].Style.Font.Bold = true;
                        //sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["E1"].AutoFitColumns();

                        //sheet.Cells["F1"].Value = "従業員名(Emp)";
                        //sheet.Cells["F1"].Style.Font.Bold = true; ;
                        //sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["F1"].AutoFitColumns();

                        //sheet.Cells["C1"].Value = "Operation Type";
                        //sheet.Cells["C1"].Style.Font.Bold = true;
                        //sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["G1:G1"].AutoFitColumns();

                        //sheet.Cells["G1"].Value = "Remaks";
                        //sheet.Cells["G1"].Style.Font.Bold = true;
                        //sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["G1:G1"].AutoFitColumns();

                        //sheet.Cells["H1"].Value = "会社(Com)	";
                        //sheet.Cells["H1"].Style.Font.Bold = true;
                        //sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["H1:H1"].AutoFitColumns();

                        //sheet.Cells["I1"].Value = "グレード(Grade)";
                        //sheet.Cells["I1"].Style.Font.Bold = true;
                        //sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["I1:I1"].AutoFitColumns();

                        //sheet.Cells["J1"].Value = "単価(Unit Price)	";
                        //sheet.Cells["J1"].Style.Font.Bold = true;
                        //sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        //sheet.Cells["J1:J1"].AutoFitColumns();

                        sheet.Cells["C1"].Value = "10";
                        sheet.Cells["C1"].Style.Font.Bold = true;
                        sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["C1"].AutoFitColumns();

                        sheet.Cells["D1"].Value = "11";
                        sheet.Cells["D1"].Style.Font.Bold = true;
                        sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["D1"].AutoFitColumns();

                        sheet.Cells["E1"].Value = "12";
                        sheet.Cells["E1"].Style.Font.Bold = true;
                        sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["E1"].AutoFitColumns();

                        sheet.Cells["F1"].Value = "1";
                        sheet.Cells["F1"].Style.Font.Bold = true;
                        sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["F1"].AutoFitColumns();

                        sheet.Cells["G1"].Value = "2";
                        sheet.Cells["G1"].Style.Font.Bold = true;
                        sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["G1"].AutoFitColumns();

                        sheet.Cells["H1"].Value = "3";
                        sheet.Cells["H1"].Style.Font.Bold = true;
                        sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["H1"].AutoFitColumns();

                        sheet.Cells["I1"].Value = "4";
                        sheet.Cells["I1"].Style.Font.Bold = true;
                        sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["I1"].AutoFitColumns();

                        sheet.Cells["J1"].Value = "5";
                        sheet.Cells["J1"].Style.Font.Bold = true;
                        sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["J1"].AutoFitColumns();

                        sheet.Cells["K1"].Value = "6";
                        sheet.Cells["K1"].Style.Font.Bold = true;
                        sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["K1"].AutoFitColumns();

                        sheet.Cells["L1"].Value = "7";
                        sheet.Cells["L1"].Style.Font.Bold = true;
                        sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["L1"].AutoFitColumns();

                        sheet.Cells["M1"].Value = "8";
                        sheet.Cells["M1"].Style.Font.Bold = true;
                        sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["M1"].AutoFitColumns();

                        sheet.Cells["N1"].Value = "9";
                        sheet.Cells["N1"].Style.Font.Bold = true;
                        sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["N1"].AutoFitColumns();

                        List<DownloadApproveHistoryViewModal> downloadApproveHistoryViewModals = new List<DownloadApproveHistoryViewModal>();
                        int count = 2;
                        foreach (var item in distinctAssignmentId)
                        {                           
                            ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();
                            AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                            _approvalHistoryViewModal = forecastBLL.GetApprovalNamesForHistory(item, hid_approve_timestamp_id);
                            
                            //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                            var employeeName = _approvalHistoryViewModal.EmployeeName;
                            var employeeId = _approvalHistoryViewModal.EmployeeId;
                            var sectionName = _approvalHistoryViewModal.SectionName;
                            var departmentName = _approvalHistoryViewModal.DepartmentName;
                            var inChargeName = _approvalHistoryViewModal.InChargeName;
                            var roleName = _approvalHistoryViewModal.RoleName;
                            var explanationName = _approvalHistoryViewModal.ExplanationName;
                            var companyName = _approvalHistoryViewModal.CompanyName;
                            var gradePoints = _approvalHistoryViewModal.GradePoints;
                            var unitPrice = _approvalHistoryViewModal.UnitPrice;
                            var remarks = _approvalHistoryViewModal.Remarks;
                            var isUpdate = _approvalHistoryViewModal.IsUpdate;
                            var isDeleteRow = _approvalHistoryViewModal.IsDeleteEmployee;
                            var isAddRow = _approvalHistoryViewModal.IsAddEmployee;
                            var isUpdateCells = _approvalHistoryViewModal.IsCellWiseUpdate;

                            var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                            var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                            var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                            var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                            var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                            var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                            var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                            var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                            var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                            var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                            var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                            var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                            var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                            var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);

                            _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

                            var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
                            var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
                            var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
                            var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
                            var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
                            var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
                            var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
                            var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
                            var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
                            var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
                            var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
                            var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

                            //first time looping: goes inside this condition
                            if (count == 2)
                            {
                                DownloadApproveHistoryViewModal downloadApproveHistoryViewModal = new DownloadApproveHistoryViewModal();

                                downloadApproveHistoryViewModal.EmployeeName = employeeName;
                                downloadApproveHistoryViewModal.EmployeeId = employeeId;
                                downloadApproveHistoryViewModal.DepartmentName = departmentName;

                                downloadApproveHistoryViewModal.OctPoints = octP;
                                downloadApproveHistoryViewModal.NovPoints = novP;
                                downloadApproveHistoryViewModal.DecPoints = decP;
                                downloadApproveHistoryViewModal.JanPoints = janP;
                                downloadApproveHistoryViewModal.FebPoints = febP;
                                downloadApproveHistoryViewModal.MarPoints = marP;
                                downloadApproveHistoryViewModal.AprPoints = aprP;
                                downloadApproveHistoryViewModal.MayPoints = mayP;
                                downloadApproveHistoryViewModal.JunPoints = junP;
                                downloadApproveHistoryViewModal.JulPoints = julP;
                                downloadApproveHistoryViewModal.AugPoints = augP;
                                downloadApproveHistoryViewModal.SepPoints = sepP;
                                downloadApproveHistoryViewModals.Add(downloadApproveHistoryViewModal);
                            }
                            else
                            {
                                //new logic: start
                                bool isQCSectionExists = false;
                                bool isSameEmployee = false;
                                bool isSameEmployeeQc = false;
                                string tempDepartmentName = "";

                                bool isNewObjAdd = false;

                                foreach (var originalItem in downloadApproveHistoryViewModals)
                                {
                                    if (originalItem.EmployeeId == employeeId)
                                    {
                                        isSameEmployee = true;
                                        tempDepartmentName = originalItem.DepartmentName;

                                        if (tempDepartmentName == "品証")
                                        {
                                            if (departmentName == "品証")
                                            {
                                                //update
                                                originalItem.OctPoints = originalItem.OctPoints + octP;
                                                originalItem.NovPoints = originalItem.NovPoints + novP;
                                                originalItem.DecPoints = originalItem.DecPoints + decP;
                                                originalItem.JanPoints = originalItem.JanPoints + janP;
                                                originalItem.FebPoints = originalItem.FebPoints + febP;
                                                originalItem.MarPoints = originalItem.MarPoints + marP;
                                                originalItem.AprPoints = originalItem.AprPoints + aprP;
                                                originalItem.MayPoints = originalItem.MayPoints + mayP;
                                                originalItem.JunPoints = originalItem.JunPoints + junP;
                                                originalItem.JulPoints = originalItem.JulPoints + julP;
                                                originalItem.AugPoints = originalItem.AugPoints + augP;
                                                originalItem.SepPoints = originalItem.SepPoints + sepP;
                                            }
                                            else
                                            {
                                                //add object
                                                isNewObjAdd = true;
                                                
                                            }
                                        }
                                        else
                                        {
                                            isNewObjAdd = true;
                                        }
                                       
                                        break;
                                        //if(originalItem.DepartmentName == )
                                    }                                          
                                }

                                if (!isSameEmployee || isNewObjAdd)
                                {
                                    DownloadApproveHistoryViewModal downloadApproveHistoryViewModal2 = new DownloadApproveHistoryViewModal();
                                    downloadApproveHistoryViewModal2.EmployeeName = employeeName;
                                    downloadApproveHistoryViewModal2.DepartmentName = departmentName;
                                    downloadApproveHistoryViewModal2.EmployeeId = employeeId;

                                    downloadApproveHistoryViewModal2.OctPoints = octP;
                                    downloadApproveHistoryViewModal2.NovPoints = novP;
                                    downloadApproveHistoryViewModal2.DecPoints = decP;
                                    downloadApproveHistoryViewModal2.JanPoints = janP;
                                    downloadApproveHistoryViewModal2.FebPoints = febP;
                                    downloadApproveHistoryViewModal2.MarPoints = marP;
                                    downloadApproveHistoryViewModal2.AprPoints = aprP;
                                    downloadApproveHistoryViewModal2.MayPoints = mayP;
                                    downloadApproveHistoryViewModal2.JunPoints = junP;
                                    downloadApproveHistoryViewModal2.JulPoints = julP;
                                    downloadApproveHistoryViewModal2.AugPoints = augP;
                                    downloadApproveHistoryViewModal2.SepPoints = sepP;
                                    downloadApproveHistoryViewModals.Add(downloadApproveHistoryViewModal2);
                                }
                                //else
                                //{
                                //    if(tempDepartmentName == "品証")
                                //    {
                                //        if (departmentName == "品証")
                                //        {
                                //            //update
                                //        }
                                //        else
                                //        {
                                //            //add object
                                //        }
                                //    }
                                //    if(departmentName == "品証" && tempDepartmentName == "品証")
                                //    {

                                //    }
                                //}
                                //new logic: end

                                //if (departmentName == "品証")
                                //{                                    
                                //    foreach (var originalItem in downloadApproveHistoryViewModals)
                                //    {
                                //        if (originalItem.DepartmentName == "品証")
                                //        {
                                //            isQCSectionExists = true;

                                //            originalItem.OctPoints = originalItem.OctPoints + octP;
                                //            originalItem.NovPoints = originalItem.NovPoints + novP;
                                //            originalItem.DecPoints = originalItem.DecPoints + decP;
                                //            originalItem.JanPoints = originalItem.JanPoints + janP;
                                //            originalItem.FebPoints = originalItem.FebPoints + febP;
                                //            originalItem.MarPoints = originalItem.MarPoints + marP;
                                //            originalItem.AprPoints = originalItem.AprPoints + aprP;
                                //            originalItem.MayPoints = originalItem.MayPoints + mayP;
                                //            originalItem.JunPoints = originalItem.JunPoints + junP;
                                //            originalItem.JulPoints = originalItem.JulPoints + julP;
                                //            originalItem.AugPoints = originalItem.AugPoints + augP;
                                //            originalItem.SepPoints = originalItem.SepPoints + sepP;
                                //        }                                       
                                //    }

                                //    if (!isQCSectionExists)
                                //    {
                                //        DownloadApproveHistoryViewModal downloadApproveHistoryViewModal2 = new DownloadApproveHistoryViewModal();
                                //        downloadApproveHistoryViewModal2.EmployeeName = employeeName;
                                //        downloadApproveHistoryViewModal2.DepartmentName = departmentName;

                                //        downloadApproveHistoryViewModal2.OctPoints = octP;
                                //        downloadApproveHistoryViewModal2.NovPoints = novP;
                                //        downloadApproveHistoryViewModal2.DecPoints = decP;
                                //        downloadApproveHistoryViewModal2.JanPoints = janP;
                                //        downloadApproveHistoryViewModal2.FebPoints = febP;
                                //        downloadApproveHistoryViewModal2.MarPoints = marP;
                                //        downloadApproveHistoryViewModal2.AprPoints = aprP;
                                //        downloadApproveHistoryViewModal2.MayPoints = mayP;
                                //        downloadApproveHistoryViewModal2.JunPoints = junP;
                                //        downloadApproveHistoryViewModal2.JulPoints = julP;
                                //        downloadApproveHistoryViewModal2.AugPoints = augP;
                                //        downloadApproveHistoryViewModal2.SepPoints = sepP;
                                //        downloadApproveHistoryViewModals.Add(downloadApproveHistoryViewModal2);
                                //    }
                                //}
                                //else
                                //{
                                //    bool isNotQCSection = false; 
                                //    foreach (var originalItem in downloadApproveHistoryViewModals)
                                //    {
                                //        if (originalItem.DepartmentName != "品証")
                                //        {
                                //            isNotQCSection = true;

                                //            originalItem.OctPoints = originalItem.OctPoints + octP;
                                //            originalItem.NovPoints = originalItem.NovPoints + novP;
                                //            originalItem.DecPoints = originalItem.DecPoints + decP;
                                //            originalItem.JanPoints = originalItem.JanPoints + janP;
                                //            originalItem.FebPoints = originalItem.FebPoints + febP;
                                //            originalItem.MarPoints = originalItem.MarPoints + marP;
                                //            originalItem.AprPoints = originalItem.AprPoints + augP;
                                //            originalItem.MayPoints = originalItem.MayPoints + marP;
                                //            originalItem.JunPoints = originalItem.JunPoints + julP;
                                //            originalItem.JulPoints = originalItem.JulPoints + julP;
                                //            originalItem.AugPoints = originalItem.AugPoints + augP;
                                //            originalItem.SepPoints = originalItem.SepPoints + sepP;
                                //        }
                                //    }
                                //    if (!isNotQCSection)
                                //    {
                                //        DownloadApproveHistoryViewModal downloadApproveHistoryViewModal3 = new DownloadApproveHistoryViewModal();
                                //        downloadApproveHistoryViewModal3.EmployeeName = employeeName;
                                //        downloadApproveHistoryViewModal3.DepartmentName = departmentName;

                                //        downloadApproveHistoryViewModal3.OctPoints = octP;
                                //        downloadApproveHistoryViewModal3.NovPoints = novP;
                                //        downloadApproveHistoryViewModal3.DecPoints = decP;
                                //        downloadApproveHistoryViewModal3.JanPoints = janP;
                                //        downloadApproveHistoryViewModal3.FebPoints = febP;
                                //        downloadApproveHistoryViewModal3.MarPoints = marP;
                                //        downloadApproveHistoryViewModal3.AprPoints = aprP;
                                //        downloadApproveHistoryViewModal3.MayPoints = mayP;
                                //        downloadApproveHistoryViewModal3.JunPoints = junP;
                                //        downloadApproveHistoryViewModal3.JulPoints = julP;
                                //        downloadApproveHistoryViewModal3.AugPoints = augP;
                                //        downloadApproveHistoryViewModal3.SepPoints = sepP;
                                //        downloadApproveHistoryViewModals.Add(downloadApproveHistoryViewModal3);
                                //    }
                                //}

                            }
                            
                            count++;
                        }

                        if (downloadApproveHistoryViewModals.Count > 0)
                        {
                            int originalIndex = 2;
                            foreach (var origianlSheetItem in downloadApproveHistoryViewModals)
                            {
                                sheet.Cells["A" + originalIndex].Value = origianlSheetItem.EmployeeName;
                                sheet.Cells["A" + originalIndex].AutoFitColumns();

                                if (origianlSheetItem.DepartmentName == "品証")
                                {
                                    sheet.Cells["B" + originalIndex].Value = origianlSheetItem.DepartmentName; ;
                                    sheet.Cells["B" + originalIndex].AutoFitColumns();

                                }
                                else
                                {
                                    sheet.Cells["B" + originalIndex].Value = "";
                                    sheet.Cells["B" + originalIndex].AutoFitColumns();

                                }

                                sheet.Cells["C" + originalIndex].Value = origianlSheetItem.OctPoints.ToString("0.0");
                                sheet.Cells["D" + originalIndex].Value = origianlSheetItem.NovPoints.ToString("0.0");
                                sheet.Cells["E" + originalIndex].Value = origianlSheetItem.DecPoints.ToString("0.0");
                                sheet.Cells["F" + originalIndex].Value = origianlSheetItem.JanPoints.ToString("0.0");
                                sheet.Cells["G" + originalIndex].Value = origianlSheetItem.FebPoints.ToString("0.0");
                                sheet.Cells["H" + originalIndex].Value = origianlSheetItem.MarPoints.ToString("0.0");
                                sheet.Cells["I" + originalIndex].Value = origianlSheetItem.AprPoints.ToString("0.0");
                                sheet.Cells["J" + originalIndex].Value = origianlSheetItem.MayPoints.ToString("0.0");
                                sheet.Cells["K" + originalIndex].Value = origianlSheetItem.JunPoints.ToString("0.0");
                                sheet.Cells["L" + originalIndex].Value = origianlSheetItem.JulPoints.ToString("0.0");
                                sheet.Cells["M" + originalIndex].Value = origianlSheetItem.AugPoints.ToString("0.0");
                                sheet.Cells["N" + originalIndex].Value = origianlSheetItem.SepPoints.ToString("0.0");

                                originalIndex++;
                            }

                        }
                        //*****************Download: Original: End***********************//

                        //*****************Download: Each Person: Start***********************//
                        var eachPersonSheet = package.Workbook.Worksheets.Add("Download(Each person)");
                        //eachPersonSheet.Cells["A1"].Value = "利用者";
                        //eachPersonSheet.Cells["A1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["A1"].Value = "従業員名(Emp)";
                        eachPersonSheet.Cells["A1"].Style.Font.Bold = true; ;
                        eachPersonSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["C1"].Value = "Operation Type";
                        //eachPersonSheet.Cells["C1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["D1"].Value = "Remaks";
                        //eachPersonSheet.Cells["D1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        //eachPersonSheet.Cells["E1"].Value = "区分(Section)	";
                        //eachPersonSheet.Cells["E1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        eachPersonSheet.Cells["B1"].Value = "部署(Dept)";
                        eachPersonSheet.Cells["B1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        //eachPersonSheet.Cells["G1"].Value = "担当作業(In chg)	";
                        //eachPersonSheet.Cells["G1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["H1"].Value = "役割(Role)";
                        //eachPersonSheet.Cells["H1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["I1"].Value = "説明(expl)";
                        //eachPersonSheet.Cells["I1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["J1"].Value = "会社(Com)	";
                        //eachPersonSheet.Cells["J1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["K1"].Value = "グレード(Grade)";
                        //eachPersonSheet.Cells["K1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //eachPersonSheet.Cells["L1"].Value = "単価(Unit Price)	";
                        //eachPersonSheet.Cells["L1"].Style.Font.Bold = true;
                        //eachPersonSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //eachPersonSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["C1"].Value = "10";
                        eachPersonSheet.Cells["C1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["D1"].Value = "11";
                        eachPersonSheet.Cells["D1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["E1"].Value = "12";
                        eachPersonSheet.Cells["E1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["F1"].Value = "1";
                        eachPersonSheet.Cells["F1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["G1"].Value = "2";
                        eachPersonSheet.Cells["G1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["H1"].Value = "3";
                        eachPersonSheet.Cells["H1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["I1"].Value = "4";
                        eachPersonSheet.Cells["I1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["J1"].Value = "5";
                        eachPersonSheet.Cells["J1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["K1"].Value = "6";
                        eachPersonSheet.Cells["K1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["L1"].Value = "7";
                        eachPersonSheet.Cells["L1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["M1"].Value = "8";
                        eachPersonSheet.Cells["M1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        eachPersonSheet.Cells["N1"].Value = "9";
                        eachPersonSheet.Cells["N1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        List<DownloadApproveHistoryViewModal> objEachPersonList = new List<DownloadApproveHistoryViewModal>();
                        int countEachPerson = 2;
                        foreach (var item in distinctAssignmentId)
                        {
                            ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();
                            AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                            _approvalHistoryViewModal = forecastBLL.GetApprovalNamesForHistory(item, hid_approve_timestamp_id);

                            //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                            var employeeName = _approvalHistoryViewModal.EmployeeName;
                            var employeeId = _approvalHistoryViewModal.EmployeeId;
                            var sectionName = _approvalHistoryViewModal.SectionName;
                            var departmentName = _approvalHistoryViewModal.DepartmentName;
                            var inChargeName = _approvalHistoryViewModal.InChargeName;
                            var roleName = _approvalHistoryViewModal.RoleName;
                            var explanationName = _approvalHistoryViewModal.ExplanationName;
                            var companyName = _approvalHistoryViewModal.CompanyName;
                            var gradePoints = _approvalHistoryViewModal.GradePoints;
                            var unitPrice = _approvalHistoryViewModal.UnitPrice;
                            var remarks = _approvalHistoryViewModal.Remarks;
                            var isUpdate = _approvalHistoryViewModal.IsUpdate;
                            var isDeleteRow = _approvalHistoryViewModal.IsDeleteEmployee;
                            var isAddRow = _approvalHistoryViewModal.IsAddEmployee;
                            var isUpdateCells = _approvalHistoryViewModal.IsCellWiseUpdate;

                            var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                            var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                            var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                            var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                            var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                            var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                            var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                            var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                            var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                            var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                            var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                            var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                            var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                            var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);

                            _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

                            var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
                            var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
                            var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
                            var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
                            var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
                            var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
                            var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
                            var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
                            var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
                            var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
                            var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
                            var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

                            if (countEachPerson == 2)
                            {
                                DownloadApproveHistoryViewModal eachPerson = new DownloadApproveHistoryViewModal();

                                eachPerson.EmployeeName = employeeName;
                                eachPerson.EmployeeId = employeeId;
                                eachPerson.DepartmentName = departmentName;

                                eachPerson.OctPoints = octP;
                                eachPerson.NovPoints = novP;
                                eachPerson.DecPoints = decP;
                                eachPerson.JanPoints = janP;
                                eachPerson.FebPoints = febP;
                                eachPerson.MarPoints = marP;
                                eachPerson.AprPoints = aprP;
                                eachPerson.MayPoints = mayP;
                                eachPerson.JunPoints = junP;
                                eachPerson.JulPoints = julP;
                                eachPerson.AugPoints = augP;
                                eachPerson.SepPoints = sepP;
                                objEachPersonList.Add(eachPerson);
                            }
                            else
                            {
                                bool isSamePersonForEachPerson = false;

                                foreach(var eachPersonFromItem in objEachPersonList)
                                {
                                    if(eachPersonFromItem.EmployeeId == employeeId)
                                    {
                                        isSamePersonForEachPerson = true;
                                        eachPersonFromItem.EmployeeName = employeeName;
                                        eachPersonFromItem.EmployeeId = employeeId;
                                        eachPersonFromItem.DepartmentName = departmentName;

                                        eachPersonFromItem.OctPoints = eachPersonFromItem.OctPoints + octP;
                                        eachPersonFromItem.NovPoints = eachPersonFromItem.NovPoints + novP;
                                        eachPersonFromItem.DecPoints = eachPersonFromItem.DecPoints + decP;
                                        eachPersonFromItem.JanPoints = eachPersonFromItem.JanPoints + janP;
                                        eachPersonFromItem.FebPoints = eachPersonFromItem.FebPoints + febP;
                                        eachPersonFromItem.MarPoints = eachPersonFromItem.MarPoints + marP;
                                        eachPersonFromItem.AprPoints = eachPersonFromItem.AprPoints + aprP;
                                        eachPersonFromItem.MayPoints = eachPersonFromItem.MayPoints + mayP;
                                        eachPersonFromItem.JunPoints = eachPersonFromItem.JunPoints + junP;
                                        eachPersonFromItem.JulPoints = eachPersonFromItem.JulPoints + julP;
                                        eachPersonFromItem.AugPoints = eachPersonFromItem.AugPoints + augP;
                                        eachPersonFromItem.SepPoints = eachPersonFromItem.SepPoints + sepP;                                        
                                    }                                    
                                }

                                if (!isSamePersonForEachPerson)
                                {
                                    DownloadApproveHistoryViewModal eachPerson2 = new DownloadApproveHistoryViewModal();

                                    eachPerson2.EmployeeName = employeeName;
                                    eachPerson2.EmployeeId = employeeId;
                                    eachPerson2.DepartmentName = departmentName;

                                    eachPerson2.OctPoints = octP;
                                    eachPerson2.NovPoints = novP;
                                    eachPerson2.DecPoints = decP;
                                    eachPerson2.JanPoints = janP;
                                    eachPerson2.FebPoints = febP;
                                    eachPerson2.MarPoints = marP;
                                    eachPerson2.AprPoints = aprP;
                                    eachPerson2.MayPoints = mayP;
                                    eachPerson2.JunPoints = junP;
                                    eachPerson2.JulPoints = julP;
                                    eachPerson2.AugPoints = augP;
                                    eachPerson2.SepPoints = sepP;
                                    objEachPersonList.Add(eachPerson2);
                                }
                            }

                            //if (isUpdate)
                            //{
                            //    eachPersonSheet.Cells["A" + countEachPerson].Value = historyList[0].CreatedBy;
                            //    eachPersonSheet.Cells["B" + countEachPerson].Value = employeeName;
                            //    eachPersonSheet.Cells["C" + countEachPerson].Value = "Updated";
                            //    eachPersonSheet.Cells["D" + countEachPerson].Value = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks;
                            //    eachPersonSheet.Cells["E" + countEachPerson].Value = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName;
                            //    eachPersonSheet.Cells["F" + countEachPerson].Value = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName;
                            //    eachPersonSheet.Cells["G" + countEachPerson].Value = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName;
                            //    eachPersonSheet.Cells["H" + countEachPerson].Value = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName;
                            //    eachPersonSheet.Cells["I" + countEachPerson].Value = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName;
                            //    eachPersonSheet.Cells["J" + countEachPerson].Value = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName;
                            //    eachPersonSheet.Cells["K" + countEachPerson].Value = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints;
                            //    eachPersonSheet.Cells["L" + countEachPerson].Value = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice;

                            //    eachPersonSheet.Cells["M" + countEachPerson].Value = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["N" + countEachPerson].Value = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["O" + countEachPerson].Value = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["P" + countEachPerson].Value = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["Q" + countEachPerson].Value = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["R" + countEachPerson].Value = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["S" + countEachPerson].Value = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["T" + countEachPerson].Value = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["U" + countEachPerson].Value = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["V" + countEachPerson].Value = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["W" + countEachPerson].Value = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0");
                            //    eachPersonSheet.Cells["X" + countEachPerson].Value = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0");
                            //}
                            //else
                            //{
                            //    eachPersonSheet.Cells["A" + countEachPerson].Value = historyList[0].CreatedBy;
                            //    eachPersonSheet.Cells["B" + countEachPerson].Value = employeeName;
                            //    eachPersonSheet.Cells["C" + countEachPerson].Value = "Inserted";
                            //    eachPersonSheet.Cells["D" + countEachPerson].Value = remarks == "" ? "" : remarks;
                            //    eachPersonSheet.Cells["E" + countEachPerson].Value = sectionName == "" ? "" : sectionName;
                            //    eachPersonSheet.Cells["F" + countEachPerson].Value = departmentName == "" ? "" : departmentName;
                            //    eachPersonSheet.Cells["G" + countEachPerson].Value = inChargeName == "" ? "" : inChargeName;
                            //    eachPersonSheet.Cells["H" + countEachPerson].Value = roleName == "" ? "" : roleName;
                            //    eachPersonSheet.Cells["I" + countEachPerson].Value = explanationName == "" ? "" : explanationName;
                            //    eachPersonSheet.Cells["J" + countEachPerson].Value = companyName == "" ? "" : companyName;
                            //    eachPersonSheet.Cells["K" + countEachPerson].Value = gradePoints == "0" ? "" : gradePoints;
                            //    eachPersonSheet.Cells["L" + countEachPerson].Value = unitPrice == "0" ? "" : unitPrice;

                            //    eachPersonSheet.Cells["M" + countEachPerson].Value = octP == 0 ? "" : octP.ToString("0.0");
                            //    eachPersonSheet.Cells["N" + countEachPerson].Value = novP == 0 ? "" : novP.ToString("0.0");
                            //    eachPersonSheet.Cells["O" + countEachPerson].Value = decP == 0 ? "" : decP.ToString("0.0");
                            //    eachPersonSheet.Cells["P" + countEachPerson].Value = janP == 0 ? "" : janP.ToString("0.0");
                            //    eachPersonSheet.Cells["Q" + countEachPerson].Value = febP == 0 ? "" : febP.ToString("0.0");
                            //    eachPersonSheet.Cells["R" + countEachPerson].Value = marP == 0 ? "" : marP.ToString("0.0");
                            //    eachPersonSheet.Cells["S" + countEachPerson].Value = aprP == 0 ? "" : aprP.ToString("0.0");
                            //    eachPersonSheet.Cells["T" + countEachPerson].Value = mayP == 0 ? "" : mayP.ToString("0.0");
                            //    eachPersonSheet.Cells["U" + countEachPerson].Value = junP == 0 ? "" : junP.ToString("0.0");
                            //    eachPersonSheet.Cells["V" + countEachPerson].Value = julP == 0 ? "" : julP.ToString("0.0");
                            //    eachPersonSheet.Cells["W" + countEachPerson].Value = augP == 0 ? "" : augP.ToString("0.0");
                            //    eachPersonSheet.Cells["X" + countEachPerson].Value = sepP == 0 ? "" : sepP.ToString("0.0");
                            //}

                            countEachPerson++;
                        }
                        
                        if (objEachPersonList.Count > 0)
                        {
                            int eachPersonIndex = 2;
                            foreach (var eachItem in objEachPersonList)
                            {
                                eachPersonSheet.Cells["A" + eachPersonIndex].Value = eachItem.EmployeeName;
                                eachPersonSheet.Cells["B" + eachPersonIndex].Value = eachItem.DepartmentName;
                                
                                eachPersonSheet.Cells["C" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                                eachPersonSheet.Cells["D" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                                eachPersonSheet.Cells["E" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                                eachPersonSheet.Cells["F" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                                eachPersonSheet.Cells["G" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                                eachPersonSheet.Cells["H" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                                eachPersonSheet.Cells["I" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                                eachPersonSheet.Cells["J" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                                eachPersonSheet.Cells["K" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                                eachPersonSheet.Cells["L" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                                eachPersonSheet.Cells["M" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                                eachPersonSheet.Cells["N" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

                                eachPersonIndex++;
                            }
                        }
                        //*****************Download: Each Person: End***********************//


                        //*****************Download: Distributed: Start***********************//
                        //var sheetSalaryMaster = package.Workbook.Worksheets.Add("Download(Distributed)");
                        //sheetSalaryMaster.Cells["A1"].Value = "利用者";
                        //sheetSalaryMaster.Cells["A1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["B1"].Value = "従業員名(Emp)";
                        //sheetSalaryMaster.Cells["B1"].Style.Font.Bold = true; ;
                        //sheetSalaryMaster.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["C1"].Value = "Operation Type";
                        //sheetSalaryMaster.Cells["C1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["D1"].Value = "Remaks";
                        //sheetSalaryMaster.Cells["D1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        //sheetSalaryMaster.Cells["E1"].Value = "区分(Section)	";
                        //sheetSalaryMaster.Cells["E1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        //sheetSalaryMaster.Cells["F1"].Value = "部署(Dept)";
                        //sheetSalaryMaster.Cells["F1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                        //sheetSalaryMaster.Cells["G1"].Value = "担当作業(In chg)	";
                        //sheetSalaryMaster.Cells["G1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["H1"].Value = "役割(Role)";
                        //sheetSalaryMaster.Cells["H1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["I1"].Value = "説明(expl)";
                        //sheetSalaryMaster.Cells["I1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["J1"].Value = "会社(Com)	";
                        //sheetSalaryMaster.Cells["J1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["K1"].Value = "グレード(Grade)";
                        //sheetSalaryMaster.Cells["K1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["L1"].Value = "単価(Unit Price)	";
                        //sheetSalaryMaster.Cells["L1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["M1"].Value = "10";
                        //sheetSalaryMaster.Cells["M1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["N1"].Value = "11";
                        //sheetSalaryMaster.Cells["N1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["O1"].Value = "12";
                        //sheetSalaryMaster.Cells["O1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["P1"].Value = "1";
                        //sheetSalaryMaster.Cells["P1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["Q1"].Value = "2";
                        //sheetSalaryMaster.Cells["Q1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["R1"].Value = "3";
                        //sheetSalaryMaster.Cells["R1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["S1"].Value = "4";
                        //sheetSalaryMaster.Cells["S1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["T1"].Value = "5";
                        //sheetSalaryMaster.Cells["T1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["U1"].Value = "6";
                        //sheetSalaryMaster.Cells["U1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["V1"].Value = "7";
                        //sheetSalaryMaster.Cells["V1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["W1"].Value = "8";
                        //sheetSalaryMaster.Cells["W1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["W1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //sheetSalaryMaster.Cells["X1"].Value = "9";
                        //sheetSalaryMaster.Cells["X1"].Style.Font.Bold = true;
                        //sheetSalaryMaster.Cells["X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //sheetSalaryMaster.Cells["X1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                        //int countDistributed = 2;
                        //foreach (var item in distinctAssignmentId)
                        //{
                        //    ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();
                        //    AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                        //    _approvalHistoryViewModal = forecastBLL.GetApprovalNamesForHistory(item, hid_approve_timestamp_id);

                        //    //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                        //    var employeeName = _approvalHistoryViewModal.EmployeeName;
                        //    var sectionName = _approvalHistoryViewModal.SectionName;
                        //    var departmentName = _approvalHistoryViewModal.DepartmentName;
                        //    var inChargeName = _approvalHistoryViewModal.InChargeName;
                        //    var roleName = _approvalHistoryViewModal.RoleName;
                        //    var explanationName = _approvalHistoryViewModal.ExplanationName;
                        //    var companyName = _approvalHistoryViewModal.CompanyName;
                        //    var gradePoints = _approvalHistoryViewModal.GradePoints;
                        //    var unitPrice = _approvalHistoryViewModal.UnitPrice;
                        //    var remarks = _approvalHistoryViewModal.Remarks;
                        //    var isUpdate = _approvalHistoryViewModal.IsUpdate;
                        //    var isDeleteRow = _approvalHistoryViewModal.IsDeleteEmployee;
                        //    var isAddRow = _approvalHistoryViewModal.IsAddEmployee;
                        //    var isUpdateCells = _approvalHistoryViewModal.IsCellWiseUpdate;

                        //    var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                        //    var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                        //    var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                        //    var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                        //    var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                        //    var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                        //    var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                        //    var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                        //    var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                        //    var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                        //    var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                        //    var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                        //    var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                        //    var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);

                        //    _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

                        //    var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
                        //    var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
                        //    var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
                        //    var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
                        //    var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
                        //    var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
                        //    var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
                        //    var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
                        //    var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
                        //    var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
                        //    var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
                        //    var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

                        //    if (isUpdate)
                        //    {
                        //        sheetSalaryMaster.Cells["A" + countDistributed].Value = historyList[0].CreatedBy;
                        //        sheetSalaryMaster.Cells["B" + countDistributed].Value = employeeName;
                        //        sheetSalaryMaster.Cells["C" + countDistributed].Value = "Updated";
                        //        sheetSalaryMaster.Cells["D" + countDistributed].Value = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks;
                        //        sheetSalaryMaster.Cells["E" + countDistributed].Value = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName;
                        //        sheetSalaryMaster.Cells["F" + countDistributed].Value = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName;
                        //        sheetSalaryMaster.Cells["G" + countDistributed].Value = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName;
                        //        sheetSalaryMaster.Cells["H" + countDistributed].Value = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName;
                        //        sheetSalaryMaster.Cells["I" + countDistributed].Value = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName;
                        //        sheetSalaryMaster.Cells["J" + countDistributed].Value = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName;
                        //        sheetSalaryMaster.Cells["K" + countDistributed].Value = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints;
                        //        sheetSalaryMaster.Cells["L" + countDistributed].Value = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice;

                        //        sheetSalaryMaster.Cells["M" + countDistributed].Value = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["N" + countDistributed].Value = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["O" + countDistributed].Value = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["P" + countDistributed].Value = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["Q" + countDistributed].Value = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["R" + countDistributed].Value = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["S" + countDistributed].Value = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["T" + countDistributed].Value = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["U" + countDistributed].Value = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["V" + countDistributed].Value = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["W" + countDistributed].Value = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0");
                        //        sheetSalaryMaster.Cells["X" + countDistributed].Value = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0");
                        //    }
                        //    else
                        //    {
                        //        sheetSalaryMaster.Cells["A" + countDistributed].Value = historyList[0].CreatedBy;
                        //        sheetSalaryMaster.Cells["B" + countDistributed].Value = employeeName;
                        //        sheetSalaryMaster.Cells["C" + countDistributed].Value = "Inserted";
                        //        sheetSalaryMaster.Cells["D" + countDistributed].Value = remarks == "" ? "" : remarks;
                        //        sheetSalaryMaster.Cells["E" + countDistributed].Value = sectionName == "" ? "" : sectionName;
                        //        sheetSalaryMaster.Cells["F" + countDistributed].Value = departmentName == "" ? "" : departmentName;
                        //        sheetSalaryMaster.Cells["G" + countDistributed].Value = inChargeName == "" ? "" : inChargeName;
                        //        sheetSalaryMaster.Cells["H" + countDistributed].Value = roleName == "" ? "" : roleName;
                        //        sheetSalaryMaster.Cells["I" + countDistributed].Value = explanationName == "" ? "" : explanationName;
                        //        sheetSalaryMaster.Cells["J" + countDistributed].Value = companyName == "" ? "" : companyName;
                        //        sheetSalaryMaster.Cells["K" + countDistributed].Value = gradePoints == "0" ? "" : gradePoints;
                        //        sheetSalaryMaster.Cells["L" + countDistributed].Value = unitPrice == "0" ? "" : unitPrice;

                        //        sheetSalaryMaster.Cells["M" + countDistributed].Value = octP == 0 ? "" : octP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["N" + countDistributed].Value = novP == 0 ? "" : novP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["O" + countDistributed].Value = decP == 0 ? "" : decP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["P" + countDistributed].Value = janP == 0 ? "" : janP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["Q" + countDistributed].Value = febP == 0 ? "" : febP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["R" + countDistributed].Value = marP == 0 ? "" : marP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["S" + countDistributed].Value = aprP == 0 ? "" : aprP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["T" + countDistributed].Value = mayP == 0 ? "" : mayP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["U" + countDistributed].Value = junP == 0 ? "" : junP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["V" + countDistributed].Value = julP == 0 ? "" : julP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["W" + countDistributed].Value = augP == 0 ? "" : augP.ToString("0.0");
                        //        sheetSalaryMaster.Cells["X" + countEachPerson].Value = sepP == 0 ? "" : sepP.ToString("0.0");
                        //    }

                        //    countEachPerson++;
                        //}

                        ////*****************Download: Distributed: End***********************//

                        var excelData = package.GetAsByteArray();
                        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var fileName = timeStampName + ".xlsx";
                    
                        return File(excelData, contentType, fileName);
                    }
                }
                else
                {
                    return File("", "", ""); ;
                }
            }
            else
            {
                return File("", "", ""); ;
            }
            //return Ok(forecastHistoryList);
        }
    }
}