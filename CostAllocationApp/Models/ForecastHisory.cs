using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class ForecastHisory:Common
    {
        public int Id { get; set; }
        public string TimeStamp { get; set; }
        public int Year { get; set; }

        public List<Forecast> Forecasts { get; set; }
    }
}