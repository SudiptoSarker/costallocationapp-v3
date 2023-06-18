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
        public ActionResult DownloadHistoryData(int hid_approve_timestamp_id=0,string hid_selected_year="")
        {
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();
            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            if (!string.IsNullOrEmpty(hid_selected_year))
            {
                employeeAssignment.Year = hid_selected_year;
                forecastAssignmentViewModels = employeeAssignmentBLL.GetAllOriginalDataForDownloadFiles(employeeAssignment);
            }
            
            ForecastBLL forecastBLL = new ForecastBLL();
            string timeStampName = forecastBLL.GetApproveHistoryTimeStampName(hid_approve_timestamp_id);


            if (!string.IsNullOrEmpty(timeStampName)) { 
                if (forecastAssignmentViewModels.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        //*****************Download: Original: Start***********************//
                        var sheet = package.Workbook.Worksheets.Add("Download(original)");

                        sheet.Cells["A1"].Value = "区分(Section)	";
                        sheet.Cells["A1"].Style.Font.Bold = true;
                        sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["A1"].AutoFitColumns();

                        sheet.Cells["B1"].Value = "部署(Dept)";
                        sheet.Cells["B1"].Style.Font.Bold = true;
                        sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["B1"].AutoFitColumns();

                        sheet.Cells["C1"].Value = "担当作業(In chg)	";
                        sheet.Cells["C1"].Style.Font.Bold = true;
                        sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["C1"].AutoFitColumns();

                        sheet.Cells["D1"].Value = "役割(Role)";
                        sheet.Cells["D1"].Style.Font.Bold = true;
                        sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["D1"].AutoFitColumns();

                        sheet.Cells["E1"].Value = "説明(expl)";
                        sheet.Cells["E1"].Style.Font.Bold = true;
                        sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["E1"].AutoFitColumns();

                        sheet.Cells["F1"].Value = "従業員名(Emp)";
                        sheet.Cells["F1"].Style.Font.Bold = true; ;
                        sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["F1"].AutoFitColumns();

                        sheet.Cells["G1"].Value = "Remaks";
                        sheet.Cells["G1"].Style.Font.Bold = true;
                        sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["G1"].AutoFitColumns();

                        sheet.Cells["H1"].Value = "会社(Com)	";
                        sheet.Cells["H1"].Style.Font.Bold = true;
                        sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["H1"].AutoFitColumns();

                        sheet.Cells["I1"].Value = "グレード(Grade)";
                        sheet.Cells["I1"].Style.Font.Bold = true;
                        sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["I1"].AutoFitColumns();

                        sheet.Cells["J1"].Value = "単価(Unit Price)	";
                        sheet.Cells["J1"].Style.Font.Bold = true;
                        sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["J1"].AutoFitColumns();


                        sheet.Cells["K1"].Value = "10月";
                        sheet.Cells["K1"].Style.Font.Bold = true;
                        sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["K1"].AutoFitColumns();

                        sheet.Cells["L1"].Value = "11月";
                        sheet.Cells["L1"].Style.Font.Bold = true;
                        sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["L1"].AutoFitColumns();

                        sheet.Cells["M1"].Value = "12月";
                        sheet.Cells["M1"].Style.Font.Bold = true;
                        sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["M1"].AutoFitColumns();

                        sheet.Cells["N1"].Value = "1月";
                        sheet.Cells["N1"].Style.Font.Bold = true;
                        sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["N1"].AutoFitColumns();

                        sheet.Cells["O1"].Value = "2月";
                        sheet.Cells["O1"].Style.Font.Bold = true;
                        sheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["O1"].AutoFitColumns();

                        sheet.Cells["P1"].Value = "3月";
                        sheet.Cells["P1"].Style.Font.Bold = true;
                        sheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["P1"].AutoFitColumns();

                        sheet.Cells["Q1"].Value = "4月";
                        sheet.Cells["Q1"].Style.Font.Bold = true;
                        sheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["Q1"].AutoFitColumns();

                        sheet.Cells["R1"].Value = "5月";
                        sheet.Cells["R1"].Style.Font.Bold = true;
                        sheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["R1"].AutoFitColumns();

                        sheet.Cells["S1"].Value = "6月";
                        sheet.Cells["S1"].Style.Font.Bold = true;
                        sheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["S1"].AutoFitColumns();

                        sheet.Cells["T1"].Value = "7月";
                        sheet.Cells["T1"].Style.Font.Bold = true;
                        sheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["T1"].AutoFitColumns();

                        sheet.Cells["U1"].Value = "8月";
                        sheet.Cells["U1"].Style.Font.Bold = true;
                        sheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["U1"].AutoFitColumns();

                        sheet.Cells["V1"].Value = "9月";
                        sheet.Cells["V1"].Style.Font.Bold = true;
                        sheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        sheet.Cells["V1"].AutoFitColumns();
                        
                        int count = 2;
                        foreach (var item in forecastAssignmentViewModels)
                        {                                                      
                            var employeeName = item.EmployeeName;
                            var rootEmployeeName = item.RootEmployeeName;
                            var employeeId = item.EmployeeId;
                            var sectionName = item.SectionName;
                            var departmentName = item.DepartmentName;
                            var inChargeName = item.InchargeName;
                            var roleName = item.RoleName;
                            var explanationName = item.ExplanationName;
                            var companyName = item.CompanyName;
                            var gradePoints = item.GradePoint;
                            var unitPrice = item.UnitPrice;
                            var remarks = item.Remarks;   
                            
                            var isDeleteRow = item.IsDeleteEmployee;
                            if(rootEmployeeName == "太田 飛鳥")
                            {
                                var testTm = "test";
                                //test
                            }
                            var isAddRow = item.IsAddEmployee;
                            var isUpdateCells = item.IsCellWiseUpdate;
                            var approvedCells = item.ApprovedCells;

                            var octPOriginal = item.OctPoints;
                            var novPOriginal = item.NovPoints;
                            var decPOriginal = item.DecPoints;
                            var janPOriginal = item.JanPoints;
                            var febPOriginal = item.FebPoints;
                            var marPOriginal = item.MarPoints;
                            var aprPOriginal = item.AprPoints;
                            var mayPOriginal = item.MayPoints;
                            var junPOriginal = item.JunPoints;
                            var julPOriginal = item.JulPoints;
                            var augPOriginal = item.AugPoints;
                            var sepPOriginal = item.SepPoints;
                            if (isAddRow)
                            {
                                sheet.Cells["A" + count].Value = sectionName;
                                sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["A" + count].AutoFitColumns();

                                sheet.Cells["B" + count].Value = departmentName;
                                sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["B" + count].AutoFitColumns();

                                sheet.Cells["C" + count].Value = inChargeName;
                                sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["C" + count].AutoFitColumns();

                                sheet.Cells["D" + count].Value = roleName;
                                sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["D" + count].AutoFitColumns();

                                sheet.Cells["E" + count].Value = explanationName;
                                sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["E" + count].AutoFitColumns();

                                sheet.Cells["F" + count].Value = employeeName;
                                sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["F" + count].AutoFitColumns();

                                sheet.Cells["G" + count].Value = remarks;
                                sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["G" + count].AutoFitColumns();

                                sheet.Cells["H" + count].Value = companyName;
                                sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["H" + count].AutoFitColumns();

                                sheet.Cells["I" + count].Value = gradePoints;
                                sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["I" + count].AutoFitColumns();

                                sheet.Cells["J" + count].Value = unitPrice;
                                sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["J" + count].AutoFitColumns();

                                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["K" + count].AutoFitColumns();

                                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["L" + count].AutoFitColumns();

                                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["M" + count].AutoFitColumns();

                                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["N" + count].AutoFitColumns();

                                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["O" + count].AutoFitColumns();

                                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["P" + count].AutoFitColumns();

                                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["Q" + count].AutoFitColumns();

                                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["R" + count].AutoFitColumns();

                                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["S" + count].AutoFitColumns();

                                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["T" + count].AutoFitColumns();

                                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["U" + count].AutoFitColumns();

                                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                                sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                sheet.Cells["V" + count].AutoFitColumns();
                            }
                            else if (isDeleteRow)
                            {
                                sheet.Cells["A" + count].Value = sectionName;
                                sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["A" + count].AutoFitColumns();

                                sheet.Cells["B" + count].Value = departmentName;
                                sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["B" + count].AutoFitColumns();

                                sheet.Cells["C" + count].Value = inChargeName;
                                sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["C" + count].AutoFitColumns();

                                sheet.Cells["D" + count].Value = roleName;
                                sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["D" + count].AutoFitColumns();

                                sheet.Cells["E" + count].Value = explanationName;
                                sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["E" + count].AutoFitColumns();

                                sheet.Cells["F" + count].Value = employeeName;
                                sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["F" + count].AutoFitColumns();

                                sheet.Cells["G" + count].Value = remarks;
                                sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["G" + count].AutoFitColumns();

                                sheet.Cells["H" + count].Value = companyName;
                                sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["H" + count].AutoFitColumns();

                                sheet.Cells["I" + count].Value = gradePoints;
                                sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["I" + count].AutoFitColumns();

                                sheet.Cells["J" + count].Value = unitPrice;
                                sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["J" + count].AutoFitColumns();

                                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["K" + count].AutoFitColumns();

                                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["L" + count].AutoFitColumns();

                                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["M" + count].AutoFitColumns();

                                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["N" + count].AutoFitColumns();

                                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["O" + count].AutoFitColumns();

                                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["P" + count].AutoFitColumns();

                                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["Q" + count].AutoFitColumns();

                                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["R" + count].AutoFitColumns();

                                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["S" + count].AutoFitColumns();

                                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["T" + count].AutoFitColumns();

                                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["U" + count].AutoFitColumns();

                                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                                sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                sheet.Cells["V" + count].AutoFitColumns();
                            }
                            else if (!string.IsNullOrEmpty(approvedCells))
                            {
                                bool isSectionApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("3", approvedCells);
                                if (isSectionApproved)
                                {
                                    sheet.Cells["A" + count].Value = sectionName;
                                    sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["A" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["A" + count].Value = sectionName;
                                    sheet.Cells["A" + count].AutoFitColumns();
                                }                                

                                
                                bool isDeptApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("4", approvedCells);
                                if (isDeptApproved)
                                {
                                    sheet.Cells["B" + count].Value = departmentName;
                                    sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["B" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["B" + count].Value = departmentName;
                                    sheet.Cells["B" + count].AutoFitColumns();
                                }
                                
                                bool isInChargeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("5", approvedCells);
                                if (isInChargeApproved)
                                {
                                    sheet.Cells["C" + count].Value = inChargeName;
                                    sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["C" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["C" + count].Value = inChargeName;
                                    sheet.Cells["C" + count].AutoFitColumns();
                                }

                                bool isRoleApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("6", approvedCells);
                                if (isRoleApproved)
                                {
                                    sheet.Cells["D" + count].Value = roleName;
                                    sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["D" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["D" + count].Value = roleName;
                                    sheet.Cells["D" + count].AutoFitColumns();
                                }

                                
                                bool isExplanationApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("7", approvedCells);
                                if (isExplanationApproved)
                                {
                                    sheet.Cells["E" + count].Value = explanationName;
                                    sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["E" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["E" + count].Value = explanationName;
                                    sheet.Cells["E" + count].AutoFitColumns();
                                }
                                
                                bool isEmployeeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("1", approvedCells);
                                if (isEmployeeApproved)
                                {
                                    sheet.Cells["F" + count].Value = employeeName;
                                    sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["F" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["F" + count].Value = employeeName;
                                    sheet.Cells["F" + count].AutoFitColumns();
                                }
                                
                                bool isRemarksApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("2", approvedCells);
                                if (isRemarksApproved)
                                {
                                    sheet.Cells["G" + count].Value = remarks;
                                    sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["G" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["F" + count].Value = employeeName;
                                    sheet.Cells["F" + count].AutoFitColumns();
                                }

                                
                                bool isCompanyApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("8", approvedCells);
                                if (isCompanyApproved)
                                {
                                    sheet.Cells["H" + count].Value = companyName;
                                    sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["H" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["H" + count].Value = companyName;
                                    sheet.Cells["H" + count].AutoFitColumns();
                                }

                                
                                bool isGradeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("9", approvedCells);
                                if (isGradeApproved)
                                {
                                    sheet.Cells["I" + count].Value = gradePoints;
                                    sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["I" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["I" + count].Value = gradePoints;
                                    sheet.Cells["I" + count].AutoFitColumns();
                                }
                                
                                bool isUnitApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("10", approvedCells);
                                if (isUnitApproved)
                                {
                                    sheet.Cells["J" + count].Value = unitPrice;
                                    sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["J" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["J" + count].Value = unitPrice;
                                    sheet.Cells["J" + count].AutoFitColumns();
                                }

                                
                                bool isOctPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("11", approvedCells);
                                if (isOctPApproved)
                                {
                                    sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                    sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["K" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                    sheet.Cells["K" + count].AutoFitColumns();
                                }

                                
                                bool isNovPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("12", approvedCells);
                                if (isNovPApproved)
                                {
                                    sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                    sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["L" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                    sheet.Cells["L" + count].AutoFitColumns();
                                }
                                
                                bool isDecPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("13", approvedCells);
                                if (isDecPApproved)
                                {
                                    sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                    sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["M" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                    sheet.Cells["M" + count].AutoFitColumns();
                                }

                                
                                bool isJanPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("14", approvedCells);
                                if (isJanPApproved)
                                {
                                    sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                    sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["N" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                    sheet.Cells["N" + count].AutoFitColumns();
                                }
                                
                                bool isFebPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("15", approvedCells);
                                if (isFebPApproved)
                                {
                                    sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                    sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["O" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                    sheet.Cells["O" + count].AutoFitColumns();
                                }

                                
                                bool isMarPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("16", approvedCells);
                                if (isMarPApproved)
                                {
                                    sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                    sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["P" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                    sheet.Cells["P" + count].AutoFitColumns();
                                }
                                
                                bool isAprPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("17", approvedCells);
                                if (isAprPApproved)
                                {
                                    sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                    sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["Q" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                    sheet.Cells["Q" + count].AutoFitColumns();
                                }
                                
                                bool isMayPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("18", approvedCells);
                                if (isMayPApproved)
                                {
                                    sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                    sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["R" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                    sheet.Cells["R" + count].AutoFitColumns();
                                }

                                
                                bool isJunPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("19", approvedCells);
                                if (isJunPApproved)
                                {
                                    sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                    sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["S" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                    sheet.Cells["S" + count].AutoFitColumns();
                                }
                                
                                bool isJulPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("20", approvedCells);
                                if (isJulPApproved)
                                {
                                    sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                    sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["T" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                    sheet.Cells["T" + count].AutoFitColumns();
                                }
                                
                                bool isAugPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("21", approvedCells);
                                if (isAugPApproved)
                                {
                                    sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                    sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["U" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                    sheet.Cells["U" + count].AutoFitColumns();
                                }
                                
                                bool isSeptPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("22", approvedCells);
                                if (isSeptPApproved)
                                {
                                    sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                                    sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                    sheet.Cells["V" + count].AutoFitColumns();
                                }
                                else
                                {
                                    sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                                    sheet.Cells["V" + count].AutoFitColumns();
                                }
                            }
                            else
                            {
                                sheet.Cells["A" + count].Value = sectionName;
                                sheet.Cells["A" + count].AutoFitColumns();

                                sheet.Cells["B" + count].Value = departmentName;
                                sheet.Cells["B" + count].AutoFitColumns();

                                sheet.Cells["C" + count].Value = inChargeName;
                                sheet.Cells["C" + count].AutoFitColumns();

                                sheet.Cells["D" + count].Value = roleName;
                                sheet.Cells["D" + count].AutoFitColumns();

                                sheet.Cells["E" + count].Value = explanationName;
                                sheet.Cells["E" + count].AutoFitColumns();

                                sheet.Cells["F" + count].Value = employeeName;
                                sheet.Cells["F" + count].AutoFitColumns();

                                sheet.Cells["G" + count].Value = remarks;
                                sheet.Cells["G" + count].AutoFitColumns();

                                sheet.Cells["H" + count].Value = companyName;
                                sheet.Cells["H" + count].AutoFitColumns();

                                sheet.Cells["I" + count].Value = gradePoints;
                                sheet.Cells["I" + count].AutoFitColumns();

                                sheet.Cells["J" + count].Value = unitPrice;
                                sheet.Cells["J" + count].AutoFitColumns();

                                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                            }
                            
                            count++;
                        }
                       
                        //*****************Download: Original: End***********************//

                        //*****************Download: Each Person: Start***********************//
                        var eachPersonSheet = package.Workbook.Worksheets.Add("Download(Each person)");

                        eachPersonSheet.Cells["A1"].Value = "従業員名(Emp)";
                        eachPersonSheet.Cells["A1"].Style.Font.Bold = true; ;
                        eachPersonSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);                        
                        eachPersonSheet.Cells["A1"].AutoFitColumns();      

                        eachPersonSheet.Cells["B1"].Value = "10";
                        eachPersonSheet.Cells["B1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["B1"].AutoFitColumns();

                        eachPersonSheet.Cells["C1"].Value = "11";
                        eachPersonSheet.Cells["C1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["C1"].AutoFitColumns();

                        eachPersonSheet.Cells["D1"].Value = "12";
                        eachPersonSheet.Cells["D1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["D1"].AutoFitColumns();

                        eachPersonSheet.Cells["E1"].Value = "1";
                        eachPersonSheet.Cells["E1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["E1"].AutoFitColumns();

                        eachPersonSheet.Cells["F1"].Value = "2";
                        eachPersonSheet.Cells["F1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["F1"].AutoFitColumns();

                        eachPersonSheet.Cells["G1"].Value = "3";
                        eachPersonSheet.Cells["G1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["G1"].AutoFitColumns();

                        eachPersonSheet.Cells["H1"].Value = "4";
                        eachPersonSheet.Cells["H1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["H1"].AutoFitColumns();

                        eachPersonSheet.Cells["I1"].Value = "5";
                        eachPersonSheet.Cells["I1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["I1"].AutoFitColumns();

                        eachPersonSheet.Cells["J1"].Value = "6";
                        eachPersonSheet.Cells["J1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["J1"].AutoFitColumns();

                        eachPersonSheet.Cells["K1"].Value = "7";
                        eachPersonSheet.Cells["K1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["K1"].AutoFitColumns();

                        eachPersonSheet.Cells["L1"].Value = "8";
                        eachPersonSheet.Cells["L1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["L1"].AutoFitColumns();

                        eachPersonSheet.Cells["M1"].Value = "9";
                        eachPersonSheet.Cells["M1"].Style.Font.Bold = true;
                        eachPersonSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        eachPersonSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        eachPersonSheet.Cells["M1"].AutoFitColumns();

                        int countEachPerson = 2;
                        List<DownloadApproveHistoryViewModal> objEachPersonList = new List<DownloadApproveHistoryViewModal>();

                        foreach (var item in forecastAssignmentViewModels)
                        {
                            var employeeName = item.EmployeeName;
                            var rootEmployeeName = item.RootEmployeeName;
                            var employeeId = item.EmployeeId;
                            var sectionName = item.SectionName;
                            var departmentName = item.DepartmentName;
                            var inChargeName = item.InchargeName;
                            var roleName = item.RoleName;
                            var explanationName = item.ExplanationName;
                            var companyName = item.CompanyName;
                            var gradePoints = item.GradePoint;
                            var unitPrice = item.UnitPrice;
                            var remarks = item.Remarks;

                            var isDeleteRow = item.IsDeleteEmployee;
                            var isAddRow = item.IsAddEmployee;
                            var isUpdateCells = item.IsCellWiseUpdate;

                            var octPOriginal = item.OctPoints;
                            var novPOriginal = item.NovPoints;
                            var decPOriginal = item.DecPoints;
                            var janPOriginal = item.JanPoints;
                            var febPOriginal = item.FebPoints;
                            var marPOriginal = item.MarPoints;
                            var aprPOriginal = item.AprPoints;
                            var mayPOriginal = item.MayPoints;
                            var junPOriginal = item.JunPoints;
                            var julPOriginal = item.JulPoints;
                            var augPOriginal = item.AugPoints;
                            var sepPOriginal = item.SepPoints;

                            if (countEachPerson == 2)
                            {
                                DownloadApproveHistoryViewModal eachPerson = new DownloadApproveHistoryViewModal();

                                eachPerson.EmployeeName = rootEmployeeName;
                                eachPerson.EmployeeId = employeeId;
                                eachPerson.DepartmentName = departmentName;

                                eachPerson.OctPoints = Convert.ToDecimal(octPOriginal);
                                eachPerson.NovPoints = Convert.ToDecimal(novPOriginal);
                                eachPerson.DecPoints = Convert.ToDecimal(decPOriginal);
                                eachPerson.JanPoints = Convert.ToDecimal(janPOriginal);
                                eachPerson.FebPoints = Convert.ToDecimal(febPOriginal); 
                                eachPerson.MarPoints = Convert.ToDecimal(marPOriginal);
                                eachPerson.AprPoints = Convert.ToDecimal(aprPOriginal);
                                eachPerson.MayPoints = Convert.ToDecimal(mayPOriginal);
                                eachPerson.JunPoints = Convert.ToDecimal(junPOriginal);
                                eachPerson.JulPoints = Convert.ToDecimal(julPOriginal);
                                eachPerson.AugPoints = Convert.ToDecimal(augPOriginal);
                                eachPerson.SepPoints = Convert.ToDecimal(sepPOriginal);

                                objEachPersonList.Add(eachPerson);
                            }
                            else
                            {
                                bool isSamePersonForEachPerson = false;

                                foreach (var eachPersonFromItem in objEachPersonList)
                                {
                                    if (eachPersonFromItem.EmployeeId == employeeId)
                                    {
                                        isSamePersonForEachPerson = true;
                                        eachPersonFromItem.EmployeeName = rootEmployeeName;
                                        eachPersonFromItem.EmployeeId = employeeId;
                                        eachPersonFromItem.DepartmentName = departmentName;

                                        eachPersonFromItem.OctPoints = eachPersonFromItem.OctPoints + Convert.ToDecimal(octPOriginal); 
                                        eachPersonFromItem.NovPoints = eachPersonFromItem.NovPoints + Convert.ToDecimal(novPOriginal); 
                                        eachPersonFromItem.DecPoints = eachPersonFromItem.DecPoints + Convert.ToDecimal(decPOriginal); 
                                        eachPersonFromItem.JanPoints = eachPersonFromItem.JanPoints + Convert.ToDecimal(janPOriginal); 
                                        eachPersonFromItem.FebPoints = eachPersonFromItem.FebPoints + Convert.ToDecimal(febPOriginal); 
                                        eachPersonFromItem.MarPoints = eachPersonFromItem.MarPoints + Convert.ToDecimal(marPOriginal); 
                                        eachPersonFromItem.AprPoints = eachPersonFromItem.AprPoints + Convert.ToDecimal(aprPOriginal); 
                                        eachPersonFromItem.MayPoints = eachPersonFromItem.MayPoints + Convert.ToDecimal(mayPOriginal); 
                                        eachPersonFromItem.JunPoints = eachPersonFromItem.JunPoints + Convert.ToDecimal(junPOriginal);
                                        eachPersonFromItem.JulPoints = eachPersonFromItem.JulPoints + Convert.ToDecimal(julPOriginal); 
                                        eachPersonFromItem.AugPoints = eachPersonFromItem.AugPoints + Convert.ToDecimal(augPOriginal); 
                                        eachPersonFromItem.SepPoints = eachPersonFromItem.SepPoints + Convert.ToDecimal(sepPOriginal); 
                                    }
                                }

                                if (!isSamePersonForEachPerson)
                                {
                                    DownloadApproveHistoryViewModal eachPerson2 = new DownloadApproveHistoryViewModal();

                                    eachPerson2.EmployeeName = rootEmployeeName;
                                    eachPerson2.EmployeeId = employeeId;
                                    eachPerson2.DepartmentName = departmentName;

                                    eachPerson2.OctPoints = Convert.ToDecimal(octPOriginal);
                                    eachPerson2.NovPoints = Convert.ToDecimal(novPOriginal);
                                    eachPerson2.DecPoints = Convert.ToDecimal(decPOriginal);
                                    eachPerson2.JanPoints = Convert.ToDecimal(janPOriginal);
                                    eachPerson2.FebPoints = Convert.ToDecimal(febPOriginal);
                                    eachPerson2.MarPoints = Convert.ToDecimal(marPOriginal);
                                    eachPerson2.AprPoints = Convert.ToDecimal(aprPOriginal);
                                    eachPerson2.MayPoints = Convert.ToDecimal(mayPOriginal);
                                    eachPerson2.JunPoints = Convert.ToDecimal(junPOriginal);
                                    eachPerson2.JulPoints = Convert.ToDecimal(julPOriginal);
                                    eachPerson2.AugPoints = Convert.ToDecimal(augPOriginal);
                                    eachPerson2.SepPoints = Convert.ToDecimal(sepPOriginal);

                                    objEachPersonList.Add(eachPerson2);
                                }
                            }                            
                            countEachPerson++;
                        }

                        if (objEachPersonList.Count > 0)
                        {
                            int eachPersonIndex = 2;
                            foreach (var eachItem in objEachPersonList)
                            {
                                eachPersonSheet.Cells["A" + eachPersonIndex].Value = eachItem.EmployeeName;
                                //eachPersonSheet.Cells["B" + eachPersonIndex].Value = eachItem.DepartmentName;

                                eachPersonSheet.Cells["B" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                                eachPersonSheet.Cells["C" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                                eachPersonSheet.Cells["D" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                                eachPersonSheet.Cells["E" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                                eachPersonSheet.Cells["F" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                                eachPersonSheet.Cells["G" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                                eachPersonSheet.Cells["H" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                                eachPersonSheet.Cells["I" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                                eachPersonSheet.Cells["J" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                                eachPersonSheet.Cells["K" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                                eachPersonSheet.Cells["L" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                                eachPersonSheet.Cells["M" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

                                eachPersonIndex++;
                            }
                        }

                        //*****************Download: Each Person: End***********************//


                        //*****************Download: Distributed: Start***********************//
                        var distributedWorksheet = package.Workbook.Worksheets.Add("Download(Distributed)");

                        distributedWorksheet.Cells["A1"].Value = "区分(Section)	";
                        distributedWorksheet.Cells["A1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["A1"].AutoFitColumns();

                        distributedWorksheet.Cells["B1"].Value = "部署(Dept)";
                        distributedWorksheet.Cells["B1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["B1"].AutoFitColumns();

                        distributedWorksheet.Cells["C1"].Value = "担当作業(In chg)	";
                        distributedWorksheet.Cells["C1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["C1"].AutoFitColumns();

                        distributedWorksheet.Cells["D1"].Value = "役割(Role)";
                        distributedWorksheet.Cells["D1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["D1"].AutoFitColumns();

                        distributedWorksheet.Cells["E1"].Value = "説明(expl)";
                        distributedWorksheet.Cells["E1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["E1"].AutoFitColumns();

                        distributedWorksheet.Cells["F1"].Value = "従業員名(Emp)";
                        distributedWorksheet.Cells["F1"].Style.Font.Bold = true; ;
                        distributedWorksheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["F1"].AutoFitColumns();

                        distributedWorksheet.Cells["G1"].Value = "Remaks";
                        distributedWorksheet.Cells["G1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["G1"].AutoFitColumns();

                        distributedWorksheet.Cells["H1"].Value = "会社(Com)	";
                        distributedWorksheet.Cells["H1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["H1"].AutoFitColumns();

                        distributedWorksheet.Cells["I1"].Value = "グレード(Grade)";
                        distributedWorksheet.Cells["I1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["I1"].AutoFitColumns();

                        distributedWorksheet.Cells["J1"].Value = "単価(Unit Price)	";
                        distributedWorksheet.Cells["J1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["J1"].AutoFitColumns();


                        distributedWorksheet.Cells["K1"].Value = "10月";
                        distributedWorksheet.Cells["K1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["K1"].AutoFitColumns();

                        distributedWorksheet.Cells["L1"].Value = "11月";
                        distributedWorksheet.Cells["L1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["L1"].AutoFitColumns();

                        distributedWorksheet.Cells["M1"].Value = "12月";
                        distributedWorksheet.Cells["M1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["M1"].AutoFitColumns();

                        distributedWorksheet.Cells["N1"].Value = "1月";
                        distributedWorksheet.Cells["N1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["N1"].AutoFitColumns();

                        distributedWorksheet.Cells["O1"].Value = "2月";
                        distributedWorksheet.Cells["O1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["O1"].AutoFitColumns();

                        distributedWorksheet.Cells["P1"].Value = "3月";
                        distributedWorksheet.Cells["P1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["P1"].AutoFitColumns();

                        distributedWorksheet.Cells["Q1"].Value = "4月";
                        distributedWorksheet.Cells["Q1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["Q1"].AutoFitColumns();

                        distributedWorksheet.Cells["R1"].Value = "5月";
                        distributedWorksheet.Cells["R1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["R1"].AutoFitColumns();

                        distributedWorksheet.Cells["S1"].Value = "6月";
                        distributedWorksheet.Cells["S1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["S1"].AutoFitColumns();

                        distributedWorksheet.Cells["T1"].Value = "7月";
                        distributedWorksheet.Cells["T1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["T1"].AutoFitColumns();

                        distributedWorksheet.Cells["U1"].Value = "8月";
                        distributedWorksheet.Cells["U1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["U1"].AutoFitColumns();

                        distributedWorksheet.Cells["V1"].Value = "9月";
                        distributedWorksheet.Cells["V1"].Style.Font.Bold = true;
                        distributedWorksheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        distributedWorksheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        distributedWorksheet.Cells["V1"].AutoFitColumns();

                        int distributedCount = 2;
                        foreach (var item in forecastAssignmentViewModels)
                        {
                            var employeeName = item.EmployeeName;
                            var rootEmployeeName = item.RootEmployeeName;
                            var employeeId = item.EmployeeId;
                            var sectionName = item.SectionName;
                            var departmentName = item.DepartmentName;
                            var inChargeName = item.InchargeName;
                            var roleName = item.RoleName;
                            var explanationName = item.ExplanationName;
                            var companyName = item.CompanyName;
                            var gradePoints = item.GradePoint;
                            var unitPrice = item.UnitPrice;
                            var remarks = item.Remarks;

                            var isDeleteRow = item.IsDeleteEmployee;
                            var isAddRow = item.IsAddEmployee;
                            var isUpdateCells = item.IsCellWiseUpdate;

                            var octPOriginal = item.OctPoints;
                            var novPOriginal = item.NovPoints;
                            var decPOriginal = item.DecPoints;
                            var janPOriginal = item.JanPoints;
                            var febPOriginal = item.FebPoints;
                            var marPOriginal = item.MarPoints;
                            var aprPOriginal = item.AprPoints;
                            var mayPOriginal = item.MayPoints;
                            var junPOriginal = item.JunPoints;
                            var julPOriginal = item.JulPoints;
                            var augPOriginal = item.AugPoints;
                            var sepPOriginal = item.SepPoints;

                            distributedWorksheet.Cells["A" + distributedCount].Value = sectionName;
                            distributedWorksheet.Cells["A" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["B" + distributedCount].Value = departmentName;
                            distributedWorksheet.Cells["B" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["C" + distributedCount].Value = inChargeName;
                            distributedWorksheet.Cells["C" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["D" + distributedCount].Value = roleName;
                            distributedWorksheet.Cells["D" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["E" + distributedCount].Value = explanationName;
                            distributedWorksheet.Cells["E" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["F" + distributedCount].Value = employeeName;
                            distributedWorksheet.Cells["F" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["G" + distributedCount].Value = remarks;
                            distributedWorksheet.Cells["G" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["H" + distributedCount].Value = companyName;
                            distributedWorksheet.Cells["H" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["I" + distributedCount].Value = gradePoints;
                            distributedWorksheet.Cells["I" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["J" + distributedCount].Value = unitPrice;
                            distributedWorksheet.Cells["J" + distributedCount].AutoFitColumns();

                            distributedWorksheet.Cells["K" + distributedCount].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["L" + distributedCount].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["M" + distributedCount].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["N" + distributedCount].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["O" + distributedCount].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["P" + distributedCount].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["Q" + distributedCount].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["R" + distributedCount].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["S" + distributedCount].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["T" + distributedCount].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["U" + distributedCount].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                            distributedWorksheet.Cells["V" + distributedCount].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");

                            distributedCount++;
                        }


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


        public ActionResult QaProportion()
        {
            return View();
        }
    }
}