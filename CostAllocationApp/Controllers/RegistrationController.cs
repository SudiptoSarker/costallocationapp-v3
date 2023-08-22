using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers
{
    public class RegistrationController : Controller
    {
        UserBLL userBLL = null;
        public RegistrationController()
        {
            userBLL = new UserBLL();
        }

        // GET: Registration
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult SetSession(string userName,string token,string lastPath)
        {
            Session["userName"] = userName;
            Session["token"] = token;
            Session["last_path"] = lastPath;
            return Json("",JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSession()
        {
            return Json(Session["userName"].ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastPath()
        {
            return Json(Session["last_path"].ToString(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetLastPath(string lastPath)
        {
            Session["last_path"] = lastPath;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserRole()
        {
            var session = System.Web.HttpContext.Current.Session;
            var user = userBLL.GetUserByUserName(session["userName"].ToString());
            return Json(user.UserRoleId, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult RemoveOtherSessions(string userName)
        //{
        //    int result = userBLL.RemoveUser(userName);
        //    return Json("ok", JsonRequestBehavior.AllowGet);
        //}

        public JsonResult RemoveSession(string userName)
        {
            Session.Abandon();
            int result = userBLL.RemoveUser(userName);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserRegistration()
        {
            return View();
        }
    }
}