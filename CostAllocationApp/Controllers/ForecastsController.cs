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

        ForecastBLL forecastBLL = new ForecastBLL();
        ExportExcelFileBLL exportExcelFileBLL = new ExportExcelFileBLL();

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

        public int EmployeeBudgetCreate(UploadExcel dt_, int i,int upload_year=0, int subCodeCount = 0,string select_budget_type="")
        {
            EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
            EmployeeBudget employeeAssignment = new EmployeeBudget();

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

            if(Convert.ToInt32(select_budget_type) == 1)
            {
                employeeAssignment.FirstHalfBudget = true;
                employeeAssignment.SecondHalfBudget = false;
            }else if (Convert.ToInt32(select_budget_type) == 2)
            {
                employeeAssignment.FirstHalfBudget = false;
                employeeAssignment.SecondHalfBudget = true;
            }
            else
            {
                employeeAssignment.FirstHalfBudget = false;
                employeeAssignment.SecondHalfBudget = false;
            }
            employeeAssignment.FinalizedBudget = false;

            //int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
            int result = employeeAssignmentBLL.CreateBudgets(employeeAssignment);
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
        public ActionResult DownloadHistoryData(int hid_approve_timestamp_id = 0,string hid_selected_year = "")
        {
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();
            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            if (!string.IsNullOrEmpty(hid_selected_year))
            {
                employeeAssignment.Year = hid_selected_year;
                forecastAssignmentViewModels = employeeAssignmentBLL.GetAllOriginalDataForDownloadFiles(employeeAssignment, hid_approve_timestamp_id);
            }                        
            string timeStampName = forecastBLL.GetApproveHistoryTimeStampName(hid_approve_timestamp_id);
  
            if (!string.IsNullOrEmpty(timeStampName)) { 
                if (forecastAssignmentViewModels.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        //*****************Download: Original: Start***********************//                        
                        var sheet = package.Workbook.Worksheets.Add("Download(original)");
                        sheet = exportExcelFileBLL.ExportOriginalExcelSheet(sheet, forecastAssignmentViewModels);                        
                        //*****************Download: Original: End***********************//

                        //*****************Download: Each Person: Start***********************//                        
                        var eachPersonSheet = package.Workbook.Worksheets.Add("Download(Each person)");
                        eachPersonSheet = exportExcelFileBLL.ExportEachPersonExcelSheet(eachPersonSheet, forecastAssignmentViewModels);
                        //*****************Download: Each Person: End***********************//

                        //*****************Download: Dev Distribution: Start***********************//                        
                        var devDistributionSheet = package.Workbook.Worksheets.Add("【開発】配置表(Dev Distribution)");
                        devDistributionSheet = exportExcelFileBLL.ExportDevDistributionExcelSheet(devDistributionSheet, forecastAssignmentViewModels);
                        //*****************Download: Dev Distribution: End***********************//

                        //*****************Download: Planning Distribution: Start***********************//                        
                        var planningDistributionSheet = package.Workbook.Worksheets.Add("【企画】配置表(Planning Distribution)");
                        planningDistributionSheet = exportExcelFileBLL.ExportPlanningDistributionExcelSheet(planningDistributionSheet, forecastAssignmentViewModels);
                        //*****************Download: Planning Distribution: End***********************//                        

                        //*****************Download: Distributed: Start***********************//
                        //var distributedWorksheet = package.Workbook.Worksheets.Add("Download(Distributed)");
                        //distributedWorksheet = exportExcelFileBLL.ExportDistributedSheet(distributedWorksheet, forecastAssignmentViewModels, hid_selected_year);                       
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
        // GET: Forecasts
        public ActionResult CreateBudget()
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
            ViewBag.ValidationMessage = "";

            return View(forecastViewModal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudget(HttpPostedFileBase uploaded_file, string upload_year,string select_budget_type)
        {
            bool isThisYearBudgetExists = false;
            int selected_year = 0;
            int budgetRequestType = 0;
            if (!string.IsNullOrEmpty(upload_year))
            {
                selected_year = Convert.ToInt32(upload_year);
                budgetRequestType = Convert.ToInt32(select_budget_type);
                isThisYearBudgetExists = employeeAssignmentBLL.CheckForBudgetYearIsExists(selected_year, budgetRequestType);
            }
            else
            {
                isThisYearBudgetExists = true;
            }

            if (!isThisYearBudgetExists)
            {
                ForecastViewModal forecastViewModal = new ForecastViewModal
                {
                    _sections = sectionBLL.GetAllSections()
                };

                TempData["seccess"] = null;
                Dictionary<int, int> check = new Dictionary<int, int>();
                if (ModelState.IsValid)
                {

                    if ((uploaded_file != null && uploaded_file.ContentLength > 0) && selected_year > 0)
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
                            ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>Failed to import!</span>";
                            return View();
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
                            int tempYear = selected_year;
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
                                    EmployeeBudgetCreate(_uploadExcel, i, selected_year, assignmentViewModels.Count, select_budget_type);                                                        
                                    tempAssignmentId = employeeAssignmentBLL.GetBudgetLastId();
                                }
                                else
                                {
                                    EmployeeBudgetCreate(_uploadExcel, i, selected_year, 0,select_budget_type);
                                    tempAssignmentId = employeeAssignmentBLL.GetBudgetLastId();
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
                            ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>Failed to import!</span>";
                            return View();
                        }

                        //DataSet result = new DataSet();
                        //result.Tables.Add(dt);
                        reader.Close();
                        reader.Dispose();
                        //DataTable tmp = result.Tables[0];
                        //Session["tmpdata"] = tmp;  //store datatable into session
                        TempData["seccess"] = "Data imported successfully";
                        ViewBag.ValidationMessage = "<span id='validation_message_success' style='margin-left: 28px;'>Data has successfully imported to " + selected_year + ".</span>";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorCount = 1;
                        ModelState.AddModelError("File", "invalid File or Year");
                    }
                }
            }

            if (isThisYearBudgetExists)
            {                
                ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>Data has already imported to " + selected_year + ".Please chooose another year to import data..</span>";
            }
            else
            {
                ViewBag.ValidationMessage = "<span id='validation_message_success' style='margin-left: 28px;'>Data has successfully imported to " + selected_year + ".</span>";
            }
            return View();
        }
        public ActionResult Total()
        {
            return View();
        }
    }
}