using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers
{
    public class DepartmentsController : Controller
    {
        UserBLL userBLL = null;
        Utility _utility = null;
        public DepartmentsController()
        {
            userBLL = new UserBLL();
            _utility = new Utility();
        }

        /*
        Get Department's Master View
        Request: Get
        Response: ActionResult
        */
        public ActionResult CreateDepartment()
        {
            // Login Authentication
            if (!_utility.CheckSession())
            {
                return RedirectToAction("Login", "Registration");
            }
            else
            {
                // Logged In User Role
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