using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopTime.Controllers
{
    public class AccessDeniedController : Controller
    {
        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult UserBanned()
        {
            return View();
        }
	}
}