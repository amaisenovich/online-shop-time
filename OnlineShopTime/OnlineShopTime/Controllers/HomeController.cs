using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            {
                IndexViewData = new IndexDataModel();
                IndexViewData.ShowString = "NewOffers";
                WorkWithOffers WWO = new WorkWithOffers(User.Identity.Name);
                IndexViewData.NewOffers = WWO.GetNewOffers();
            }

            ViewBag.ViewData = IndexViewData;
            Session["IndexData"] = IndexViewData;
            return View();
        }

        public ActionResult TabClick(int tabID)
        {
            IndexViewData = (IndexDataModel)Session["IndexData"];

            WorkWithOffers WWO = new WorkWithOffers(User.Identity.Name);
            if (IndexViewData == null)
                IndexViewData = new IndexDataModel();
            switch (tabID)
            {

                case 1:                    
                    //IndexViewData.ShowTopOffers();
                    //IndexViewData.TopOffers = WWO.GetTopOffers();
                    break;
                case 2:
                    WorkWithUsers WWU = new WorkWithUsers();
                    IndexViewData.ShowTopUsers();
                    IndexViewData.TopUsers = WWU.GetTopUsers();
                    break;
                case 3:
                    IndexViewData.ShowNewOffers();
                    IndexViewData.NewOffers = WWO.GetNewOffers();
                    break;
            }

            Session["IndexData"] = IndexViewData;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeCulture(string lang)
        {
            string callbackUrl = Request.UrlReferrer.AbsolutePath;
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            return Redirect(callbackUrl);
        }

        public ActionResult ChangeStyle(string style)
        {
            string callbackUrl = Request.UrlReferrer.AbsolutePath;
            var styleCookie = new HttpCookie("style", style) { HttpOnly = true };
            Response.AppendCookie(styleCookie);
            return Redirect(callbackUrl);
        }
    }
}