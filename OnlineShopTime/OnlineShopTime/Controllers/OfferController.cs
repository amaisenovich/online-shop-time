using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;
using CloudinaryDotNet;
using System.IO;

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
            return View(new Offers());
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            WWO = new WorkWithOffers(User.Identity.Name);
            string defaultImage = null;
            newOffer.Photo1URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo2URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo3URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo4URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            WWO.AddNewOffer(newOffer);
            imageURLs.Clear();
            return RedirectToAction("Index", "Home");
        }

        static Queue<string> imageURLs = new Queue<string>();

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void AddImageToOffer()
        {
            if (imageURLs.Count >= 4)
                return;

            var headers = HttpContext.Request.Headers;

            string content = null;
            using (StreamReader reader = new StreamReader(HttpContext.Request.InputStream))
            {
                content = reader.ReadToEnd();
            }

            if (String.IsNullOrEmpty(content)) return;

            Dictionary<string, string> results = new Dictionary<string, string>();

            string[] pairs = content.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                string[] splittedPair = pair.Split('=');

                results.Add(splittedPair[0], splittedPair[1]);
            }

            imageURLs.Enqueue(results["public_id"]);
        }
    }
}