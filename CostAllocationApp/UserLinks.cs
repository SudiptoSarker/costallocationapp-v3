using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp
{
    public class UserLinks
    {
        public static List<string> adminLinks = new List<string>() {
            "Forecasts/CreateForecast",
            "Forecasts/GetHistories",
            "Forecasts/ActualCosts",
            "Sections/CreateSection",
            "Departments/CreateDepartment",
            "InCharges/CreateInCharge",
            "Roles/CreateRoles",
            "Explanations/CreateExplanation",
            "Companies/CreateCompany",
            "Salaries/CreateSalary",
            "Employees/CreateNewEmployee",
            "Users/CreateUsers",
            "Forecasts/CalculateActualCost"
        };

        public static List<string> editorLinks = new List<string>() {
            "Forecasts/CreateForecast",
            "Forecasts/GetHistories",
            "Forecasts/ActualCosts",
            "Forecasts/CalculateActualCost"
        };
    }
}