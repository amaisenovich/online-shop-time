using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class OfferController : Controller
    {
        WorkWithOffers WWO;

        [HttpGet]
        public ActionResult Create()
        {
            if (WWO == null)
                WWO = new WorkWithOffers(User.Identity.Name);
            Session["WWO"] = WWO;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            WWO = (WorkWithOffers)Session["WWO"];
            WWO.AddNewOffer(newOffer);
            Session["WWO"] = WWO;
            return RedirectToAction("Index", "Home"); 
        }
    }
}