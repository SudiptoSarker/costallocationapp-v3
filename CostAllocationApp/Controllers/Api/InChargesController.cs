using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers.Api
{
    public class InChargesController : ApiController
    {
        InChargeBLL inChargeBLL = null;
        public InChargesController()
        {
            inChargeBLL = new InChargeBLL();
        } 

        [HttpPost]
        public IHttpActionResult CreateInCharge(InCharge inCharge)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(inCharge.InChargeName))
            {
                return BadRequest("InCharge Name Required");
            }
            else
            {                

                if (inChargeBLL.CheckInCharge(inCharge.InChargeName))
                {
                    return BadRequest("担当は登録済みです!!!");
                }
                else
                {
                    int result = 0;
                    if (inCharge.IsUpdate)
                    {
                        inCharge.UpdatedBy = session["userName"].ToString();
                        inCharge.UpdatedDate = DateTime.Now;
                        inCharge.IsActive = true;

                        result = inChargeBLL.UpdateIncharge(inCharge);
                    }
                    else
                    {
                        inCharge.CreatedBy = session["userName"].ToString();
                        inCharge.CreatedDate = DateTime.Now;
                        inCharge.IsActive = true;

                        result = inChargeBLL.CreateInCharge(inCharge);
                    }
                    
                    if (result > 0)
                    {
                        return Ok("データが保存されました!");
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
                
            }
        }
        [HttpGet]
        public IHttpActionResult InCharges()
        {
            List<InCharge> inCharges = inChargeBLL.GetAllInCharges();
            return Ok(inCharges);
        }

        [HttpDelete]
        public IHttpActionResult RemoveInCharge([FromUri] string inChargeIds)
        {
            int result = 0;


            if (!String.IsNullOrEmpty(inChargeIds))
            {
                string[] ids = inChargeIds.Split(',');

                foreach (var item in ids)
                {
                    result += inChargeBLL.RemoveInCharge(Convert.ToInt32(item));
                }

                if (result == ids.Length)
                {
                    return Ok("正常に削除がされました!");
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Select InCharge Id!");
            }

        }

    }
}