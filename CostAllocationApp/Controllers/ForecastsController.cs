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




            //List<object> forecastHistoryList = new List<object>();
            //List<Forecast> historyList = forecastBLL.GetAssignmentHistoriesByTimeStampId(hid_approve_timestamp_id);
            //string timeStampName = forecastBLL.GetHistoryTimeStampName(hid_approve_timestamp_id);
            //var excelData;
            //List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            string timeStampName = "testing";
            if (distinctAssignmentId.Count > 0)
            {
                using (var package = new ExcelPackage())
                {

                    var sheet = package.Workbook.Worksheets.Add("Sheet1");
                    sheet.Cells["A1"].Value = "利用者";
                    sheet.Cells["A1"].Style.Font.Bold = true;
                    sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["B1"].Value = "従業員名(Emp)";
                    sheet.Cells["B1"].Style.Font.Bold = true; ;
                    sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["C1"].Value = "Operation Type";
                    sheet.Cells["C1"].Style.Font.Bold = true;
                    sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["D1"].Value = "Remaks";
                    sheet.Cells["D1"].Style.Font.Bold = true;
                    sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                    sheet.Cells["E1"].Value = "区分(Section)	";
                    sheet.Cells["E1"].Style.Font.Bold = true;
                    sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                    sheet.Cells["F1"].Value = "部署(Dept)";
                    sheet.Cells["F1"].Style.Font.Bold = true;
                    sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


                    sheet.Cells["G1"].Value = "担当作業(In chg)	";
                    sheet.Cells["G1"].Style.Font.Bold = true;
                    sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["H1"].Value = "役割(Role)";
                    sheet.Cells["H1"].Style.Font.Bold = true;
                    sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["I1"].Value = "説明(expl)";
                    sheet.Cells["I1"].Style.Font.Bold = true;
                    sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["J1"].Value = "会社(Com)	";
                    sheet.Cells["J1"].Style.Font.Bold = true;
                    sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["K1"].Value = "グレード(Grade)";
                    sheet.Cells["K1"].Style.Font.Bold = true;
                    sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["L1"].Value = "単価(Unit Price)	";
                    sheet.Cells["L1"].Style.Font.Bold = true;
                    sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["M1"].Value = "10";
                    sheet.Cells["M1"].Style.Font.Bold = true;
                    sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["N1"].Value = "11";
                    sheet.Cells["N1"].Style.Font.Bold = true;
                    sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["O1"].Value = "12";
                    sheet.Cells["O1"].Style.Font.Bold = true;
                    sheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["P1"].Value = "1";
                    sheet.Cells["P1"].Style.Font.Bold = true;
                    sheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["Q1"].Value = "2";
                    sheet.Cells["Q1"].Style.Font.Bold = true;
                    sheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["R1"].Value = "3";
                    sheet.Cells["R1"].Style.Font.Bold = true;
                    sheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["S1"].Value = "4";
                    sheet.Cells["S1"].Style.Font.Bold = true;
                    sheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["T1"].Value = "5";
                    sheet.Cells["T1"].Style.Font.Bold = true;
                    sheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["U1"].Value = "6";
                    sheet.Cells["U1"].Style.Font.Bold = true;
                    sheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["V1"].Value = "7";
                    sheet.Cells["V1"].Style.Font.Bold = true;
                    sheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["W1"].Value = "8";
                    sheet.Cells["W1"].Style.Font.Bold = true;
                    sheet.Cells["W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["W1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    sheet.Cells["X1"].Value = "9";
                    sheet.Cells["X1"].Style.Font.Bold = true;
                    sheet.Cells["X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["X1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

                    int count = 2;
                    foreach (var item in distinctAssignmentId)
                    {
                        ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();
                        AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                        _approvalHistoryViewModal = forecastBLL.GetApprovalNamesForHistory(item, hid_approve_timestamp_id);

                        //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                        var employeeName = _approvalHistoryViewModal.EmployeeName;
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

                        if (isUpdate)
                        {
                            sheet.Cells["A" + count].Value = historyList[0].CreatedBy;
                            sheet.Cells["B" + count].Value = employeeName;
                            sheet.Cells["C" + count].Value = "Updated";
                            sheet.Cells["D" + count].Value = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks;
                            sheet.Cells["E" + count].Value = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName;
                            sheet.Cells["F" + count].Value = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName;
                            sheet.Cells["G" + count].Value = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName;
                            sheet.Cells["H" + count].Value = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName;
                            sheet.Cells["I" + count].Value = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName;
                            sheet.Cells["J" + count].Value = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName;
                            sheet.Cells["K" + count].Value = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints;
                            sheet.Cells["L" + count].Value = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice;

                            sheet.Cells["M" + count].Value = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0");
                            sheet.Cells["N" + count].Value = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0");
                            sheet.Cells["O" + count].Value = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0");
                            sheet.Cells["P" + count].Value = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0");
                            sheet.Cells["Q" + count].Value = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0");
                            sheet.Cells["R" + count].Value = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0");
                            sheet.Cells["S" + count].Value = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0");
                            sheet.Cells["T" + count].Value = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0");
                            sheet.Cells["U" + count].Value = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0");
                            sheet.Cells["V" + count].Value = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0");
                            sheet.Cells["W" + count].Value = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0");
                            sheet.Cells["X" + count].Value = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0");                            
                        }
                        else
                        {
                            sheet.Cells["A" + count].Value = historyList[0].CreatedBy;
                            sheet.Cells["B" + count].Value = employeeName;
                            sheet.Cells["C" + count].Value = "Inserted";
                            sheet.Cells["D" + count].Value = remarks == "" ? "" : remarks;
                            sheet.Cells["E" + count].Value = sectionName == "" ? "" : sectionName;
                            sheet.Cells["F" + count].Value = departmentName == "" ? "" : departmentName;
                            sheet.Cells["G" + count].Value = inChargeName == "" ? "" : inChargeName;
                            sheet.Cells["H" + count].Value = roleName == "" ? "" : roleName;
                            sheet.Cells["I" + count].Value = explanationName == "" ? "" : explanationName;
                            sheet.Cells["J" + count].Value = companyName == "" ? "" : companyName;
                            sheet.Cells["K" + count].Value = gradePoints == "0" ? "" : gradePoints;
                            sheet.Cells["L" + count].Value = unitPrice == "0" ? "" : unitPrice;

                            sheet.Cells["M" + count].Value = octP == 0 ? "" : octP.ToString("0.0");
                            sheet.Cells["N" + count].Value = novP == 0 ? "" : novP.ToString("0.0");
                            sheet.Cells["O" + count].Value = decP == 0 ? "" : decP.ToString("0.0");
                            sheet.Cells["P" + count].Value = janP == 0 ? "" : janP.ToString("0.0");
                            sheet.Cells["Q" + count].Value = febP == 0 ? "" : febP.ToString("0.0");
                            sheet.Cells["R" + count].Value = marP == 0 ? "" : marP.ToString("0.0");
                            sheet.Cells["S" + count].Value = aprP == 0 ? "" : aprP.ToString("0.0");
                            sheet.Cells["T" + count].Value = mayP == 0 ? "" : mayP.ToString("0.0");
                            sheet.Cells["U" + count].Value = junP == 0 ? "" : junP.ToString("0.0");
                            sheet.Cells["V" + count].Value = julP == 0 ? "" : julP.ToString("0.0");
                            sheet.Cells["W" + count].Value = augP == 0 ? "" : augP.ToString("0.0");
                            sheet.Cells["X" + count].Value = sepP == 0 ? "" : sepP.ToString("0.0");                            
                        }

                        count++;
                    }

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

            //return Ok(forecastHistoryList);
        }
    }
}