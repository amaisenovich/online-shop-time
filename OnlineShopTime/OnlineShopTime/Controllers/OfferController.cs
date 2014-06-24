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
        public ActionResult Create()
        {
            return View(new Offers());
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
            newOffer = WWO.CompleteOfferWithData(newOffer, User.Identity.Name);
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
            return View();
        }
    }
}