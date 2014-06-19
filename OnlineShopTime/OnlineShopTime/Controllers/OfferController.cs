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
        WorkWithOffers wO;
        [HttpGet]
        public ActionResult Create()
        {
            if (wO == null)
                wO = new WorkWithOffers(User.Identity.Name);
            Session["wO"] = wO;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            wO = (WorkWithOffers)Session["wO"];
            bool trie = ModelState.IsValid;
            wO.AddNewOffer(newOffer);
            return RedirectToAction("Index", "Home"); 
        }
    }
}