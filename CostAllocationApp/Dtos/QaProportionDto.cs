using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.ViewModels;

namespace CostAllocationApp.Dtos
{
    public class QaProportionDto
    {
        public List<QaProportionViewModel> QaProportionViewModels { get; set; }
        public int Year { get; set; }
    }
}