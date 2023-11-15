using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DynamicMethodDefinition
    {
        private List<DynamicMethodDefinition> _methodList = new List<DynamicMethodDefinition>();

        public int Id { get; set; }
        public string MethodName { get; set; }
        public string Dependency { get; set; }
        public string Syntex { get; set; }

        public static List<DynamicMethodDefinition> GetMethods()
        {
            DynamicMethodDefinition dynamicMethod = new DynamicMethodDefinition();

            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 1, MethodName = "Cost for department without QA proration", Dependency = "dp",Syntex= "GetTotalWithoutQA" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 2, MethodName = "Cost for department with QA proration", Dependency = "dp", Syntex = "GetTotalWithQA" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 3, MethodName = "Cost by Incharge", Dependency = "in", Syntex = "GetTotalByIncharge" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 4, MethodName = "Cost by department from budget table", Dependency = "dp", Syntex = "GetInitialBudgetForTotal" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 5, MethodName = "Manmonth by Department", Dependency = "dp", Syntex = "GetManmonthByDepartment" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 6, MethodName = "Manmonth by Incharge", Dependency = "in", Syntex = "GetManmonthByIncharges" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 7, MethodName = "Get Head Count by Department", Dependency = "dp", Syntex = "GetHeadCount" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 8, MethodName = "Get Head Count by Incharge", Dependency = "in", Syntex = "GetHeadCountByIncharges" });

            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 9, MethodName = "Difference between budget and yearly data of Cost for department without qa proration", Dependency = "dp", Syntex = "GetDifferenceWithoutQAByDepartment" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 10, MethodName = "Difference between budget and yearly data of Cost for department with qa proration", Dependency = "dp", Syntex = "GetDifferenceWithQAByDepartment" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 11, MethodName = "Difference between budget and yearly data of Cost for in chg", Dependency = "in", Syntex = "GetDifferenceCostByIncharge" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 12, MethodName = "Difference between budget and yearly data of Man month for department", Dependency = "dp", Syntex = "GetDifferenceManmonthByDepartment" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 13, MethodName = "Difference between budget and yearly data of Man month for in chg", Dependency = "in", Syntex = "GetDifferenceManmonthByIncharge" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 14, MethodName = "Difference between budget and yearly data of Headcount for department", Dependency = "dp", Syntex = "GetDifferenceHeadCountByDepartment" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 15, MethodName = "Difference between budget and yearly data of Headcount for in chg", Dependency = "in", Syntex = "GetDifferenceHeadCountByIncharge" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 16, MethodName = "Difference between budget and yearly data of Cost for department from budget table", Dependency = "dp", Syntex = "" });


            return dynamicMethod._methodList;
        }
    }
}