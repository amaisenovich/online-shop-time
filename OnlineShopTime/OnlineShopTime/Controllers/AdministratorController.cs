using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/
        public ActionResult AdminPanel()
        {
            WorkWithUsers WWU = new WorkWithUsers();
            IQueryable<Users> Users = WWU.GetUsersList();
            return View(Users);
        }
        public ActionResult SetRights(string UserID, string Rights)
        {
            WorkWithUsers WWU = new WorkWithUsers();
            WWU.SetUserRights(UserID, Rights);
            return RedirectToAction("AdminPanel");
        }
	}
}