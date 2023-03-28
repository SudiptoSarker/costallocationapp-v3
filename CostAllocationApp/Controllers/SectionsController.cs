using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostAllocationApp.Controllers
{
    public class SectionsController : Controller
    {       
        /***************************\                           
            Section master: Section List and Section Registration UI                               
        \***************************/ 
        public ActionResult CreateSection()
        {
            return View();
        }
    }
}