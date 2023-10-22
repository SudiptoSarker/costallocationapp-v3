using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Http;
using CostAllocationApp.BLL;
using CostAllocationApp.Dtos;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using System.Globalization;

namespace CostAllocationApp.Controllers.Api
{
    public class DashController : ApiController
    {
        ActualCostBLL actualCostBLL = null;
        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        DepartmentBLL departmentBLL = null;
        CompanyBLL companyBLL = null;
        TotalBLL totalBLL = null;

        public DashController()
        {
            actualCostBLL = new ActualCostBLL();
            employeeAssignmentBLL = new EmployeeAssignmentBLL();
            departmentBLL = new DepartmentBLL();
            companyBLL = new CompanyBLL();
            totalBLL = new TotalBLL();
        }

        [HttpGet]
        [Route("api/dash/GetTotalCost/")]
        public IHttpActionResult GetTotalCost()
        {
            string[] departmentListToShow = { "New BLEND", "導入", "移行", "自治体", "運用保守", "その他" };
            var year = 0;
            var companyIds = "";
            double totalCost = 0;

            double _octTotalCost = 0;
            double _novTotalCost = 0;
            double _decTotalCost = 0;
            double _janTotalCost = 0;
            double _febTotalCost = 0;
            double _marTotalCost = 0;
            double _aprTotalCost = 0;
            double _mayTotalCost = 0;
            double _junTotalCost = 0;
            double _julTotalCost = 0;
            double _augTotalCost = 0;
            double _sepTotalCost = 0;

            double _octActualCost = 0;
            double _novActualCost = 0;
            double _decActualCost = 0;
            double _janActualCost = 0;
            double _febActualCost = 0;
            double _marActualCost = 0;
            double _aprActualCost = 0;
            double _mayActualCost = 0;
            double _junActualCost = 0;
            double _julActualCost = 0;
            double _augActualCost = 0;
            double _sepActualCost = 0;

            var chartData = new ArrayList();
            var data = new ArrayList();
            var monthlyForecastData = new Dictionary<string, object>();
            var monthlyActualData = new Dictionary<string, object>();
            var response = new Dictionary<string, object>();

            data.Add("Departments");
            data.Add("Cost");
            chartData.Add(data);

            List<int> companyIDList = new List<int>();

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            //year = 2029;

            List<Department> departments = departmentBLL.GetAllDepartments();
            List<Company> companies = companyBLL.GetAllCompanies();

            double totaleOtherCost = 0;

            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);
            foreach (var department in departments)
            {
                data = new ArrayList();
                double totalDeptCost = 0;

                if (departmentListToShow.Contains(department.DepartmentName))
                {
                    data.Add(department.DepartmentName);

                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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

                        totalDeptCost += (_octActualCostTotal > 0) ? _octActualCostTotal : _octTotal;
                        _octTotalCost += _octTotal;
                        _octActualCost += _octActualCostTotal;

                        totalDeptCost += (_novActualCostTotal > 0) ? _novActualCostTotal : _novTotal;
                        _novTotalCost += _novTotal;
                        _novActualCost += _novActualCostTotal;

                        totalDeptCost += (_decActualCostTotal > 0) ? _decActualCostTotal : _decTotal;
                        _decTotalCost += _decTotal;
                        _decActualCost += _decActualCostTotal;

                        totalDeptCost += (_janActualCostTotal > 0) ? _janActualCostTotal : _janTotal;
                        _janTotalCost += _janTotal;
                        _janActualCost += _janActualCostTotal;

                        totalDeptCost += (_febActualCostTotal > 0) ? _febActualCostTotal : _febTotal;
                        _febTotalCost += _febTotal;
                        _febActualCost += _febActualCostTotal;

                        totalDeptCost += (_marActualCostTotal > 0) ? _marActualCostTotal : _marTotal;
                        _marTotalCost += _marTotal;
                        _marActualCost += _marActualCostTotal;

                        totalDeptCost += (_aprActualCostTotal > 0) ? _aprActualCostTotal : _aprTotal;
                        _aprTotalCost += _aprTotal;
                        _aprActualCost += _aprActualCostTotal;

                        totalDeptCost += (_mayActualCostTotal > 0) ? _mayActualCostTotal : _mayTotal;
                        _mayTotalCost += _mayTotal;
                        _mayActualCost += _mayActualCostTotal;

                        totalDeptCost += (_junActualCostTotal > 0) ? _junActualCostTotal : _junTotal;
                        _junTotalCost += _junTotal;
                        _junActualCost += _junActualCostTotal;

                        totalDeptCost += (_julActualCostTotal > 0) ? _julActualCostTotal : _julTotal;
                        _julTotalCost += _julTotal;
                        _julActualCost += _julActualCostTotal;

                        totalDeptCost += (_augActualCostTotal > 0) ? _augActualCostTotal : _augTotal;
                        _augTotalCost += _augTotal;
                        _augActualCost += _augActualCostTotal;

                        totalDeptCost += (_sepActualCostTotal > 0) ? _sepActualCostTotal : _sepTotal;
                        _sepTotalCost += _sepTotal;
                        _sepActualCost += _sepActualCostTotal;


                        totalCost += totalDeptCost;
                        data.Add(totalDeptCost);
                    }
                    else
                    {
                        totalCost += totalDeptCost;
                        data.Add(totalDeptCost);
                    }
                    chartData.Add(data);
                }
                else
                {
                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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

                        totaleOtherCost += (_octActualCostTotal > 0) ? _octActualCostTotal : _octTotal;
                        _octTotalCost += _octTotal;
                        _octActualCost += _octActualCostTotal;

                        totaleOtherCost += (_novActualCostTotal > 0) ? _novActualCostTotal : _novTotal;
                        _novTotalCost += _novTotal;
                        _novActualCost += _novActualCostTotal;

                        totaleOtherCost += (_decActualCostTotal > 0) ? _decActualCostTotal : _decTotal;
                        _decTotalCost += _decTotal;
                        _decActualCost += _decActualCostTotal;

                        totaleOtherCost += (_janActualCostTotal > 0) ? _janActualCostTotal : _janTotal;
                        _janTotalCost += _janTotal;
                        _janActualCost += _janActualCostTotal;

                        totaleOtherCost += (_febActualCostTotal > 0) ? _febActualCostTotal : _febTotal;
                        _febTotalCost += _febTotal;
                        _febActualCost += _febActualCostTotal;

                        totaleOtherCost += (_marActualCostTotal > 0) ? _marActualCostTotal : _marTotal;
                        _marTotalCost += _marTotal;
                        _marActualCost += _marActualCostTotal;

                        totaleOtherCost += (_aprActualCostTotal > 0) ? _aprActualCostTotal : _aprTotal;
                        _aprTotalCost += _aprTotal;
                        _aprActualCost += _aprActualCostTotal;

                        totaleOtherCost += (_mayActualCostTotal > 0) ? _mayActualCostTotal : _mayTotal;
                        _mayTotalCost += _mayTotal;
                        _mayActualCost += _mayActualCostTotal;

                        totaleOtherCost += (_junActualCostTotal > 0) ? _junActualCostTotal : _junTotal;
                        _junTotalCost += _junTotal;
                        _junActualCost += _junActualCostTotal;

                        totaleOtherCost += (_julActualCostTotal > 0) ? _julActualCostTotal : _julTotal;
                        _julTotalCost += _julTotal;
                        _julActualCost += _julActualCostTotal;

                        totaleOtherCost += (_augActualCostTotal > 0) ? _augActualCostTotal : _augTotal;
                        _augTotalCost += _augTotal;
                        _augActualCost += _augActualCostTotal;

                        totaleOtherCost += (_sepActualCostTotal > 0) ? _sepActualCostTotal : _sepTotal;
                        _sepTotalCost += _sepTotal;
                        _sepActualCost += _sepActualCostTotal;
                    }
                }
            }
            //Add other department costs
            data = new ArrayList();
            totalCost += totaleOtherCost;
            data.Add("その他");
            data.Add(totaleOtherCost);
            chartData.Add(data);

            //Prepare monthly forecast cost data
            monthlyForecastData.Add("10", _octTotalCost);
            monthlyForecastData.Add("11", _novTotalCost);
            monthlyForecastData.Add("12", _decTotalCost);
            monthlyForecastData.Add("1", _janTotalCost);
            monthlyForecastData.Add("2", _febTotalCost);
            monthlyForecastData.Add("3", _marTotalCost);
            monthlyForecastData.Add("4", _aprTotalCost);
            monthlyForecastData.Add("5", _mayTotalCost);
            monthlyForecastData.Add("6", _junTotalCost);
            monthlyForecastData.Add("7", _julTotalCost);
            monthlyForecastData.Add("8", _augTotalCost);
            monthlyForecastData.Add("9", _sepTotalCost);

            //prepare monthly actual cost data
            monthlyActualData.Add("10", _octActualCost);
            monthlyActualData.Add("11", _novActualCost);
            monthlyActualData.Add("12", _decActualCost);
            monthlyActualData.Add("1", _janActualCost);
            monthlyActualData.Add("2", _febActualCost);
            monthlyActualData.Add("3", _marActualCost);
            monthlyActualData.Add("4", _aprActualCost);
            monthlyActualData.Add("5", _mayActualCost);
            monthlyActualData.Add("6", _junActualCost);
            monthlyActualData.Add("7", _julActualCost);
            monthlyActualData.Add("8", _augActualCost);
            monthlyActualData.Add("9", _sepActualCost);

            //Prepare response data
            response.Add("chartData", chartData);
            response.Add("Year", year);
            response.Add("totalCost", string.Format("{0:N2}M", totalCost / 1000000));
            response.Add("forecastCost", monthlyForecastData);
            response.Add("actualCost", monthlyActualData);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/dash/GetBudgetCost/")]
        public IHttpActionResult GetBudgetCost()
        {
            int year = 0;
            var companyIds = "";
            var response = new Dictionary<string, object>();
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
            double _octBudgetCost = 0;
            double _novBudgetCost = 0;
            double _decBudgetCost = 0;
            double _janBudgetCost = 0;
            double _febBudgetCost = 0;
            double _marBudgetCost = 0;
            double _aprBudgetCost = 0;
            double _mayBudgetCost = 0;
            double _junBudgetCost = 0;
            double _julBudgetCost = 0;
            double _augBudgetCost = 0;
            double _sepBudgetCost = 0;

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            //year = 2029;
            List<Department> departments = departmentBLL.GetAllDepartments();
            List<int> companyIDList = new List<int>();

            List<Company> companies = companyBLL.GetAllCompanies();
            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);


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

            foreach (var department in departments)
            {
                if (department.Id == qaDepartmentByName.Id)
                {
                    continue;
                }
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = totalBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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
                        _octBudgetCost += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);

                    {
                        _novBudgetCost += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    {
                        _decBudgetCost += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    {
                        _janBudgetCost += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    {
                        _febBudgetCost += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    {
                        _marBudgetCost += _marTotal + _marCalculation;
                    }
                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    {
                        _aprBudgetCost += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    {
                        _mayBudgetCost += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    {
                        _junBudgetCost += _junTotal + _junCalculation;
                    }
                    var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    {
                        _julBudgetCost += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    {
                        _augBudgetCost += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    {
                        _sepBudgetCost += _sepTotal + _sepCalculation;
                    }
                }

            }

            response.Add("10", _octBudgetCost);
            response.Add("11", _novBudgetCost);
            response.Add("12", _decBudgetCost);
            response.Add("1", _janBudgetCost);
            response.Add("2", _febBudgetCost);
            response.Add("3", _marBudgetCost);
            response.Add("4", _aprBudgetCost);
            response.Add("5", _mayBudgetCost);
            response.Add("6", _junBudgetCost);
            response.Add("7", _julBudgetCost);
            response.Add("8", _augBudgetCost);
            response.Add("9", _sepBudgetCost);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/dash/GetHeadCount")]
        public IHttpActionResult GetHeadCount()
        {
            //initial the variables
            int year = 0;
            var companyIds = "";
            double totalHeadCount = 0;
            double budgetHeadCount = 0;
            double _octHeadCount = 0;
            double _novHeadCount = 0;
            double _decHeadCount = 0;
            double _janHeadCount = 0;
            double _febHeadCount = 0;
            double _marHeadCount = 0;
            double _aprHeadCount = 0;
            double _mayHeadCount = 0;
            double _junHeadCount = 0;
            double _julHeadCount = 0;
            double _augHeadCount = 0;
            double _sepHeadCount = 0;
            var response = new Dictionary<string, object>();
            var deptHeadCount = new Dictionary<object, object>();
            var monthlyHeadCount = new Dictionary<string, object>();
            List<int> companyIDList = new List<int>();

            //Get the latest year
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;

            //prepare the instances
            List<Department> departments = departmentBLL.GetAllDepartments();
            List<Company> companies = companyBLL.GetAllCompanies();
            List<SubCategory> subCategories = departmentBLL.GetAllSubCategories();
            List<HeadCountInner> _headCountList = new List<HeadCountInner>();
            List<ForecastAssignmentViewModel> _allforecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            //prepare company ids
            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);

            //Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            foreach (var department in departments)
            {
                var subCategory = subCategories.Where(sc => sc.Id == Convert.ToInt32(department.SubCategoryId)).SingleOrDefault();

                _headCountList.Add(new HeadCountInner
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.DepartmentName,
                    CategoryName = subCategory.CategoryName,
                    SubCategoryName = subCategory.SubCategoryName,
                    OctCount = 0,
                    NovCount = 0,
                    DecCount = 0,
                    JanCount = 0,
                    FebCount = 0,
                    MarCount = 0,
                    AprCount = 0,
                    MayCount = 0,
                    JunCount = 0,
                    JulCount = 0,
                    AugCount = 0,
                    SepCount = 0,
                });

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);

                if (forecastAssignmentViewModels.Count > 0)
                {
                    _allforecastAssignmentViewModels.AddRange(forecastAssignmentViewModels);
                }
            }

            if (_allforecastAssignmentViewModels.Count > 0)
            {
                var _uniqueItemList = _allforecastAssignmentViewModels.GroupBy(x => x.EmployeeId).Select(x => x.First()).ToList();
                var _uniqueEmployeeIdList = _uniqueItemList.Select(x => x.EmployeeId).ToList();

                foreach (var employeeId in _uniqueEmployeeIdList)
                {
                    var filteredByEmployeeId = _allforecastAssignmentViewModels.Where(x => x.EmployeeId == employeeId).ToList();
                    if (filteredByEmployeeId.Count == 1)
                    {
                        foreach (var item in filteredByEmployeeId)
                        {
                            var getSingleDeptHeadCount = _headCountList.Where(h => h.DepartmentId == Convert.ToInt32(item.DepartmentId)).SingleOrDefault();
                            if (Convert.ToDouble(item.OctPoints) > 0)
                            {
                                getSingleDeptHeadCount.OctCount++;
                                _octHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.NovPoints) > 0)
                            {
                                getSingleDeptHeadCount.NovCount++;
                                _novHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.DecPoints) > 0)
                            {
                                getSingleDeptHeadCount.DecCount++;
                                _decHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.JanPoints) > 0)
                            {
                                getSingleDeptHeadCount.JanCount++;
                                _janHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.FebPoints) > 0)
                            {
                                getSingleDeptHeadCount.FebCount++;
                                _febHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.MarPoints) > 0)
                            {
                                getSingleDeptHeadCount.MarCount++;
                                _marHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.AprPoints) > 0)
                            {
                                getSingleDeptHeadCount.AprCount++;
                                _aprHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.MayPoints) > 0)
                            {
                                getSingleDeptHeadCount.MayCount++;
                                _mayHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.JunPoints) > 0)
                            {
                                getSingleDeptHeadCount.JunCount++;
                                _junHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.JulPoints) > 0)
                            {
                                getSingleDeptHeadCount.JulCount++;
                                _julHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.AugPoints) > 0)
                            {
                                getSingleDeptHeadCount.AugCount++;
                                _augHeadCount++;
                                totalHeadCount++;
                            }
                            if (Convert.ToDouble(item.SepPoints) > 0)
                            {
                                getSingleDeptHeadCount.SepCount++;
                                _sepHeadCount++;
                                totalHeadCount++;
                            }
                        }
                    }
                    else if (filteredByEmployeeId.Count > 1)
                    {
                        List<int> _octDeptId = new List<int>();
                        List<int> _novDeptId = new List<int>();
                        List<int> _decDeptId = new List<int>();
                        List<int> _janDeptId = new List<int>();
                        List<int> _febDeptId = new List<int>();
                        List<int> _marDeptId = new List<int>();
                        List<int> _aprDeptId = new List<int>();
                        List<int> _mayDeptId = new List<int>();
                        List<int> _junDeptId = new List<int>();
                        List<int> _julDeptId = new List<int>();
                        List<int> _augDeptId = new List<int>();
                        List<int> _sepDeptId = new List<int>();

                        for (int i = 0; i < filteredByEmployeeId.Count; i++)
                        {

                            ForecastAssignmentViewModel _filterForOct, _filterForNov, _filterForDec, _filterForJan, _filterForFeb, _filterForMar, _filterForApr, _filterForMay, _filterForJun, _filterForJul, _filterForAug, _filterForSep;
                            //October count
                            var _octVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.OctPoints));
                            if (_octVal == 0)
                            {
                                _filterForOct = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForOct = filteredByEmployeeId.Where(a => Convert.ToDouble(a.OctPoints) == _octVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForOct.OctPoints) > 0)
                            {
                                _octDeptId.Add(Convert.ToInt32(_filterForOct.DepartmentId));
                            }
                            //November count
                            var _novVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.NovPoints));
                            if (_novVal == 0)
                            {
                                _filterForNov = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForNov = filteredByEmployeeId.Where(a => Convert.ToDouble(a.NovPoints) == _novVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForNov.NovPoints) > 0)
                            {
                                _novDeptId.Add(Convert.ToInt32(_filterForNov.DepartmentId));
                            }
                            //December count
                            var _decVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.DecPoints));
                            if (_decVal == 0)
                            {
                                _filterForDec = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForDec = filteredByEmployeeId.Where(a => Convert.ToDouble(a.DecPoints) == _decVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForDec.DecPoints) > 0)
                            {
                                _decDeptId.Add(Convert.ToInt32(_filterForDec.DepartmentId));
                            }
                            //January count
                            var _janVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JanPoints));
                            if (_janVal == 0)
                            {
                                _filterForJan = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJan = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JanPoints) == _janVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJan.JanPoints) > 0)
                            {
                                _janDeptId.Add(Convert.ToInt32(_filterForJan.DepartmentId));
                            }
                            //February count
                            var _febVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.FebPoints));
                            if (_febVal == 0)
                            {
                                _filterForFeb = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForFeb = filteredByEmployeeId.Where(a => Convert.ToDouble(a.FebPoints) == _febVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForFeb.FebPoints) > 0)
                            {
                                _febDeptId.Add(Convert.ToInt32(_filterForFeb.DepartmentId));
                            }
                            //March count
                            var _marVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MarPoints));
                            if (_marVal == 0)
                            {
                                _filterForMar = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMar = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MarPoints) == _marVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMar.MarPoints) > 0)
                            {
                                _marDeptId.Add(Convert.ToInt32(_filterForMar.DepartmentId));
                            }
                            //April count
                            var _aprVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AprPoints));
                            if (_aprVal == 0)
                            {
                                _filterForApr = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForApr = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AprPoints) == _aprVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForApr.AprPoints) > 0)
                            {
                                _aprDeptId.Add(Convert.ToInt32(_filterForApr.DepartmentId));
                            }
                            //May count
                            var _mayVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MayPoints));
                            if (_mayVal == 0)
                            {
                                _filterForMay = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMay = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MayPoints) == _mayVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMay.MayPoints) > 0)
                            {
                                _mayDeptId.Add(Convert.ToInt32(_filterForMay.DepartmentId));
                            }
                            //June count
                            var _junVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JunPoints));
                            if (_junVal == 0)
                            {
                                _filterForJun = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJun = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JunPoints) == _junVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJun.JunPoints) > 0)
                            {
                                _junDeptId.Add(Convert.ToInt32(_filterForJun.DepartmentId));
                            }
                            //July count
                            var _julVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JulPoints));
                            if (_julVal == 0)
                            {
                                _filterForJul = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJul = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JulPoints) == _julVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJul.JulPoints) > 0)
                            {
                                _julDeptId.Add(Convert.ToInt32(_filterForJul.DepartmentId));
                            }
                            //August count
                            var _augVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AugPoints));
                            if (_augVal == 0)
                            {
                                _filterForAug = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForAug = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AugPoints) == _augVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForAug.AugPoints) > 0)
                            {
                                _augDeptId.Add(Convert.ToInt32(_filterForAug.DepartmentId));
                            }
                            //September count
                            var _sepVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.SepPoints));
                            if (_sepVal == 0)
                            {
                                _filterForSep = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForSep = filteredByEmployeeId.Where(a => Convert.ToDouble(a.SepPoints) == _sepVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForSep.SepPoints) > 0)
                            {
                                _sepDeptId.Add(Convert.ToInt32(_filterForSep.DepartmentId));
                            }
                        }

                        if (_octDeptId.Count > 0)
                        {
                            var val = _octDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.OctCount++;
                            _octHeadCount++;
                            totalHeadCount++;
                        }
                        if (_novDeptId.Count > 0)
                        {
                            var val = _novDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.NovCount++;
                            _novHeadCount++;
                            totalHeadCount++;
                        }
                        if (_decDeptId.Count > 0)
                        {
                            var val = _decDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.DecCount++;
                            _decHeadCount++;
                            totalHeadCount++;
                        }
                        if (_janDeptId.Count > 0)
                        {
                            var val = _janDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JanCount++;
                            _janHeadCount++;
                            totalHeadCount++;
                        }
                        if (_febDeptId.Count > 0)
                        {
                            var val = _febDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.FebCount++;
                            _febHeadCount++;
                            totalHeadCount++;
                        }
                        if (_marDeptId.Count > 0)
                        {
                            var val = _marDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.MarCount++;
                            _marHeadCount++;
                            totalHeadCount++;
                        }
                        if (_aprDeptId.Count > 0)
                        {
                            var val = _aprDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.AprCount++;
                            _aprHeadCount++;
                            totalHeadCount++;
                        }
                        if (_mayDeptId.Count > 0)
                        {
                            var val = _mayDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.MayCount++;
                            _mayHeadCount++;
                            totalHeadCount++;
                        }
                        if (_junDeptId.Count > 0)
                        {
                            var val = _junDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JunCount++;
                            _junHeadCount++;
                            totalHeadCount++;
                        }
                        if (_julDeptId.Count > 0)
                        {
                            var val = _julDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JulCount++;
                            _julHeadCount++;
                            totalHeadCount++;
                        }
                        if (_augDeptId.Count > 0)
                        {
                            var val = _augDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.AugCount++;
                            _augHeadCount++;
                            totalHeadCount++;
                        }
                        if (_sepDeptId.Count > 0)
                        {
                            var val = _sepDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.SepCount++;
                            _sepHeadCount++;
                            totalHeadCount++;
                        }
                    }
                }
            }
            monthlyHeadCount.Add("10", _octHeadCount);
            monthlyHeadCount.Add("11", _novHeadCount);
            monthlyHeadCount.Add("12", _decHeadCount);
            monthlyHeadCount.Add("1", _janHeadCount);
            monthlyHeadCount.Add("2", _febHeadCount);
            monthlyHeadCount.Add("3", _marHeadCount);
            monthlyHeadCount.Add("4", _aprHeadCount);
            monthlyHeadCount.Add("5", _mayHeadCount);
            monthlyHeadCount.Add("6", _junHeadCount);
            monthlyHeadCount.Add("7", _julHeadCount);
            monthlyHeadCount.Add("8", _augHeadCount);
            monthlyHeadCount.Add("9", _sepHeadCount);
            response.Add("totalHeadcount", totalHeadCount);
            response.Add("monthlyHeadcount", monthlyHeadCount);

            string[] departmentListToShow = { "New BLEND", "導入", "移行", "自治体", "運用保守", "その他" };
            double listedDeptHeadCount = 0;
            double otherDeptHeadCount = 0;
            var data = new ArrayList();
            var chartData = new ArrayList();
            data.Add("Departments");
            data.Add("Count");
            chartData.Add(data);

            foreach (var item in _headCountList)
            {
                data = new ArrayList();
                listedDeptHeadCount = 0;
                if (departmentListToShow.Contains(item.DepartmentName))
                {
                    data.Add(item.DepartmentName);
                    listedDeptHeadCount = item.OctCount + item.NovCount + item.DecCount + item.JanCount + item.FebCount + item.MarCount + item.AprCount + item.MayCount + item.JunCount + item.JulCount + item.AugCount + item.SepCount;
                    data.Add(listedDeptHeadCount);
                    chartData.Add(data);
                }
                else
                {
                    otherDeptHeadCount += item.OctCount + item.NovCount + item.DecCount + item.JanCount + item.FebCount + item.MarCount + item.AprCount + item.MayCount + item.JunCount + item.JulCount + item.AugCount + item.SepCount;
                }
            }

            data = new ArrayList();
            data.Add("その他");
            data.Add(otherDeptHeadCount);
            chartData.Add(data);
            response.Add("ChartData", chartData);
            return Ok(response);
        }


        [HttpGet]
        [Route("api/dash/GetBudgetHeadCount")]
        public IHttpActionResult GetBudgetHeadCount()
        {
            double _octHeadCount = 0;
            double _novHeadCount = 0;
            double _decHeadCount = 0;
            double _janHeadCount = 0;
            double _febHeadCount = 0;
            double _marHeadCount = 0;
            double _aprHeadCount = 0;
            double _mayHeadCount = 0;
            double _junHeadCount = 0;
            double _julHeadCount = 0;
            double _augHeadCount = 0;
            double _sepHeadCount = 0;

            var monthlyHeadCount = new Dictionary<string, object>();
            var response = new Dictionary<string, object>();

            //Get the latest year
            int year = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;

            //Get company Ids
            var companyIds = "";
            List<int> companyIDList = new List<int>();

            List<Company> companies = companyBLL.GetAllCompanies();
            //prepare company ids
            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);

            //Get departments
            List<Department> departments = departmentBLL.GetAllDepartments();

            List<HeadCountInner> _budgetHeadCountList = new List<HeadCountInner>();

            List<ForecastAssignmentViewModel> _allbudgetAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            foreach (var department in departments)
            {
                List<ForecastAssignmentViewModel> budgetAssignmentViewModels = employeeAssignmentBLL.GetEmployeesBudgetByDepartments_Company(department.Id, companyIds, year);
                
                _budgetHeadCountList.Add(new HeadCountInner
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.DepartmentName,
                    OctCount = 0,
                    NovCount = 0,
                    DecCount = 0,
                    JanCount = 0,
                    FebCount = 0,
                    MarCount = 0,
                    AprCount = 0,
                    MayCount = 0,
                    JunCount = 0,
                    JulCount = 0,
                    AugCount = 0,
                    SepCount = 0,
                });

                if (budgetAssignmentViewModels.Count > 0)
                {
                    _allbudgetAssignmentViewModels.AddRange(budgetAssignmentViewModels);
                }
            }            

            if (_allbudgetAssignmentViewModels.Count > 0)
            {
                var _uniqueItemList = _allbudgetAssignmentViewModels.GroupBy(x => x.EmployeeId).Select(x => x.First()).ToList();
                var _uniqueEmployeeIdList = _uniqueItemList.Select(x => x.EmployeeId).ToList();

                foreach (var employeeId in _uniqueEmployeeIdList)
                {
                    var filteredByEmployeeId = _allbudgetAssignmentViewModels.Where(x => x.EmployeeId == employeeId).ToList();
                    if (filteredByEmployeeId.Count == 1)
                    {
                        foreach(var item in filteredByEmployeeId)
                        {                            
                            if (Convert.ToDouble(item.OctPoints) > 0)
                            {
                                _octHeadCount++;
                            }
                            if (Convert.ToDouble(item.NovPoints) > 0)
                            {
                                _novHeadCount++;
                            }
                            if (Convert.ToDouble(item.DecPoints) > 0)
                            {
                                _decHeadCount++;
                            }
                            if (Convert.ToDouble(item.JanPoints) > 0)
                            {
                                _janHeadCount++;
                            }
                            if (Convert.ToDouble(item.FebPoints) > 0)
                            {
                                _febHeadCount++;
                            }
                            if (Convert.ToDouble(item.MarPoints) > 0)
                            {
                                _marHeadCount++;
                            }
                            if (Convert.ToDouble(item.AprPoints) > 0)
                            {
                                _aprHeadCount++;
                            }
                            if (Convert.ToDouble(item.MayPoints) > 0)
                            {
                                _mayHeadCount++;
                            }
                            if (Convert.ToDouble(item.JunPoints) > 0)
                            {
                                _junHeadCount++;
                            }
                            if (Convert.ToDouble(item.JulPoints) > 0)
                            {
                                _julHeadCount++;
                            }
                            if (Convert.ToDouble(item.AugPoints) > 0)
                            {
                                _augHeadCount++;
                            }
                            if (Convert.ToDouble(item.SepPoints) > 0)
                            {
                                _sepHeadCount++;
                            }
                        }
                    } else if(filteredByEmployeeId.Count > 1)
                    {
                        List<int> _octDeptId = new List<int>();
                        List<int> _novDeptId = new List<int>();
                        List<int> _decDeptId = new List<int>();
                        List<int> _janDeptId = new List<int>();
                        List<int> _febDeptId = new List<int>();
                        List<int> _marDeptId = new List<int>();
                        List<int> _aprDeptId = new List<int>();
                        List<int> _mayDeptId = new List<int>();
                        List<int> _junDeptId = new List<int>();
                        List<int> _julDeptId = new List<int>();
                        List<int> _augDeptId = new List<int>();
                        List<int> _sepDeptId = new List<int>();

                        for(int i = 0; i < filteredByEmployeeId.Count; i++)
                        {
                            ForecastAssignmentViewModel _filterForOct, _filterForNov, _filterForDec, _filterForJan, _filterForFeb, _filterForMar, _filterForApr, _filterForMay, _filterForJun, _filterForJul, _filterForAug, _filterForSep;
                            //October count
                            var _octVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.OctPoints));
                            if (_octVal == 0)
                            {
                                _filterForOct = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForOct = filteredByEmployeeId.Where(a => Convert.ToDouble(a.OctPoints) == _octVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForOct.OctPoints) > 0)
                            {
                                _octDeptId.Add(Convert.ToInt32(_filterForOct.DepartmentId));
                            }
                            //November count
                            var _novVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.NovPoints));
                            if (_novVal == 0)
                            {
                                _filterForNov = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForNov = filteredByEmployeeId.Where(a => Convert.ToDouble(a.NovPoints) == _novVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForNov.NovPoints) > 0)
                            {
                                _novDeptId.Add(Convert.ToInt32(_filterForNov.DepartmentId));
                            }
                            //December count
                            var _decVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.DecPoints));
                            if (_decVal == 0)
                            {
                                _filterForDec = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForDec = filteredByEmployeeId.Where(a => Convert.ToDouble(a.DecPoints) == _decVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForDec.DecPoints) > 0)
                            {
                                _decDeptId.Add(Convert.ToInt32(_filterForDec.DepartmentId));
                            }
                            //January count
                            var _janVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JanPoints));
                            if (_janVal == 0)
                            {
                                _filterForJan = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJan = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JanPoints) == _janVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJan.JanPoints) > 0)
                            {
                                _janDeptId.Add(Convert.ToInt32(_filterForJan.DepartmentId));
                            }
                            //February count
                            var _febVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.FebPoints));
                            if (_febVal == 0)
                            {
                                _filterForFeb = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForFeb = filteredByEmployeeId.Where(a => Convert.ToDouble(a.FebPoints) == _febVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForFeb.FebPoints) > 0)
                            {
                                _febDeptId.Add(Convert.ToInt32(_filterForFeb.DepartmentId));
                            }
                            //March count
                            var _marVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MarPoints));
                            if (_marVal == 0)
                            {
                                _filterForMar = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMar = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MarPoints) == _marVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMar.MarPoints) > 0)
                            {
                                _marDeptId.Add(Convert.ToInt32(_filterForMar.DepartmentId));
                            }
                            //April count
                            var _aprVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AprPoints));
                            if (_aprVal == 0)
                            {
                                _filterForApr = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForApr = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AprPoints) == _aprVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForApr.AprPoints) > 0)
                            {
                                _aprDeptId.Add(Convert.ToInt32(_filterForApr.DepartmentId));
                            }
                            //May count
                            var _mayVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MayPoints));
                            if (_mayVal == 0)
                            {
                                _filterForMay = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMay = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MayPoints) == _mayVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMay.MayPoints) > 0)
                            {
                                _mayDeptId.Add(Convert.ToInt32(_filterForMay.DepartmentId));
                            }
                            //June count
                            var _junVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JunPoints));
                            if (_junVal == 0)
                            {
                                _filterForJun = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJun = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JunPoints) == _junVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJun.JunPoints) > 0)
                            {
                                _junDeptId.Add(Convert.ToInt32(_filterForJun.DepartmentId));
                            }
                            //July count
                            var _julVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JulPoints));
                            if (_julVal == 0)
                            {
                                _filterForJul = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJul = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JulPoints) == _julVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJul.JulPoints) > 0)
                            {
                                _julDeptId.Add(Convert.ToInt32(_filterForJul.DepartmentId));
                            }
                            //August count
                            var _augVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AugPoints));
                            if (_augVal == 0)
                            {
                                _filterForAug = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForAug = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AugPoints) == _augVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForAug.AugPoints) > 0)
                            {
                                _augDeptId.Add(Convert.ToInt32(_filterForAug.DepartmentId));
                            }
                            //September count
                            var _sepVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.SepPoints));
                            if (_sepVal == 0)
                            {
                                _filterForSep = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForSep = filteredByEmployeeId.Where(a => Convert.ToDouble(a.SepPoints) == _sepVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForSep.SepPoints) > 0)
                            {
                                _sepDeptId.Add(Convert.ToInt32(_filterForSep.DepartmentId));
                            }
                        }
                        if (_octDeptId.Count > 0)
                        {
                            _octHeadCount++;
                        }
                        if (_novDeptId.Count > 0)
                        {
                            _novHeadCount++;
                        }
                        if (_decDeptId.Count > 0)
                        {
                            _decHeadCount++;
                        }
                        if (_janDeptId.Count > 0)
                        {
                            _janHeadCount++;
                        }
                        if (_febDeptId.Count > 0)
                        {
                            _febHeadCount++;
                        }
                        if (_marDeptId.Count > 0)
                        {
                            _marHeadCount++;
                        }
                        if (_aprDeptId.Count > 0)
                        {
                            _aprHeadCount++;
                        }
                        if (_mayDeptId.Count > 0)
                        {
                            _mayHeadCount++;
                        }
                        if (_junDeptId.Count > 0)
                        {
                            _junHeadCount++;
                        }
                        if (_julDeptId.Count > 0)
                        {
                            _julHeadCount++;
                        }
                        if (_augDeptId.Count > 0)
                        {
                            _augHeadCount++;
                        }
                        if (_sepDeptId.Count > 0)
                        {
                            _sepHeadCount++;
                        }
                    }
                }
            }

            monthlyHeadCount.Add("10", _octHeadCount);
            monthlyHeadCount.Add("11", _novHeadCount);
            monthlyHeadCount.Add("12", _decHeadCount);
            monthlyHeadCount.Add("1", _janHeadCount);
            monthlyHeadCount.Add("2", _febHeadCount);
            monthlyHeadCount.Add("3", _marHeadCount);
            monthlyHeadCount.Add("4", _aprHeadCount);
            monthlyHeadCount.Add("5", _mayHeadCount);
            monthlyHeadCount.Add("6", _junHeadCount);
            monthlyHeadCount.Add("7", _julHeadCount);
            monthlyHeadCount.Add("8", _augHeadCount);
            monthlyHeadCount.Add("9", _sepHeadCount);

            response.Add("monthlyBudgetHeadcount", monthlyHeadCount);

            return Ok(response);
        }
    }

    internal class HeadCountInner
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int InchargeId { get; set; }
        public string InchagreName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public double OctCount { get; set; }
        public double NovCount { get; set; }
        public double DecCount { get; set; }
        public double JanCount { get; set; }
        public double FebCount { get; set; }
        public double MarCount { get; set; }
        public double AprCount { get; set; }
        public double MayCount { get; set; }
        public double JunCount { get; set; }
        public double JulCount { get; set; }
        public double AugCount { get; set; }
        public double SepCount { get; set; }
    }
}