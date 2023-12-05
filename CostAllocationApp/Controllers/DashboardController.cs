using CostAllocationApp.BLL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostAllocationApp.Controllers
{
    public class DashboardController : Controller
    {
        Utility _utility = new Utility();
        UserBLL userBLL = new UserBLL();
        // GET: Dashboard
        public ActionResult Index()
        {
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