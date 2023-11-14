using System;
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
        public TotalBLL()
        {
            totalDAL = new TotalDAL();
            departmentBLL = new DepartmentBLL();
            inchargeBLL = new InChargeBLL();
            categoryBLL = new CategoryBLL();
            subCategoryBLL = new SubCategoryBLL();
            detailsItemBLL = new DetailsItemBLL();
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


    }
}