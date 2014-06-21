using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;
using CloudinaryDotNet;

namespace OnlineShopTime.Controllers
{
    public class OfferController : Controller
    {
        static Cloudinary m_cloudinary;
        WorkWithOffers WWO;

        public OfferController()
        {
            Account acc = new Account(
                    Properties.Settings.Default.CloudName,
                    Properties.Settings.Default.ApiKey,
                    Properties.Settings.Default.ApiSecret);
            m_cloudinary = new Cloudinary(acc);
        }

        [HttpGet]
        public ActionResult Create()
        {            
            return View(new Offers(m_cloudinary));
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            WWO = new WorkWithOffers(User.Identity.Name);
            newOffer = WWO.CompleteOfferWithData(newOffer);
            WWO.AddNewOffer(newOffer);
            return RedirectToAction("Index", "Home");
        }
    }
}