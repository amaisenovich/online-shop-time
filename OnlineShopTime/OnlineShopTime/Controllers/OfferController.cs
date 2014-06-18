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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            if (newOffer.Name == "LOL")
            {
 
            }
            return RedirectToAction("Home", "Index");
        }
	}
}