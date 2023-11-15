﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.BLL
{
    public class TotalBLL
    {
        TotalDAL totalDAL = null;
        DepartmentBLL departmentBLL = null;
        InChargeBLL inchargeBLL = null;
        CategoryBLL categoryBLL = null;
        SubCategoryBLL subCategoryBLL = null;
        DetailsItemBLL detailsItemBLL = null;

        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        SalaryBLL salaryBLL = null;
        SectionBLL sectionBLL = null;
        CompanyBLL companyBLL = null;
        RoleBLL roleBLL = null;
        ExplanationsBLL explanationsBLL = null;
        ForecastBLL forecastBLL = null;
        EmployeeBLL employeeBLL = null;
        UserBLL userBLL = null;
        ActualCostBLL actualCostBLL = null;
        UserRoleBLL userRoleBLL = null;
        QaProportionBLL qaProportionBLL = null;   


        public TotalBLL()
        {
            totalDAL = new TotalDAL();
            departmentBLL = new DepartmentBLL();
            inchargeBLL = new InChargeBLL();
            categoryBLL = new CategoryBLL();
            subCategoryBLL = new SubCategoryBLL();
            detailsItemBLL = new DetailsItemBLL();

            employeeAssignmentBLL = new EmployeeAssignmentBLL();
            salaryBLL = new SalaryBLL();
            sectionBLL = new SectionBLL();
            companyBLL = new CompanyBLL();
            roleBLL = new RoleBLL();
            explanationsBLL = new ExplanationsBLL();
            forecastBLL = new ForecastBLL();
            employeeBLL = new EmployeeBLL();
            userBLL = new UserBLL();
            actualCostBLL = new ActualCostBLL();
            userRoleBLL = new UserRoleBLL();
            qaProportionBLL = new QaProportionBLL();
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(int departmentId, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = totalDAL.GetEmployeesForecastByDepartments_Company(departmentId, companyIds, year);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = totalDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }

                }
            }
            return forecastAssignments;
        }

        public int CreateDynamicTable(DynamicTable dynamicTable)
        {
            return totalDAL.CreateDynamicTable(dynamicTable);
        }
        public List<DynamicTable> GetAllDynamicTables()
        {
            return totalDAL.GetAllDynamicTables();
        }
        public int RemoveDynamicTable(DynamicTable dynamicTable)
        {
            List<Category> categories = categoryBLL.GetAllCategoriesByDynamicTableId(dynamicTable.Id);
            List<SubCategory> subCategories = new List<SubCategory>();
            List<DeatailsItem> detailsItems = new List<DeatailsItem>();


            if (categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    var subCategoryList = subCategoryBLL.GetSubCategoryByCategoryId(category.Id);
                    subCategories.AddRange(subCategoryList);
                }

                foreach (var category in categories)
                {
                    categoryBLL.RemoveCategory(category);
                }
            }

            if (subCategories.Count > 0)
            {
                foreach (var subCategory in subCategories)
                {
                    var deatailsItemsList = detailsItemBLL.GetDetailsItemBySubItemsId(subCategory.Id);
                    detailsItems.AddRange(deatailsItemsList);
                }

                foreach (var subCategory in subCategories)
                {
                    subCategoryBLL.RemoveSubCategory(subCategory);
                }
            }

            if (detailsItems.Count > 0)
            {
                foreach (var detailsItem in detailsItems)
                {
                    detailsItemBLL.RemoveDetailsItem(detailsItem);
                }

            }

            return totalDAL.RemoveDynamicTable(dynamicTable);
        }
        public int DeleteDynamicTableSettings(string dynamicTableid, string dynamicSettingsIds)
        {
            return totalDAL.DeleteDynamicTableSettings(dynamicTableid, dynamicSettingsIds);
        }
        public int UpdateDynamicTable(DynamicTable dynamicTable)
        {
            return totalDAL.UpdateDynamicTable(dynamicTable);
        }
        public int CreateDynamicSetting(DynamicSetting dynamicSetting)
        {
            return totalDAL.CreateDynamicSetting(dynamicSetting);
        }
        public DynamicTable GetDynamicTableById(int tableId)
        {
            return totalDAL.GetDynamicTableById(tableId);
        }

        public List<DynamicSetting> GetDynamicSettings()
        {
            return totalDAL.GetDynamicSettings();
        }

        public List<DynamicSetting> GetDynamicSettingsByDynamicTableId(int dynamicTableId)
        {
            List<DynamicSetting> dynamicSettings = totalDAL.GetDynamicSettingsByDynamicTableId(dynamicTableId);
            if (dynamicSettings.Count > 0)
            {
                foreach (var dynamicSetting in dynamicSettings)
                {
                    var dynamicMethodDefinition = DynamicMethodDefinition.GetMethods().Where(dmd => dmd.Id == Convert.ToInt32(dynamicSetting.MethodId)).SingleOrDefault();
                    dynamicSetting.MethodName = dynamicMethodDefinition.MethodName;
                    if (!String.IsNullOrEmpty(dynamicSetting.ParameterId))
                    {
                        string dependencyNames = "";
                        string[] parameterNames = dynamicSetting.ParameterId.Split(',');
                        if (dynamicMethodDefinition.Dependency == "dp")
                        {
                            dynamicSetting.ParameterType = "department";
                            foreach (var item in parameterNames)
                            {
                                dependencyNames += departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item)).DepartmentName + ", ";
                            }
                        }
                        if (dynamicMethodDefinition.Dependency == "in")
                        {
                            dynamicSetting.ParameterType = "incharge";
                            foreach (var item in parameterNames)
                            {
                                dependencyNames += inchargeBLL.GetInChargeByInChargeId(Convert.ToInt32(item)).InChargeName + ", ";
                            }
                        }


                        dependencyNames.TrimEnd(',');
                        dynamicSetting.CommaSeperatedParameterName = dependencyNames;
                    }

                }
            }

            return dynamicSettings;
        }

        public int UpdateDynamicTablesTitle(DynamicTable dynamicTable)
        {
            return totalDAL.UpdateDynamicTablesTitle(dynamicTable);
        }

        public bool IsNameAndPositionExists(string tableName, int tablePoisition, int tableId, string checkType)
        {
            return totalDAL.IsNameAndPositionExists(tableName, tablePoisition, tableId, checkType);
        }
        public int UpdateDynamicTableSettings(DynamicSetting dynamicSetting)
        {
            return totalDAL.UpdateDynamicTableSettings(dynamicSetting);
        }
        public string GetCostTableHeaderPart(string main_header, string sub_header, string detial_header, string tableTitle, string year)
        {
            string strTableHeader = "";
            //strTableHeader = "<p class'font-weight-bold' id='p-total' style='margin-top:20px;'><u>" + tableTitle + ":</u></p>";

            strTableHeader = strTableHeader + "<thead>";
            strTableHeader = strTableHeader + "	<tr>";
            if (!string.IsNullOrEmpty(main_header))
            {
                strTableHeader = strTableHeader + "<th>" + main_header + "</th>";
            }
            if (!string.IsNullOrEmpty(sub_header))
            {
                strTableHeader = strTableHeader + "<th>" + sub_header + "</th>";
            }
            if (!string.IsNullOrEmpty(detial_header))
            {
                strTableHeader = strTableHeader + "<th>" + detial_header + "</th>";
            }
            strTableHeader = strTableHeader + "		<th>10月</th>";
            strTableHeader = strTableHeader + "		<th>11月</th>";
            strTableHeader = strTableHeader + "		<th>12月</th>";
            strTableHeader = strTableHeader + "		<th>1月</th>";
            strTableHeader = strTableHeader + "		<th>2月</th>";
            strTableHeader = strTableHeader + "		<th>3月</th>";
            strTableHeader = strTableHeader + "		<th>4月</th>";
            strTableHeader = strTableHeader + "		<th>5月</th>";
            strTableHeader = strTableHeader + "		<th>6月</th>";
            strTableHeader = strTableHeader + "		<th>7月</th>";
            strTableHeader = strTableHeader + "		<th>8月</th>";
            strTableHeader = strTableHeader + "		<th>9月</th>";

            strTableHeader = strTableHeader + "		<th>FY$" + year + "計</th>";
            strTableHeader = strTableHeader + "		<th>上期</th>";
            strTableHeader = strTableHeader + "		<th>下期 </th>";
            strTableHeader = strTableHeader + "	</tr>";
            strTableHeader = strTableHeader + "</thead>";

            return strTableHeader;
        }

        public string GetDynamicTableTitleByPosition(string tablePosition)
        {
            return totalDAL.GetDynamicTableTitleByPosition(tablePosition);
        }
        public string GetDynamicTableTitle(string tableId)
        {
            return totalDAL.GetDynamicTableTitle(tableId);
        }
        public string GetTotalCostTableBody(DynamicSetting settingItem, int totalTableIndexCount, string year, string timestampsId, string companiIds)
        {
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";

            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                if (!string.IsNullOrEmpty(settingItem.MethodId))
                {
                    totalTableTd = totalTableTd + GetCostTableBodyWithCalculation(settingItem, Convert.ToInt32(year), timestampsId, companiIds);
                }
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }
        public string GetParameterIdsByMethodId(string tableId, string methodId)
        {
            return totalDAL.GetParameterIdsByMethodId(tableId, methodId);
        }
        public DynamicTableViewModal GetAllCostSum(DynamicTableViewModal _singleMethodCost)
        {
            DynamicTableViewModal _totalCost = new DynamicTableViewModal();
            if (string.IsNullOrEmpty(_totalCost.OctTotalCost.ToString()))
            {
                _totalCost.OctTotalCost = _singleMethodCost.OctTotalCost;
            }
            else
            {
                _totalCost.OctTotalCost = _totalCost.OctTotalCost + _singleMethodCost.OctTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.NovTotalCost.ToString()))
            {
                _totalCost.NovTotalCost = _singleMethodCost.NovTotalCost;
            }
            else
            {
                _totalCost.NovTotalCost = _totalCost.NovTotalCost + _singleMethodCost.NovTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.DecTotalCost.ToString()))
            {
                _totalCost.DecTotalCost = _singleMethodCost.DecTotalCost;
            }
            else
            {
                _totalCost.DecTotalCost = _totalCost.DecTotalCost + _singleMethodCost.DecTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.JanTotalCost.ToString()))
            {
                _totalCost.JanTotalCost = _singleMethodCost.JanTotalCost;
            }
            else
            {
                _totalCost.JanTotalCost = _totalCost.JanTotalCost + _singleMethodCost.JanTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.FebTotalCost.ToString()))
            {
                _totalCost.FebTotalCost = _singleMethodCost.FebTotalCost;
            }
            else
            {
                _totalCost.FebTotalCost = _totalCost.FebTotalCost + _singleMethodCost.FebTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.MarTotalCost.ToString()))
            {
                _totalCost.MarTotalCost = _singleMethodCost.MarTotalCost;
            }
            else
            {
                _totalCost.MarTotalCost = _totalCost.MarTotalCost + _singleMethodCost.MarTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.AprTotalCost.ToString()))
            {
                _totalCost.AprTotalCost = _singleMethodCost.AprTotalCost;
            }
            else
            {
                _totalCost.AprTotalCost = _totalCost.AprTotalCost + _singleMethodCost.AprTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.MayTotalCost.ToString()))
            {
                _totalCost.MayTotalCost = _singleMethodCost.MayTotalCost;
            }
            else
            {
                _totalCost.MayTotalCost = _totalCost.MayTotalCost + _singleMethodCost.MayTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.JunTotalCost.ToString()))
            {
                _totalCost.JunTotalCost = _singleMethodCost.JunTotalCost;
            }
            else
            {
                _totalCost.JunTotalCost = _totalCost.JunTotalCost + _singleMethodCost.JunTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.JulTotalCost.ToString()))
            {
                _totalCost.JulTotalCost = _singleMethodCost.JulTotalCost;
            }
            else
            {
                _totalCost.JulTotalCost = _totalCost.JulTotalCost + _singleMethodCost.JulTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.AugTotalCost.ToString()))
            {
                _totalCost.AugTotalCost = _singleMethodCost.AugTotalCost;
            }
            else
            {
                _totalCost.AugTotalCost = _totalCost.AugTotalCost + _singleMethodCost.AugTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.SepTotalCost.ToString()))
            {
                _totalCost.SepTotalCost = _singleMethodCost.SepTotalCost;
            }
            else
            {
                _totalCost.SepTotalCost = _totalCost.SepTotalCost + _singleMethodCost.SepTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.FirstHalfTotalCost.ToString()))
            {
                _totalCost.FirstHalfTotalCost = _singleMethodCost.FirstHalfTotalCost;
            }
            else
            {
                _totalCost.FirstHalfTotalCost = _totalCost.FirstHalfTotalCost + _singleMethodCost.FirstHalfTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.SecondHalfTotalCost.ToString()))
            {
                _totalCost.SecondHalfTotalCost = _singleMethodCost.SecondHalfTotalCost;
            }
            else
            {
                _totalCost.SecondHalfTotalCost = _totalCost.SecondHalfTotalCost + _singleMethodCost.SecondHalfTotalCost;
            }

            if (string.IsNullOrEmpty(_totalCost.YearTotalCost.ToString()))
            {
                _totalCost.YearTotalCost = _singleMethodCost.YearTotalCost;
            }
            else
            {
                _totalCost.YearTotalCost = _totalCost.YearTotalCost + _singleMethodCost.YearTotalCost;
            }
            return _totalCost;
        }
        public string GetCostTableBodyWithCalculation(DynamicSetting settingItem, int year, string timestampsId, string companyIds)
        {
            DynamicTableViewModal _totalCost = new DynamicTableViewModal();
            string totalTableTd = "";
            //1: Cost for department without QA proration
            if (Convert.ToInt32(settingItem.MethodId) == 1)
            {
                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                dynamicTableViewModal = GetTotalCostWithoutQA(settingItem, companyIds, year, timestampsId);
                //_totalCost = GetAllCostSum(dynamicTableViewModal);

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";

            }
            //2: Cost for department from QA prorationt
            else if (Convert.ToInt32(settingItem.MethodId) == 2)
            {
                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                //dynamicTableViewModal = GetCostForDepartmentFromQAProration(settingItem);

                dynamicTableViewModal = GetTotalCostFromQA(settingItem, companyIds, year, timestampsId);
                //_totalCost = GetAllCostSum(dynamicTableViewModal);

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";
            }
            //3: Cost for department with QA proration
            else if (Convert.ToInt32(settingItem.MethodId) == 3)
            {
                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                //dynamicTableViewModal = GetCostForDepartmentWithQAProration(settingItem);
                dynamicTableViewModal = GetTotalCostWithQA(settingItem, companyIds, year, timestampsId);
                //_totalCost = GetAllCostSum(dynamicTableViewModal);

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";
            }

            return totalTableTd;
        }        

        public string GetTotalTableBodyPart(DynamicSetting settingItem, int totalTableIndexCount)
        {
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";

            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }

        public string GetBudgetTableBodyPart(DynamicSetting settingItem, int totalTableIndexCount,string companyIds,int year,string timestampsId)
        {
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";

            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                //dynamicTableViewModal = GetCostForDepartmentWithQAProration(settingItem);
                dynamicTableViewModal = GetBudgetCostWithoutQA(settingItem, companyIds, year, timestampsId);
                //_totalCost = GetAllCostSum(dynamicTableViewModal);

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";

                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";                
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }
        public string GeDifferenceTableBodyPart(DynamicSetting settingItem, int totalTableIndexCount, string companyIds, int year, string timestampsId)
        {
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";

            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                DynamicTableViewModal costWithQAProportion = new DynamicTableViewModal();
                DynamicTableViewModal budgetWithQAProportion = new DynamicTableViewModal();

                costWithQAProportion = GetTotalCostWithQA(settingItem, companyIds, year, timestampsId);
                budgetWithQAProportion = GetBudgetCostWithoutQA(settingItem, companyIds, year, timestampsId);

                dynamicTableViewModal.OctTotalCost = costWithQAProportion.OctTotalCost - budgetWithQAProportion.OctTotalCost;
                dynamicTableViewModal.NovTotalCost = costWithQAProportion.NovTotalCost - budgetWithQAProportion.NovTotalCost;
                dynamicTableViewModal.DecTotalCost = costWithQAProportion.DecTotalCost - budgetWithQAProportion.DecTotalCost;
                dynamicTableViewModal.JanTotalCost = costWithQAProportion.JanTotalCost - budgetWithQAProportion.JanTotalCost;
                dynamicTableViewModal.FebTotalCost = costWithQAProportion.FebTotalCost - budgetWithQAProportion.FebTotalCost;
                dynamicTableViewModal.MarTotalCost = costWithQAProportion.MarTotalCost - budgetWithQAProportion.MarTotalCost;
                dynamicTableViewModal.AprTotalCost = costWithQAProportion.AprTotalCost - budgetWithQAProportion.AprTotalCost;
                dynamicTableViewModal.MayTotalCost = costWithQAProportion.MayTotalCost - budgetWithQAProportion.MayTotalCost;
                dynamicTableViewModal.JunTotalCost = costWithQAProportion.JunTotalCost - budgetWithQAProportion.JunTotalCost;
                dynamicTableViewModal.JulTotalCost = costWithQAProportion.JulTotalCost - budgetWithQAProportion.JulTotalCost;
                dynamicTableViewModal.AugTotalCost = costWithQAProportion.AugTotalCost - budgetWithQAProportion.AugTotalCost;
                dynamicTableViewModal.SepTotalCost = costWithQAProportion.SepTotalCost - budgetWithQAProportion.SepTotalCost;

                dynamicTableViewModal.FirstHalfTotalCost = costWithQAProportion.FirstHalfTotalCost - budgetWithQAProportion.FirstHalfTotalCost;
                dynamicTableViewModal.SecondHalfTotalCost = costWithQAProportion.SecondHalfTotalCost - budgetWithQAProportion.SecondHalfTotalCost;
                dynamicTableViewModal.YearTotalCost = costWithQAProportion.YearTotalCost - budgetWithQAProportion.YearTotalCost;

                if(Convert.ToInt32(dynamicTableViewModal.OctTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.OctTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.NovTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right'  style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.NovTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.DecTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.DecTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.JanTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JanTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.FebTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FebTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.MarTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MarTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.AprTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AprTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.MayTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.MayTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.JunTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JunTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.JulTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.JulTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.AugTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.AugTotalCost).ToString("N0") + "</td>";
                }
                if (Convert.ToInt32(dynamicTableViewModal.SepTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SepTotalCost).ToString("N0") + "</td>";
                }

                if (Convert.ToInt32(dynamicTableViewModal.YearTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.YearTotalCost).ToString("N0") + "</td>";
                }

                if (Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.FirstHalfTotalCost).ToString("N0") + "</td>";
                }

                if (Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost) < 0)
                {
                    totalTableTd = totalTableTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td class='text-right'>" + Convert.ToInt32(dynamicTableViewModal.SecondHalfTotalCost).ToString("N0") + "</td>";
                }                
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }

        public DynamicTableViewModal GetTotalCostWithoutQA(DynamicSetting settingItem, string companyIds, int year, string timestampsId)
        {
            DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();

            string parameterIds = GetParameterIdsByMethodId(settingItem.DynamicTableId, settingItem.MethodId);
            ActualCostBLL actualCostBLL = new ActualCostBLL();
            EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            double octTotalCost = 0;
            double novTotalCost = 0;
            double decTotalCost = 0;
            double janTotalCost = 0;
            double febTotalCost = 0;
            double marTotalCost = 0;
            double aprTotalCost = 0;
            double mayTotalCost = 0;
            double junTotalCost = 0;
            double julTotalCost = 0;
            double augTotalCost = 0;
            double sepTotalCost = 0;

            double firstSlotCost = 0;
            double secondSlotCost = 0;
            double totalCost = 0;
            double _octHinsho = 0, _novHinsho = 0, _decHinsho = 0, _janHinsho = 0, _febHinsho = 0, _marHinsho = 0, _aprHinsho = 0, _mayHinsho = 0, _junHinsho = 0, _julHinsho = 0, _augHinsho = 0, _sepHinsho = 0;

            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();            
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companyIds, year);            
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            List<Department> departments = departmentBLL.GetDepartmentsById(parameterIds);            

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
                if (string.IsNullOrEmpty(timestampsId))
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
                }
                else
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company_Timestamps(department.Id, companyIds, year, timestampsId);
                }

                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    if (_octActualCostTotal > 0)
                    {
                        rowTotal += _octActualCostTotal + _octHinsho;
                        octTotalCost += _octActualCostTotal + _octHinsho;
                    }
                    else
                    {
                        rowTotal += _octTotal + _octHinsho;
                        octTotalCost += _octTotal + _octHinsho;
                    }
                    if (_novActualCostTotal > 0)
                    {
                        rowTotal += _novActualCostTotal + _novHinsho;
                        novTotalCost += _novActualCostTotal + _novHinsho;
                    }
                    else
                    {
                        rowTotal += _novTotal + _novHinsho;
                        novTotalCost += _novTotal + _novHinsho;
                    }
                    if (_decActualCostTotal > 0)
                    {
                        rowTotal += _decActualCostTotal + _decHinsho;
                        decTotalCost += _decActualCostTotal + _decHinsho;
                    }
                    else
                    {
                        rowTotal += _decTotal + _decHinsho;
                        decTotalCost += _decTotal + _decHinsho;
                    }
                    if (_janActualCostTotal > 0)
                    {
                        rowTotal += _janActualCostTotal + _janHinsho;
                        janTotalCost += _janActualCostTotal + _janHinsho;
                    }
                    else
                    {
                        rowTotal += _janTotal + _janHinsho;
                        janTotalCost += _janTotal + _janHinsho;
                    }
                    if (_febActualCostTotal > 0)
                    {
                        rowTotal += _febActualCostTotal + _febHinsho;
                        febTotalCost += _febActualCostTotal + _febHinsho;
                    }
                    else
                    {
                        rowTotal += _febTotal + _febHinsho;
                        febTotalCost += _febTotal + _febHinsho;
                    }
                    if (_marActualCostTotal > 0)
                    {
                        rowTotal += _marActualCostTotal + _marHinsho;
                        marTotalCost += _marActualCostTotal + _marHinsho;
                    }
                    else
                    {
                        rowTotal += _marTotal + _marHinsho;
                        marTotalCost += _marTotal + _marHinsho;
                    }
                    firstSlot = rowTotal;
                    firstSlotCost += firstSlot;

                    if (_aprActualCostTotal > 0)
                    {
                        rowTotal += _aprActualCostTotal + _aprHinsho;
                        aprTotalCost += _aprActualCostTotal + _aprHinsho;
                    }
                    else
                    {
                        rowTotal += _aprTotal + _aprHinsho;
                        aprTotalCost += _aprTotal + _aprHinsho;
                    }
                    if (_mayActualCostTotal > 0)
                    {
                        rowTotal += _mayActualCostTotal + _mayHinsho;
                        mayTotalCost += _mayActualCostTotal + _mayHinsho;
                    }
                    else
                    {
                        rowTotal += _mayTotal + _mayHinsho;
                        mayTotalCost += _mayTotal + _mayHinsho;
                    }
                    if (_junActualCostTotal > 0)
                    {
                        rowTotal += _junActualCostTotal + _junHinsho;
                        junTotalCost += _junActualCostTotal + _junHinsho;
                    }
                    else
                    {
                        rowTotal += _junTotal + _junHinsho;
                        junTotalCost += _junTotal + _junHinsho;
                    }
                    if (_julActualCostTotal > 0)
                    {
                        rowTotal += _julActualCostTotal + _julHinsho;
                        julTotalCost += _julActualCostTotal + _julHinsho;
                    }
                    else
                    {
                        rowTotal += _julTotal + _julHinsho;
                        julTotalCost += _julTotal + _julHinsho;
                    }
                    if (_augActualCostTotal > 0)
                    {
                        rowTotal += _augActualCostTotal + _augHinsho;
                        augTotalCost += _augActualCostTotal + _augHinsho;
                    }
                    else
                    {
                        rowTotal += _augTotal + _augHinsho;
                        augTotalCost += _augTotal + _augHinsho;
                    }
                    if (_sepActualCostTotal > 0)
                    {
                        rowTotal += _sepActualCostTotal + _sepHinsho;
                        sepTotalCost += _sepActualCostTotal + _sepHinsho;
                    }
                    else
                    {
                        rowTotal += _sepTotal + _sepHinsho;
                        sepTotalCost += _sepTotal + _sepHinsho;
                    }

                    double tempSecondSlotCost = rowTotal - firstSlot;
                    secondSlotCost += tempSecondSlotCost;
                }
                else
                {
                    octTotalCost = octTotalCost + 0;
                    novTotalCost = novTotalCost + 0;
                    decTotalCost = decTotalCost + 0;
                    janTotalCost = janTotalCost + 0;
                    febTotalCost = febTotalCost + 0;
                    marTotalCost = marTotalCost + 0;
                    aprTotalCost = aprTotalCost + 0;
                    mayTotalCost = mayTotalCost + 0;
                    junTotalCost = junTotalCost + 0;
                    julTotalCost = julTotalCost + 0;
                    augTotalCost = augTotalCost + 0;
                    sepTotalCost = sepTotalCost + 0;

                    firstSlotCost = firstSlotCost + 0;
                    secondSlotCost = secondSlotCost + 0;
                }
            }

            totalCost = firstSlotCost + secondSlotCost;

            dynamicTableViewModal.OctTotalCost = octTotalCost;
            dynamicTableViewModal.NovTotalCost = novTotalCost;
            dynamicTableViewModal.DecTotalCost = decTotalCost;
            dynamicTableViewModal.JanTotalCost = janTotalCost;
            dynamicTableViewModal.FebTotalCost = febTotalCost;
            dynamicTableViewModal.MarTotalCost = marTotalCost;
            dynamicTableViewModal.AprTotalCost = aprTotalCost;
            dynamicTableViewModal.MayTotalCost = mayTotalCost;
            dynamicTableViewModal.JunTotalCost = junTotalCost;
            dynamicTableViewModal.JulTotalCost = julTotalCost;
            dynamicTableViewModal.AugTotalCost = augTotalCost;
            dynamicTableViewModal.SepTotalCost = sepTotalCost;

            dynamicTableViewModal.FirstHalfTotalCost = firstSlotCost;
            dynamicTableViewModal.SecondHalfTotalCost = secondSlotCost;
            dynamicTableViewModal.YearTotalCost = totalCost;

            return dynamicTableViewModal;
        }

        public DynamicTableViewModal GetTotalCostWithQA(DynamicSetting settingItem, string companyIds, int year, string timestampsId)
        {
            DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();

            string parameterIds = GetParameterIdsByMethodId(settingItem.DynamicTableId, settingItem.MethodId);
            ActualCostBLL actualCostBLL = new ActualCostBLL();
            EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();

            double octTotalCost = 0;
            double novTotalCost = 0;
            double decTotalCost = 0;
            double janTotalCost = 0;
            double febTotalCost = 0;
            double marTotalCost = 0;
            double aprTotalCost = 0;
            double mayTotalCost = 0;
            double junTotalCost = 0;
            double julTotalCost = 0;
            double augTotalCost = 0;
            double sepTotalCost = 0;

            double firstSlotCost = 0;
            double secondSlotCost = 0;
            double totalCost = 0;
            double _octHinsho = 0, _novHinsho = 0, _decHinsho = 0, _janHinsho = 0, _febHinsho = 0, _marHinsho = 0, _aprHinsho = 0, _mayHinsho = 0, _junHinsho = 0, _julHinsho = 0, _augHinsho = 0, _sepHinsho = 0;

            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companyIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            List<Department> departments = departmentBLL.GetDepartmentsById(parameterIds);

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;

                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;

                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                // update hinso variables by percentage.
                {
                    _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                }

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
                if (string.IsNullOrEmpty(timestampsId))
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
                }
                else
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company_Timestamps(department.Id, companyIds, year, timestampsId);
                }
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                    if (_octActualCostTotal > 0)
                    {                        
                        rowTotal += _octActualCostTotal + _octHinsho;
                        octTotalCost += _octActualCostTotal + _octHinsho;
                    }
                    else
                    {                                            
                        rowTotal += _octTotal + _octHinsho;
                        octTotalCost += _octTotal + _octHinsho; 
                    }
                    if (_novActualCostTotal > 0)
                    {                    
                        rowTotal += _novActualCostTotal + _novHinsho;
                        novTotalCost += _novActualCostTotal + _novHinsho;
                    }
                    else
                    {                    
                        rowTotal += _novTotal + _novHinsho;
                        novTotalCost += _novTotal + _novHinsho;
                    }
                    if (_decActualCostTotal > 0)
                    {                       
                        rowTotal += _decActualCostTotal + _decHinsho;
                        decTotalCost += _decActualCostTotal + _decHinsho; 
                    }
                    else
                    {
                        rowTotal += _decTotal + _decHinsho;
                        decTotalCost += _decTotal + _decHinsho; 
                    }
                    if (_janActualCostTotal > 0)
                    {                       
                        rowTotal += _janActualCostTotal + _janHinsho;
                        janTotalCost += _janActualCostTotal + _janHinsho; 
                    }
                    else
                    {                    
                        rowTotal += _janTotal + _janHinsho;
                        janTotalCost += _janTotal + _janHinsho;
                    }
                    if (_febActualCostTotal > 0)
                    {              
                        rowTotal += _febActualCostTotal + _febHinsho;
                        febTotalCost += _febActualCostTotal + _febHinsho; 
                    }
                    else
                    {                 
                        rowTotal += _febTotal + _febHinsho;
                        febTotalCost += _febTotal + _febHinsho; 
                    }
                    if (_marActualCostTotal > 0)
                    {
                        rowTotal += _marActualCostTotal + _marHinsho;
                        marTotalCost += _marActualCostTotal + _marHinsho; ;
                    }
                    else
                    {
                        rowTotal += _marTotal + _marHinsho;
                        marTotalCost += _marTotal + _marHinsho;
                    }
                    firstSlot = rowTotal;
                    firstSlotCost += firstSlot;                    

                    if (_aprActualCostTotal > 0)
                    {                       
                        rowTotal += _aprActualCostTotal + _aprHinsho;
                        aprTotalCost += _aprActualCostTotal + _aprHinsho;
                    }
                    else
                    {                       
                        rowTotal += _aprTotal + _aprHinsho;
                        aprTotalCost += _aprTotal + _aprHinsho; ;
                    }
                    if (_mayActualCostTotal > 0)
                    {
                        rowTotal += _mayActualCostTotal + _mayHinsho;
                        mayTotalCost += _mayActualCostTotal + _mayHinsho;
                    }
                    else
                    {
                        rowTotal += _mayTotal + _mayHinsho;
                        mayTotalCost += _mayTotal + _mayHinsho; ;
                    }
                    if (_junActualCostTotal > 0)
                    {
                        rowTotal += _junActualCostTotal + _junHinsho;
                        junTotalCost += _junActualCostTotal + _junHinsho; ;
                    }
                    else
                    {
                        rowTotal += _junTotal + _junHinsho;
                        junTotalCost += _junTotal + _junHinsho;
                    }
                    if (_julActualCostTotal > 0)
                    {                    
                        rowTotal += _julActualCostTotal + _julHinsho;
                        julTotalCost += _julActualCostTotal + _julHinsho;
                    }
                    else
                    {
                        rowTotal += _julTotal + _julHinsho;
                        julTotalCost += _julTotal + _julHinsho;
                    }
                    if (_augActualCostTotal > 0)
                    {
                        rowTotal += _augActualCostTotal + _augHinsho;
                        augTotalCost += _augActualCostTotal + _augHinsho;
                    }
                    else
                    {
                        rowTotal += _augTotal + _augHinsho;
                        augTotalCost += _augTotal + _augHinsho;
                    }
                    if (_sepActualCostTotal > 0)
                    {
                        rowTotal += _sepActualCostTotal + _sepHinsho;
                        sepTotalCost += _sepActualCostTotal + _sepHinsho;
                    }
                    else
                    {                     
                        rowTotal += _sepTotal + _sepHinsho;
                        sepTotalCost += _sepTotal + _sepHinsho;
                    }

                    double tempSecondSlotCost = rowTotal - firstSlot;
                    secondSlotCost += tempSecondSlotCost;
                }
                else
                {
                    octTotalCost = octTotalCost + 0;
                    novTotalCost = novTotalCost + 0;
                    decTotalCost = decTotalCost + 0;
                    janTotalCost = janTotalCost + 0;
                    febTotalCost = febTotalCost + 0;
                    marTotalCost = marTotalCost + 0;
                    aprTotalCost = aprTotalCost + 0;
                    mayTotalCost = mayTotalCost + 0;
                    junTotalCost = junTotalCost + 0;
                    julTotalCost = julTotalCost + 0;
                    augTotalCost = augTotalCost + 0;
                    sepTotalCost = sepTotalCost + 0;

                    firstSlotCost = firstSlotCost + 0;
                    secondSlotCost = secondSlotCost + 0;
                }
            }

            totalCost = firstSlotCost + secondSlotCost;

            dynamicTableViewModal.OctTotalCost = octTotalCost;
            dynamicTableViewModal.NovTotalCost = novTotalCost;
            dynamicTableViewModal.DecTotalCost = decTotalCost;
            dynamicTableViewModal.JanTotalCost = janTotalCost;
            dynamicTableViewModal.FebTotalCost = febTotalCost;
            dynamicTableViewModal.MarTotalCost = marTotalCost;
            dynamicTableViewModal.AprTotalCost = aprTotalCost;
            dynamicTableViewModal.MayTotalCost = mayTotalCost;
            dynamicTableViewModal.JunTotalCost = junTotalCost;
            dynamicTableViewModal.JulTotalCost = julTotalCost;
            dynamicTableViewModal.AugTotalCost = augTotalCost;
            dynamicTableViewModal.SepTotalCost = sepTotalCost;

            dynamicTableViewModal.FirstHalfTotalCost = firstSlotCost;
            dynamicTableViewModal.SecondHalfTotalCost = secondSlotCost;
            dynamicTableViewModal.YearTotalCost = totalCost;

            return dynamicTableViewModal;
        }

        public DynamicTableViewModal GetTotalCostFromQA(DynamicSetting settingItem, string companyIds, int year, string timestampsId)
        {
            DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
            string parameterIds = GetParameterIdsByMethodId(settingItem.DynamicTableId, settingItem.MethodId);

            ActualCostBLL actualCostBLL = new ActualCostBLL();
            EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();

            List<Department> departments = departmentBLL.GetDepartmentsById(parameterIds);
            List<Department> allDepartments = departmentBLL.GetAllDepartments();
            Department qaDepartmentByName = allDepartments.Where(d => d.DepartmentName == "品証").SingleOrDefault();

            double _octHinsho = 0, _novHinsho = 0, _decHinsho = 0, _janHinsho = 0, _febHinsho = 0, _marHinsho = 0, _aprHinsho = 0, _mayHinsho = 0, _junHinsho = 0, _julHinsho = 0, _augHinsho = 0, _sepHinsho = 0;
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companyIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            double octTotalCost = 0;
            double novTotalCost = 0;
            double decTotalCost = 0;
            double janTotalCost = 0;
            double febTotalCost = 0;
            double marTotalCost = 0;
            double aprTotalCost = 0;
            double mayTotalCost = 0;
            double junTotalCost = 0;
            double julTotalCost = 0;
            double augTotalCost = 0;
            double sepTotalCost = 0;

            double firstSlotCost = 0;
            double secondSlotCost = 0;
            double totalCost = 0;

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;

                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
                if (string.IsNullOrEmpty(timestampsId))
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
                }
                else
                {
                    forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company_Timestamps(department.Id, companyIds, year, timestampsId);
                }

                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    if (_octActualCostTotal > 0)
                    {
                        rowTotal += _octActualCostTotal + _octCalculation;
                        octTotalCost += _octActualCostTotal + _octCalculation;
                    }
                    else
                    {
                        rowTotal += _octTotal + _octCalculation;
                        octTotalCost += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    if (_novActualCostTotal > 0)
                    {
                        rowTotal += _novActualCostTotal + _novCalculation;
                        novTotalCost += _novActualCostTotal + _novCalculation;
                    }
                    else
                    {
                        rowTotal += _novTotal + _novCalculation;
                        novTotalCost += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    if (_decActualCostTotal > 0)
                    {
                        rowTotal += _decActualCostTotal + _decCalculation;
                        decTotalCost += _decActualCostTotal + _decCalculation;
                    }
                    else
                    {
                        rowTotal += _decTotal + _decCalculation;
                        decTotalCost += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    if (_janActualCostTotal > 0)
                    {
                        rowTotal += _janActualCostTotal + _janCalculation;
                        janTotalCost += _janActualCostTotal + _janCalculation;
                    }
                    else
                    {
                        rowTotal += _janTotal + _janCalculation;
                        janTotalCost += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    if (_febActualCostTotal > 0)
                    {
                        rowTotal += _febActualCostTotal + _febCalculation;
                        febTotalCost += _febActualCostTotal + _febCalculation;
                    }
                    else
                    {
                        rowTotal += _febTotal + _febCalculation;
                        febTotalCost += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    if (_marActualCostTotal > 0)
                    {
                        rowTotal += _marActualCostTotal + _marCalculation;
                        marTotalCost += _marActualCostTotal + _marCalculation;
                    }
                    else
                    {
                        rowTotal += _marTotal + _marCalculation;
                        marTotalCost += _marTotal + _marCalculation;
                    }
                    firstSlot = rowTotal;
                    firstSlotCost += firstSlot;

                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    if (_aprActualCostTotal > 0)
                    {
                        rowTotal += _aprActualCostTotal + _aprCalculation;
                        aprTotalCost += _aprActualCostTotal + _aprCalculation;
                    }
                    else
                    {
                        rowTotal += _aprTotal + _aprCalculation;
                        aprTotalCost += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    if (_mayActualCostTotal > 0)
                    {
                        rowTotal += _mayActualCostTotal + _mayCalculation;
                        mayTotalCost += _mayActualCostTotal + _mayCalculation;
                    }
                    else
                    {
                        rowTotal += _mayTotal + _mayCalculation;
                        mayTotalCost += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    if (_junActualCostTotal > 0)
                    {
                        rowTotal += _junActualCostTotal + _junCalculation;
                        junTotalCost += _junActualCostTotal + _junCalculation; 
                    }
                    else
                    {
                        rowTotal += _junTotal + _junCalculation;
                        junTotalCost += _junTotal + _junCalculation;
                    }
                   var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    if (_julActualCostTotal > 0)
                    {
                        rowTotal += _julActualCostTotal + _julCalculation;
                        julTotalCost += _julActualCostTotal + _julCalculation;
                    }
                    else
                    {
                        rowTotal += _julTotal + _julCalculation;
                        julTotalCost += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    if (_augActualCostTotal > 0)
                    {
                        rowTotal += _augActualCostTotal + _augCalculation;
                        augTotalCost += _augActualCostTotal + _augCalculation;
                    }
                    else
                    {
                        rowTotal += _augTotal + _augCalculation;
                        augTotalCost += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    if (_sepActualCostTotal > 0)
                    {
                        rowTotal += _sepActualCostTotal + _sepCalculation;
                        sepTotalCost += _sepActualCostTotal + _sepCalculation;
                    }
                    else
                    {
                        rowTotal += _sepTotal + _sepCalculation;
                        sepTotalCost += _sepTotal + _sepCalculation;
                    }

                    double tempSecondSlotCost = rowTotal - firstSlot;
                    secondSlotCost += tempSecondSlotCost;
                }
                else
                {
                    octTotalCost = octTotalCost + 0;
                    novTotalCost = novTotalCost + 0;
                    decTotalCost = decTotalCost + 0;
                    janTotalCost = janTotalCost + 0;
                    febTotalCost = febTotalCost + 0;
                    marTotalCost = marTotalCost + 0;
                    aprTotalCost = aprTotalCost + 0;
                    mayTotalCost = mayTotalCost + 0;
                    junTotalCost = junTotalCost + 0;
                    julTotalCost = julTotalCost + 0;
                    augTotalCost = augTotalCost + 0;
                    sepTotalCost = sepTotalCost + 0;

                    firstSlotCost = firstSlotCost + 0;
                    secondSlotCost = secondSlotCost + 0;

                }
            }

            totalCost = firstSlotCost + secondSlotCost;

            dynamicTableViewModal.OctTotalCost = octTotalCost;
            dynamicTableViewModal.NovTotalCost = novTotalCost;
            dynamicTableViewModal.DecTotalCost = decTotalCost;
            dynamicTableViewModal.JanTotalCost = janTotalCost;
            dynamicTableViewModal.FebTotalCost = febTotalCost;
            dynamicTableViewModal.MarTotalCost = marTotalCost;
            dynamicTableViewModal.AprTotalCost = aprTotalCost;
            dynamicTableViewModal.MayTotalCost = mayTotalCost;
            dynamicTableViewModal.JunTotalCost = junTotalCost;
            dynamicTableViewModal.JulTotalCost = julTotalCost;
            dynamicTableViewModal.AugTotalCost = augTotalCost;
            dynamicTableViewModal.SepTotalCost = sepTotalCost;

            dynamicTableViewModal.FirstHalfTotalCost = firstSlotCost;
            dynamicTableViewModal.SecondHalfTotalCost = secondSlotCost;
            dynamicTableViewModal.YearTotalCost = totalCost;

            return dynamicTableViewModal;
        }
        public DynamicTableViewModal GetBudgetCostWithoutQA(DynamicSetting settingItem, string companyIds, int year, string timestampsId)
        {
            DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();

            string parameterIds = GetParameterIdsByMethodId(settingItem.DynamicTableId, settingItem.MethodId);
            ActualCostBLL actualCostBLL = new ActualCostBLL();
            EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            double octTotalCost = 0;
            double novTotalCost = 0;
            double decTotalCost = 0;
            double janTotalCost = 0;
            double febTotalCost = 0;
            double marTotalCost = 0;
            double aprTotalCost = 0;
            double mayTotalCost = 0;
            double junTotalCost = 0;
            double julTotalCost = 0;
            double augTotalCost = 0;
            double sepTotalCost = 0;

            double firstSlotCost = 0;
            double secondSlotCost = 0;
            double totalCost = 0;
            double _octHinsho = 0, _novHinsho = 0, _decHinsho = 0, _janHinsho = 0, _febHinsho = 0, _marHinsho = 0, _aprHinsho = 0, _mayHinsho = 0, _junHinsho = 0, _julHinsho = 0, _augHinsho = 0, _sepHinsho = 0;

            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            var hinsoData = GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companyIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            List<Department> departments = departmentBLL.GetDepartmentsById(parameterIds);

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
                forecastAssignmentViewModels = GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);

                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = 0;//forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    if (_octActualCostTotal > 0)
                    {
                        rowTotal += _octActualCostTotal + _octHinsho;
                        octTotalCost += _octActualCostTotal + _octHinsho;
                    }
                    else
                    {
                        rowTotal += _octTotal + _octHinsho;
                        octTotalCost += _octTotal + _octHinsho;
                    }
                    if (_novActualCostTotal > 0)
                    {
                        rowTotal += _novActualCostTotal + _novHinsho;
                        novTotalCost += _novActualCostTotal + _novHinsho;
                    }
                    else
                    {
                        rowTotal += _novTotal + _novHinsho;
                        novTotalCost += _novTotal + _novHinsho;
                    }
                    if (_decActualCostTotal > 0)
                    {
                        rowTotal += _decActualCostTotal + _decHinsho;
                        decTotalCost += _decActualCostTotal + _decHinsho;
                    }
                    else
                    {
                        rowTotal += _decTotal + _decHinsho;
                        decTotalCost += _decTotal + _decHinsho;
                    }
                    if (_janActualCostTotal > 0)
                    {
                        rowTotal += _janActualCostTotal + _janHinsho;
                        janTotalCost += _janActualCostTotal + _janHinsho;
                    }
                    else
                    {
                        rowTotal += _janTotal + _janHinsho;
                        janTotalCost += _janTotal + _janHinsho;
                    }
                    if (_febActualCostTotal > 0)
                    {
                        rowTotal += _febActualCostTotal + _febHinsho;
                        febTotalCost += _febActualCostTotal + _febHinsho;
                    }
                    else
                    {
                        rowTotal += _febTotal + _febHinsho;
                        febTotalCost += _febTotal + _febHinsho;
                    }
                    if (_marActualCostTotal > 0)
                    {
                        rowTotal += _marActualCostTotal + _marHinsho;
                        marTotalCost += _marActualCostTotal + _marHinsho;
                    }
                    else
                    {
                        rowTotal += _marTotal + _marHinsho;
                        marTotalCost += _marTotal + _marHinsho;
                    }
                    firstSlot = rowTotal;
                    firstSlotCost += firstSlot;

                    if (_aprActualCostTotal > 0)
                    {
                        rowTotal += _aprActualCostTotal + _aprHinsho;
                        aprTotalCost += _aprActualCostTotal + _aprHinsho;
                    }
                    else
                    {
                        rowTotal += _aprTotal + _aprHinsho;
                        aprTotalCost += _aprTotal + _aprHinsho;
                    }
                    if (_mayActualCostTotal > 0)
                    {
                        rowTotal += _mayActualCostTotal + _mayHinsho;
                        mayTotalCost += _mayActualCostTotal + _mayHinsho;
                    }
                    else
                    {
                        rowTotal += _mayTotal + _mayHinsho;
                        mayTotalCost += _mayTotal + _mayHinsho;
                    }
                    if (_junActualCostTotal > 0)
                    {
                        rowTotal += _junActualCostTotal + _junHinsho;
                        junTotalCost += _junActualCostTotal + _junHinsho;
                    }
                    else
                    {
                        rowTotal += _junTotal + _junHinsho;
                        junTotalCost += _junTotal + _junHinsho;
                    }
                    if (_julActualCostTotal > 0)
                    {
                        rowTotal += _julActualCostTotal + _julHinsho;
                        julTotalCost += _julActualCostTotal + _julHinsho;
                    }
                    else
                    {
                        rowTotal += _julTotal + _julHinsho;
                        julTotalCost += _julTotal + _julHinsho;
                    }
                    if (_augActualCostTotal > 0)
                    {
                        rowTotal += _augActualCostTotal + _augHinsho;
                        augTotalCost += _augActualCostTotal + _augHinsho;
                    }
                    else
                    {
                        rowTotal += _augTotal + _augHinsho;
                        augTotalCost += _augTotal + _augHinsho;
                    }
                    if (_sepActualCostTotal > 0)
                    {
                        rowTotal += _sepActualCostTotal + _sepHinsho;
                        sepTotalCost += _sepActualCostTotal + _sepHinsho;
                    }
                    else
                    {
                        rowTotal += _sepTotal + _sepHinsho;
                        sepTotalCost += _sepTotal + _sepHinsho;
                    }

                    double tempSecondSlotCost = rowTotal - firstSlot;
                    secondSlotCost += tempSecondSlotCost;
                }
                else
                {
                    octTotalCost = octTotalCost + 0;
                    novTotalCost = novTotalCost + 0;
                    decTotalCost = decTotalCost + 0;
                    janTotalCost = janTotalCost + 0;
                    febTotalCost = febTotalCost + 0;
                    marTotalCost = marTotalCost + 0;
                    aprTotalCost = aprTotalCost + 0;
                    mayTotalCost = mayTotalCost + 0;
                    junTotalCost = junTotalCost + 0;
                    julTotalCost = julTotalCost + 0;
                    augTotalCost = augTotalCost + 0;
                    sepTotalCost = sepTotalCost + 0;

                    firstSlotCost = firstSlotCost + 0;
                    secondSlotCost = secondSlotCost + 0;
                }
            }

            totalCost = firstSlotCost + secondSlotCost;

            dynamicTableViewModal.OctTotalCost = octTotalCost;
            dynamicTableViewModal.NovTotalCost = novTotalCost;
            dynamicTableViewModal.DecTotalCost = decTotalCost;
            dynamicTableViewModal.JanTotalCost = janTotalCost;
            dynamicTableViewModal.FebTotalCost = febTotalCost;
            dynamicTableViewModal.MarTotalCost = marTotalCost;
            dynamicTableViewModal.AprTotalCost = aprTotalCost;
            dynamicTableViewModal.MayTotalCost = mayTotalCost;
            dynamicTableViewModal.JunTotalCost = junTotalCost;
            dynamicTableViewModal.JulTotalCost = julTotalCost;
            dynamicTableViewModal.AugTotalCost = augTotalCost;
            dynamicTableViewModal.SepTotalCost = sepTotalCost;

            dynamicTableViewModal.FirstHalfTotalCost = firstSlotCost;
            dynamicTableViewModal.SecondHalfTotalCost = secondSlotCost;
            dynamicTableViewModal.YearTotalCost = totalCost;

            return dynamicTableViewModal;
        }
        public List<SukeyQADto> GetTotalCostForDifferenceWithQA(string companiIds, int departmentId, int year)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            //int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            //int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            //year = forecastLeatestYear;
            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            if (qaDepartmentByName == null)
            {
                return sukeyQADtos;
            }
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            List<Department> departments = departmentBLL.GetAllDepartments();
            foreach (var department in departments)
            {
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DependencyName = department.DepartmentName;
                if (department.Id == departmentId)
                {
                    var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == departmentId).SingleOrDefault();
                    if (apportionmentByDepartment == null)
                    {
                        apportionmentByDepartment = new Apportionment();
                    }

                    // update hinso variables by percentage.
                    {
                        _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                        _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                        _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                        _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                        _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                        _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                        _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                        _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                        _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                        _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                        _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                        _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    }


                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                    if (forecastAssignmentViewModels.Count > 0)
                    {
                        double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                        double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                        double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                        double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                        double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                        double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                        double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                        double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                        double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                        double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                        double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                        double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                        double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                        double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                        double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                        double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                        double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                        double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                        double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                        double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                        double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                        double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                        double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                        double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                        if (_octActualCostTotal > 0)
                        {
                            //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octActualCostTotal);
                            sukeyDto.OctCost.Add(Math.Round(_octHinsho, 2));
                            sukeyDto.OctCost.Add(_octActualCostTotal + _octHinsho);

                            rowTotalDept += _octActualCostTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octActualCostTotal + _octHinsho;
                        }
                        else
                        {
                            //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octTotal);
                            sukeyDto.OctCost.Add(Math.Round(_octHinsho, 2));
                            sukeyDto.OctCost.Add(_octTotal + _octHinsho);

                            rowTotalDept += _octTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octTotal + _octHinsho;
                        }
                        if (_novActualCostTotal > 0)
                        {
                            //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novActualCostTotal);
                            sukeyDto.NovCost.Add(Math.Round(_novHinsho, 2));
                            sukeyDto.NovCost.Add(_novActualCostTotal + _novHinsho);

                            rowTotalDept += _novActualCostTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novActualCostTotal + _novHinsho;
                        }
                        else
                        {
                            //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novTotal);
                            sukeyDto.NovCost.Add(Math.Round(_novHinsho, 2));
                            sukeyDto.NovCost.Add(_novTotal + _novHinsho);

                            rowTotalDept += _novTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novTotal + _novHinsho;
                        }
                        if (_decActualCostTotal > 0)
                        {
                            //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decActualCostTotal);
                            sukeyDto.DecCost.Add(Math.Round(_decHinsho, 2));
                            sukeyDto.DecCost.Add(_decActualCostTotal + _decHinsho);

                            rowTotalDept += _decActualCostTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decActualCostTotal + _decHinsho;
                        }
                        else
                        {
                            //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decTotal);
                            sukeyDto.DecCost.Add(Math.Round(_decHinsho, 2));
                            sukeyDto.DecCost.Add(_decTotal + _decHinsho);

                            rowTotalDept += _decTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decTotal + _decHinsho;
                        }
                        if (_janActualCostTotal > 0)
                        {
                            //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janActualCostTotal);
                            sukeyDto.JanCost.Add(Math.Round(_janHinsho, 2));
                            sukeyDto.JanCost.Add(_janActualCostTotal + _janHinsho);

                            rowTotalDept += _janActualCostTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janActualCostTotal + _janHinsho;
                        }
                        else
                        {
                            //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janTotal);
                            sukeyDto.JanCost.Add(Math.Round(_janHinsho, 2));
                            sukeyDto.JanCost.Add(_janTotal + _janHinsho);

                            rowTotalDept += _janTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janTotal + _janHinsho;
                        }
                        if (_febActualCostTotal > 0)
                        {
                            //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febActualCostTotal);
                            sukeyDto.FebCost.Add(Math.Round(_febHinsho, 2));
                            sukeyDto.FebCost.Add(_febActualCostTotal + _febHinsho);

                            rowTotalDept += _febActualCostTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febActualCostTotal + _febHinsho;
                        }
                        else
                        {
                            //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febTotal);
                            sukeyDto.FebCost.Add(Math.Round(_febHinsho, 2));
                            sukeyDto.FebCost.Add(_febTotal + _febHinsho);

                            rowTotalDept += _febTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febTotal + _febHinsho;
                        }
                        if (_marActualCostTotal > 0)
                        {
                            //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marActualCostTotal);
                            sukeyDto.MarCost.Add(Math.Round(_marHinsho, 2));
                            sukeyDto.MarCost.Add(_marActualCostTotal + _marHinsho);

                            rowTotalDept += _marActualCostTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marActualCostTotal + _marHinsho;
                        }
                        else
                        {
                            //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marTotal);
                            sukeyDto.MarCost.Add(Math.Round(_marHinsho, 2));
                            sukeyDto.MarCost.Add(_marTotal + _marHinsho);

                            rowTotalDept += _marTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marTotal + _marHinsho;
                        }

                        deptFirstSlot = rowTotalDept;
                        qaFirstSlot = rowTotalQa;
                        totalFirstSlot = rowTotal;


                        if (_aprActualCostTotal > 0)
                        {
                            //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprActualCostTotal);
                            sukeyDto.AprCost.Add(Math.Round(_aprHinsho, 2));
                            sukeyDto.AprCost.Add(_aprActualCostTotal + _aprHinsho);

                            rowTotalDept += _aprActualCostTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprActualCostTotal + _aprHinsho;
                        }
                        else
                        {
                            //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprTotal);
                            sukeyDto.AprCost.Add(Math.Round(_aprHinsho, 2));
                            sukeyDto.AprCost.Add(_aprTotal + _aprHinsho);


                            rowTotalDept += _aprTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprTotal + _aprHinsho;
                        }
                        if (_mayActualCostTotal > 0)
                        {
                            //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayActualCostTotal);
                            sukeyDto.MayCost.Add(Math.Round(_mayHinsho, 2));
                            sukeyDto.MayCost.Add(_mayActualCostTotal + _mayHinsho);

                            rowTotalDept += _mayActualCostTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayActualCostTotal + _mayHinsho;
                        }
                        else
                        {
                            //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayTotal);
                            sukeyDto.MayCost.Add(Math.Round(_mayHinsho, 2));
                            sukeyDto.MayCost.Add(_mayTotal + _mayHinsho);

                            rowTotalDept += _mayTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayTotal + _mayHinsho;
                        }
                        if (_junActualCostTotal > 0)
                        {
                            //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junActualCostTotal);
                            sukeyDto.JunCost.Add(Math.Round(_junHinsho, 2));
                            sukeyDto.JunCost.Add(_junActualCostTotal + _junHinsho);

                            rowTotalDept += _junActualCostTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junActualCostTotal + _junHinsho;
                        }
                        else
                        {
                            //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junTotal);
                            sukeyDto.JunCost.Add(Math.Round(_junHinsho, 2));
                            sukeyDto.JunCost.Add(_junTotal + _junHinsho);

                            rowTotalDept += _junTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junTotal + _junHinsho;
                        }
                        if (_julActualCostTotal > 0)
                        {
                            //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julActualCostTotal);
                            sukeyDto.JulCost.Add(Math.Round(_julHinsho, 2));
                            sukeyDto.JulCost.Add(_julActualCostTotal + _julHinsho);

                            rowTotalDept += _julActualCostTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julActualCostTotal + _julHinsho;
                        }
                        else
                        {
                            //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julTotal);
                            sukeyDto.JulCost.Add(Math.Round(_julHinsho, 2));
                            sukeyDto.JulCost.Add(_julTotal + _julHinsho);

                            rowTotalDept += _julTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julTotal + _julHinsho;
                        }
                        if (_augActualCostTotal > 0)
                        {
                            //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augActualCostTotal);
                            sukeyDto.AugCost.Add(Math.Round(_augHinsho, 2));
                            sukeyDto.AugCost.Add(_augActualCostTotal + _augHinsho);

                            rowTotalDept += _augActualCostTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augActualCostTotal + _augHinsho;
                        }
                        else
                        {
                            //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augTotal);
                            sukeyDto.AugCost.Add(Math.Round(_augHinsho, 2));
                            sukeyDto.AugCost.Add(_augTotal + _augHinsho);

                            rowTotalDept += _augTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augTotal + _augHinsho;
                        }
                        if (_sepActualCostTotal > 0)
                        {
                            //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepActualCostTotal);
                            sukeyDto.SepCost.Add(Math.Round(_sepHinsho, 2));
                            sukeyDto.SepCost.Add(_sepActualCostTotal + _sepHinsho);

                            rowTotalDept += _sepActualCostTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepActualCostTotal + _sepHinsho;
                        }
                        else
                        {
                            //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepTotal);
                            sukeyDto.SepCost.Add(Math.Round(_sepHinsho, 2));
                            sukeyDto.SepCost.Add(_sepTotal + _sepHinsho);

                            rowTotalDept += _sepTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepTotal + _sepHinsho;
                        }

                        sukeyDto.RowTotal.Add(rowTotalDept);
                        sukeyDto.RowTotal.Add(rowTotalQa);
                        sukeyDto.RowTotal.Add(rowTotal);

                        sukeyDto.FirstSlot.Add(deptFirstSlot);
                        sukeyDto.FirstSlot.Add(qaFirstSlot);
                        sukeyDto.FirstSlot.Add(totalFirstSlot);

                        sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotalQa - qaFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
                    }
                    else
                    {
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);

                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);

                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);

                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);

                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);

                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);

                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);

                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);

                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);

                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);

                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);

                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);

                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);

                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);

                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                    }



                    sukeyQADtos.Add(sukeyDto);
                }
            }
            return sukeyQADtos;
        }

        public List<SukeyQADto> GetTotalBudgetCost(string companiIds, int departmentId, int year)
        {
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;

            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            List<Department> departments = departmentBLL.GetAllDepartments();            
           
            double rowTotal = 0;
            double firstSlot = 0;
            Department department = departmentBLL.GetDepartmentByDepartemntId(departmentId);
            SukeyQADto sukeyDto = new SukeyQADto();
            sukeyDto.DepartmentId = department.Id.ToString();
            sukeyDto.DependencyName = department.DepartmentName;
            //if (department.Id == qaDepartmentByName.Id)
            //{
            //    continue;
            //}
            var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
            if (apportionmentByDepartment == null)
            {
                apportionmentByDepartment = new Apportionment();
            }

            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
            if (forecastAssignmentViewModels.Count > 0)
            {
                double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                {
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(_octTotal + _octCalculation);
                    rowTotal += _octTotal + _octCalculation;
                }
                var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);

                {
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(_novTotal + _novCalculation);
                    rowTotal += _novTotal + _novCalculation;
                }
                var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);

                {
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(_decTotal + _decCalculation);
                    rowTotal += _decTotal + _decCalculation;
                }
                var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);

                {
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(_janTotal + _janCalculation);
                    rowTotal += _janTotal + _janCalculation;
                }
                var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);

                {
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(_febTotal + _febCalculation);
                    rowTotal += _febTotal + _febCalculation;
                }
                var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);

                {
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(_marTotal + _marCalculation);
                    rowTotal += _marTotal + _marCalculation;
                }
                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(rowTotal);
                firstSlot = rowTotal;

                var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);

                {
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
                    rowTotal += _aprTotal + _aprCalculation;
                }
                var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);

                {
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
                    rowTotal += _mayTotal + _mayCalculation;
                }
                var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);

                {
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(_junTotal + _junCalculation);
                    rowTotal += _junTotal + _junCalculation;
                }
                var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);

                {
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(_julTotal + _julCalculation);
                    rowTotal += _julTotal + _julCalculation;
                }
                var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);

                {
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(_augTotal + _augCalculation);
                    rowTotal += _augTotal + _augCalculation;
                }
                var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);

                {
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
                    rowTotal += _sepTotal + _sepCalculation;
                }
                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(rowTotal);

                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

            }
            else
            {
                sukeyDto.OctCost.Add(0);
                sukeyDto.OctCost.Add(0);
                sukeyDto.OctCost.Add(0);

                sukeyDto.NovCost.Add(0);
                sukeyDto.NovCost.Add(0);
                sukeyDto.NovCost.Add(0);

                sukeyDto.DecCost.Add(0);
                sukeyDto.DecCost.Add(0);
                sukeyDto.DecCost.Add(0);

                sukeyDto.JanCost.Add(0);
                sukeyDto.JanCost.Add(0);
                sukeyDto.JanCost.Add(0);

                sukeyDto.FebCost.Add(0);
                sukeyDto.FebCost.Add(0);
                sukeyDto.FebCost.Add(0);

                sukeyDto.MarCost.Add(0);
                sukeyDto.MarCost.Add(0);
                sukeyDto.MarCost.Add(0);

                sukeyDto.AprCost.Add(0);
                sukeyDto.AprCost.Add(0);
                sukeyDto.AprCost.Add(0);

                sukeyDto.MayCost.Add(0);
                sukeyDto.MayCost.Add(0);
                sukeyDto.MayCost.Add(0);

                sukeyDto.JunCost.Add(0);
                sukeyDto.JunCost.Add(0);
                sukeyDto.JunCost.Add(0);

                sukeyDto.JulCost.Add(0);
                sukeyDto.JulCost.Add(0);
                sukeyDto.JulCost.Add(0);

                sukeyDto.AugCost.Add(0);
                sukeyDto.AugCost.Add(0);
                sukeyDto.AugCost.Add(0);

                sukeyDto.SepCost.Add(0);
                sukeyDto.SepCost.Add(0);
                sukeyDto.SepCost.Add(0);

                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(0);

                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(0);

                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(0);
            }
            
            sukeyQADtos.Add(sukeyDto);            

            return sukeyQADtos;
        }

        public List<SukeyQADto> GetDifferenceCostByDepartments(List<SukeyQADto> _objYearDataCost, List<SukeyQADto> _objTotalBudgetCost)
        {
            //year data-budget
            List<SukeyQADto> _objDifferenceTotals = new List<SukeyQADto>();
            foreach(var itemYearly in _objYearDataCost)
            {
                foreach (var itemBudget in _objTotalBudgetCost)
                {
                    SukeyQADto _objDifferenceTotal = new SukeyQADto();
                    
                    _objDifferenceTotal.OctCost.Add(Convert.ToDouble(itemYearly.OctCost[0]) - Convert.ToDouble(itemBudget.OctCost[0]));
                    _objDifferenceTotal.OctCost.Add(Convert.ToDouble(itemYearly.OctCost[1]) - Convert.ToDouble(itemBudget.OctCost[1]));
                    _objDifferenceTotal.OctCost.Add(Convert.ToDouble(itemYearly.OctCost[2]) - Convert.ToDouble(itemBudget.OctCost[2]));

                    _objDifferenceTotal.NovCost.Add(Convert.ToDouble(itemYearly.NovCost[0]) - Convert.ToDouble(itemBudget.NovCost[0]));
                    _objDifferenceTotal.NovCost.Add(Convert.ToDouble(itemYearly.NovCost[1]) - Convert.ToDouble(itemBudget.NovCost[1]));
                    _objDifferenceTotal.NovCost.Add(Convert.ToDouble(itemYearly.NovCost[2]) - Convert.ToDouble(itemBudget.NovCost[2]));

                    _objDifferenceTotal.DecCost.Add(Convert.ToDouble(itemYearly.DecCost[0]) - Convert.ToDouble(itemBudget.DecCost[0]));
                    _objDifferenceTotal.DecCost.Add(Convert.ToDouble(itemYearly.DecCost[1]) - Convert.ToDouble(itemBudget.DecCost[1]));
                    _objDifferenceTotal.DecCost.Add(Convert.ToDouble(itemYearly.DecCost[2]) - Convert.ToDouble(itemBudget.DecCost[2]));

                    _objDifferenceTotal.JanCost.Add(Convert.ToDouble(itemYearly.JanCost[0]) - Convert.ToDouble(itemBudget.JanCost[0]));
                    _objDifferenceTotal.JanCost.Add(Convert.ToDouble(itemYearly.JanCost[1]) - Convert.ToDouble(itemBudget.JanCost[1]));
                    _objDifferenceTotal.JanCost.Add(Convert.ToDouble(itemYearly.JanCost[2]) - Convert.ToDouble(itemBudget.JanCost[2]));

                    _objDifferenceTotal.FebCost.Add(Convert.ToDouble(itemYearly.FebCost[0]) - Convert.ToDouble(itemBudget.FebCost[0]));
                    _objDifferenceTotal.FebCost.Add(Convert.ToDouble(itemYearly.FebCost[1]) - Convert.ToDouble(itemBudget.FebCost[1]));
                    _objDifferenceTotal.FebCost.Add(Convert.ToDouble(itemYearly.FebCost[2]) - Convert.ToDouble(itemBudget.FebCost[2]));

                    _objDifferenceTotal.MarCost.Add(Convert.ToDouble(itemYearly.MarCost[0]) - Convert.ToDouble(itemBudget.MarCost[0]));
                    _objDifferenceTotal.MarCost.Add(Convert.ToDouble(itemYearly.MarCost[1]) - Convert.ToDouble(itemBudget.MarCost[1]));
                    _objDifferenceTotal.MarCost.Add(Convert.ToDouble(itemYearly.MarCost[2]) - Convert.ToDouble(itemBudget.MarCost[2]));

                    _objDifferenceTotal.AprCost.Add(Convert.ToDouble(itemYearly.AprCost[0]) - Convert.ToDouble(itemBudget.AprCost[0]));
                    _objDifferenceTotal.AprCost.Add(Convert.ToDouble(itemYearly.AprCost[1]) - Convert.ToDouble(itemBudget.AprCost[1]));
                    _objDifferenceTotal.AprCost.Add(Convert.ToDouble(itemYearly.AprCost[2]) - Convert.ToDouble(itemBudget.AprCost[2]));

                    _objDifferenceTotal.MayCost.Add(Convert.ToDouble(itemYearly.MayCost[0]) - Convert.ToDouble(itemBudget.MayCost[0]));
                    _objDifferenceTotal.MayCost.Add(Convert.ToDouble(itemYearly.MayCost[1]) - Convert.ToDouble(itemBudget.MayCost[1]));
                    _objDifferenceTotal.MayCost.Add(Convert.ToDouble(itemYearly.MayCost[2]) - Convert.ToDouble(itemBudget.MayCost[2]));

                    _objDifferenceTotal.JunCost.Add(Convert.ToDouble(itemYearly.JunCost[0]) - Convert.ToDouble(itemBudget.JunCost[0]));
                    _objDifferenceTotal.JunCost.Add(Convert.ToDouble(itemYearly.JunCost[1]) - Convert.ToDouble(itemBudget.JunCost[1]));
                    _objDifferenceTotal.JunCost.Add(Convert.ToDouble(itemYearly.JunCost[2]) - Convert.ToDouble(itemBudget.JunCost[2]));

                    _objDifferenceTotal.JulCost.Add(Convert.ToDouble(itemYearly.JulCost[0]) - Convert.ToDouble(itemBudget.JulCost[0]));
                    _objDifferenceTotal.JulCost.Add(Convert.ToDouble(itemYearly.JulCost[1]) - Convert.ToDouble(itemBudget.JulCost[1]));
                    _objDifferenceTotal.JulCost.Add(Convert.ToDouble(itemYearly.JulCost[2]) - Convert.ToDouble(itemBudget.JulCost[2]));

                    _objDifferenceTotal.AugCost.Add(Convert.ToDouble(itemYearly.AugCost[0]) - Convert.ToDouble(itemBudget.AugCost[0]));
                    _objDifferenceTotal.AugCost.Add(Convert.ToDouble(itemYearly.AugCost[1]) - Convert.ToDouble(itemBudget.AugCost[1]));
                    _objDifferenceTotal.AugCost.Add(Convert.ToDouble(itemYearly.AugCost[2]) - Convert.ToDouble(itemBudget.AugCost[2]));

                    _objDifferenceTotal.SepCost.Add(Convert.ToDouble(itemYearly.SepCost[0]) - Convert.ToDouble(itemBudget.SepCost[0]));
                    _objDifferenceTotal.SepCost.Add(Convert.ToDouble(itemYearly.SepCost[1]) - Convert.ToDouble(itemBudget.SepCost[1]));
                    _objDifferenceTotal.SepCost.Add(Convert.ToDouble(itemYearly.SepCost[2]) - Convert.ToDouble(itemBudget.SepCost[2]));

                    _objDifferenceTotal.RowTotal.Add(Convert.ToDouble(itemYearly.RowTotal[0]) - Convert.ToDouble(itemBudget.RowTotal[0]));
                    _objDifferenceTotal.RowTotal.Add(Convert.ToDouble(itemYearly.RowTotal[1]) - Convert.ToDouble(itemBudget.RowTotal[1]));
                    _objDifferenceTotal.RowTotal.Add(Convert.ToDouble(itemYearly.RowTotal[2]) - Convert.ToDouble(itemBudget.RowTotal[2]));

                    _objDifferenceTotal.FirstSlot.Add(Convert.ToDouble(itemYearly.FirstSlot[0]) - Convert.ToDouble(itemBudget.FirstSlot[0]));
                    _objDifferenceTotal.FirstSlot.Add(Convert.ToDouble(itemYearly.FirstSlot[1]) - Convert.ToDouble(itemBudget.FirstSlot[1]));
                    _objDifferenceTotal.FirstSlot.Add(Convert.ToDouble(itemYearly.FirstSlot[2]) - Convert.ToDouble(itemBudget.FirstSlot[2]));

                    _objDifferenceTotal.SecondSlot.Add(Convert.ToDouble(itemYearly.SecondSlot[0]) - Convert.ToDouble(itemBudget.SecondSlot[0]));
                    _objDifferenceTotal.SecondSlot.Add(Convert.ToDouble(itemYearly.SecondSlot[1]) - Convert.ToDouble(itemBudget.SecondSlot[1]));
                    _objDifferenceTotal.SecondSlot.Add(Convert.ToDouble(itemYearly.SecondSlot[2]) - Convert.ToDouble(itemBudget.SecondSlot[2]));

                    _objDifferenceTotals.Add(_objDifferenceTotal);
                }
            }

            return _objDifferenceTotals;
        }
        public List<SukeyQADto> GetTotalCostForDifferenceWithoutQA(string companiIds, int departmentIds, int year)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            double rowTotal = 0;
            double rowTotalQa = 0;
            double rowTotalDept = 0;
            double deptFirstSlot = 0;
            double qaFirstSlot = 0;
            double totalFirstSlot = 0;
            SukeyQADto sukeyDto = new SukeyQADto();

            List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(departmentIds, companiIds, year);
            if (forecastAssignmentViewModels.Count > 0)
            {
                double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                if (_octActualCostTotal > 0)
                {
                    //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(_octActualCostTotal);

                    rowTotalDept += _octActualCostTotal;
                    //rowTotalQa += _octHinsho;
                    rowTotal += _octActualCostTotal;
                }
                else
                {
                    //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(_octTotal);

                    rowTotalDept += _octTotal;
                    //rowTotalQa += _octHinsho;
                    rowTotal += _octTotal;
                }
                if (_novActualCostTotal > 0)
                {
                    //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(_novActualCostTotal);

                    rowTotalDept += _novActualCostTotal;
                    //rowTotalQa += _novHinsho;
                    rowTotal += _novActualCostTotal;
                }
                else
                {
                    //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(_novTotal);

                    rowTotalDept += _novTotal;
                    //rowTotalQa += _novHinsho;
                    rowTotal += _novTotal;
                }
                if (_decActualCostTotal > 0)
                {
                    //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(_decActualCostTotal);

                    rowTotalDept += _decActualCostTotal;
                    //rowTotalQa += _decHinsho;
                    rowTotal += _decActualCostTotal;
                }
                else
                {
                    //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(_decTotal);

                    rowTotalDept += _decTotal;
                    //rowTotalQa += _decHinsho;
                    rowTotal += _decTotal;
                }
                if (_janActualCostTotal > 0)
                {
                    //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(_janActualCostTotal);

                    rowTotalDept += _janActualCostTotal;
                    //rowTotalQa += _janHinsho;
                    rowTotal += _janActualCostTotal;
                }
                else
                {
                    //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(_janTotal);

                    rowTotalDept += _janTotal;
                    //rowTotalQa += _janHinsho;
                    rowTotal += _janTotal;
                }
                if (_febActualCostTotal > 0)
                {
                    //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(_febActualCostTotal);

                    rowTotalDept += _febActualCostTotal;
                    //rowTotalQa += _febHinsho;
                    rowTotal += _febActualCostTotal;
                }
                else
                {
                    //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(_febTotal);

                    rowTotalDept += _febTotal;
                    //rowTotalQa += _febHinsho;
                    rowTotal += _febTotal;
                }
                if (_marActualCostTotal > 0)
                {
                    //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(_marActualCostTotal);

                    rowTotalDept += _marActualCostTotal;
                    //rowTotalQa += _marHinsho;
                    rowTotal += _marActualCostTotal;
                }
                else
                {
                    //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(_marTotal);

                    rowTotalDept += _marTotal;
                    //rowTotalQa += _marHinsho;
                    rowTotal += _marTotal;
                }

                deptFirstSlot = rowTotalDept;
                //qaFirstSlot = rowTotalQa;
                totalFirstSlot = rowTotal;


                if (_aprActualCostTotal > 0)
                {
                    //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(_aprActualCostTotal);

                    rowTotalDept += _aprActualCostTotal;
                    //rowTotalQa += _aprHinsho;
                    rowTotal += _aprActualCostTotal;
                }
                else
                {
                    //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(_aprTotal);


                    rowTotalDept += _aprTotal;
                    //rowTotalQa += _aprHinsho;
                    rowTotal += _aprTotal;
                }
                if (_mayActualCostTotal > 0)
                {
                    //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(_mayActualCostTotal);

                    rowTotalDept += _mayActualCostTotal;
                    //rowTotalQa += _mayHinsho;
                    rowTotal += _mayActualCostTotal;
                }
                else
                {
                    //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(_mayTotal);

                    rowTotalDept += _mayTotal;
                    //rowTotalQa += _mayHinsho;
                    rowTotal += _mayTotal;
                }
                if (_junActualCostTotal > 0)
                {
                    //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(_junActualCostTotal);

                    rowTotalDept += _junActualCostTotal;
                    //rowTotalQa += _junHinsho;
                    rowTotal += _junActualCostTotal;
                }
                else
                {
                    //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(_junTotal);

                    rowTotalDept += _junTotal;
                    //rowTotalQa += _junHinsho;
                    rowTotal += _junTotal;
                }
                if (_julActualCostTotal > 0)
                {
                    //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(_julActualCostTotal);

                    rowTotalDept += _julActualCostTotal;
                    //rowTotalQa += _julHinsho;
                    rowTotal += _julActualCostTotal;
                }
                else
                {
                    //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(_julTotal);

                    rowTotalDept += _julTotal;
                    //rowTotalQa += _julHinsho;
                    rowTotal += _julTotal;
                }
                if (_augActualCostTotal > 0)
                {
                    //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(_augActualCostTotal);

                    rowTotalDept += _augActualCostTotal;
                    //rowTotalQa += _augHinsho;
                    rowTotal += _augActualCostTotal;
                }
                else
                {
                    //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(_augTotal);

                    rowTotalDept += _augTotal;
                    //rowTotalQa += _augHinsho;
                    rowTotal += _augTotal;
                }
                if (_sepActualCostTotal > 0)
                {
                    //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(_sepActualCostTotal);

                    rowTotalDept += _sepActualCostTotal;
                    //rowTotalQa += _sepHinsho;
                    rowTotal += _sepActualCostTotal;
                }
                else
                {
                    //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(_sepTotal);

                    rowTotalDept += _sepTotal;
                    //rowTotalQa += _sepHinsho;
                    rowTotal += _sepTotal;
                }

                sukeyDto.RowTotal.Add(rowTotalDept);
                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(rowTotal);

                sukeyDto.FirstSlot.Add(deptFirstSlot);
                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(totalFirstSlot);

                sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
            }
            else
            {
                sukeyDto.OctCost.Add(0);
                sukeyDto.OctCost.Add(0);
                sukeyDto.OctCost.Add(0);

                sukeyDto.NovCost.Add(0);
                sukeyDto.NovCost.Add(0);
                sukeyDto.NovCost.Add(0);

                sukeyDto.DecCost.Add(0);
                sukeyDto.DecCost.Add(0);
                sukeyDto.DecCost.Add(0);

                sukeyDto.JanCost.Add(0);
                sukeyDto.JanCost.Add(0);
                sukeyDto.JanCost.Add(0);

                sukeyDto.FebCost.Add(0);
                sukeyDto.FebCost.Add(0);
                sukeyDto.FebCost.Add(0);

                sukeyDto.MarCost.Add(0);
                sukeyDto.MarCost.Add(0);
                sukeyDto.MarCost.Add(0);

                sukeyDto.AprCost.Add(0);
                sukeyDto.AprCost.Add(0);
                sukeyDto.AprCost.Add(0);

                sukeyDto.MayCost.Add(0);
                sukeyDto.MayCost.Add(0);
                sukeyDto.MayCost.Add(0);

                sukeyDto.JunCost.Add(0);
                sukeyDto.JunCost.Add(0);
                sukeyDto.JunCost.Add(0);

                sukeyDto.JulCost.Add(0);
                sukeyDto.JulCost.Add(0);
                sukeyDto.JulCost.Add(0);

                sukeyDto.AugCost.Add(0);
                sukeyDto.AugCost.Add(0);
                sukeyDto.AugCost.Add(0);

                sukeyDto.SepCost.Add(0);
                sukeyDto.SepCost.Add(0);
                sukeyDto.SepCost.Add(0);

                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(0);
                sukeyDto.RowTotal.Add(0);

                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(0);
                sukeyDto.FirstSlot.Add(0);

                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(0);
                sukeyDto.SecondSlot.Add(0);
            }

            sukeyQADtos.Add(sukeyDto);

            return sukeyQADtos;
        }
    }
}