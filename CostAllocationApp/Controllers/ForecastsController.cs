﻿using System;
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
        DepartmentBLL departmentBLL = new DepartmentBLL();
        InChargeBLL inChargeBLL = new InChargeBLL();
        RoleBLL roleBLL = new RoleBLL();
        ExplanationBLL explanationsBLL = new ExplanationBLL();
        CompanyBLL companyBLL = new CompanyBLL();        

        ForecastBLL forecastBLL = new ForecastBLL();
        ExportExcelFileBLL exportExcelFileBLL = new ExportExcelFileBLL();
        ActualCostBLL actualCostBLL = new ActualCostBLL();

        public ForecastsController()
        {
            userBLL = new UserBLL();
        }
        public ActionResult CreateForecast()
        {
            //authentication
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase uploaded_file, int upload_year)
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
            return View("CreateForecast", new { forecastType = "imprt" });
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

        public int CreateAssignmentForExcelUpload(UploadExcel dt_, int i, int upload_year = 0, int subCodeCount = 0)
        {
            EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            employeeAssignmentDTO = new EmployeeAssignmentDTO();
            employeeAssignment.EmployeeId = dt_.EmployeeId.ToString();
            employeeAssignment.SectionId = String.IsNullOrEmpty(dt_.SectionId.ToString()) ? null : dt_.SectionId;
            employeeAssignment.InchargeId = String.IsNullOrEmpty(dt_.InchargeId.ToString()) ? null : dt_.InchargeId;
            employeeAssignment.DepartmentId = String.IsNullOrEmpty(dt_.DepartmentId.ToString()) ? null : dt_.DepartmentId;
            employeeAssignment.RoleId = String.IsNullOrEmpty(dt_.RoleId.ToString()) ? null : dt_.RoleId;
            employeeAssignment.CompanyId = String.IsNullOrEmpty(dt_.CompanyId.ToString()) ? null : dt_.CompanyId;
            employeeAssignment.ExplanationId = String.IsNullOrEmpty(dt_.ExplanationId.ToString()) ? null : dt_.ExplanationId.ToString().Trim(trimElements);
            employeeAssignment.UnitPrice = Convert.ToInt32(dt_.UnitPrice.ToString().Trim(trimElements));
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

        public int EmployeeBudgetCreate(UploadExcel dt_,int i, int upload_year = 0, int subCodeCount = 0, string select_budget_type = "")
        {
            EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
            EmployeeBudget employeeAssignment = new EmployeeBudget();

            employeeAssignmentDTO = new EmployeeAssignmentDTO();
            employeeAssignment.EmployeeId = dt_.EmployeeId.ToString();
            employeeAssignment.SectionId = String.IsNullOrEmpty(dt_.SectionId.ToString()) ? null : dt_.SectionId;
            employeeAssignment.InchargeId = String.IsNullOrEmpty(dt_.InchargeId.ToString()) ? null : dt_.InchargeId;
            employeeAssignment.DepartmentId = String.IsNullOrEmpty(dt_.DepartmentId.ToString()) ? null : dt_.DepartmentId;
            employeeAssignment.RoleId = String.IsNullOrEmpty(dt_.RoleId.ToString()) ? null : dt_.RoleId;
            employeeAssignment.CompanyId = String.IsNullOrEmpty(dt_.CompanyId.ToString()) ? null : dt_.CompanyId;
            employeeAssignment.ExplanationId = String.IsNullOrEmpty(dt_.ExplanationId.ToString()) ? null : dt_.ExplanationId.ToString().Trim(trimElements);
            employeeAssignment.UnitPrice = Convert.ToInt32(dt_.UnitPrice.ToString().Trim(trimElements));
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
            employeeAssignment.EmployeeModifiedName = dt_.EmployeeName;

            employeeAssignment.DuplicateFrom = dt_.DuplicateFrom;
            employeeAssignment.DuplicateCount = dt_.DuplicateCount;
            employeeAssignment.RoleChanged = dt_.RoleChanged;
            employeeAssignment.UnitPriceChanged = dt_.UnitPriceChanged;


            if (Convert.ToInt32(select_budget_type) == 1)
            {
                employeeAssignment.FirstHalfBudget = true;
                employeeAssignment.SecondHalfBudget = false;
            }
            else if (Convert.ToInt32(select_budget_type) == 2)
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


            int result = employeeAssignmentBLL.CreateBudgets(employeeAssignment);
            if (result == 0)
            {
                throw new Exception();
            }
            return result;
        }

        public ActionResult GetHistories()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        public ActionResult ActualCosts()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        public ActionResult ActualCostConfirm()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();


        }

        public ActionResult ApproveForecast()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        public ActionResult ApproveHistories()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult DownloadHistoryData(int hid_approve_timestamp_id = 0, string hid_selected_year = "")
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
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();
            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            if (!string.IsNullOrEmpty(hid_selected_year))
            {
                employeeAssignment.Year = hid_selected_year;
                forecastAssignmentViewModels = employeeAssignmentBLL.GetAllOriginalDataForDownloadFiles(employeeAssignment, hid_approve_timestamp_id);
            }
            string timeStampName = forecastBLL.GetApproveHistoryTimeStampName(hid_approve_timestamp_id);

            if (!string.IsNullOrEmpty(timeStampName))
            {
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

                        //*****************Download: Planning Distribution: Start***********************//                        
                        var planningDistributionSheet = package.Workbook.Worksheets.Add("【企画】配置表(Planning Distribution)");
                        planningDistributionSheet = exportExcelFileBLL.ExportPlanningDistributionExcelSheet(planningDistributionSheet, forecastAssignmentViewModels, employeeAssignment.Year);
                        //*****************Download: Planning Distribution: End***********************//         

                        //*****************Download: Dev Distribution: Start***********************//                        
                        var devDistributionSheet = package.Workbook.Worksheets.Add("【開発】配置表(Dev Distribution)");
                        devDistributionSheet = exportExcelFileBLL.ExportDevDistributionExcelSheet(devDistributionSheet, forecastAssignmentViewModels, employeeAssignment.Year);
                        //*****************Download: Dev Distribution: End***********************//                                       

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
        }


        public ActionResult QaProportion()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        // GET: 
        public ActionResult CreateBudget()
        {
            // user authentication
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                    ViewBag.ValidationMessage = "";
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }                                
            }
            
            return View();
        }

        //budget excel submit page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudget(HttpPostedFileBase uploaded_file, string upload_year, string select_budget_type)
        {
            // user authentication
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }


            var session = System.Web.HttpContext.Current.Session;
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

                        if (uploaded_file.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            ViewBag.ErrorCount = 1;
                            ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>インポートに失敗しました!</span>";
                            return View();
                        }
                        DataRow row;
                        DataTable dt_ = new DataTable();
                        try
                        {
                            int tempAssignmentId = 0;
                            string tempRow = "";
                            int tempYear = selected_year;
                            dt_ = reader.AsDataSet().Tables[0];
                            int rowcount = dt_.Rows.Count;
                            string userName = session["userName"].ToString();

                            List<string> uniqueEmployeeNameList = new List<string>();

                            // get unique employee names
                            for (int i = 2; i < rowcount; i++)
                            {
                                if (!uniqueEmployeeNameList.Contains(dt_.Rows[i][5].ToString()))
                                {
                                    uniqueEmployeeNameList.Add(dt_.Rows[i][5].ToString());
                                }
                            }

                            List<string> assignedEmployeeName = new List<string>();
                            
                            foreach (string employeeName in uniqueEmployeeNameList)
                            {
                                if (employeeName== "王　翔（オウ ショウ）")
                                {

                                }
                                if (!assignedEmployeeName.Contains(employeeName))
                                {
                                    List<string> _matchedItems = new List<string>();
                                    List<DataRow> _dataRows = new List<DataRow>();
                                    for (int i = 2; i < rowcount; i++)
                                    {
                                        if (dt_.Rows[i][5].ToString() == employeeName)
                                        {
                                            _dataRows.Add(dt_.Rows[i]);
                                        }
                                    }
                                    _dataRows = _dataRows.OrderBy(r => Convert.ToInt32(r[11])).ToList();

                                    foreach (var _row in _dataRows)
                                    {
                                        if (_row[5].ToString() == employeeName)
                                        {
                                            _uploadExcel = new UploadExcel();

                                            //section: read/write
                                            if (!string.IsNullOrEmpty(_row[0].ToString()))
                                            {
                                                _uploadExcel.SectionId = sectionBLL.RetrieveSectionIdBySectionName(_row[0].ToString().Trim(trimElements), userName);
                                            }
                                            else
                                            {
                                                _uploadExcel.SectionId = sectionBLL.RetrieveSectionIdBySectionName("未設定", userName);
                                            }
                                            //department : read/write
                                            if (!string.IsNullOrEmpty(_row[1].ToString()))
                                            {
                                                int sectionId = Convert.ToInt32(_uploadExcel.SectionId);
                                                _uploadExcel.DepartmentId = departmentBLL.RetrieveDepartmentIdByDepartmentName(_row[1].ToString(), sectionId, userName);
                                            }
                                            else
                                            {
                                                int sectionId = Convert.ToInt32(_uploadExcel.SectionId);
                                                _uploadExcel.DepartmentId = departmentBLL.RetrieveDepartmentIdByDepartmentName("未設定", sectionId, userName);                                                
                                            }

                                            //incharge: read/write
                                            if (!string.IsNullOrEmpty(_row[2].ToString()))
                                            {
                                                _uploadExcel.InchargeId = inChargeBLL.RetrieveInChargeIdByInchargeName(_row[2].ToString().Trim(trimElements), userName);
                                            }
                                            else
                                            {
                                                _uploadExcel.InchargeId = 0;
                                            }

                                            //role: read/write
                                            if (!string.IsNullOrEmpty(_row[3].ToString()))
                                            {
                                                _uploadExcel.RoleId = roleBLL.RetrieveRoleIdByRoleName(_row[3].ToString().Trim(trimElements), userName);
                                            }
                                            else
                                            {
                                                _uploadExcel.RoleId = roleBLL.RetrieveRoleIdByRoleName("未設定", userName);
                                            }

                                            //explanation: read/write
                                            if (!string.IsNullOrEmpty(_row[4].ToString()))
                                            {
                                                _uploadExcel.ExplanationId = explanationsBLL.RetrieveExplanationIdByExplanationName(_row[4].ToString(), userName);
                                            }

                                            //compnay: read/write
                                            if (!string.IsNullOrEmpty(_row[6].ToString()))
                                            {
                                                _uploadExcel.CompanyId = companyBLL.RetrieveCompanyIdByCompanyName(_row[6].ToString().Trim(trimElements), userName);
                                            }
                                            else
                                            {
                                                _uploadExcel.CompanyId = companyBLL.RetrieveCompanyIdByCompanyName("未設定", userName);
                                            }

                                            //grade and unit price: read/write
                                            bool isGradeEmpty = false;
                                            bool isUnitPriceEmpty = false;
                                            if (string.IsNullOrEmpty(_row[7].ToString()))
                                            {
                                                isGradeEmpty = true;
                                            }
                                            if (string.IsNullOrEmpty(_row[8].ToString()))
                                            {
                                                isUnitPriceEmpty = true;
                                            }
                                            else if (Convert.ToInt32(_row[8]) < 0)
                                            {
                                                isUnitPriceEmpty = true;
                                            }

                                            if (!isGradeEmpty && !isUnitPriceEmpty)
                                            {
                                                _uploadExcel.GradeId = _uploadExcelBll.GetGradeIdByGradeName(_row[7].ToString().Trim(trimElements));
                                                _uploadExcel.UnitPrice = Convert.ToInt32(_row[8].ToString().Trim(trimElements));
                                            }
                                            else if (!isGradeEmpty)
                                            {
                                                _uploadExcel.GradeId = _uploadExcelBll.GetGradeIdByGradeName(_row[7].ToString().Trim(trimElements));
                                                _uploadExcel.UnitPrice = _uploadExcelBll.GetUnitPriceByGradeName(_row[7].ToString().Trim(trimElements));
                                            }
                                            else if (!isUnitPriceEmpty)
                                            {
                                                _uploadExcel.GradeId = 0;
                                                _uploadExcel.UnitPrice = Convert.ToInt32(_row[8].ToString().Trim(trimElements));
                                            }

                                            // new code added by shekhar
                                            if (_matchedItems.Count > 0)
                                            {
                                                foreach (var matchedItem in _matchedItems)
                                                {
                                                    var _tempData = matchedItem.Split('_');
                                                    if (Convert.ToInt32(_tempData[0]) == Convert.ToInt32(_row[10].ToString()))
                                                    {
                                                        _uploadExcel.DuplicateFrom = _tempData[1];
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(_row[10].ToString()))
                                                {
                                                    _uploadExcel.DuplicateFrom = _row[10].ToString().Trim(trimElements);
                                                }
                                                else
                                                {
                                                    _uploadExcel.DuplicateFrom = "0";
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(_row[11].ToString()))
                                            {
                                                _uploadExcel.DuplicateCount = _row[11].ToString().Trim(trimElements);
                                            }
                                            else
                                            {
                                                _uploadExcel.DuplicateCount = "0";
                                            }
                                            if (!string.IsNullOrEmpty(_row[12].ToString()))
                                            {
                                                _uploadExcel.RoleChanged = _row[12].ToString().Trim(trimElements);
                                            }
                                            else
                                            {
                                                _uploadExcel.RoleChanged = "0";
                                            }
                                            if (!string.IsNullOrEmpty(_row[13].ToString()))
                                            {
                                                _uploadExcel.UnitPriceChanged = _row[13].ToString().Trim(trimElements);
                                            }
                                            else
                                            {
                                                _uploadExcel.UnitPriceChanged = "0";
                                            }
                                            _uploadExcel.Remarks = "";

                                            //name: read/write
                                            if (string.IsNullOrEmpty(_row[5].ToString().Trim(trimElements)))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                employee.IsActive = true;
                                                employee.CreatedBy = "";
                                                employee.CreatedDate = DateTime.Now;
                                                employee.FullName = employeeBLL.GetFullNameFromCSV(_row[5].ToString().Trim(trimElements));
                                                if (string.IsNullOrEmpty(employee.FullName))
                                                {
                                                    continue;
                                                }
                                                else
                                                {
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
                                            }


                                            var assignmentViewModels = employeeAssignmentBLL.GetEmployeesByName(employee.FullName);

                                            if (assignmentViewModels.Count > 0)
                                            {
                                                EmployeeBudgetCreate(_uploadExcel, 0, selected_year, assignmentViewModels.Count, select_budget_type);
                                                tempAssignmentId = employeeAssignmentBLL.GetBudgetLastId();
                                            }
                                            else
                                            {
                                                EmployeeBudgetCreate(_uploadExcel, 0, selected_year, 0, select_budget_type);
                                                tempAssignmentId = employeeAssignmentBLL.GetBudgetLastId();
                                            }

                                            _matchedItems.Add(_row[9].ToString() + "_" + tempAssignmentId);


                                            //get the man month from budget.
                                            decimal octInput = 0;
                                            decimal.TryParse(_row[14].ToString(), out octInput);

                                            decimal novInput = 0;
                                            decimal.TryParse(_row[15].ToString(), out novInput);

                                            decimal decInput = 0;
                                            decimal.TryParse(_row[16].ToString(), out decInput);

                                            decimal janInput = 0;
                                            decimal.TryParse(_row[17].ToString(), out janInput);

                                            decimal febInput = 0;
                                            decimal.TryParse(_row[18].ToString(), out febInput);

                                            decimal marInput = 0;
                                            decimal.TryParse(_row[19].ToString(), out marInput);

                                            decimal aprInput = 0;
                                            decimal.TryParse(_row[20].ToString(), out aprInput);

                                            decimal mayInput = 0;
                                            decimal.TryParse(_row[21].ToString(), out mayInput);

                                            decimal junInput = 0;
                                            decimal.TryParse(_row[22].ToString(), out junInput);

                                            decimal julInput = 0;
                                            decimal.TryParse(_row[23].ToString(), out julInput);

                                            decimal augInput = 0;
                                            decimal.TryParse(_row[24].ToString(), out augInput);

                                            decimal septInput = 0;
                                            decimal.TryParse(_row[25].ToString(), out septInput);




                                            tempRow = $@"10_{octInput}_{octInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},11_{novInput}_{novInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},12_{decInput}_{decInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},1_{janInput}_{janInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},2_{febInput}_{febInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},3_{marInput}_{marInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},4_{aprInput}_{aprInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},5_{mayInput}_{mayInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},6_{junInput}_{junInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},7_{julInput}_{julInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},8_{augInput}_{augInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},9_{septInput}_{septInput * Convert.ToDecimal(_uploadExcel.UnitPrice)}";

                                            SendToApi(tempAssignmentId, tempRow, tempYear);


                                        }
                                    }
                                }

                                assignedEmployeeName.Add(employeeName);
                            }
                          

                            

                            //2nd half budget: start
                            //logic: approved man month from edit page data from oct. to april. will updated to the budget man month for 2nd half budget
                            if (!string.IsNullOrEmpty(select_budget_type))
                            {
                                if(Convert.ToInt32(select_budget_type) == 2)
                                {
                                    List<EmployeeBudget> _employeeBudgets = new List<EmployeeBudget>();
                                    _employeeBudgets = employeeAssignmentBLL.GetSecondHlafBudgetData(Convert.ToInt32(upload_year),Convert.ToInt32(select_budget_type));

                                    int returnAssingmentId = 0;
                                    foreach (var budgetItem in _employeeBudgets)
                                    {
                                        returnAssingmentId = 0;
                                        returnAssingmentId = employeeAssignmentBLL.IsBudgetMatchWithAssignmentData(budgetItem);

                                        List<ForecastDto> _forecastDto = new List<ForecastDto>();
                                        if (returnAssingmentId > 0)
                                        {                                            
                                            _forecastDto = employeeAssignmentBLL.GettForecastDataForSecondHalfBudgetByAssignmentId(returnAssingmentId, Convert.ToInt32(upload_year));                                                                           
                                        }
                                        if (_forecastDto.Count > 0)
                                        {
                                            foreach(var forecastItem in _forecastDto)
                                            {
                                                Forecast _forecast = new Forecast();
                                                _forecast.Points = forecastItem.Points;
                                                _forecast.Total = Convert.ToDecimal(forecastItem.Total);
                                                _forecast.Year = forecastItem.Year;
                                                _forecast.EmployeeAssignmentId = budgetItem.Id;
                                                _forecast.Month = forecastItem.Month;
                                                _forecast.UpdatedBy = session["userName"].ToString();

                                                if (forecastItem.Month == 10)
                                                {                                                    
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 11);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 10, Convert.ToInt32(upload_year));                                                        
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }                                                    
                                                }
                                                if (forecastItem.Month == 11)
                                                {
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 12);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 11, Convert.ToInt32(upload_year));
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                }
                                                if (forecastItem.Month == 12)
                                                {
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 13);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 12, Convert.ToInt32(upload_year));
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                }
                                                if (forecastItem.Month == 1)
                                                {
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 14);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 1, Convert.ToInt32(upload_year));
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                }
                                                if (forecastItem.Month == 2)
                                                {
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 15);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 2, Convert.ToInt32(upload_year));
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                }
                                                if (forecastItem.Month == 3)
                                                {
                                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(upload_year), 16);
                                                    if (isOriginal)
                                                    {
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                    else
                                                    {
                                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 3, Convert.ToInt32(upload_year));
                                                        int results = forecastBLL.UpdateBudgetForecast(_forecast);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Forecast _forecast = new Forecast();
                                            _forecast.Points = 0;
                                            _forecast.Total = 0;
                                            _forecast.Year = Convert.ToInt32(upload_year);
                                            _forecast.EmployeeAssignmentId = budgetItem.Id;                                            
                                            _forecast.UpdatedBy = session["userName"].ToString();

                                            int results = 0;

                                            _forecast.Month = 10;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);                                        
                                            _forecast.Month = 11;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                                            _forecast.Month = 12;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                                            _forecast.Month = 1;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                                            _forecast.Month = 2;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                                            _forecast.Month = 3;
                                            results = forecastBLL.UpdateBudgetForecast(_forecast);                                            
                                        }
                                    }
                                }                                                                                            
                            }
                        }
                        //2nd half budget: end
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("File", ex);
                            ViewBag.ErrorCount = 1;
                            ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>インポートに失敗しました</span>";
                            return View();
                        }
                        reader.Close();
                        reader.Dispose();
                        //store datatable into session
                        TempData["seccess"] = "Data imported successfully";
                        ViewBag.ValidationMessage = "<span id='validation_message_success' style='margin-left: 28px;'>インポートデータは正常に処理されました </span>";
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
                ViewBag.ValidationMessage = "<span id='validation_message_failed' style='margin-left: 28px;'>選択した予算時期は既に登録済みです。他の予算時期を選択し、再度インポートしてください</span>";
            }
            else
            {
                ViewBag.ValidationMessage = "<span id='validation_message_success' style='margin-left: 28px;'>インポートデータは正常に処理されました </span>";
            }
            return View();
        }

        // GET: Forecasts
        public ActionResult EditBudget()
        {
            //authentication
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }
                       
            ViewBag.ValidationMessage = "";
            return View();
        }
        public ActionResult Total()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }

        public ActionResult CreateDynamicTable()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }
        public ActionResult DynamicTableView()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult ExportBudgetByYear(int hid_budget_year = 0, int hid_budget_type = 0)
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
            /************* Export Budget: Start *************/
            DepartmentBLL _departmentBll = new DepartmentBLL();

            //get export data and create the xlsx
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();
            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
            if (!string.IsNullOrEmpty(hid_budget_year.ToString()))
            {

                forecastAssignmentViewModels = employeeAssignmentBLL.GetBudgetDataByYearAndType(hid_budget_year, hid_budget_type);
            }

            /************* Export Budget: End *************/

            string budgetTypeName = "";
            if (hid_budget_type == 1)
            {
                budgetTypeName = "Budget-"+hid_budget_year+"-年初期";
            }
            else
            {
                budgetTypeName = "Budget-" + hid_budget_year + "-年下半期";
            }

            if (!string.IsNullOrEmpty(budgetTypeName))
            {
                if (forecastAssignmentViewModels.Count > 0)
                {
                    using (var package = new ExcelPackage())
                    {
                        //*****************Download: Original: Start***********************//
                        var sheet = package.Workbook.Worksheets.Add("Budget "+ hid_budget_year);
                        sheet = exportExcelFileBLL.ExportBudgetExcelSheet(sheet, forecastAssignmentViewModels);
                        //*****************Download: Original: End***********************//
                        
                        var excelData = package.GetAsByteArray();
                        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var fileName = budgetTypeName + ".xlsx";

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
        }

    }
}