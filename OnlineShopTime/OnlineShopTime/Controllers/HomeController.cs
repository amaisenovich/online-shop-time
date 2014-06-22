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

            Account acc = new Account(
                    Properties.Settings.Default.CloudName,
                    Properties.Settings.Default.ApiKey,
                    Properties.Settings.Default.ApiSecret);
            Cloudinary m_cloudinary = new Cloudinary(acc);

            ViewBag.Cloudinary = m_cloudinary;

            return View();
        }

        public ActionResult TabClick(int tabID)
        {
            IndexViewData = (IndexDataModel)Session["IndexData"];
            WorkWithOffers WWO = new WorkWithOffers(User.Identity.Name);
            WorkWithUsers WWU = new WorkWithUsers();
            switch (tabID)
            {
                case 1:
                    IndexViewData.ShowTopOffers();
                    //IndexViewData.TopOffers = WWO.GetTopOffers();
                    break;
                case 2:
                    IndexViewData.ShowTopUsers();
                    IndexViewData.TopUsers = WWU.GetTopUsers();
                    break;
                case 3:
                    IndexViewData.ShowNewOffers();
                    //IndexViewData.NewOffers = WWO.GetNewOffers();
                    break;
            }
            Session["IndexData"] = IndexViewData;
            return RedirectToAction("Index", "Home");
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