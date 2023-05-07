using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers
{
    public class EmployeesController : Controller
    {
        UserBLL userBLL = null;
        public EmployeesController()
        {
            userBLL = new UserBLL();
        }
        [NonAction]
        public ActionResult CreateEmployee()
        {
            return View();
        }
        // GET: Employees
        [NonAction]
        public ActionResult NameList()
        {
            return View();
        }
        [NonAction]
        public ActionResult CreateAssignment()
        {
            return View();
        }
        public ActionResult NameRegistration()
        {
            return View();
        }

        public ActionResult CreateNewEmployee()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                Session["token"] = null;
                Session["userName"] = null;
                return RedirectToAction("Login", "Registration");
            }

            {
                User user = userBLL.GetUserByUserName(Session["userName"].ToString());
                List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
                var link = userPermissions.Where(up => up.Link.ToLower() == "Employees/CreateNewEmployee".ToLower()).SingleOrDefault();
                if (link == null)
                {
                    ViewBag.linkFlag = false;
                }
                else
                {
                    ViewBag.linkFlag = true;
                }
            }
            return View();
        }
    }
}