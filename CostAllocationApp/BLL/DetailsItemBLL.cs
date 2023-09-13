using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class DetailsItemBLL
    {
        DetailsItemDAL detailsItemDAL = null;
        public DetailsItemBLL()
        {
            detailsItemDAL = new DetailsItemDAL();
        }

        public int CreateDetailsItem(DeatailsItem detailsItem)
        {
            return detailsItemDAL.CreateDetailsItem(detailsItem);
        }

        public DeatailsItem GetDetailsItemById(int detailsItemId)
        {
            return detailsItemDAL.GetDetailsItemById(detailsItemId);
        }
        public List<DeatailsItem> GetDetailsItemBySubItemsId(int subItemId)
        {
            return detailsItemDAL.GetDetailsItemBySubItemsId(subItemId);
        }
    }
}