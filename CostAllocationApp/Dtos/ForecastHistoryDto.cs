using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Dtos
{
    public class ForecastHistoryDto
    {
        public List<ForecastUpdateHistoryDto> ForecastUpdateHistoryDtos { get; set; }
        public string HistoryName { get; set; }
        public List<string> CellInfo { get; set; }
    }
}