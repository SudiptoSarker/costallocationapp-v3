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
        public DepartmentsController()
        {
            userBLL = new UserBLL();
        }
        // GET: Departments
        public ActionResult CreateDepartment()
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
                var link = userPermissions.Where(up => up.Link.ToLower() == "Departments/CreateDepartment".ToLower()).SingleOrDefault();
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