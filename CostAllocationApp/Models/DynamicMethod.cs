using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DynamicMethod
    {
        private Dictionary<int, string> _methodList = new Dictionary<int, string>();
        
        public static Dictionary<int, string> GetMethods()
        {
            DynamicMethod dynamicMethod = new DynamicMethod();
            dynamicMethod._methodList.Add(1, "Hello");
            dynamicMethod._methodList.Add(2, "Hello");
            dynamicMethod._methodList.Add(3, "Hello");
            dynamicMethod._methodList.Add(4, "Hello");
            dynamicMethod._methodList.Add(5, "Hello");
            dynamicMethod._methodList.Add(6, "Hello");
            dynamicMethod._methodList.Add(7, "Hello");
            dynamicMethod._methodList.Add(8, "Hello");
            dynamicMethod._methodList.Add(9, "Hello");
            dynamicMethod._methodList.Add(10, "Hello");
            dynamicMethod._methodList.Add(11, "Hello");

            return dynamicMethod._methodList;
        }
    }
}