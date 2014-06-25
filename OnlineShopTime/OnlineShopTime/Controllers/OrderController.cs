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
	}
}