﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers
{
    public class UsersController : Controller
    {
        Utility _utility = new Utility();
        UserBLL userBLL = new UserBLL();
        public UsersController()
        {
            userBLL = new UserBLL();
        }
        // GET: Users
        [NonAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUsers()
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