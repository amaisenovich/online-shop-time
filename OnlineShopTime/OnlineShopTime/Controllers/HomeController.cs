using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class HomeController : Controller
    {
        IndexDataModel IndexViewData;

        public ActionResult Index()
        {
            IndexViewData = (IndexDataModel)Session["IndexData"];
            if (IndexViewData == null)
                IndexViewData = new IndexDataModel();
            Session["IndexData"] = IndexViewData;
            ViewBag.ViewData = IndexViewData;
            return View();
        }

        public ActionResult TabClick(int tabID)
        {
            IndexViewData = (IndexDataModel)Session["IndexData"];
            switch (tabID)
            {
                case 1:
                    IndexViewData.ShowTopOffers();
                    break;
                case 2:
                    IndexViewData.ShowTopUsers();
                    break;
                case 3:
                    IndexViewData.ShowNewOffers();
                    break;
            }
            Session["IndexData"] = IndexViewData;
            return Index();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult ChangeCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeStyle(string style)
        {
            var styleCookie = new HttpCookie("style", style) { HttpOnly = true };
            Response.AppendCookie(styleCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}