using CostAllocationApp.BLL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostAllocationApp.Controllers
{
    public class CategoryController : Controller
    {
        UserBLL userBLL = null;
        Utility _utility = null;
        public CategoryController()
        {
            userBLL = new UserBLL();
            _utility = new Utility();
        }

        /***************************\                           
            Category master: Category List and Category Creation UI                               
        \***************************/
        public ActionResult CreateCategory()
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

        /***************************\                           
            SubCategory master: SubCategory List and SubCategory Registration UI                               
        \***************************/
        public ActionResult CreateSubCategory()
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
        /***************************\                           
            DepartmentsWithSubCategory master: Section List and Section Registration UI                               
        \***************************/
        public ActionResult DepartmentsWithSubCategory()
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