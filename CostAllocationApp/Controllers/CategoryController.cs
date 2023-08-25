using CostAllocationApp.BLL;
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
        public CategoryController()
        {
            userBLL = new UserBLL();
        }
        /***************************\                           
            Category master: Category List and Section Registration UI                               
        \***************************/
        //public ActionResult CreateCategory()
        //{
        //    if (Session["token"] == null)
        //    {
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
        //    {
        //        Session["token"] = null;
        //        Session["userName"] = null;
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    {
        //        User user = userBLL.GetUserByUserName(Session["userName"].ToString());
        //        List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
        //        var link = userPermissions.Where(up => up.Link.ToLower() == "Sections/CreateSection".ToLower()).SingleOrDefault();
        //        if (link == null)
        //        {
        //            ViewBag.linkFlag = false;
        //        }
        //        else
        //        {
        //            ViewBag.linkFlag = true;
        //        }
        //    }
        //    return View();
        //}
        /***************************\                           
            Section master: Section List and Section Registration UI                               
        \***************************/
        //public ActionResult CreateSubCategory()
        //{
        //    if (Session["token"] == null)
        //    {
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
        //    {
        //        Session["token"] = null;
        //        Session["userName"] = null;
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    {
        //        User user = userBLL.GetUserByUserName(Session["userName"].ToString());
        //        List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
        //        var link = userPermissions.Where(up => up.Link.ToLower() == "Sections/CreateSection".ToLower()).SingleOrDefault();
        //        if (link == null)
        //        {
        //            ViewBag.linkFlag = false;
        //        }
        //        else
        //        {
        //            ViewBag.linkFlag = true;
        //        }
        //    }
        //    return View();
        //}
        /***************************\                           
            Section master: Section List and Section Registration UI                               
        \***************************/
        //public ActionResult DepartmentsWithSubCategory()
        //{
        //    if (Session["token"] == null)
        //    {
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
        //    {
        //        Session["token"] = null;
        //        Session["userName"] = null;
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    {
        //        User user = userBLL.GetUserByUserName(Session["userName"].ToString());
        //        List<UserPermission> userPermissions = userBLL.GetUserPermissionsByUserId(user.Id);
        //        var link = userPermissions.Where(up => up.Link.ToLower() == "Sections/CreateSection".ToLower()).SingleOrDefault();
        //        if (link == null)
        //        {
        //            ViewBag.linkFlag = false;
        //        }
        //        else
        //        {
        //            ViewBag.linkFlag = true;
        //        }
        //    }
        //    return View();
        //}
    }
}