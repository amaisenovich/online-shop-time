using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopTime.Models;
using System.IO;

namespace OnlineShopTime.Controllers
{
    public class OfferController : Controller
    {
        WorkWithOffers WWO;

        [HttpGet]
        public ActionResult Create(string OfferID)
        {
            Offers Offer;
            if (OfferID == null)
            {
                Offer = new Offers();
            }
            else
            {
                WorkWithOffers WWO = new WorkWithOffers();
                Offer = WWO.GetOfferByID(OfferID);
            }
            return View(Offer);
        }

        [HttpPost]
        public ActionResult Create(Offers newOffer)
        {
            WWO = new WorkWithOffers();
            string defaultImage = null;
            newOffer.Photo1URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo2URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo3URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            newOffer.Photo4URL = imageURLs.Count > 0 ? imageURLs.Dequeue() : defaultImage;
            WWO.AndNewOrModify(newOffer, User.Identity.Name);
            imageURLs.Clear();
            return RedirectToAction("TabClick", "Home", new { TabID = 3 });
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

        [HttpGet]
        public ActionResult ShowUserOffers(String UserID)
        {
            if (UserID == null)
                UserID = (string)Session["UserID"];

            WorkWithOffers WWO = new WorkWithOffers();
            WorkWithUsers WWU = new WorkWithUsers();

            IQueryable<Offers> UserOffers = WWO.GetUserOffers(UserID);

            Users User = WWU.GetUserByID(UserID);
            ViewBag.UserName = User.FirstName + " " + User.LastName;

            Session["UserID"] = UserID;
            return View(UserOffers);
        }
        public ActionResult OfferPage(String OfferID)
        {
            WorkWithOffers WWO = new WorkWithOffers();
            Offers ShowOffer = new Offers();
            if (OfferID == null)
            {
                ShowOffer = (Offers)Session["ShowOffer"];
            }
            else
                ShowOffer = WWO.GetOfferByID(OfferID);         
            Session["ShowOffer"] = ShowOffer;
            return View(ShowOffer);
        }

        public ActionResult Delete(string OfferID)
        {
            WorkWithOffers WWO = new WorkWithOffers();
            WWO.DeleteOffer(OfferID);            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteOffer(string OfferID)
        {
            if (OfferID == null)
                OfferID = (string)Session["OfferID"];            
            Session["OfferID"] = OfferID;
            WorkWithOffers WWO = new WorkWithOffers();
            return View(WWO.GetOfferByID(OfferID));
        }
    }
}