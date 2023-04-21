using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostAllocationApp.Controllers
{
    public class EmployeesController : Controller
    {
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
            return View();
        }
    }
}