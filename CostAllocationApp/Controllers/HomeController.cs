﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.Dtos;
using CostAllocationApp.BLL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;
using CostAllocationApp.DAL;

namespace CostAllocationApp.Controllers
{
    public class HomeController : Controller
    {
        EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
        UploadExcelBLL _uploadExcelBll = new UploadExcelBLL();
        UploadExcel _uploadExcel;
        Utility _utility = new Utility();

        char[] trimElements = { '\r', '\n', ' ' };
        // GET: Home
        public ActionResult Index()
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
            DataTable dt = new DataTable();

            try
            {
                dt = (DataTable)Session["tmpdata"];
            }
            catch (Exception ex)
            {

            }

            return View(dt);
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
            Dictionary<int, int> check = new Dictionary<int, int>();
            if (ModelState.IsValid)
            {

                if (uploaded_file != null && uploaded_file.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = uploaded_file.InputStream;

                    IExcelDataReader reader = null;


                    if (uploaded_file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploaded_file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
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
                        int tempYear = upload_year;
                        dt_ = reader.AsDataSet().Tables[0];
                        int rowcount = dt_.Rows.Count;
                        
                        for (int i = 1; i < rowcount; i++)
                        {
                            _uploadExcel = new UploadExcel();
                            if (i == 56)
                            {

                            }
                            if (string.IsNullOrEmpty(dt_.Rows[i][0].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.SectionId = _uploadExcelBll.GetSectionIdByName(dt_.Rows[i][0].ToString().Trim(trimElements));
                                if (_uploadExcel.SectionId == 0)
                                {
                                    continue;
                                }
                            }
                            
                            if (string.IsNullOrEmpty(dt_.Rows[i][1].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.DepartmentId = _uploadExcelBll.GetDepartmentIdByName(dt_.Rows[i][1].ToString().Trim(trimElements));
                                if (_uploadExcel.DepartmentId == 0)
                                {
                                    continue;
                                }
                            }
                            
                            if (string.IsNullOrEmpty(dt_.Rows[i][2].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.InchargeId = _uploadExcelBll.GetInchargeIdByName(dt_.Rows[i][2].ToString().Trim(trimElements));
                                if (_uploadExcel.InchargeId == 0)
                                {
                                    continue;
                                }
                            }
                            
                            if (string.IsNullOrEmpty(dt_.Rows[i][3].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.RoleId = _uploadExcelBll.GetRoleIdByName(dt_.Rows[i][3].ToString().Trim(trimElements));
                                if (_uploadExcel.RoleId == 0)
                                {
                                    continue;
                                }
                            }
                            
                            if (!string.IsNullOrEmpty(dt_.Rows[i][4].ToString()))
                            {
                                _uploadExcel.ExplanationId = _uploadExcelBll.GetExplanationIdByName(dt_.Rows[i][4].ToString().Trim(trimElements));
                            }
                            else
                            {
                                _uploadExcel.ExplanationId = null;
                            }

                            if (string.IsNullOrEmpty(dt_.Rows[i][6].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.CompanyId = _uploadExcelBll.GetCompanyIdByName(dt_.Rows[i][6].ToString().Trim(trimElements));
                                if (_uploadExcel.CompanyId == 0)
                                {
                                    continue;
                                }
                            }


                            if (string.IsNullOrEmpty(dt_.Rows[i][8].ToString()))
                            {
                                continue;
                            }
                            else
                            {
                                
                                decimal tempUnitPrice = 0;
                                decimal.TryParse(dt_.Rows[i][8].ToString(), out tempUnitPrice);
                                if (tempUnitPrice == 0)
                                {
                                    continue;
                                }
                                else
                                {                                    
                                    _uploadExcel.GradeId = _uploadExcelBll.GetGradeIdByUnitPrice(dt_.Rows[i][8].ToString().Trim(trimElements));
                                    if (_uploadExcel.GradeId == 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        _uploadExcel.UnitPrice = Convert.ToInt32(dt_.Rows[i][8].ToString().Trim(trimElements));
                                    }

                                }
                            }
                           
                           
                            if (string.IsNullOrEmpty(dt_.Rows[i][5].ToString().Trim(trimElements)))
                            {
                                continue;
                            }
                            else
                            {
                                _uploadExcel.EmployeeId = 0;//dt_.Rows[i][5].ToString().Trim(trimElements);
                            }                            
                            var assignmentViewModels = employeeAssignmentBLL.GetEmployeesByName(dt_.Rows[i][5].ToString().Trim(trimElements));
                            
                            

                            if (assignmentViewModels.Count > 0)
                            {
                                CreateAssignmentForExcelUpload(_uploadExcel, i, assignmentViewModels.Count);
                                tempAssignmentId = employeeAssignmentBLL.GetLastId();
                            }
                            else
                            {
                                //CreateAssignmentForExcelUpload(dt_, i);
                                CreateAssignmentForExcelUpload(_uploadExcel, i);
                                tempAssignmentId = employeeAssignmentBLL.GetLastId();
                            }
                            decimal octInput = 0;
                            decimal.TryParse(dt_.Rows[i][9].ToString(),out octInput);

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

                            tempRow = $@"10_{octInput}_{octInput*Convert.ToDecimal(_uploadExcel.UnitPrice) },11_{novInput}_{novInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},12_{decInput}_{decInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},1_{janInput}_{janInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},2_{febInput}_{febInput* Convert.ToDecimal( _uploadExcel.UnitPrice)},3_{marInput}_{marInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},4_{aprInput}_{aprInput * Convert.ToDecimal(_uploadExcel.UnitPrice)},5_{mayInput}_{mayInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},6_{junInput}_{junInput* Convert.ToDecimal( _uploadExcel.UnitPrice)},7_{julInput}_{julInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},8_{augInput}_{augInput* Convert.ToDecimal(_uploadExcel.UnitPrice)},9_{septInput}_{septInput* Convert.ToDecimal(_uploadExcel.UnitPrice)}";

                            SendToApi(tempAssignmentId, tempRow, tempYear);
                        }


                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("File", ex);
                        return View();
                    }

                    //DataSet result = new DataSet();
                    //result.Tables.Add(dt);
                    reader.Close();
                    reader.Dispose();
                    //DataTable tmp = result.Tables[0];
                    //Session["tmpdata"] = tmp;  //store datatable into session
                    return RedirectToAction("Index");
                }
                else if (upload_year==0)
                {
                    ModelState.AddModelError("Year", "年度を選択してください");
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View("CreateForecast", "Forecasts");
        }

        public void SendToApi(int assignmentId, string row, int year)
        {

            SendToForecaseApiDto sendToForecaseApiDto = new SendToForecaseApiDto();
            sendToForecaseApiDto.Data = row;
            sendToForecaseApiDto.Year = year;
            sendToForecaseApiDto.AssignmentId = assignmentId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(""+ _utility.Address+ "/api/Forecasts?data=" + row + "&year=" + year + "&assignmentId=" + assignmentId);                
                //HTTP POST
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // write code here if needed.
                }
            }

        }

        public int CreateAssignmentForExcelUpload(UploadExcel dt_, int i, int subCodeCount = 0)
        {
            EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            employeeAssignmentDTO = new EmployeeAssignmentDTO();
            employeeAssignment.EmployeeId = dt_.EmployeeId.ToString();
            employeeAssignment.SectionId = Convert.ToInt32(dt_.SectionId.ToString().Trim(trimElements));
            employeeAssignment.InchargeId = Convert.ToInt32(dt_.InchargeId.ToString().Trim(trimElements));
            employeeAssignment.DepartmentId = Convert.ToInt32(dt_.DepartmentId.ToString().Trim(trimElements));
            employeeAssignment.RoleId = Convert.ToInt32(dt_.RoleId.ToString().Trim(trimElements));
            employeeAssignment.CompanyId = Convert.ToInt32(dt_.CompanyId.ToString().Trim(trimElements));
            employeeAssignment.ExplanationId = String.IsNullOrEmpty(dt_.ExplanationId.ToString()) ? null : dt_.ExplanationId.ToString().Trim(trimElements);
            employeeAssignment.UnitPrice = Convert.ToInt32(dt_.UnitPrice.ToString().Trim(trimElements));
            employeeAssignment.GradeId = Convert.ToInt32(dt_.GradeId.ToString().Trim(trimElements));
            employeeAssignment.SubCode = subCodeCount + 1;                                    
            
            employeeAssignment.CreatedBy = "";
            employeeAssignment.CreatedDate = DateTime.Now;
            employeeAssignment.IsActive = "1";
            employeeAssignment.Remarks = "";


            int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
            if (result == 0)
            {
                throw new Exception();
            }
            return result;
        }

    }
}