using Lucene.Net.Documents;
using OnlineShopTime.Models;
using SimpleLucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopTime.Utils
{
    public class OfferResultDefinition : IResultDefinition<Offers>
    {
        public Offers Convert(Document document)
        {
            var offer = new Offers();
            offer.OfferID = document.GetValue<string>("OfferID");
            offer.Name = document.GetValue("Name");
            offer.Photo1URL = document.GetValue("Photo1URL");
            offer.Price = document.GetValue<string>("Price");
            offer.OfferedBy = document.GetValue<string>("OfferedBy");
            string userID = document.GetValue("User");
            using (var db = new ShopDBEntities()) { offer.Users = db.Users.First(m => m.UserID == userID); }
            offer.Tags = document.GetValue("Tags").Split(" ".First()).Select(m => new Tags() { Name = m }).ToList();
            offer.OfferRaiting = document.GetValue("Rating").Split(" ".First()).Select(m => new OfferRaiting() { Raiting = m != "" ? Single.Parse(m) : 0 }).ToList();
            offer.DateAndTime = DateTools.StringToDate(document.GetValue("CreationDate"));
            offer.Description = document.GetValue("Description");
            return offer;
        }
    }
}