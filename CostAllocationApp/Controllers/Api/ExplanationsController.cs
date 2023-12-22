using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers.Api
{
    public class ExplanationsController : ApiController
    {
        ExplanationsBLL explanationsBLL = null;
        public ExplanationsController()
        {
            explanationsBLL = new ExplanationsBLL();
        }

        /*
          Description: create explanation.
          Type: POST
         */
        [HttpPost]
        public IHttpActionResult CreateExplanation(Explanation explanation)
        {
            var session = System.Web.HttpContext.Current.Session;

            // checking null or empty
            if (String.IsNullOrEmpty(explanation.ExplanationName))
            {
                return BadRequest("Explanation Name Required");
            }
            else
            {
                int result = 0;
                if (explanation.IsUpdate)
                {
                    explanation.UpdatedBy = session["userName"].ToString();
                    explanation.UpdatedDate = DateTime.Now;
                    explanation.IsActive = true;
                    result = explanationsBLL.UpdateExplanations(explanation);
                }
                else
                {
                    explanation.CreatedBy = session["userName"].ToString();
                    explanation.CreatedDate = DateTime.Now;
                    explanation.IsActive = true;
                    result = explanationsBLL.CreateExplanation(explanation);
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
        /*
          Description: get explanations.
          Type: GET
         */
        [HttpGet]
        public IHttpActionResult Explanations()
        {
            List<Explanation> explanations = explanationsBLL.GetAllExplanations();
            return Ok(explanations);
        }

        /*
          Description: remove explanations.
          Type: DELETE
         */
        [HttpDelete]
        public IHttpActionResult RemoveExplanations([FromUri] string explanationIds)
        {
            int result = 0;


            if (!String.IsNullOrEmpty(explanationIds))
            {
                string[] ids = explanationIds.Split(',');

                foreach (var item in ids)
                {
                    result += explanationsBLL.RemoveExplanations(Convert.ToInt32(item));
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