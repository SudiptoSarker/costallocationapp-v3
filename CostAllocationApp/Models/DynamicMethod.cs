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

        public static List<DynamicMethodDefinition> GetMethods()
        {
            DynamicMethodDefinition dynamicMethod = new DynamicMethodDefinition();

            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 1, MethodName = "Cost for department without QA proration", Dependency = "dp" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 2, MethodName = "Cost for department from QA prorationt", Dependency = "dp" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 3, MethodName = "Cost for department with QA proration", Dependency = "dp" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 4, MethodName = "Cost for department from budget table", Dependency = "dp" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 5, MethodName = "Difference between yearly data and budget table of cost for departement with QA proration", Dependency = "dp" });
            dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 6, MethodName = "Headcount for department", Dependency = "dp" });            

            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 1, MethodName = "Headcount for department", Dependency="dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 2, MethodName = "Headcount for in chg", Dependency = "in" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 3, MethodName = "Man month for department", Dependency="dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 4, MethodName = "Man month for in chg", Dependency="in" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 5, MethodName = "Cost for department without QA preparation", Dependency="dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 6, MethodName = "Cost for department with QA preparation", Dependency="dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 7, MethodName = "Cost for in chg", Dependency="in" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 8, MethodName = "Cost for QA proration for department", Dependency="dp" });

            ////difference method
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 9, MethodName = "Difference between budget and yearly data of Cost for department without qa proration", Dependency = "dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 10, MethodName = "Difference between budget and yearly data of Cost for department with qa proration", Dependency = "dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 11, MethodName = "Difference between budget and yearly data of Cost for in chg", Dependency = "in" });            
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 12, MethodName = "Difference between budget and yearly data of Man month for department", Dependency = "dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 13, MethodName = "Difference between budget and yearly data of Man month for in chg", Dependency = "in" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 14, MethodName = "Difference between budget and yearly data of Headcount for department", Dependency = "dp" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 15, MethodName = "Difference between budget and yearly data of Headcount for in chg", Dependency = "in" });
            //dynamicMethod._methodList.Add(new DynamicMethodDefinition { Id = 16, MethodName = "Difference between budget and yearly data of Cost for department from budget table", Dependency = "dp" });

            return dynamicMethod._methodList;
        }
    }
}