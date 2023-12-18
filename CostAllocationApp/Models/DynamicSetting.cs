using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DynamicSetting:Common
    {
        public int Id { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string DetailsId { get; set; }

        public string DetailsItemName { get; set; }

        public string MethodId { get; set; }

        public string MethodName { get; set; }

        public string ParameterId { get; set; }

        public bool IsActive { get; set; }

        public string DynamicTableId { get; set; }

        public string DynamicTableName { get; set; }

        public string DynamicTableTitle { get; set; }

        public bool IsMainTotal { get; set; }

        public bool IsSubTotal { get; set; }

        public string CommaSeperatedParameterName { get; set; }        
        // for receive data from client
        public List<DynamicSetting> DynamicSettings { get; set; }
        public string ParameterType { get; set; }
    }
}