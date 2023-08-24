using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.BLL
{
    public class InChargeBLL
    {
        InChargeDAL inChargeDAL = null;
        public InChargeBLL()
        {
            inChargeDAL = new InChargeDAL();
        }
        public int CreateInCharge(InCharge inCharge)
        {
            return inChargeDAL.CreateInCharge(inCharge);
        }
        public List<InCharge> GetAllInCharges()
        {
            return inChargeDAL.GetAllInCharges();
        }
        public int RemoveInCharge(int inChargeId)
        {
            return inChargeDAL.RemoveInCharge(inChargeId);
        }
        public bool CheckInCharge(string incharegeName)
        {
            return inChargeDAL.CheckInCharge(incharegeName);
        }
        public int GetInChargeCountWithEmployeeAsignment(int inChargeId)
        {
            return inChargeDAL.GetInChargeCountWithEmployeeAsignment(inChargeId);
        }
        public InCharge GetInChargeByInChargeId(int inChargeId)
        {
            return inChargeDAL.GetInChargeByInChargeId(inChargeId);
        }
        public int RetrieveInChargeIdByInchargeName(string inchargeName,string userName)
        {
            InCharge inCharge = new InCharge();            
            int inchargeId = 0;

            if (!string.IsNullOrEmpty(inchargeName))
            {
                inchargeId = inChargeDAL.GetInchargeIdByName(inchargeName);

                if (inchargeId > 0)
                {
                    return inchargeId;
                }
                else
                {
                    inCharge.CreatedBy = userName;
                    inCharge.CreatedDate = DateTime.Now;
                    inCharge.IsActive = true;
                    inCharge.InChargeName = inchargeName;

                    int result = inChargeDAL.CreateInCharge(inCharge);
                    inchargeId = inChargeDAL.GetInchargeIdByName(inchargeName);
                    return inchargeId;
                }
            }
            else
            {
                return inchargeId;
            }

        }
    }
}