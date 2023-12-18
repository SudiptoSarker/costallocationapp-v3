using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;
 
namespace CostAllocationApp.Controllers.Api
{
    public class SectionsController : ApiController
    {
        SectionBLL sectionBLL = null;
        public SectionsController()
        {
            sectionBLL = new SectionBLL();
        }

        /*
        Get Section List
        Request: Get
        Response: List of sections
        */
        [HttpGet]
        public IHttpActionResult Sections()
        {
            // Get Section List
            List<Section> sections = sectionBLL.GetAllSections();
            return Ok(sections);
        }

        /*
        Create/Update Sections
        Request: Post
        Response: string
        */
        [HttpPost]
        public IHttpActionResult CreateSection(Section section)
        {
            var session = System.Web.HttpContext.Current.Session;

            // Section Name Validation: The section must have a value
            if (String.IsNullOrEmpty(section.SectionName))
            {
                return BadRequest("セクション名は必須です!");
            }
            else
            {
                // Section Name must be unique
                if (sectionBLL.CheckSection(section.SectionName))
                {
                    return BadRequest("区分は登録済みです!");
                }
                else
                {
                    int result = 0;
                    if (section.IsUpdate)
                    {
                        section.UpdatedBy = session["userName"].ToString();
                        section.UpdatedDate = DateTime.Now;
                        section.IsActive = true;

                        // Update Section
                        result = sectionBLL.UpdateSection(section);
                    }
                    else
                    {
                        section.CreatedBy = session["userName"].ToString();
                        section.CreatedDate = DateTime.Now;
                        section.IsActive = true;

                        // Create New Section
                        result = sectionBLL.CreateSection(section);
                    }

                    if (result > 0)
                    {
                        return Ok("データが保存されました!");
                    }
                    else
                    {
                        return BadRequest("何か問題が発生しました");
                    }
                }                
            }
        }

        /*
        Delete Sections
        Request: HttpDelete
        Response: string
        */
        [HttpDelete]
        public IHttpActionResult RemoveSection([FromUri] string sectionIds)
        {
            int result = 0;
            // Section Validation: The section must have a value
            if (!String.IsNullOrEmpty(sectionIds))
            {
                string[] ids = sectionIds.Split(',');

                // Section Deletion 
                foreach (var item in ids)
                {
                    result += sectionBLL.RemoveSection(Convert.ToInt32(item));
                }
                
                if (result == ids.Length)
                {
                    return Ok("正常に削除がされました!");
                }
                else
                {
                    return BadRequest("何か問題が発生しました");
                }
            }
            else
            {
                return BadRequest("セクションIDを選択してください");
            }
        }
    }
}
