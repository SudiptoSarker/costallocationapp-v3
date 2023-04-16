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

        public JsonResult SetSession(string userName)
        {
            Session["userName"] = userName;
            return Json("",JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RemoveSession(string userName)
        {
            Session["userName"] = null;
            int result = userBLL.RemoveUser(userName);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}