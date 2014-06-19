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
            if (wO != null)
                wO = new WorkWithOffers();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            if (ModelState.IsValid)
            {
                
                ShopDBEntities db = new ShopDBEntities();
                newOffer.DateAndTime = DateTime.Now;
                newOffer.OfferedBy = User.Identity.Name;
            }
            //else
            //{
 
            //}
            return RedirectToAction("Index", "Home");
        }
	}
}