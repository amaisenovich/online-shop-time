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
                WorkWithOffers WWO = new WorkWithOffers();
                IndexViewData.NewOffers = WWO.GetNewOffers();
            }

            ViewBag.ViewData = IndexViewData;
            Session["IndexData"] = IndexViewData;
            return View();
        }

        public ActionResult TabClick(int TabID)
        {
            IndexViewData = (IndexDataModel)Session["IndexData"];

            WorkWithOffers WWO = new WorkWithOffers();
            if (IndexViewData == null)
                IndexViewData = new IndexDataModel();
            switch (TabID)
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

        public void SetRating()
        {
            float value = float.Parse(Request.Form["value"]);
            string offerID = Request.Form["id"];
            string userID = Request.Form["userID"];

            using (ShopDBEntities db  =  new ShopDBEntities()) {
                if (db.OfferRaiting.Any(m => m.OfferID == offerID && m.UserID == userID))
                    db.OfferRaiting.First(m => m.OfferID == offerID && m.UserID == userID).Raiting = value;
                else
                    db.OfferRaiting.Add(new OfferRaiting() { OfferID = offerID, Raiting = value, UserID = userID });
                db.SaveChanges();
            }
        }
    }
}