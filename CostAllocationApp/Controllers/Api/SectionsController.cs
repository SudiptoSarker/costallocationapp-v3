﻿using System;
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

        /***************************\                           
            Section Master Api: Section created through this api.                               
        \***************************/ 
        [HttpPost]
        public IHttpActionResult CreateSection(Section section)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(section.SectionName))
            {
                return BadRequest("Section Name Required");
            }
            else
            {
                section.CreatedBy = session["userName"].ToString();
                section.CreatedDate = DateTime.Now;
                section.IsActive = true;

                if (sectionBLL.CheckSection(section.SectionName))
                {
                    return BadRequest("Section Already Exists!!!");
                }
                else
                {
                    int result = sectionBLL.CreateSection(section);
                    if (result > 0)
                    {
                        return Ok("Data Saved Successfully!");
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }

                }
               
            }



            
        }

        /***************************\                           
            Section Master Api: All the sections are fetched using this api.                            
        \***************************/
        [HttpGet]
        public IHttpActionResult Sections()
        {
            List<Section> sections = sectionBLL.GetAllSections();
            return Ok(sections);
        }

        /***************************\                           
            Section Master Api: Sections are removed using this api.                          
        \***************************/
        [HttpDelete]
        public IHttpActionResult RemoveSection([FromUri] string sectionIds)
        {
            int result = 0;
            

            if (!String.IsNullOrEmpty(sectionIds))
            {
                string[] ids = sectionIds.Split(',');

                foreach (var item in ids)
                {
                    result += sectionBLL.RemoveSection(Convert.ToInt32(item));
                }
                
                if (result == ids.Length)
                {
                    return Ok("Data Removed Successfully!");
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Select Section Id!");
            }

        }

    }
}
