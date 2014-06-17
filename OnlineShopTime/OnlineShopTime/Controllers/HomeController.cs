using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;
using OnlineShopTime.Models;

namespace OnlineShopTime.Controllers
{
    public class HomeController : Controller {
        static Cloudinary m_cloudinary;

        static HomeController() {
            ShopDBEntities db = new ShopDBEntities();
            db.Database.Initialize(false);

            Account acc = new Account(
                    Properties.Settings.Default.CloudName,
                    Properties.Settings.Default.ApiKey,
                    Properties.Settings.Default.ApiSecret);

            m_cloudinary = new Cloudinary(acc);

            
        }

        public ActionResult Index()
        {
            //TopOffers to = new TopOffers();
            ShopDBEntities db = new ShopDBEntities();
            Tags tag = new Tags();

            tag.Name = "abc";
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeStyle(string style)
        {
            var styleCookie = new HttpCookie("style", style) { HttpOnly = true };
            Response.AppendCookie(styleCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void UploadDirect() {
            var headers = HttpContext.Request.Headers;

            string content = null;
            using (StreamReader reader = new StreamReader(HttpContext.Request.InputStream)) {
                content = reader.ReadToEnd();
            }

            if (String.IsNullOrEmpty(content)) return;

            Dictionary<string, string> results = new Dictionary<string, string>();

            string[] pairs = content.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs) {
                string[] splittedPair = pair.Split('=');

                results.Add(splittedPair[0], splittedPair[1]);
            }

            //Photo p = new Photo() {
            //    Bytes = Int32.Parse(results["bytes"]),
            //    CreatedAt = DateTime.ParseExact(HttpUtility.UrlDecode(results["created_at"]), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            //    Format = results["format"],
            //    Height = Int32.Parse(results["height"]),
            //    Path = results["path"],
            //    PublicId = results["public_id"],
            //    ResourceType = results["resource_type"],
            //    SecureUrl = results["secure_url"],
            //    Signature = results["signature"],
            //    Type = results["type"],
            //    Url = results["url"],
            //    Version = Int32.Parse(results["version"]),
            //    Width = Int32.Parse(results["width"]),
            //};

            ShopDBEntities album = new ShopDBEntities();

            //album.Offers.Add(

            //album.SaveChanges();
        }
    }
}