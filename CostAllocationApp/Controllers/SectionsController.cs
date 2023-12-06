using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers
{
    public class SectionsController : Controller
    {
        UserBLL userBLL = null;
        Utility _utility = null;
        public SectionsController()
        {
            userBLL = new UserBLL();
            _utility = new Utility();
        }
        /***************************\                           
            Section master: Section List and Section Registration UI                               
        \***************************/
        public ActionResult CreateSection()
        {
            //authentications
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                string userRole = "";
                string loggedIn_userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(loggedIn_userName))
                {
                    userRole = userBLL.GetUserRoleByUserName(loggedIn_userName);
                    ViewBag.UserRole = userRole;
                }
                else
                {
                    return RedirectToAction("Login", "Registration");
                }
            }

            return View();
        }
    }
}