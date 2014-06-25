using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult OrderConfirm(string OfferID)
        {
            if (OfferID == null)
                OfferID = (string)Session["OfferID"];
            Session["OfferID"] = OfferID;
            WorkWithOffers WWO = new WorkWithOffers();
            return View(WWO.GetOfferByID(OfferID));
        }

        public ActionResult OrderOffer(string OfferID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();
            WWOR.OrderOffer(OfferID, User.Identity.Name);
            return RedirectToAction("TabClick", "Home", new { TabID = 3 });
        }
        public ActionResult ShowOrders(string UserID)
        {
            WorkWithOrders WWOR = new WorkWithOrders();

            CheckUserOrders(UserID);

            if (UserID == null)
            {
                ViewBag.DataToShow = "MyOrders";
                ViewBag.Data = WWOR.GetUserOrders(UserID);
                UserID = (string)Session["UserID"];
            }

            ViewBag.MyOffers = Resources.Home.MyOrders;         
            Session["UserID"] = UserID;
            return View(WWOR.GetUserOrders(UserID));
        }
        private void CheckUserOrders(string UserID)
        {
            if ((string)Session["UserID"] != UserID)
            {
                WorkWithOrders WWOR = new WorkWithOrders();
                WWOR.CheckUserOrders(UserID);
                Session["UserID"] = UserID;
            }
        }
        public ActionResult TabClick(int TabID)
        {
            switch (TabID)
            {
                case 1:

                    break;
                case 2:
                    break;
            }
            return RedirectToAction("ShowOrders", "Orders");
        }
    }
}